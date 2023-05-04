using System;


namespace Plugins.Scripts.Battle.BattleAI
{
	public abstract class State
	{
		protected IEnemyAiBehaviour enemy;
		protected IEnemyAiStates enemyAiStates;
		protected StateMachine stateMachine;


		protected State(IEnemyAiBehaviour enemy,IEnemyAiStates enemyAiStates, StateMachine stateMachine)
		{
			this.enemy = enemy;
			this.enemyAiStates = enemyAiStates;
			this.stateMachine = stateMachine;
		}


		public abstract void Enter();



		public abstract void LogicUpdate();



		public abstract void Exit();
	}
}
