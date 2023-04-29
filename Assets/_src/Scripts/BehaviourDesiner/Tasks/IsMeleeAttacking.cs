using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Melee;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Is melee attacking.")]
	[TaskCategory("Attack")]
	public class IsMeleeAttacking : Conditional
	{
		[SerializeField]
		private TaskStatus _trueStatus;


		[SerializeField]
		private TaskStatus _falseStatus;


		private CharacterMelee _melee;


		public override void OnAwake()
		{
			_melee = gameObject.GetComponent<CharacterMelee>();
		}


		public override TaskStatus OnUpdate()
		{
			if (_melee == null)
			{
				Debug.LogError($"[{nameof(IsMeleeAttacking)}] CharacterMelee is null");

				return _falseStatus;
			}

			return _melee.IsAttacking ? _trueStatus : _falseStatus;
		}
	}
}
