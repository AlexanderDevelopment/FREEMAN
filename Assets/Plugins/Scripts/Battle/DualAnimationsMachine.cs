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
		private List<CinemachineVirtualCamera> finisherCameras;


		[SerializeField]
		private bool isEnemy;


		[SerializeField, Required, DisableIf("isEnemy", Value = false)]
		private CharacterFeedbacks characterFeedbacks;



		public List<CinemachineVirtualCamera> FinisherCameras
			=> finisherCameras;


		private CharacterAnimator attackerAnimatorController;

		private CharacterAnimator receiverAnimatorController;


		[ShowInInspector, ReadOnly]
		public bool isPlayingFinisher;



		[SerializeField]
		private List<Finisher> finishers = new();


		public async UniTask StartDoubleAnimation(Character attacker, Character receiver)
		{
			if (!isEnemy)
				return;

			var attackerDualAnimationsMachine = attacker.GetComponent<DualAnimationsMachine>();
			attackerAnimatorController = attacker.GetComponent<CharacterAnimator>();
			receiverAnimatorController = receiver.GetComponent<CharacterAnimator>();
			attackerDualAnimationsMachine.isPlayingFinisher = isPlayingFinisher = true;

			CharacterLock(attacker);
			CharacterLock(receiver);

			//Important rotate character after lock its, becoause we disable they's character controllers which block hard rotation
			RotateActorsEachOthers(attacker, receiver);

			var attackerVirtualCamera = attackerDualAnimationsMachine.finisherCameras[Random.Range(0, attackerDualAnimationsMachine.finisherCameras.Count)];
			attackerVirtualCamera.m_Priority = 2;
			await PlayFinisher();
			attackerVirtualCamera.m_Priority = 0;
			CharacterUnlock(attacker);
			attackerDualAnimationsMachine.isPlayingFinisher = isPlayingFinisher = false;
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

				StopListenFeedbacksOnAnimationEvent(attackerCharacter);
			}
		}


		private void RotateActorsEachOthers(Character attacker, Character receiver)
		{
			var attackerDirection = receiver.gameObject.transform.position - attacker.gameObject.transform.position;
			var receiverDirection = attacker.gameObject.transform.position - receiver.gameObject.transform.position;
			attacker.characterLocomotion.SetRotation(attackerDirection);
			receiver.characterLocomotion.SetRotation(receiverDirection);
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
