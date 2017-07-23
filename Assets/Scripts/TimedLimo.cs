using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLimo : MonoBehaviour {

  Scoring scoring;
  public Timer timer;
  public float timeLimit = 90f;
  public float spawnInterval = 4f;
  public event System.Action<string> GameOver = delegate { };

  public GameObject limo;

  // waypoints to pass to spawner
  public Transform towardPlayer;
  public Transform playerArea;
  public Transform mansionDoor;
  public Transform towardElimination;
  public Transform eliminationSpot;

  public Transform limoDestination;

  void Awake () {
    scoring = GetComponent<Scoring> ();
  }

  void Start () {
    timer.OutOfTime += Timer_OutOfTime;
  }

  public void Play () {
    CreateLimo ();
  }

  void CreateLimo () {
    GameObject newLimo = Instantiate(limo);

    Limo limoController = newLimo.GetComponent<Limo>();
    limoController.target = limoDestination;
    limoController.spawnInterval = spawnInterval;

    limoController.spawner.towardPlayer = towardPlayer;
    limoController.spawner.playerArea = playerArea;
    limoController.spawner.mansionDoor = mansionDoor;
    limoController.spawner.towardElimination = towardElimination;
    limoController.spawner.eliminationSpot = eliminationSpot;
  }
	
  void Timer_OutOfTime () {
    GameOver ("Out of time! Insert stats here...");
  }
}
