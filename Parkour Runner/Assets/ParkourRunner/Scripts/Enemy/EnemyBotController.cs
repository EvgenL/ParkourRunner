using UnityEngine;

namespace ParkourRunner.Scripts.Enemy
{
    public class EnemyBotController : MonoBehaviour
    {

        public bool ChangeAttackPosition;
        public Vector3 AttackPosition;

        public bool ChangeAttackHeight;
        public float AttackHeight;

        public bool ChangeAttackSpeed;
        public float AttackSpeed;

        public bool ChangeAttackLookTarget;
        public Vector3 AttackLookTarget;

        public float _frontOffsetFromPlayer;
        public float _flyHeight;
        public float _maxSpeed;


        public Vector3 FormationPosOffset;

        protected Vector3 _targetPosition;

        public Transform Player;

        public BotState State;


        private void FixedUpdate()
        {
            switch (State)
            {
                case BotState.Stay:
                    return;

                case BotState.Enter:
                    FlyToTargetPos();
                    break;

                case BotState.Follow:
                    FollowPlayer();
                    break;

                case BotState.Attack:
                    AttackPlayer();
                    break;
            }

        }

        //Робот занимает позицию перед игроком
        private void FlyToTargetPos()
        {
            _targetPosition = Player.position + new Vector3(0, _flyHeight, _frontOffsetFromPlayer);
            _targetPosition += FormationPosOffset;
            var newPos = Vector3.Lerp(transform.position, _targetPosition, _maxSpeed * Time.fixedDeltaTime);
            transform.position = newPos;
        }

        private void FollowPlayer()
        {
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
            _targetPosition += FormationPosOffset;
            /*
        if (ChangeAttackLookTarget)
        {
            var newRot = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(AttackLookTarget - transform.position), 0.1f);
            transform.rotation = newRot;
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        */
            float speed = ChangeAttackSpeed ? AttackSpeed : _maxSpeed;

            var newPos = Vector3.Lerp(transform.position, _targetPosition, speed * Time.fixedDeltaTime);
            transform.position = newPos;
        }

        private void AttackPlayer()
        {
            throw new System.NotImplementedException();
        }
    }
}
