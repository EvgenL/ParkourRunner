using System.Collections.Generic;
using System.Linq;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track
{
    public class ObstacleInputZone : MonoBehaviour
    {

        public bool ReadJumpInput = true;
        public bool ReadRollInput = false;

        public List<vTriggerGenericAction> JumpTriggers = new List<vTriggerGenericAction>();
        public List<vTriggerGenericAction> RollTriggers = new List<vTriggerGenericAction>();

        //public float DisableTriggersForSeconds = 2f;

        private List<ObstacleInputZone> _inputZones = new List<ObstacleInputZone>();

        private void Awake()
        {

            //Чтоб наш геймдизайнер руками не добавлял в листы
            if (transform.parent.childCount > 1)
            {
                _inputZones = transform.parent.GetComponentsInChildren<ObstacleInputZone>().ToList();
                _inputZones.Remove(this);

            }

            foreach (var iz in _inputZones)
            {
                iz.GetComponent<BoxCollider>().enabled = false;
            }
            //JumpTriggers = transform.parent.GetComponentsInChildren<vTriggerGenericAction>()
            //   .Where((x) => x.playAnimation != ("Roll")).ToList();
        }

        private ParkourThirdPersonInput _input;
        private bool _isReady;

        private void Start()
        {
            foreach (var trig in JumpTriggers)
            {
                trig.OnDoAction.AddListener(OnTriggerUsed);
            }
            foreach (var trig in RollTriggers)
            {
                trig.OnDoAction.AddListener(OnTriggerUsed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;

            ParkourThirdPersonInput c = other.GetComponent<ParkourThirdPersonInput>();
            if (c == null) return;
            _input = c;
            _input.EnterInputZone(this);

            //Если игрок "влетел" в зону
            if (c.cc.isJumping && ReadJumpInput)
            {
                OnPalyerJump();
            }
            else if (c.cc.isRolling && ReadRollInput)
            {
                OnPalyerRoll();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_input)
                _input.EnterInputZone(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_input)
                _input.ExitInputZone();
            ReadyRoll(false);
            ReadyJump(false);
        }

        private void ReadyRoll(bool mode)
        {
            //Когда зона получает инпут от игрока, на экране появляется вспышка
            if (mode && _isReady != mode)
            {
                CheckScore(RollTriggers[0]);
                HUDManager.Instance.Flash();
            }
            _isReady = mode;

            foreach (var trig in RollTriggers)
            {
                trig.autoAction = mode;
            }
        }

        private void ReadyJump(bool mode)
        {
            //Когда зона получает инпут от игрока, на экране появляется вспышка
            if (mode && _isReady != mode)
            {
                CheckScore(FarJumpTrigger());
                HUDManager.Instance.Flash();
            }
            _isReady = mode;

            foreach (var trig in JumpTriggers)
            {
                trig.autoAction = mode;
            }
        }

        private void CheckScore(vTriggerGenericAction trigger)
        {
            HUDManager.Messages msg;
            var hm =  HUDManager.Instance;
            var gm =  GameManager.Instance;
            if (_input == null) _input = FindObjectOfType<ParkourThirdPersonInput>();
            if (_input.cc.isJumping || _input.cc.isRolling)
            {
                print("entered rolling (jumping)");
                msg = HUDManager.Messages.NoMessage;
                hm.ShowGreatMessage(msg);
                return;
            }

            //Находим позицию игрока на оси z относительно препятствия
            var triggerPos = trigger.transform.position;

            var playerX0pos = _input.transform.position;
            playerX0pos.x = triggerPos.x;
            playerX0pos.y = triggerPos.y;

            //Выясняем, не пробежал ли игрок уже мимо триггера
            if (playerX0pos.z + 0.5f > triggerPos.z)
            {
                print("passed");
                msg = HUDManager.Messages.Ok;
                hm.ShowGreatMessage(msg);
                gm.TrickMultipiler = 1f;
                return;
            }

            //Если игрок нажал вовремя = проверяем тайминг для награды
            float dist = Vector3.Distance(playerX0pos, triggerPos);
            if (dist < 1.5f)
            {
                print("dist < 1f");
                msg = HUDManager.Messages.Perfect;
                gm.TrickMultipiler = 1.5f;
            }
            else if (dist < 2.5f)
            {
                print("dist < 2.5f");
                gm.TrickMultipiler = 1f;
                msg = HUDManager.Messages.Great;
            }
            else
            {
                print("dist > 2.5f");
                gm.TrickMultipiler = 0.5f;
                msg = HUDManager.Messages.Ok;
            }
            hm.ShowGreatMessage(msg);
        }

        private vTriggerGenericAction FarJumpTrigger()
        {
            vTriggerGenericAction farTrigger = JumpTriggers.Find(x => x.transform.name.Contains("Far"));
            if (farTrigger == null)
            {
                farTrigger = JumpTriggers[0];
            }
            return farTrigger;
        }

        public void OnTriggerUsed()
        {
            if (_input != null)
                _input.ExitInputZone();
            ReadyRoll(false);
            ReadyJump(false);
            ActivateNextIz();
        }

        private void ActivateNextIz()
        {
            foreach (var iz in _inputZones)
            {
                iz.OnPalyerJump();
            }
        }

        public void OnPalyerJump()
        {
            if (ReadJumpInput)
                ReadyJump(true);
        }
        public void OnPalyerRoll()
        {
            if (ReadRollInput)
                ReadyRoll(true);
        }

        public void OnPlayerRegainBalance()
        {
            foreach (var trig in JumpTriggers)
            {
                trig.autoAction = true;
            }

            foreach (var trig in RollTriggers)
            {
                trig.autoAction = true;
            }
        }
    }
}
