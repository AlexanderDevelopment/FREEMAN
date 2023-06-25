using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


namespace Plugins.Scripts.Ads
{
	public class AdManager : MonoBehaviour
	{
		public static AdManager Instance;

		[FormerlySerializedAs("IronSource")]
		public IronSourceAds _ironSourceAds = new();

		private void Awake()
		{
			Instance = this;
			SceneManager.sceneLoaded += StartListenPlayerDeath;
		}


		private void StartListenPlayerDeath(Scene arg0, LoadSceneMode arg1)
		{
			var characters = FindObjectsOfType<DamageHandler>();

			foreach (var character in characters)
			{
				if (character.CompareTag("Player"))
				{
					character.OnPlayerDead.AddListener(_ironSourceAds.ShowInterstitialAd);
				}
			}
		}
		

		
	}
}
