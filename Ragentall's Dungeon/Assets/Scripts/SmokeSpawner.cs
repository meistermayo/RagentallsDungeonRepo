using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSpawner : MonoBehaviour {
	[SerializeField] GameObject smokePrefab;
	[SerializeField] float spawnDuration;
	float spawnTimer;

	void FixedUpdate()
	{
		RunTimer ();
	}

	void SpawnSmoke()
	{
		Instantiate (smokePrefab, transform.position, Quaternion.identity);
	}

	void RunTimer()
	{
		if (spawnTimer > 0f) {
			spawnTimer--;
		} else {
			spawnTimer = spawnDuration;
			SpawnSmoke ();
		}
	}
}
