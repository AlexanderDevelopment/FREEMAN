using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Melee;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Set character melee focus.")]
	[TaskCategory("Attack")]
	public class SetTargetFocus : Action
	{
		[SerializeField]
		private SharedGameObject _target;


		private CharacterMelee _melee;


		public override void OnAwake()
		{
			_melee = gameObject.GetComponent<CharacterMelee>();
		}


		public override TaskStatus OnUpdate()
		{
			if (_melee == null)
			{
				Debug.LogError($"[{nameof(SetTargetFocus)}] CharacterMelee is null");

				return TaskStatus.Failure;
			}

			var target = _target.Value;

			if (target == null)
			{
				Debug.LogError($"[{nameof(SetTargetFocus)}] Target is null");

				return TaskStatus.Failure;
			}

			var targetMelee = target.GetComponent<TargetMelee>();

			if (targetMelee == null)
			{
				Debug.LogError($"[{nameof(SetTargetFocus)}] Target melee is null");

				return TaskStatus.Failure;
			}

			_melee.SetTargetFocus(targetMelee);

			return TaskStatus.Success;
		}
	}
}
