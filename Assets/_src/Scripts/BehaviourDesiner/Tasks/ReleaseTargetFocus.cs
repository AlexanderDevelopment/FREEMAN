using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Melee;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Release character melee focus.")]
	[TaskCategory("Attack")]
	public class ReleaseTargetFocus : Action
	{
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

			_melee.ReleaseTargetFocus();

			return TaskStatus.Success;
		}
	}
}
