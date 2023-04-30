using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Stats;
using UnityEngine;

namespace _src.Scripts.BehaviourDesiner.Conditions
{
    [TaskDescription("Is Death")]
    [TaskCategory("Stats")]
    public class IsDeath : Conditional
    {
        private const string HEALTH_ATTR_NAME = "health";

        private Stats stats;

        public override void OnStart()
        {
            TryGetComponent(out stats);
        }

        public override TaskStatus OnUpdate()
        {
            if (stats)
            {
                return stats.GetAttrValue(HEALTH_ATTR_NAME) <= 0
                    ? TaskStatus.Success
                    : TaskStatus.Failure;
            }
            else
                Debug.LogError($"No have stats on {gameObject.name}");

            return TaskStatus.Failure;
        }
    }
}