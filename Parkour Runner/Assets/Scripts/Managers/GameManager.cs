using System.Collections;
using System.Collections.Generic;
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

    ////Состояние
    //Наличие конечностей
    private bool _leftHand = true;
    private bool _rightHand = true;
    private bool _leftLeg = true;
    private bool _rightLeg = true;



    private ParkourThirdPersonController _player;
    private Animator _playerAnimator;

    private void Start()
    {
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
    public void Limb(Bodypart bodypart, bool dismember)
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
}
