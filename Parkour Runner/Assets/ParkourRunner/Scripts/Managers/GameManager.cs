using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.Player;
using ParkourRunner.Scripts.Player.InvectorMods;
using ParkourRunner.Scripts.Track.Generator;
using ParkourRunner.Scripts.Track.Pick_Ups;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
{

    public class GameManager : MonoBehaviour
    {

        public enum GameState
        {
            Run,
            Pause,
            Dead
        }
        #region Singleton

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        public bool PlayerCanBeDismembered = true; //Можно ли оторвать конечность? Используется скриптом на каждой кости рэгдолла.

        public float VelocityToDismember = 10f;

        public int CoinsThisRun { get; private set; }
        public int CoinMultipiler = 1;

        public float GameSpeed = 1f;

        public List<BonusName> ActiveBonuses;

        public float DistanceRun;
        private float _distanceRunOffset;

        public GameState gameState { get; private set; }

        ////Состояние
        //Наличие конечностей
        [SerializeField] private bool _leftHand = true;
        [SerializeField] private bool _rightHand = true;
        [SerializeField] private bool _leftLeg = true;
        [SerializeField] private bool _rightLeg = true;

        public MuscleDismember[] Limbs;

        public List<Coin> Coins;

        private ParkourThirdPersonController _player;
        private Animator _playerAnimator;

        private HUDManager _hud;



        private void Start()
        {
            FindObjectOfType<BehaviourPuppet>().onLoseBalance.unityEvent.AddListener(ResetSpeed);

            ActiveBonuses = new List<BonusName>();
            _hud = HUDManager.Instance;
            Limbs = FindObjectsOfType<MuscleDismember>();
            _player = ParkourThirdPersonController.instance;
            _playerAnimator = _player.GetComponent<Animator>();
            
            StartGame();
        }

        private void StartGame()
        {
            StartCoroutine(IncreaseGameSpeed());
            gameState = GameState.Run;
        }

        // Update is called once per frame
        void Update ()
        {
            DistanceRun = _player.transform.position.z;
            _hud.UpdateDistance(DistanceRun + _distanceRunOffset);
        }

        //Оторвать конечность (или приклеить обратно)
        public void SetLimbState(Bodypart bodypart, bool dismember)
        {
            switch (bodypart)
            {
                case (Bodypart.Body): //or
                case (Bodypart.Head):
                    if (!dismember) Die();
                    break;

                case (Bodypart.LHand):
                    _leftHand = dismember;
                    break;

                case (Bodypart.RHand):
                    _rightHand = dismember;
                    break;

                case (Bodypart.RLeg):
                    _rightLeg = dismember;
                    break;

                case (Bodypart.LLeg):
                    _leftLeg = dismember;
                    break;
            }

            CheckGameState();
        }

        private void CheckGameState()
        {
            _playerAnimator.SetBool("LeftHand", _leftHand);
            _playerAnimator.SetBool("RightHand", _rightHand);
            _playerAnimator.SetBool("LeftLeg", _leftLeg);
            _playerAnimator.SetBool("RightLeg", _rightLeg);

            if (!_leftHand && !_rightHand)
            {
                Die();
            }
            if (!_leftLeg && !_rightLeg)
            {
                Die();
            }
        }

        private void Die()
        {
            gameState = GameState.Dead;
            _player.Die();
            Invoke("ShowPostMortem", 4f);
        }

        public void ShowPostMortem()
        {
            _hud.ShowPostMortem();
        }

        public void Revive()
        {
            while (HealLimb());
            _player.Revive();
            gameState = GameState.Run;

            LevelGenerator.Instance.GenerateRewardOnRevive();
        }

        public bool HealLimb()
        {
            foreach (var limb in Limbs)
            {
                if (limb.IsDismembered)
                {
                    limb.HealRecursive(); 
                    SetLimbState(limb.Bodypart, true); //Записываем в аниматор что подлечились
                    return true;
                }
            }
            return false;
        }

        public void AddCoin(int amount = 1)
        {
            CoinsThisRun += amount * CoinMultipiler;
            _hud.SetCoins(CoinsThisRun);
        }

        public void AddBonus(BonusName bonusName)
        {
            switch (bonusName)
            {
                case (BonusName.Magnet):
                    if (ActiveBonuses.Contains(bonusName)) GetComponent<MagnetBonus>().RefreshTime();
                    else gameObject.AddComponent<MagnetBonus>();
                    break;
                case (BonusName.Jump):
                    if (ActiveBonuses.Contains(bonusName)) GetComponent<JumpBonus>().RefreshTime();
                    else gameObject.AddComponent<JumpBonus>();
                    break;
                case (BonusName.Shield):
                    if (ActiveBonuses.Contains(bonusName)) GetComponent<ShieldBonus>().RefreshTime();
                    else gameObject.AddComponent<ShieldBonus>();
                    break;
                case (BonusName.DoubleCoins):
                    if (ActiveBonuses.Contains(bonusName)) GetComponent<DoubleCoins>().RefreshTime();
                    else gameObject.AddComponent<DoubleCoins>();
                    break;
                case (BonusName.Boost):
                    if (ActiveBonuses.Contains(bonusName)) GetComponent<Boost>().RefreshTime();
                    else gameObject.AddComponent<Boost>();
                    break;
            }
        }

        public List<Coin> GetCoins()
        {
            return Coins;
        }

        public Transform GetRandomLimb()
        {
            var list = Limbs.ToList().Where(x => !x.IsDismembered).ToList();
            return list[UnityEngine.Random.Range(0, list.Count())].transform;
        }

        private IEnumerator IncreaseGameSpeed()
        {
            while (true)
            {
                GameSpeed += StaticConst.SpeedGrowPerSec * Time.deltaTime;
                GameSpeed = Math.Min(GameSpeed, StaticConst.MaxGameSpeed);
                yield return null;
            }
        }

        public void ResetSpeed()
        {
            GameSpeed = 1f;
        }
    }
}