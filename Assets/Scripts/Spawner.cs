using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

	public GameObject[] spawnees;

	public Transform playerArea;
	public Transform mansionDoor;
	public Transform towardElimination;
	public Transform eliminationSpot;

	int randomSpawnIndex;
	public Scoring scoring;

	void Start () {
		InvokeRepeating("SpawnRandom", 1f, 4f);
	}

	void SpawnRandom () {
		randomSpawnIndex = Random.Range (0, spawnees.Length);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], transform.position, transform.rotation);

		Movement movement = newSpawn.GetComponent<Movement> ();
		movement.scoring = scoring;
		movement.playerArea = playerArea;
		movement.mansionDoor = mansionDoor;
		movement.towardElimination = towardElimination;
		movement.eliminationSpot = eliminationSpot;
	}
}
