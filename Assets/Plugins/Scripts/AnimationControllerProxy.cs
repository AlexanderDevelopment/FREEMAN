using UnityEngine;
using UnityEngine.Events;


namespace Plugins.Scripts
{
	public class AnimationControllerProxy : MonoBehaviour
	{
		public UnityEvent OnAttackAnimationEvent = new();


		public void AttackAnimationEvent()
			=> OnAttackAnimationEvent.Invoke();
	}
}
