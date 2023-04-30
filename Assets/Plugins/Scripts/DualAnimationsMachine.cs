using System;
using System.Collections.Generic;
using _src.Data.Finishers;
using BehaviorDesigner.Runtime;
using Cinemachine;
using Cysharp.Threading.Tasks;
using GameCreator.Characters;
using GameCreator.Melee;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Plugins.Scripts
{
	public class DualAnimationsMachine : SerializedMonoBehaviour
	{
		[SerializeField, Required]
		private CinemachineVirtualCamera virtualCamera;


		[SerializeField]
		private bool isEnemy;
		
		[SerializeField, Required, DisableIf ("isEnemy", Value = false)]
		private CharacterFeedbacks characterFeedbacks;


		


		public CinemachineVirtualCamera VirtualCamera
			=> virtualCamera;


		private CharacterAnimator attackerAnimatorController;

		private CharacterAnimator receiverAnimatorController;

		private bool isPlayingFinisher;


		public bool IsPlayingFinisher
			=> isPlayingFinisher;


		[SerializeField]
		private List<Finisher> finishers = new();


		public async UniTask StartDoubleAnimation(Character actorAttacker, Character actorReceiver)
		{
			if (!isEnemy)
				return;

			attackerAnimatorController = actorAttacker.GetComponent<CharacterAnimator>();
			receiverAnimatorController = actorReceiver.GetComponent<CharacterAnimator>();
			CharacterLock(actorAttacker);
			CharacterLock(actorReceiver);
			var attackerVirtualCamera = actorAttacker.GetComponent<DualAnimationsMachine>().virtualCamera;
			attackerVirtualCamera.m_Priority = 2;
			await PlayFinisher();
			attackerVirtualCamera.m_Priority = 0;
			CharacterUnlock(actorAttacker);
		}


		private void CharacterLock(Character character)
		{
			character.characterLocomotion.Stop();
			character.gameObject.GetComponent<CharacterMelee>().enabled = false;
			character.gameObject.GetComponent<CharacterController>().enabled = false;
			character.gameObject.TryGetComponent(out BehaviorTree behaviourTree);
			character.characterLocomotion.isControllable = false;

			//if is enemy
			if (behaviourTree)
			{
				behaviourTree.StopAllTaskCoroutines();
				behaviourTree.DisableBehavior();
				behaviourTree.enabled = false;
			}
		}


		private void CharacterUnlock(Character character)
		{
			character.characterLocomotion.Stop();
			character.gameObject.GetComponent<CharacterMelee>().enabled = true;
			character.gameObject.GetComponent<CharacterController>().enabled = true;
			character.characterLocomotion.isControllable = true;
		}


		private async UniTask PlayFinisher()
		{
			var dualAnimation = finishers[Random.Range(0, finishers.Count)];
			var attackerCharacter = attackerAnimatorController.GetComponent<Character>();
			var attackerFeedbacks = attackerAnimatorController.GetComponent<CharacterFeedbacks>();
			isPlayingFinisher = true;

			if (attackerAnimatorController && receiverAnimatorController)
			{
				StartListenFeedbacksOnAnimationEvent(attackerCharacter,
					attackerFeedbacks.CharacterFeedbacksCollection[CharacterFeedbacks.FeedBacksName.FinisherAttack]
				);

				attackerAnimatorController.CrossFadeGesture(dualAnimation.AttackerAnimation, 1, null, 0);
				receiverAnimatorController.StopGesture();
				receiverAnimatorController.SetState(dualAnimation.ReceiverAnimation, null, 1, 0, 1, CharacterAnimation.Layer.Layer1);

				await UniTask.Delay(
					TimeSpan.FromSeconds((int)Math.Round(dualAnimation.ReceiverAnimation.length))
				);

				isPlayingFinisher = false;
				StopListenFeedbacksOnAnimationEvent(attackerCharacter);
			}
		}


		private void StartListenFeedbacksOnAnimationEvent(Character character, MMF_Player feedbacks)
		{
			var animationControllerProxy = character.GetComponentInChildren<AnimationControllerProxy>();

			if (!animationControllerProxy)
			{
				Debug.LogError($"No have animationControllerProxy in {character.gameObject}");

				return;
			}

			animationControllerProxy.OnAttackAnimationEvent.AddListener(feedbacks.PlayFeedbacks);
		}


		private void StopListenFeedbacksOnAnimationEvent(Character character)
		{
			var animationControllerProxy = character.GetComponentInChildren<AnimationControllerProxy>();

			if (!animationControllerProxy)
			{
				Debug.LogError($"No have animationControllerProxy in {character.gameObject}");

				return;
			}

			animationControllerProxy.OnAttackAnimationEvent.RemoveAllListeners();
		}
	}
}
