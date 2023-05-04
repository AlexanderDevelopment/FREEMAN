using Cysharp.Threading.Tasks;
using GameCreator.Characters;
using GameCreator.Melee;
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	public interface IEnemyAiBehaviour
	{
		public EnemyAiData EnemyAiData { get; }
		
		public UniTask AttackCombo();


		public void SetTargetFocus(CharacterMelee target);


		public Character Player { get; }
		
		public DamageHandler DamageHandler { get; }
		
		public Character CurrentEnemy { get; }

		public BattleCircle PlayerBattleCircle { get; }
		
		public Transform transform { get; }
	}
}
