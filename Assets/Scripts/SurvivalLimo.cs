using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLimo : MonoBehaviour {

  public event System.Action<string> GameOver = delegate { };

  // waypoints to pass to spawner
  public Transform towardPlayer;
  public Transform playerArea;
  public Transform mansionDoor;
  public Transform towardElimination;
  public Transform eliminationSpot;

  public GameObject limo;
  public float spawnInterval = 7f;
  public Transform[] limoDestinations;

  public void Play () {
    StartCoroutine(CreateMultipleLimos(37f));
  }

  IEnumerator CreateMultipleLimos (float delay) {
    for (int i = 0; i < limoDestinations.Length; i++) {
      CreateLimo(limoDestinations[i]);
      yield return new WaitForSeconds(delay);
    }
  }

  void CreateLimo (Transform destination) {
    GameObject newLimo = Instantiate(limo);

    Limo limoController = newLimo.GetComponent<Limo>();
    limoController.target = destination;
    limoController.spawnInterval = spawnInterval;

    limoController.spawner.towardPlayer = towardPlayer;
    limoController.spawner.playerArea = playerArea;
    limoController.spawner.mansionDoor = mansionDoor;
    limoController.spawner.towardElimination = towardElimination;
    limoController.spawner.eliminationSpot = eliminationSpot;
  }

}
