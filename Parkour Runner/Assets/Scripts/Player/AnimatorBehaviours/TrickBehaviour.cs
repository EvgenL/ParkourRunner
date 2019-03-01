using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.AnimatorBehaviours
{
    public class TrickBehaviour : StateMachineBehaviour
    {
        public bool OverrideActionParameters = true;


        public bool disableGravity = true;
        public bool disableCollision = true;
        public float exitTime = 0.8f;

        public AvatarTarget avatarTarget;
        public Vector3 matchTargetMask;
        public float startMatchTarget;
        public float endMatchTarget;
        public bool resetPlayerSettings  = true;

        public bool UseRootMotion = true;

        public bool SwitchHandsOnDismember = true;

        private bool _oldUseRootMotion;

        private bool _handsSwitched = false;

        private vTriggerGenericAction _oldActionParameters = new vTriggerGenericAction();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            if (!OverrideActionParameters) return;
            if (vThirdPersonController.instance == null) return;
            //Получаем из игрока скрипт, взаимодействующий с триггерами
            var action = vThirdPersonController.instance.GetComponent<GenericActionPlusPuppet>();
            _oldUseRootMotion = action.useRootMotion;
            action.useRootMotion = UseRootMotion;
        
            //Если нет руки, которая нужна для трюка - отражаем анимацию
            CheckLimbs(animator);

            //Получаем из игрока информацию о триггере, на который он наступил
            vTriggerGenericAction trigger = action.triggerAction;
            if (trigger)
            {
                //Сохораняем старые параметры с триггера
                _oldActionParameters.endExitTimeAnimation = trigger.endExitTimeAnimation;
                _oldActionParameters.matchTargetMask = trigger.matchTargetMask;
                _oldActionParameters.avatarTarget = trigger.avatarTarget;
                _oldActionParameters.startMatchTarget = trigger.startMatchTarget;
                _oldActionParameters.endMatchTarget = trigger.endMatchTarget;
                _oldActionParameters.disableGravity = trigger.disableGravity;
                _oldActionParameters.disableCollision = trigger.disableCollision;
                _oldActionParameters.resetPlayerSettings = trigger.resetPlayerSettings;

                //Записываем в него новые
                trigger.endExitTimeAnimation = exitTime;
                trigger.matchTargetMask = matchTargetMask;
                trigger.avatarTarget = avatarTarget;
                trigger.startMatchTarget = startMatchTarget;
                trigger.endMatchTarget = endMatchTarget;
                trigger.disableGravity = disableGravity;
                trigger.disableCollision = disableCollision;
                trigger.resetPlayerSettings = resetPlayerSettings;
            }
        }

        private void CheckLimbs(Animator animator)
        {
            if (!SwitchHandsOnDismember) return;
            if (animator.GetBool("RightHand") && animator.GetBool("LeftHand"))
            {
                _handsSwitched = false;
                return;
            }

            //Если нужна левая рука
            if (avatarTarget == AvatarTarget.LeftHand)
            {
                //но она отвалилась
                if (!animator.GetBool("LeftHand"))
                {
                    //Будем делать трюк правой рукой
                    avatarTarget = AvatarTarget.RightHand;
                    animator.SetBool("MirrorHands", true);

                    //_handsSwitched = true;
                }
                /*else if (_handsSwitched)
                {
                    animator.SetBool("MirrorHands", false);
                }*/
            }
            /*
            //Если нужна правая рука
            if (avatarTarget == AvatarTarget.RightHand)
            {
                //но она отвалилась
                if (!animator.GetBool("RightHand"))
                {
                    //Будем делать трюк левой рукой
                    avatarTarget = AvatarTarget.LeftHand;
                    animator.SetBool("MirrorHands", true);
                    _handsSwitched = true;
                }
                else if (_handsSwitched)
                {
                    animator.SetBool("MirrorHands", false);
                }
            }*/
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {

            if (!OverrideActionParameters) return;
            if (vThirdPersonController.instance == null) return;

            var action = vThirdPersonController.instance.GetComponent<GenericActionPlusPuppet>();
            action.useRootMotion = _oldUseRootMotion;

            vTriggerGenericAction trigger = action.triggerAction;
            trigger.endExitTimeAnimation = _oldActionParameters.endExitTimeAnimation;
            trigger.matchTargetMask = _oldActionParameters.matchTargetMask;
            trigger.avatarTarget = _oldActionParameters.avatarTarget;
            trigger.startMatchTarget = _oldActionParameters.startMatchTarget;
            trigger.endMatchTarget = _oldActionParameters.endMatchTarget;
            trigger.disableGravity = _oldActionParameters.disableGravity;
            trigger.disableCollision = _oldActionParameters.disableCollision;
            trigger.resetPlayerSettings = _oldActionParameters.resetPlayerSettings;
        }


    }
}
