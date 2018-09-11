using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Pick_Ups;
using Assets.Scripts.Pick_Ups.Bonuses;
using Assets.Scripts.Pick_Ups.Effects;
using Assets.Scripts.Player.InvectorMods;
using RootMotion.Dynamics;
using UnityEngine;

enum GameState
{
    Run,
    Pause,
    Dead
}

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
            Destroy(gameObject);
        }
    }

    #endregion

    public bool PlayerCanBeDismembered = true; //Можно ли оторвать конечность? Используется скриптом на каждой кости рэгдолла.

    public float VelocityToDismember = 10f;

    public int CoinsThisRun { get; private set; }
    public int CoinMultipiler = 1;


    public List<BonusName> ActiveBonuses;

    public float DistanceRun;
    private float _distanceRunOffset;

    private GameState GameState;

    ////Состояние
    //Наличие конечностей
    private bool _leftHand = true;
    private bool _rightHand = true;
    private bool _leftLeg = true;
    private bool _rightLeg = true;

    public MuscleDismember[] Limbs;

    public List<Coin> Coins;

    private ParkourThirdPersonController _player;
    private Animator _playerAnimator;

    private HUDManager _hud;

    private void Start()
    {

        GameState = GameState.Run;


        ActiveBonuses = new List<BonusName>();
        _hud = HUDManager.Instance;
        Limbs = FindObjectsOfType<MuscleDismember>();
        _player = FindObjectOfType<ParkourThirdPersonController>();
        _playerAnimator = _player.GetComponent<Animator>();
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
                if (dismember) GameState = GameState.Dead;
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
            GameState = GameState.Dead;
        }
        if (!_leftLeg && !_rightLeg)
        {
            GameState = GameState.Dead;
        }

        if (GameState == GameState.Dead)
        {
            Die();
        }
    }

    private void Die()
    {
        _player.Die();
        //TODO Предложить просмотр рекламы
    }

    private void Revive()
    {
        throw new NotImplementedException();

        while (HealLimb());
        _player.Revive();
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
}
