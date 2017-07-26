using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

	public GameObject[] spawnees;

  // waypoints to pass to suitors
  public Transform towardPlayer;
  public Transform playerArea;
  public Transform mansionDoor;
  public Transform towardElimination;
  public Transform eliminationSpot;

	int randomSpawnIndex;
  public LimoGame game;

  float spawnEscalationInterval = 50f;

  public void StartSpawning (float spawnInterval) {
    InvokeRepeating ("SpawnRandom", 2f, spawnInterval);

    if (!game.survivalMode) {
      Invoke ("StartTimer", 2f);
    } else {
      StartCoroutine(EscalatingSpawn(spawnInterval));
    }


    if (game.survivalMode && !game.activeStopwatch) {
      Invoke ("StartStopwatch", 2f);
    }
  }

  IEnumerator EscalatingSpawn (float startingSpawnInterval) {
    float currentSpawnInterval = startingSpawnInterval;

    yield return new WaitForSeconds (spawnEscalationInterval);
    while (currentSpawnInterval >= 1f) {
      CancelInvoke();
      currentSpawnInterval--;
      InvokeRepeating("SpawnRandom", 0f, currentSpawnInterval);
      yield return new WaitForSeconds (spawnEscalationInterval);
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
