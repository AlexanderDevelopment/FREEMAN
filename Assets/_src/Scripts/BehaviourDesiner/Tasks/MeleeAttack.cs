using System;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using GameCreator.Melee;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Melee attack.")]
	[TaskCategory("Attack")]
	public class MeleeAttack : Action
	{
		[SerializeField]
		private CharacterMelee.ActionKey _actionKey;


		[SerializeField]
		private float attackInterval;


		[SerializeField]
		private int attackCount;


		private CharacterMelee _melee;

		private UniTask attack;


		public override void OnAwake()
		{
			_melee = gameObject.GetComponent<CharacterMelee>();
			attack = AttackCombo();
		}


		public override TaskStatus OnUpdate()
		{
			if (_melee == null)
			{
				Debug.LogError($"[{nameof(MeleeAttack)}] CharacterMelee is null");

				return TaskStatus.Failure;
			}

			if (attack.Status == UniTaskStatus.Pending)
			{
				return TaskStatus.Running;
			}
			else
			{
				return TaskStatus.Success;
			}
		}


		private async UniTask AttackCombo()
		{
			for (int i = 0; i < attackCount; i++)
			{
				_melee.Execute(_actionKey);
				await UniTask.Delay(TimeSpan.FromSeconds(attackInterval));
			}
		}
	}
}
