using System;
using System.ComponentModel;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using GameCreator.Characters;
using GameCreator.Core.Hooks;
using GameCreator.Melee;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public class EnemyAiBehaviour : SerializedMonoBehaviour, IEnemyAiBehaviour, IEnemyAiStates
	{
		[SerializeField]
		private EnemyAiData _enemyAiData;


		private BattleCircle playerBattleCircle;

		[ShowInInspector, Sirenix.OdinInspector.ReadOnly]
		private StateMachine stateMachine;
		private AttackPlayerState attackPlayerState;
		private IdleState idleState;
		private CloseUpWithPlayerState closeUpWithPlayerState;
		private SawPlayerState sawPlayerState;
		private ReturnToCircleState returnToCircleState;
		private DeathState deathState;

		private Character player;
		private Character currentEnemy;
		private CharacterMelee characterMelee;
		private DamageHandler damageHandler;


		private void Start()
		{
			currentEnemy = GetComponent<Character>();
			characterMelee = GetComponent<CharacterMelee>();
			damageHandler = GetComponent<DamageHandler>();
			player = HookPlayer.Instance.GetComponent<Character>();
			playerBattleCircle = HookPlayer.Instance.GetComponent<BattleCircle>();
			stateMachine = new StateMachine();
			attackPlayerState = new AttackPlayerState(this, this, stateMachine);
			idleState = new IdleState(this, this, stateMachine);
			closeUpWithPlayerState = new CloseUpWithPlayerState(this, this, stateMachine);
			sawPlayerState = new SawPlayerState(this, this, stateMachine);
			returnToCircleState = new ReturnToCircleState(this, this, stateMachine);
			deathState = new DeathState(this, this, stateMachine);
			stateMachine.Initialize(idleState);
		}


		private void Update()
		{
			stateMachine.CurrentState.LogicUpdate();
		}



		public IdleState IdleState
			=> idleState;


		public SawPlayerState SawPlayerState
			=> sawPlayerState;


		public CloseUpWithPlayerState CloseUpWithPlayerState
			=> closeUpWithPlayerState;


		public AttackPlayerState AttackPlayerState
			=> attackPlayerState;


		public ReturnToCircleState ReturnToCircleState
			=> returnToCircleState;


		public DeathState DeathState
			=> deathState;

		
		public EnemyAiData EnemyAiData
			=> _enemyAiData;


		public async UniTask AttackCombo()
		{
			for (int i = 0; i < _enemyAiData.MeleeAttackCount; i++)
			{
				characterMelee.Execute(_enemyAiData.MeleeKey);
				await UniTask.Delay(TimeSpan.FromSeconds(_enemyAiData.MeleeInputInterval));
			}
		}


		public void SetTargetFocus(CharacterMelee target)
		{
			characterMelee.SetTargetFocus(target);
		}


		public Character Player
			=> player;


		public DamageHandler DamageHandler
			=> damageHandler;


		public Character CurrentEnemy
			=> currentEnemy;


		public BattleCircle PlayerBattleCircle
			=> playerBattleCircle;
	}
}
