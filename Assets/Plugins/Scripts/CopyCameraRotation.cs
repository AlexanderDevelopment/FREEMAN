using System;
using GameCreator.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.Scripts.Utils
{
    public class CopyCameraRotation : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}