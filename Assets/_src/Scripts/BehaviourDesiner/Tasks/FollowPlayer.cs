using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Characters;
using GameCreator.Core.Hooks;
using GameCreator.Melee;
using UnityEngine;

namespace _src.Scripts.Ai.Tasks
{
    [TaskDescription("Follow Player.")]
    [TaskCategory("Movement")]
    public class FollowPlayer : Action
    {

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Start moving towards the target if the target is further than the specified distance")]
        public SharedFloat attackDistance = 2;
        
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        private Character _character;
        private Transform player;
        private float _attackDistance;


        public override void OnAwake()
        {
            _character = gameObject.GetComponent<Character>();
            player= HookPlayer.Instance.transform;
            _attackDistance = (float)attackDistance.GetValue();
        }


        public override TaskStatus OnUpdate()
        {
            if (player == null) return TaskStatus.Failure;


            _character.characterLocomotion.SetTarget(
                player.transform.position,
               null,
                this._attackDistance
            );
            return Vector3.Distance(player.position, _character.transform.position)
                   < (float)attackDistance.GetValue() ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}
