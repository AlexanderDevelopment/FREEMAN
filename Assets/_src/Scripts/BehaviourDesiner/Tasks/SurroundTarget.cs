using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using Plugins.Scripts;
using UnityEngine;

namespace _src.Scripts.Ai.Tasks
{
    [TaskDescription("Surround target")]
    [TaskCategory("Movement")]
    public class SurroundTarget : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The radius of the agents that should surround the target")]
        public SharedFloat radius = 10;

        public SharedGameObject Target;


        private Character character;
        private bool isSurrounding;
        private Vector3 randomPoint;
        private BattleCircle battleCircle;


        public override void OnStart()
        {
            character = GetComponent<Character>();
            battleCircle = Target.Value.GetComponent<BattleCircle>();
            isSurrounding = false;
            randomPoint = RandomPointAroundTarget();
            if (character && !isSurrounding)
            {
                isSurrounding = true;
                character.characterLocomotion.SetTarget(randomPoint, null, 0.03f);
                battleCircle.AttackerRemove(character);
            }
        }

        public override TaskStatus OnUpdate()
        {
            return CompletePath(new Vector2(randomPoint.x, randomPoint.z),
                new Vector2(character.transform.position.x, character.transform.position.z))
                ? TaskStatus.Success
                : TaskStatus.Running;
        }

        private bool CompletePath(Vector2 pointA, Vector2 pointB)
        {
            return Vector2.Distance(pointA, pointB) < 0.5f;
        }


        private Vector3 RandomPointAroundTarget()
        {
            var battleCircle = Target.Value.GetComponent<BattleCircle>();
            return battleCircle.GetClosestCirclePosition(transform.position);
        }
    }
}