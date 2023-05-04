using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class CloseUpWithPlayerState : State
	{
		

		public override void Enter()
		{
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			
			if (!enemy.PlayerBattleCircle.PlayerIsBusy)
			{
				enemy.PlayerBattleCircle.AttackerAdd(enemy.CurrentEnemy);
				enemy.CurrentEnemy.characterLocomotion.SetTarget(enemy.Player.transform.position, null, 0);

				
			}

			if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= enemy.EnemyAiData.AttackDistance)
			{
				stateMachine.ChangeState(enemyAiStates.AttackPlayerState);
			}
			
		}


		public override void Exit()
		{
			
		}


		public CloseUpWithPlayerState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
		}
	}
}
