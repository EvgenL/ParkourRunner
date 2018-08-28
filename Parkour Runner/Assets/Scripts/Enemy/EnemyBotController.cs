using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;

public enum BotState
{
    Enter,
    Follow,
    Stay
}

public class EnemyBotController : MonoBehaviour
{

    public bool ChangeAttackPosition;
    public Vector3 AttackPosition;

    public bool ChangeAttackHeight;
    public float AttackHeight;

    public bool ChangeAttackSpeed;
    public float AttackSpeed;

    public float _frontOffsetFromPlayer;
    public float _flyHeight;
    public float _maxSpeed;
    
    private Vector3 _targetPosition;

    public Transform Player;

    protected BotState _botState;


    private void FixedUpdate()
    {
        if (_botState == BotState.Stay) return;
        //TODO random attack position
        if (ChangeAttackPosition)
        {
            _targetPosition = AttackPosition;
        }
        else if (ChangeAttackHeight)
        {
            _targetPosition = Player.position + new Vector3(0, AttackHeight, _frontOffsetFromPlayer);
        }
        else 
        {
            _targetPosition = Player.position + new Vector3(0, _flyHeight, _frontOffsetFromPlayer);
        }

        float speed = ChangeAttackSpeed ? AttackSpeed : _maxSpeed;

        var newPos = Vector3.Lerp(transform.position, _targetPosition, speed * Time.fixedDeltaTime);
        newPos.z = _targetPosition.z; //По оси z робот будет иметь скорость игрока
        transform.position = newPos;
    }
}
