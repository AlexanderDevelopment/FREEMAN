using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using UnityEngine;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Play gesture.")]
	[TaskCategory("Animations")]
	public class PlayGesture : Action
	{
		[SerializeField]
		private AvatarMask _avatarMask;


		[SerializeField]
		private AnimationClip _clip;


		[SerializeField]
		private float _fadeIn = 0.1f;


		[SerializeField]
		private float _fadeOut = 0.1f;


		[SerializeField]
		private float _speed = 1.0f;


		[SerializeField]
		private bool _waitToComplete = true;


		[SerializeField]
		[Tooltip("[0.0f, 1.0f]")]
		private float _animClipExitPoint;


		private CharacterAnimator _characterAnimator;
		private float _waitUntil = -1f;


		public override void OnAwake()
		{
			_characterAnimator = gameObject.GetComponent<CharacterAnimator>();
		}


		public override void OnStart()
		{
			if (_characterAnimator == null || _clip == null)
			{
				Debug.LogError($"[{nameof(PlayGesture)}] Animator or Clip is null", Owner);

				return;
			}

			_waitUntil = Time.time + _clip.length * _animClipExitPoint / _speed;
			_characterAnimator.CrossFadeGesture(_clip, _speed, _avatarMask, _fadeIn, _fadeOut);
		}


		public override TaskStatus OnUpdate()
		{
			if (_waitToComplete)
			{
				if (Time.time <= _waitUntil)
					return TaskStatus.Running;
			}

			return TaskStatus.Success;
		}


		public override void OnEnd()
		{
			if (_waitToComplete && _characterAnimator)
				_characterAnimator.StopGesture(_fadeOut);

			_waitUntil = -1f;
		}
	}
}
