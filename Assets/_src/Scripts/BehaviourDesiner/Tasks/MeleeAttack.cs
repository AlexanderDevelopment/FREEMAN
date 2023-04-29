using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Melee;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Melee attack.")]
	[TaskCategory("Attack")]
	public class MeleeAttack : Action
	{
		[SerializeField]
		private CharacterMelee.ActionKey _actionKey;


		private CharacterMelee _melee;


		public override void OnAwake()
		{
			_melee = gameObject.GetComponent<CharacterMelee>();
		}


		public override TaskStatus OnUpdate()
		{
			if (_melee == null)
			{
				Debug.LogError($"[{nameof(MeleeAttack)}] CharacterMelee is null");

				return TaskStatus.Failure;
			}

			_melee.Execute(_actionKey);

			return TaskStatus.Success;
		}
	}
}
