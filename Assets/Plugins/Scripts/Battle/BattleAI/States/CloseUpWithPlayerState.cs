using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class CloseUpWithPlayerState : State
	{

		private DualAnimationsMachine playerDualAnimationsMachine;

		public override void Enter()
		{
			playerDualAnimationsMachine = enemy.Player.GetComponent<DualAnimationsMachine>();
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			
			if (!enemy.PlayerBattleCircle.PlayerIsBusy && !playerDualAnimationsMachine.IsPlayingFinisher)
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
