using System;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Plugins.Scripts.Ads
{
	[Serializable]
	public class IronSourceAds
	{
		public string AppKey = "1a912563d";


		private bool _IsRewardedAvailable
			=> IronSource.Agent.isRewardedVideoAvailable();


		[Button(ButtonSizes.Medium)]
		public void Initialize()
		{
			IronSource.Agent.init(AppKey);
			IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
		}


		private void SdkInitializationCompletedEvent()
		{
			Debug.Log("IronSource SDK initialized");
			IronSource.Agent.validateIntegration();
			IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
			IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
			IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
			IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
			IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
			IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
			IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

			//Add AdInfo Interstitial Events
			IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
			IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
			IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
			IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
			IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
			IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
			IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
		}


		/************* RewardedVideo AdInfo Delegates *************/
		// Indicates that there’s an available ad.
		// The adInfo object includes information about the ad that was loaded successfully
		// This replaces the RewardedVideoAvailabilityChangedEvent(true) event
		void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
		{
		}


		// Indicates that no ads are available to be displayed
		// This replaces the RewardedVideoAvailabilityChangedEvent(false) event
		void RewardedVideoOnAdUnavailable()
		{
		}


		// The Rewarded Video ad view has opened. Your activity will loose focus.
		void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
		{
		}


		// The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
		void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
		{
		}


		// The user completed to watch the video, and should be rewarded.
		// The placement parameter will include the reward data.
		// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
		void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
		{
			Debug.Log("rewarded is watched");
		}


		// The rewarded video ad was failed to show.
		void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
		{
		}


		// Invoked when the video ad was clicked.
		// This callback is not supported by all networks, and we recommend using it only if
		// it’s supported by all networks you included in your build.
		void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
		{
		}


		/************* Interstitial AdInfo Delegates *************/
		// Invoked when the interstitial ad was loaded succesfully.
		void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
		{
		}


		// Invoked when the initialization process has failed.
		void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
		{
		}


		// Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
		void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
		{
		}


		// Invoked when end user clicked on the interstitial ad
		void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
		{
		}


		// Invoked when the ad failed to show.
		void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
		{
		}


		// Invoked when the interstitial ad closed and the user went back to the application screen.
		void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
		{
		}


		// Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
		// This callback is not supported by all networks, and we recommend using it only if  
		// it's supported by all networks you included in your build. 
		void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
		{
			Debug.Log("Interstitial ad showed");
		}


		[Button(ButtonSizes.Medium)]
		public void ShowRewardedVideo()
		{
			IronSource.Agent.showRewardedVideo();
		}


		[Button(ButtonSizes.Medium)]
		public void ShowInterstitialAd()
		{
			IronSource.Agent.showInterstitial();
		}
		
	}
}
