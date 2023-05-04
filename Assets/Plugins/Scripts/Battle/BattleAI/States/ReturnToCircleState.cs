
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class ReturnToCircleState : State
	{
		public ReturnToCircleState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
		}


		public override void Enter()
		{
			enemy.CurrentEnemy.characterLocomotion.SetTarget(enemy.PlayerBattleCircle.GetRandomCirclePoint(), null, enemy.EnemyAiData.AttackDistance);
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			
			if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) >= enemy.PlayerBattleCircle.BattleCircleRadius)
			{
				stateMachine.ChangeState(enemyAiStates.CloseUpWithPlayerState);
			}
		}


		public override void Exit()
		{
			
		}
	}
}
