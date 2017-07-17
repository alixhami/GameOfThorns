using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

	public GameObject[] spawnees;
 	public GameObject[] waypoints;
	int randomSpawnIndex;
	public Scoring scoring;

	void Start () {
		InvokeRepeating("SpawnRandom", 1.0f, 2.0f);
	}

	void SpawnRandom () {
		randomSpawnIndex = Random.Range (0, spawnees.Length);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], transform.position, transform.rotation);
		newSpawn.GetComponent<Movement>().waypoints = waypoints;
		newSpawn.GetComponent<Movement> ().scoring = scoring;
	}
}
