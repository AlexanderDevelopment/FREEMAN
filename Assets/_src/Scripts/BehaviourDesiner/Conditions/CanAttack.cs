using GameCreator.Melee;

namespace _src.Scripts.BehaviourDesiner.Conditions
{
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using GameCreator.Stats;
    using UnityEngine;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;


    namespace _src.Scripts.Ai.Tasks
    {
        [TaskDescription("Can attack")]
        [TaskCategory("Attack")]
        public class CanAttack : Conditional
        {
            public SharedFloat AttackDistance;

            public SharedGameObject Target;
            private CharacterMelee _melee;

            public override void OnStart()
            {
                TryGetComponent(out CharacterMelee melee);
                if (melee)
                    _melee = melee;
            }

            public override TaskStatus OnUpdate()
            {
                if (!_melee) return TaskStatus.Failure;

                return IsOnAttackDistance(new Vector2(_melee.transform.position.x, _melee.transform.position.z),
                    new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.z)) && !_melee.IsStaggered
                    ? TaskStatus.Success
                    : TaskStatus.Failure;
            }
            private bool IsOnAttackDistance(Vector2 pointA, Vector2 pointB)
            {
                return Vector2.Distance(pointA, pointB) <= AttackDistance.Value;
            }
        }
    }
}