using GameCreator.Core;
using Plugins.Scripts.Ads;
using UnityEngine;


namespace Plugins.Scripts.GameCreatorCustomActions
{
	[AddComponentMenu("")]
	public class ActionShowAds : IAction
	{
		public enum AdType
		{
			Rewarded,
			Interstitial,
            
		}
		
		[SerializeField]
		private AdType _adType;

		// EXECUTABLE: ----------------------------------------------------------------------------

		public override bool InstantExecute(GameObject target, IAction[] actions, int index)
		{
			if (_adType == AdType.Rewarded)
			{
				AdManager.Instance._ironSourceAds.ShowRewardedVideo();
			}
			else
			{
				AdManager.Instance._ironSourceAds.ShowInterstitialAd();
			}
           
			return true;
		}

		// +--------------------------------------------------------------------------------------+
		// | EDITOR                                                                               |
		// +--------------------------------------------------------------------------------------+

		#if UNITY_EDITOR

		public static new string NAME = "Ads/Show ads";
		#endif
	}
	}
