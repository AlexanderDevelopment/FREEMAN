using System;
using GameCreator.Characters;
using GameCreator.Melee;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Plugins.Scripts.Battle.BattleAI
{
	[CreateAssetMenu(fileName = "EnemyAiData", menuName = "FREEMAN/AI/EnemyAiData", order = 1)]
	public class EnemyAiData : ScriptableObject
	{
		[Title("AttackData")]
		public float AttackDistance;
		public float AggroDistance;
		public float MeleeInputInterval;
		public int MeleeAttackCount;
		public CharacterMelee.ActionKey MeleeKey;

	}
}
