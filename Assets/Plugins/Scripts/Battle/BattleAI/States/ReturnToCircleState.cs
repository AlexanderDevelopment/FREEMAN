
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class ReturnToCircleState : State
	{
		private Vector3 circlePoint;
		public ReturnToCircleState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
		}


		public override void Enter()
		{
			circlePoint = enemy.PlayerBattleCircle.GetRandomCirclePoint();
			enemy.CurrentEnemy.characterLocomotion.SetTarget(circlePoint, null, 0);
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			if (Vector3.Distance(enemy.transform.position, circlePoint) <= 1f)
			{
				stateMachine.ChangeState(enemyAiStates.CloseUpWithPlayerState);
			}
		}


		public override void Exit()
		{
			
		}
	}
}
