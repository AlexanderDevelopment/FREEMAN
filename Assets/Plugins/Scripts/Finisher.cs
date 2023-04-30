using MoreMountains.Feedbacks;
using Plugins.Scripts;
using UnityEngine;


namespace _src.Data.Finishers
{
	[CreateAssetMenu(fileName = "Finisher", menuName = "FREEMAN/Finisher", order = 1)]
	public class Finisher : ScriptableObject
	{
		
		public AnimationClip AttackerAnimation;

		public AnimationClip ReceiverAnimation;
	}
}
