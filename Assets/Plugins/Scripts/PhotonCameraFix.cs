using System;
using Cinemachine;
using Photon.Pun;
using UnityEngine;


namespace Plugins.Scripts
{
	public class PhotonCameraFix: MonoBehaviour
	{
		[SerializeField]
		private PhotonView photonView;


		[SerializeField]
		private CinemachineVirtualCamera camera;


		private void Awake()
		{
			if (!photonView.IsMine)
			{
				camera.gameObject.SetActive(false);
			}
		}
	}
}
