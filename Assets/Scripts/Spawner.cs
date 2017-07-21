using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

	public GameObject[] spawnees;

	int randomSpawnIndex;
  public GameManager game;

  public void StartSpawning (float spawnInterval) {
    InvokeRepeating ("SpawnRandom", 2f, spawnInterval);

    if (!game.survivalMode) {
      Invoke ("StartTimer", 2f);
    } else if (game.survivalMode && !game.activeStopwatch) {
      Invoke ("StartStopwatch", 2f);
    }
  }

  void StartTimer () {
    game.StartTimer();
  }

  void StartStopwatch () {
    game.StartStopwatch();
  }

	void SpawnRandom () {
		randomSpawnIndex = Random.Range (0, spawnees.Length);

		GameObject newSpawn = Instantiate (spawnees [randomSpawnIndex], transform.position, transform.rotation);

		Movement movement = newSpawn.GetComponent<Movement> ();
    movement.game = game;
	}
}
