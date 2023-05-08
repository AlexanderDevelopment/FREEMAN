using System;
using Doozy.Runtime.UIManager.Components;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;


namespace Plugins.Scripts
{
	public class SoundVolumeProvider : MonoBehaviour
	{
		[SerializeField, Required]
		private MMSoundManager soundManager;


		[SerializeField, Required]
		private UISlider musicVolume;


		[SerializeField, Required]
		private UISlider sfxVolume;


		private void Start()
		{
			if (!soundManager)
				soundManager = GetComponent<MMSoundManager>();

			if (musicVolume && sfxVolume)
			{
				musicVolume.value = soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, false);
				sfxVolume.value = soundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, false);
				musicVolume.OnValueChanged.AddListener(SetMusicVolume);
				sfxVolume.OnValueChanged.AddListener(SetSfxVolume);
			}
			else
			{
				Debug.Log($"No have sfx and music sliders in {gameObject.name}");
			}
		}


		private void SetMusicVolume(float volume)
			=> soundManager.SetVolumeMusic(volume);


		private void SetSfxVolume(float volume)
			=> soundManager.SetVolumeSfx(volume);
	}
}
