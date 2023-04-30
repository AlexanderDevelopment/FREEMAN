using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Plugins.Scripts
{
	public class CharacterFeedbacks : SerializedMonoBehaviour
	{
		public enum FeedBacksName
		{
			FinisherAttack,
			
		}


		[SerializeField]
		private Dictionary<FeedBacksName, MMF_Player> _feedbacks = new();


		public Dictionary<FeedBacksName, MMF_Player> CharacterFeedbacksCollection
			=> _feedbacks;
	}
}
