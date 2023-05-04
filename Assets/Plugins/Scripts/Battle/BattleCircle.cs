using System;
using System.Collections.Generic;
using GameCreator.Characters;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Plugins.Scripts
{
	public class BattleCircle : MonoBehaviour
	{
		[SerializeField]
		private int maximumAttackers;


		[SerializeField]
		private int maximumAttackersInSurround;


		[SerializeField]
		private float battleCircleRadius;


		public float BattleCircleRadius
			=> battleCircleRadius;

		[ShowInInspector,ReadOnly]
		private List<Character> attackers = new();

		[ShowInInspector,ReadOnly]
		public bool PlayerIsBusy
			=> attackers.Count >= maximumAttackers;


		[ShowInInspector]
		private List<Vector3> surroundingPoints = new();


		private void Start()
		{
			GenerateSurroundingPoints();
		}


		public void AttackerAdd(Character attacker)
		{
			if (attackers.Contains(attacker))
				return;

			attackers.Add(attacker);
		}


		public void AttackerRemove(Character attacker)
		{
			if (attackers.Contains(attacker))
				attackers.Remove(attacker);
		}

		


		private void GenerateSurroundingPoints()
		{
			surroundingPoints.Clear();
			float angleStep = 2 * Mathf.PI / maximumAttackersInSurround;
			float angle = 0f;

			for (int i = 0; i < maximumAttackersInSurround; i++)
			{
				float x = transform.position.x +( battleCircleRadius * Mathf.Cos(angle));
				float z = transform.position.z + ( battleCircleRadius * Mathf.Sin(angle));
				var newPoint = new Vector3(x, 0, z);
				angle += angleStep;
				NavMeshHit hit;

				if (NavMesh.SamplePosition(newPoint, out hit, 1.0f, NavMesh.AllAreas))
				{
					surroundingPoints.Add(hit.position);
				}
			}
		}


		


		public Vector3 GetClosestCirclePosition(Vector3 invoker)
		{
			GenerateSurroundingPoints();
				float closestDistance = Single.MaxValue;

				Vector3 closestPoint = Vector3.zero;


				for (int i = 0; i < surroundingPoints.Count; i++)
				{
					var distance = Vector3.Distance(surroundingPoints[i], invoker);

					if (distance < closestDistance)
					{
						closestPoint = surroundingPoints[i];
					}

					closestDistance = distance;
				}


				return closestPoint;
			
		}


		public Vector3 GetRandomCirclePoint()
		{
			GenerateSurroundingPoints();

			return surroundingPoints[Random.Range(0, surroundingPoints.Count)];
		}


		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;

			foreach (var vector in surroundingPoints)
			{
				Gizmos.DrawSphere(vector, 1);
			}
		}
	}
}
