using System;
using System.Collections.Generic;
using GameCreator.Characters;
using GameCreator.Melee;
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class SawPlayerState : State
	{
		


		public override void Enter()
		{
			var closetsPoint = enemy.PlayerBattleCircle.GetClosestCirclePosition(enemy.transform.position);
			enemy.CurrentEnemy.characterLocomotion.SetTarget(closetsPoint, null, 0);
		}


		public override void LogicUpdate()
		{
			if (enemy.DamageHandler.IsDead)
			{
				stateMachine.ChangeState(enemyAiStates.DeathState);
			}
			
			if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= enemy.PlayerBattleCircle.BattleCircleRadius)
			{
				stateMachine.ChangeState(enemyAiStates.CloseUpWithPlayerState);
			}
			
		}


		public override void Exit()
		{
			enemy.CurrentEnemy.characterLocomotion.Stop();
			enemy.SetTargetFocus(enemy.Player.GetComponent<CharacterMelee>());
		}


		public SawPlayerState(IEnemyAiBehaviour enemy, IEnemyAiStates enemyAiStates, StateMachine stateMachine) : base(enemy, enemyAiStates, stateMachine)
		{
			
		}
	}
}
