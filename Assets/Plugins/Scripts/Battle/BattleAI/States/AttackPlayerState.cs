using Cysharp.Threading.Tasks;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class AttackPlayerState : State
	{
		private UniTask enemyAttack;
		private DualAnimationsMachine playerDualAnimationsMachine;
		private bool startAttack;


		public override void Enter()
		{
			playerDualAnimationsMachine = enemy.Player.GetComponent<DualAnimationsMachine>();
		}


		public override void Exit()
		{
			startAttack = false;
			enemy.PlayerBattleCircle.AttackerRemove(enemy.CurrentEnemy);
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}

			if (!playerDualAnimationsMachine.isPlayingFinisher && !startAttack)
			{
				enemyAttack = enemy.AttackCombo();
				startAttack = true;
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
