using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class IdleState : State
	{
	
	public IdleState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
			
		}

		public override void Enter()
		{
			
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= enemy.EnemyAiData.AggroDistance)
			{
				enemy.CurrentEnemy.GetCharacterAnimator().ResetState(0,0);
				stateMachine.ChangeState(enemyAiStates.SawPlayerState);
			}
		}


		public override void Exit()
		{
			
		}


	
	}
}
