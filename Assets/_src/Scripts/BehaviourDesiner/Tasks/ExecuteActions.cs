using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using GameCreator.Core;
using UnityEngine;


namespace _src.Scripts.Ai.Tasks
{
	[TaskDescription("Execute GC Actions")]
	[TaskCategory("GameCreator")]
	public class ExecuteActions : Action
	{
		[SerializeField]
		private Actions _actions;


		[SerializeField]
		private SharedBool _waitToFinish;


		private bool _isFinished;
		

		public override void OnStart()
		{
			var actions = _actions;

			if (actions == null)
			{
				_isFinished = true;
			}
			else if (_waitToFinish.Value)
			{
				_isFinished = false;
				actions.actionsList.Execute(gameObject, () => _isFinished = true);
			}
			else
			{
				actions.Execute(gameObject);
				_isFinished = true;
			}
		}


		public override TaskStatus OnUpdate()
		{
			if (_isFinished)
				return TaskStatus.Success;

			return TaskStatus.Running;
		}


		public override void OnEnd()
		{
			_isFinished = false;
		}
	}
}
