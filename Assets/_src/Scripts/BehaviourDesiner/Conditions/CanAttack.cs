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
                return _melee.IsStaggered ? TaskStatus.Failure : TaskStatus.Success;
            }
        }
    }
}