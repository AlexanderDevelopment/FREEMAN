using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Plugins.Scripts
{
	public class SalamiSpawner : MonoBehaviour
	{
		[SerializeField, Required]
		private Rigidbody salamiPart;


		[SerializeField]
		private Transform salamiSpawnPoint;


		[SerializeField]
		private Vector3 salamiSpawnPointOffset;


		[SerializeField]
		private float salamiSpawnDuration;


		[SerializeField]
		private float salamiLifeTime;


		[SerializeField]
		private float minBurstPower;


		[SerializeField]
		private float maxBurstPower;



		[SerializeField]
		private int poolSize;


		[SerializeField, ReadOnly]
		private float timer;


		private List<Rigidbody> salamiPool = new();



		private void Start()
		{
			timer = 0;
			InitializePool();
		}


		private void Update()
		{
			timer += Time.deltaTime;

			if (timer >= salamiSpawnDuration && PoolIsReadyForGetSalami())
			{
				SpawnSalamiPart();
				timer = 0;
			}
		}


		private void SpawnSalamiPart()
		{
			var newSalami = GetSalamiFromPool();

			if (newSalami)
			{
				newSalami.useGravity = false;
				newSalami.isKinematic = true;
				newSalami.velocity = Vector3.zero;
				newSalami.transform.position = salamiSpawnPoint.position + salamiSpawnPointOffset;
				newSalami.transform.rotation.eulerAngles.Set(-75, 0, 0);
				newSalami.isKinematic = false;
				newSalami.useGravity = true;
				newSalami.AddForce(transform.forward * Random.Range(minBurstPower, maxBurstPower));
				var leftOrRightVector = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
				newSalami.AddForce(leftOrRightVector * (Random.Range(minBurstPower, maxBurstPower) / 5));
				newSalami.AddForce(Vector3.up * maxBurstPower);
				newSalami.AddRelativeTorque(Vector3.forward * (Random.Range(minBurstPower, maxBurstPower) / 1000), ForceMode.Impulse);
				newSalami.AddRelativeTorque(Vector3.left * (Random.Range(minBurstPower, maxBurstPower) / 1000), ForceMode.Impulse);
				ReturnSalamiPartToPool(newSalami.gameObject).Forget();
			}
		}


		private async UniTask ReturnSalamiPartToPool(GameObject salamiGameObject)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(salamiLifeTime));
			salamiGameObject.SetActive(false);
		}


		private Rigidbody GetSalamiFromPool()
		{
			foreach (var salami in salamiPool)
			{
				if (!salami.gameObject.activeInHierarchy)
				{
					salami.gameObject.SetActive(true);

					return salami;
				}
			}

			return null;
		}


		private void InitializePool()
		{
			for (int i = 0; i < poolSize; i++)
			{
				var newSalamiPart = Instantiate(salamiPart);
				salamiPool.Add(newSalamiPart);
				newSalamiPart.gameObject.SetActive(false);
			}
		}


		private bool PoolIsReadyForGetSalami()
		{
			foreach (var salami in salamiPool)
			{
				if (!salami.gameObject.activeInHierarchy)
				{
					return true;
				}
			}

			return false;
		}


		#if UNITY_EDITOR
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Vector3 direction = transform.TransformDirection(Vector3.forward);
			Gizmos.DrawRay(transform.position, direction);
			Gizmos.DrawSphere(transform.forward, 1f);
		}
		#endif
	}
}
