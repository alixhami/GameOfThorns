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
  public GameManager game;

  void Awake () {
    playerArea = game.playerArea;
    mansionDoor = game.mansionDoor;
    towardElimination = game.towardElimination;
    eliminationSpot = game.eliminationSpot;
  }

	void Start () {
		InvokeRepeating("SpawnRandom", 1f, 4f);
    Invoke("StartTimer", 1f);
	}

  void StartTimer () {
    game.StartTimer();
  }

	void SpawnRandom () {
		randomSpawnIndex = Random.Range (0, spawnees.Length);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], transform.position, transform.rotation);

		Movement movement = newSpawn.GetComponent<Movement> ();
    movement.game = game;
		movement.playerArea = playerArea;
		movement.mansionDoor = mansionDoor;
		movement.towardElimination = towardElimination;
		movement.eliminationSpot = eliminationSpot;
	}
}
