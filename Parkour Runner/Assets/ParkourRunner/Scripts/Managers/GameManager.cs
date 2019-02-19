﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.Player;
using ParkourRunner.Scripts.Player.InvectorMods;
using ParkourRunner.Scripts.Track.Generator;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using RootMotion.Dynamics;
using UnityEngine;
using AEngine;

namespace ParkourRunner.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnDie;

        public enum GameState
        {
            Run,
            Pause,
            Dead
        }
                
        #region Singleton
        public static GameManager Instance;
        private AudioManager _audio;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                this.ActiveBonuses = new List<BonusName>();
                _wallet = Wallet.Instance;
                _audio = AudioManager.Instance;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public List<BonusName> ActiveBonuses { get; set; }

        public MuscleDismember[] Limbs;
        
        public GameState gameState { get; private set; }

        public bool PlayerCanBeDismembered = true; //Можно ли оторвать конечность? Используется скриптом на каждой кости рэгдолла.

        public float VelocityToDismember = 10f;

        public int CoinMultipiler = 1;
        public float TrickMultipiler = 1;

        public float GameSpeed = 1f;

        public int ReviveCost
        {
            get { return (StaticConst.InitialReviveCost + (int) DistanceRun / 10) * (_revives + 1); }
        }

        public float DistanceRun;
        private float _distanceRunOffset; //TODO origin reset

        private int _revives; //Сколько раз игрок возрождался за этот забег?


        ////Состояние
        //Наличие конечностей
        [SerializeField] private bool _leftHand = true;
        [SerializeField] private bool _rightHand = true;
        [SerializeField] private bool _leftLeg = true;
        [SerializeField] private bool _rightLeg = true;


        private ParkourThirdPersonController _player;
        private Animator _playerAnimator;

        private HUDManager _hud;
        private Wallet _wallet;
        
        private void Start()
        {
            FindObjectOfType<BehaviourPuppet>().onLoseBalance.unityEvent.AddListener(ResetSpeed);

            _hud = HUDManager.Instance;
            Limbs = FindObjectsOfType<MuscleDismember>();
            _player = ParkourThirdPersonController.instance;
            _playerAnimator = _player.GetComponent<Animator>();
            
            StartGame();

            _audio.LoadAudioBlock(AudioBlocks.Game);
            _audio.PlayMusic();
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
                    if (!dismember)
                    {
                        print("HEAD DIE");
                        Die();
                    }
                    else
                    {
                        ParkourCamera.Instance.OnHeadRegenerated();
                    }
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

            _playerAnimator.SetBool("LeftHand", _leftHand);
            _playerAnimator.SetBool("RightHand", _rightHand);
            _playerAnimator.SetBool("LeftLeg", _leftLeg);
            _playerAnimator.SetBool("RightLeg", _rightLeg);

            if (!dismember)
                CheckLimbLost(); //Смотрим не проиграли ли мы ещё
        }

        private void CheckLimbLost()
        {
                if (!_leftHand && !_rightHand)
                {
                    Die();
                }
                else if (!_leftLeg && !_rightLeg)
                {

                    Die();
                }
        }

        private void Die()
        {
            if (gameState != GameState.Dead)
            {
                gameState = GameState.Dead;
                _player.Die();
                OnDie.SafeInvoke();

                this.ActiveBonuses.Clear();
                _player.Immune = false;

                Invoke("ShowPostMortem", 4f);

                _audio.PlaySound(Sounds.GameOver);
            }
        }

        public void ShowPostMortem()
        {
            _hud.ShowPostMortem();
        }

        public void Revive()
        {
            var cb = LevelGenerator.Instance.CenterBlock;
            Vector3 newPos = cb.transform.position;
            newPos.z -= LevelGenerator.Instance.BlockSide / 2f - 2f;
            FindObjectOfType<PuppetMaster>().enabled = false; //mode = PuppetMaster.Mode.Disabled;
                _player.transform.position = newPos;
            FindObjectOfType<PuppetMaster>().enabled = true; //.mode = PuppetMaster.Mode.Kinematic;
            _player.transform.root.position = newPos;


            //Heal player
            gameState = GameState.Run;
            HealFull();
            _player.Revive();

            _revives++;
        }

        private void HealFull()
        {
            while (HealLimb()) ; //Heal while theres limbs
        }

        public bool HealLimb() //public чтобы лечиться с heal бонуса
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

        public void DoTrick(Trick trick)
        {
            AddCoin((int)(trick.MoneyReward * CoinMultipiler * TrickMultipiler));
            HUDManager.Instance.ShowTrickReward(trick, TrickMultipiler * CoinMultipiler);
        }

        public void AddCoin(int amount = 1)
        {
            _wallet.AddCoins(amount * CoinMultipiler, Wallet.WalletMode.InGame);
            _audio.PlaySound(Sounds.Coin);
        }

        public void AddBonus(BonusName bonusName)
        {
            switch (bonusName)
            {
                case (BonusName.Magnet):
                    GetComponent<MagnetBonus>().RefreshTime();
                    break;
                case (BonusName.Jump):
                    GetComponent<JumpBonus>().RefreshTime();
                    break;
                case (BonusName.Shield):
                    GetComponent<ShieldBonus>().RefreshTime();
                    break;
                case (BonusName.DoubleCoins):
                    GetComponent<DoubleCoinsBonus>().RefreshTime();
                    break;
                case (BonusName.Boost):
                    GetComponent<BoostBonus>().RefreshTime();
                    break;
            }
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

        public void OnHeadLost(Transform head)
        {
            ParkourCamera.Instance.OnHeadLost(head);//todo
        }

        public void Pause()
        {
            gameState = GameState.Pause;
            ParkourSlowMo.Instance.SmoothlyStopTime();
        }

        public void UnPause()
        {
            gameState = GameState.Run;
            ParkourSlowMo.Instance.SmoothlyContinueTime();
        }
    }
}