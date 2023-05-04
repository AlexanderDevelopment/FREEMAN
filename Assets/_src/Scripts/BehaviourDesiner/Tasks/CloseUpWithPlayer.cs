using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using GameCreator.Core.Hooks;
using GameCreator.Melee;
using Plugins.Scripts;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("CloseUp with Player to attack distance.")]
	[TaskCategory("Attack")]
	public class CloseUpWithPlayer : Action
	{
		[BehaviorDesigner.Runtime.Tasks.Tooltip("Start moving towards the target if the target is further than the specified distance")]
		public SharedFloat attackDistance = 2;


		private Character _character;
		private Transform player;
		private float _attackDistance;
		private BattleCircle battleCircle;


		public override void OnAwake()
		{
			_character = gameObject.GetComponent<Character>();
			player = HookPlayer.Instance.transform;
			battleCircle = player.GetComponent<BattleCircle>();
			_attackDistance = (float)attackDistance.GetValue();
			
			if (!battleCircle.PlayerIsBusy)
			{
				_character.characterLocomotion.SetTarget(
					player.transform.position,
					null,
					this._attackDistance
				);
				battleCircle.AttackerAdd(_character);
			}
		}


		public override TaskStatus OnUpdate()
		{
			if (player == null)
				return TaskStatus.Failure;

			if (Vector3.Distance(player.position, _character.transform.position)
			    < (float)attackDistance.GetValue() + 0.3f)
			{
				_character.characterLocomotion.Stop();

				return TaskStatus.Success;
			}

			return TaskStatus.Running;
		}
		
	}
}
