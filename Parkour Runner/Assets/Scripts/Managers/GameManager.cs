using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Pick_Ups;
using Assets.Scripts.Pick_Ups.Bonuses;
using Assets.Scripts.Pick_Ups.Effects;
using Assets.Scripts.Player.InvectorMods;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            Destroy(this);
        }
    }

    #endregion

    public bool PlayerCanBeDismembered = true; //Можно ли оторвать конечность? Используется скриптом на каждой кости рэгдолла.

    public float VelocityToDismember = 10f;

    public int CoinsThisRun { get; private set; }
    public int CoinMultipiler = 1;

    public List<BonusName> ActiveBonuses;

    

    ////Состояние
    //Наличие конечностей
    private bool _leftHand = true;
    private bool _rightHand = true;
    private bool _leftLeg = true;
    private bool _rightLeg = true;

    private MuscleDismember[] _limbs;

    public List<Coin> Coins;

    private ParkourThirdPersonController _player;
    private Animator _playerAnimator;

    private HUDManager _hud;

    private void Start()
    {
        //TODO TEST
        //TODO добавляем это из генератора
        Coins = FindObjectsOfType<Coin>().ToList();
        //TODO TEST


        ActiveBonuses = new List<BonusName>();
        _hud = HUDManager.Instance;
        _limbs = FindObjectsOfType<MuscleDismember>();
        _player = FindObjectOfType<ParkourThirdPersonController>();
        _playerAnimator = _player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        _playerAnimator.SetBool("LeftHand", _leftHand);
        _playerAnimator.SetBool("RightHand", _rightHand);
        _playerAnimator.SetBool("LeftLeg", _leftLeg);
        _playerAnimator.SetBool("RightLeg", _rightLeg);
    }

    //Оторвать конечность (или приклеить обратно)
    public void SetLimbState(Bodypart bodypart, bool dismember)
    {
        switch (bodypart)
        {
            case (Bodypart.Body): //or
            case (Bodypart.Head):
                //if (dismember) Lose();
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
    }

    public bool HealLimb()
    {
        foreach (var limb in _limbs)
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
        }
    }

    public List<Coin> GetCoins()
    {
        return Coins;
    }
}
