using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Stats;
using UnityEngine;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;


namespace _src.Scripts.Ai.Tasks
{
    [TaskDescription("Comparison with health percent")]
    [TaskCategory("Character")]
    public class IsHealthValue : Conditional
    {
        private const string HEALTH_ATTR_NAME = "health";


        [SerializeField] [Tooltip("If null then self gameObject will be used")]
        private SharedGameObject _target;


        [SerializeField] [Tooltip("[0.0f, 1.0f]")]
        private float _normalizedPercent;


        [SerializeField] private StackedAction.ComparisonType _comparisonType;


        private Stats _stats;

        public override TaskStatus OnUpdate()
        {
            if (_stats == null || _stats.gameObject != _target.Value)
            {
                if (!_target.Value.TryGetComponent(out _stats))
                {
                    Debug.Log(_target);
                    Debug.LogError($"[{nameof(IsHealthValue)}] Stats is null");

                    return TaskStatus.Failure;
                }
            }

            var attrPercent = _stats.GetAttrValuePercent(HEALTH_ATTR_NAME);

            if (attrPercent >= _normalizedPercent)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}