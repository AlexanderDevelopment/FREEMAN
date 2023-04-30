using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
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


        public override void OnStart()
        {
            character = GetComponent<Character>();
            isSurrounding = false;
        }

        public override TaskStatus OnUpdate()
        {
            var targetPos = Target.Value.transform.position;

            Vector3 randomPoint = targetPos +
                                  new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f).normalized * radius.Value;
            if (character && !isSurrounding)
            {
                isSurrounding = true;
                character.characterLocomotion.SetTarget(randomPoint, null, 0.03f);
            }

            return CompletePath(new Vector2(randomPoint.x, randomPoint.z),
                new Vector2(character.transform.position.x, character.transform.position.z))
                ? TaskStatus.Success
                : TaskStatus.Running;
        }

        private bool CompletePath(Vector2 pointA, Vector2 pointB)
        {
            return Vector2.Distance(pointA, pointB) < 0.5f;
        }
    }
}