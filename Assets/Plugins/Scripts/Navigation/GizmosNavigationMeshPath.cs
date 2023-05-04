using UnityEngine;
using UnityEngine.AI;


namespace Plugins.Scripts.Navigation
{
	public class GizmosNavigationMeshPath : MonoBehaviour
	{
		public Color pathColor = Color.red;
		[SerializeField]
		private NavMeshAgent agent;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
		}


		private void OnDrawGizmos()
		{
			if (agent is not null)
			{
				if (agent.isActiveAndEnabled && agent.hasPath)
				{
					if (agent.pathStatus == NavMeshPathStatus.PathComplete)
					{
						Gizmos.color = pathColor;

						for (int i = 0; i < agent.path.corners.Length - 1; i++)
						{
							Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
						}
					}
				}
			}
		}
	}
}
