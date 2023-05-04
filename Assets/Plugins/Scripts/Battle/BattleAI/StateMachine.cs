using System;
using Sirenix.OdinInspector;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class StateMachine
	{
		public State CurrentState { get; private set; }


		[ShowInInspector]
		public string CurrentStateName
			=> CurrentState.ToString();
		public void Initialize(State startingState)
		{
			CurrentState = startingState;
			startingState.Enter();
		}

		public void ChangeState(State newState)
		{
			CurrentState.Exit();

			CurrentState = newState;
			newState.Enter();
		}
	}
}
