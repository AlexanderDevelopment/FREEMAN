using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using UnityEngine;

namespace _src.Scripts.Ai.Tasks
{
    public class Death : Action
    {
        [SerializeField] private AnimationClip clip;

        [SerializeField] private AvatarMask avatarMask;

        [SerializeField] private float transitionTime = 0;


        [SerializeField] private float speed = 1.0f;


        private CharacterAnimator characterAnimator;
        private CharacterController characterController;
        private Character character;
        private float waitUntil = -1f;
        private bool isDead;


        public override void OnStart()
        {
            if (!isDead)
            {
                TryGetComponent(out character);
                TryGetComponent(out characterAnimator);
                TryGetComponent(out characterController);
                if (characterAnimator == null || clip == null || characterController == null || character == null)
                {
                    Debug.LogError($"[{nameof(PlayGesture)}] One of required components is null", Owner);

                    return;
                }

                characterController.enabled = false;
                character.characterLocomotion.Stop();
                characterAnimator.SetState(
                    this.clip,
                    this.avatarMask,
                    1,
                    transitionTime,
                    speed,
                    CharacterAnimation.Layer.Layer1
                );
                isDead = true;
            }
        }
    }
}