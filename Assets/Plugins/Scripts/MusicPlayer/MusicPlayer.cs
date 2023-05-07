using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Plugins.Scripts.MusicPlayer
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField, Required]
		private AudioSource player;


		[SerializeField]
		private List<AudioClip> musicCollection = new();



		private void Start()
		{
			player.clip = RandomClip();
			player.loop = false;
			player.Play();
			WaitForTrackEnd().Forget();
		}


		private async UniTask WaitForTrackEnd()
		{
			while (player.isPlaying)
			{
				await UniTask.DelayFrame(30);
			}

			player.clip = NextRandomClip(player.clip);
			player.loop = true;
			player.Play();
		}


		private AudioClip NextRandomClip(AudioClip currentTrack = null)
		{
			var nextClip = RandomClip();

			if (currentTrack is not null)
			{
				while (currentTrack == nextClip)
				{
					nextClip = RandomClip();
				}
			}

			return nextClip;
		}


		private AudioClip RandomClip()
		{
			return musicCollection[Random.Range(0, musicCollection.Count)];
		}
	}
}
