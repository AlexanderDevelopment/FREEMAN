using Cysharp.Threading.Tasks;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class AttackPlayerState : State
	{
		private UniTask enemyAttack;


		public override void Enter()
		{
			enemyAttack = enemy.AttackCombo();
		}


		public override void Exit()
		{
			enemy.PlayerBattleCircle.AttackerRemove(enemy.CurrentEnemy);
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			
			if (enemyAttack.Status == UniTaskStatus.Succeeded)
			{
				stateMachine.ChangeState(enemyAiStates.ReturnToCircleState);
			}
		}


		public AttackPlayerState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
		}
	}
}
