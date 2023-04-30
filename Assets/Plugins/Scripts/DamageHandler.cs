using System;
using BehaviorDesigner.Runtime;
using GameCreator.Characters;
using GameCreator.Stats;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _src.Plugins.Scripts
{
    public class DamageHandler: MonoBehaviour
    {
        [SerializeField, AttributeSelector]
        private AttrAsset healthAttribute;
        
        [SerializeField, StatSelector]
        private StatAsset strength;
        [Title("Death")]
        [SerializeField] private AnimationClip clip;

        [SerializeField] private AvatarMask avatarMask;

        [SerializeField] private float transitionTime = 0;


        [SerializeField] private float speed = 1.0f;


        private CharacterAnimator characterAnimator;
        private CharacterController characterController;
        private Character character;
        private float waitUntil = -1f;
        private bool isDead;
        

        public float UnitDamage
            => stats.GetStat(strength.stat.uniqueName);

        public bool IsDead
            => stats.GetAttrValue(healthAttribute.attribute.uniqueName) <= 0;

        [SerializeField]
        private Stats stats;

        public UnityEvent<float> OnRecieveDamage = new();

        private void Start()
        {
            TryGetComponent(out stats);
            if (!stats)
                Debug.LogError($"No have stats on {gameObject.name}");
            OnRecieveDamage.AddListener(ApplyDamage);
        }


        private void ApplyDamage(float damage)
        {
            var currentHealth = stats.GetAttrValue(healthAttribute.attribute.uniqueName);
            stats.SetAttrValue(healthAttribute.attribute.uniqueName, currentHealth - damage);
            CheckHealthForZero();
        }

        private void CheckHealthForZero()
        {
            var currentHealth = stats.GetAttrValue(healthAttribute.attribute.uniqueName);
            if (currentHealth <= 0)
                Death();
        }

        private void Death()
        {
            if (!isDead)
            {
                TryGetComponent(out character);
                TryGetComponent(out characterAnimator);
                TryGetComponent(out characterController);
                TryGetComponent(out BehaviorTree behaviorTree);
                if (characterAnimator == null || clip == null || characterController == null || character == null)
                {
                    Debug.LogError("[One of required components is null");
                    return;
                }

                if (behaviorTree)
                {
                    behaviorTree.DisableBehavior();
                    behaviorTree.enabled = false;
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