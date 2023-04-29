using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using GameCreator.Core.Hooks;
using UnityEngine;

namespace _src.Scripts.Ai.Tasks
{
    [TaskDescription("Look at target.")]
    [TaskCategory("Movement")]
    public class LookAtTarget : Action
    {
        private Character _character;
        public SharedGameObject Target;
        private float _attackDistance;
        private GameObject target;


        public override void OnAwake()
        {
            _character = gameObject.GetComponent<Character>();
            target = (GameObject)Target.GetValue();
        }


        public override TaskStatus OnUpdate()
        {
            if (target == null) return TaskStatus.Failure;


            _character.characterLocomotion.SetRotation(
                 target.transform.position- _character.transform.position
            );
            return TaskStatus.Success;
        }
    }
}