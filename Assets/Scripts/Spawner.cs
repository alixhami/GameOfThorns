﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] spawnees;
	int randomSpawnIndex;

	void Start () {
		InvokeRepeating("SpawnRandom", 1.0f, 2.0f);
	}

	void SpawnRandom () {
		randomSpawnIndex = Random.Range (0, spawnees.Length);

		// Set random position on x axis, based on position of spawn origin
		Vector3 randomPos = transform.position;
		randomPos.x = Random.Range (-5f, 5f);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], randomPos, transform.rotation)as GameObject;
	}
}
