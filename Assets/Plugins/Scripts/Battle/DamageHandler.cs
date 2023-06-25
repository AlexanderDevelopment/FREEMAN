
using Cysharp.Threading.Tasks;
using GameCreator.Characters;
using GameCreator.Melee;
using GameCreator.Stats;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


namespace Plugins.Scripts
{
	public class DamageHandler : MonoBehaviour
	{
		[SerializeField, AttributeSelector]
		private AttrAsset healthAttribute;


		[SerializeField, StatSelector]
		private StatAsset strength;


		[SerializeField, Required]
		private DualAnimationsMachine dualAnimationsMachine;


		[SerializeField]
		private float finisherPercentStarter = 20;


		[SerializeField]
		private float finisherChance = 100;


		[Title("Feedbacks")]
		[SerializeField, Required]
		private MMF_Player lowHealthFeedbacks;


		[SerializeField]
		private float lowHealthFeedbacksActivationHealthValue;


		[Title("Death")]
		[SerializeField]
		private AnimationClip clip;


		[SerializeField, Required]
		private MMF_Player deathFeedbacks;


		[SerializeField]
		private AvatarMask avatarMask;


		[SerializeField]
		private float transitionTime = 0;


		[SerializeField]
		private float speed = 1.0f;


		private CharacterAnimator characterAnimator;
		private CharacterController characterController;
		private Character character;
		private float waitUntil = -1f;
		private bool isDead;


		public float UnitDamage
			=> stats.GetStat(strength.stat.uniqueName);


		public bool IsDead
			=> stats.GetAttrValue(healthAttribute.attribute.uniqueName) <= 0;


		public float CurrentHealth
			=> stats.GetAttrValue(healthAttribute.attribute.uniqueName);


		[SerializeField]
		private Stats stats;


		public Stats Stats
			=> stats;


		public UnityEvent<float, Character, Character> OnReceiveDamage = new();

		public UnityEvent OnPlayerDead = new();

		private void Start()
		{
			TryGetComponent(out stats);

			if (!stats)
				Debug.LogError($"No have stats on {gameObject.name}");

			OnReceiveDamage.AddListener((damage, attacker, receiver) =>
				{
					ApplyDamage(damage);

					if (!isDead && !dualAnimationsMachine.isPlayingFinisher)
						CheckHealthForStartFinisher(attacker, receiver);
				}
			);
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

			if (currentHealth <= lowHealthFeedbacksActivationHealthValue)
				lowHealthFeedbacks.PlayFeedbacks();

			if (currentHealth <= 0)
			{
				lowHealthFeedbacks.StopFeedbacks();
				Death();
			}
		}


		private async UniTask CheckHealthForStartFinisher(Character attacker, Character receiver)
		{
			if (Random.Range(0, 100) <= finisherChance)
			{
				var currentHealth = stats.GetAttrValuePercent(healthAttribute.attribute.uniqueName);

				if (currentHealth <= finisherPercentStarter * 0.01)
				{
					ApplyDamage(CurrentHealth);
					await dualAnimationsMachine.StartDoubleAnimation(attacker, receiver);
				}
			}
		}



		private void Death()
		{
			if (!isDead)
			{
				if (deathFeedbacks)
					deathFeedbacks.PlayFeedbacks();
				
				isDead = true;
				TryGetComponent(out character);
				TryGetComponent(out characterAnimator);
				TryGetComponent(out characterController);
				TryGetComponent(out CharacterMelee melee);

				if (characterAnimator == null || clip == null || characterController == null || character == null ||
				    melee == null)
				{
					Debug.LogError("[One of required components is null");

					return;
				}
				

				characterController.enabled = false;
				melee.ReleaseTargetFocus();
				melee.enabled = false;
				character.characterLocomotion.Stop();



				if (dualAnimationsMachine.isPlayingFinisher)
					return;



				characterAnimator.SetState(
					this.clip,
					this.avatarMask,
					1,
					transitionTime,
					speed,
					CharacterAnimation.Layer.Layer1
				);
			}
			OnPlayerDead.Invoke();
		}
	}
}
