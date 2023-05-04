using BehaviorDesigner.Runtime.Tasks;
using Plugins.Scripts;
using UnityEngine;

namespace _src.Scripts.BehaviourDesiner.Conditions
{
    [TaskDescription("Is not Death")]
    [TaskCategory("Stats")]
    public class IsDeath : Conditional
    {
        private DamageHandler damageHandler;
        public override void OnStart()
        {
            TryGetComponent(out damageHandler);
        }

        public override TaskStatus OnUpdate()
        {
            if (damageHandler)
            {
                return damageHandler.IsDead
                    ? TaskStatus.Success
                    : TaskStatus.Failure;
            }
            else
                Debug.LogError($"No have stats on {gameObject.name}");

            return TaskStatus.Failure;
        }
    }
}