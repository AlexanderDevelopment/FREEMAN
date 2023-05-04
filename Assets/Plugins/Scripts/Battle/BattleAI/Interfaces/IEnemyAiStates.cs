namespace Plugins.Scripts.Battle.BattleAI
{
	public interface IEnemyAiStates
	{
		public IdleState IdleState { get; }
		
		public SawPlayerState SawPlayerState { get; }
		
		public CloseUpWithPlayerState CloseUpWithPlayerState { get; }
		
		public AttackPlayerState AttackPlayerState { get; }
		
		public ReturnToCircleState ReturnToCircleState { get; }
		
		public DeathState DeathState { get; }
	}
}
