using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		// Set random position on x axis, based on position of spawn origin
		Vector3 randomPos = transform.position;
		randomPos.z += Random.Range (-3f, 3f);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], randomPos, transform.rotation);
    	newSpawn.GetComponent<Movement>().waypoints = waypoints;
		newSpawn.GetComponent<Movement> ().scoring = scoring;
	}
}
