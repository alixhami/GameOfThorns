using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager gameManagerInstance;

	public Timer timer;
  public Stopwatch stopwatch;
	public Scoring scoring;
	public PlayerAlerts playerAlerts;
	public GameObject limo;
	public Menu menu;
  public Hands leftHand;
  public Hands rightHand;

  public bool playingGame = false;
  public bool survivalMode;
  public bool activeStopwatch;

  // waypoints to pass to spawner
  public Transform towardPlayer;
	public Transform playerArea;
	public Transform mansionDoor;
	public Transform towardElimination;
	public Transform eliminationSpot;

  public Transform[] limoDestinations;

  public float slowSpawnInterval = 7f;
  public float mediumSpawnInterval = 6f;
  public float fastSpawnInterval = 4f;

	public AudioSource music;
	public AudioSource soundEffects;

	public AudioClip badAlertSound;
	public AudioClip slapSound;

	int villainsSnuckInCount;
	int villainsWithRosesCount;
	int villainsSlappedCount;
	int goodGuysWithRosesCount;
	int goodGuysSlappedCount;

  public GameObject gameSelectPillars;
  public GameCard[] gameCards;

  void Start() {
    foreach (var gameCard in gameCards) {
      gameCard.TriggerGame += GameCard_TriggerGame;
    }

    leftHand.Slap += Hands_Slap;
    rightHand.Slap += Hands_Slap;
  }

  void Hands_Slap (bool goodGuy, bool receivedRose) {
    if (goodGuy) {
      GoodGuySlapped(receivedRose);
    } else {
      VillainSlapped(receivedRose);
    }
  }

  private void GameCard_TriggerGame(string gameName) {
    Debug.Log(gameName);
    switch(gameName) {
      case "LimoTimed":
        StartTimedLimoGame();
        break;
      case "LimoSurvival":
        StartSurvivalLimoGame();
        break;
      case "TargetPractice":
        StartTargetPractice();
        break;
    }
    gameSelectPillars.SetActive(false);
    menu.HideMenus();
  }

  public void StartTimedLimoGame () {
    playingGame = true;
    survivalMode = false;

    stopwatch.Hide();
    scoring.ResetScores();
    scoring.transform.gameObject.SetActive(true);
    timer.SetTimer(90f);

    CreateLimo(limoDestinations[0], fastSpawnInterval);
  }

  public void StartSurvivalLimoGame () {
    playingGame = true;
    survivalMode = true;

    scoring.Hide();
    stopwatch.ResetTime();

    StartCoroutine(CreateMultipleLimos(37f, slowSpawnInterval));
  }

  public void StartTargetPractice() {
  }

  void CreateLimo (Transform destination, float spawnInterval) {
    GameObject newLimo = Instantiate(limo);

    Limo limoController = newLimo.GetComponent<Limo>();
    limoController.spawner.game = this;

    limoController.target = destination;
    limoController.spawnInterval = spawnInterval;
  }

  IEnumerator CreateMultipleLimos (float delay, float spawnInterval) {
    for (int i = 0; i < limoDestinations.Length; i++) {
      CreateLimo(limoDestinations[i], spawnInterval);
      yield return new WaitForSeconds(delay);
    }
  }

  public void ShowInstructions () {
    if (!playingGame) {
      menu.ShowInstructions ();
    }
  }

  public void StartTimer () {
    timer.StartCountdown();
  }

  public void StartStopwatch () {
    stopwatch.StartTiming();
  }

  public void GameOver (string gameOverMessage) {
    playingGame = false;
    StopAllCoroutines ();
    DestroyAllWithTag("Prop");
    DestroyAllWithTag("Suitor");

    timer.Hide ();
    stopwatch.StopTime();
    playerAlerts.hideMessage ();
    menu.DisplayGameOverMenu(gameOverMessage);
    gameSelectPillars.SetActive(true);
  }

  void DestroyAllWithTag (string tag) {
    GameObject[] targets = GameObject.FindGameObjectsWithTag (tag);

    for(var i = 0 ; i < targets.Length ; i ++) {
      Destroy(targets[i]);
    }
  }

  public void VillainSneaksIn () {
    playerAlerts.displayNegativeAlert ("A villain snuck in!");
    soundEffects.PlayOneShot (badAlertSound);

    if (survivalMode) {
      GameOver("A villain snuck in!");
    } else {
      scoring.ChangeVillainCount(1);
      villainsSnuckInCount ++;
    }
  }

  public void VillainGetsRose () {
    StartCoroutine (PlaySoundDelayed (badAlertSound, 0.2f));
    playerAlerts.displayNegativeAlert ("He's a villain!");
    villainsWithRosesCount++;
    scoring.ChangeVillainCount (1);

    if (survivalMode) {
      GameOver("You gave a rose to a villain!");
    }
  }

  public void VillainSlapped (bool hasRose) {
    soundEffects.PlayOneShot (slapSound);
    villainsSlappedCount ++;
    if (hasRose) {
      villainsWithRosesCount --;
      scoring.ChangeVillainCount(-1);
    }
  }

  public void GoodGuyInHouse () {
    goodGuysWithRosesCount ++;
  }

  public void GoodGuyGetsRose () {
    scoring.ChangeGoodGuyCount(1);
  }

  public void GoodGuySlapped (bool hasRose) {
    soundEffects.PlayOneShot (slapSound);
    playerAlerts.displayNegativeAlert ("He was a good guy!");
    StartCoroutine (PlaySoundDelayed (badAlertSound, 0.2f));
    goodGuysSlappedCount++;

    if (hasRose) {
      scoring.ChangeGoodGuyCount (-1);
    }

    if (survivalMode) {
      GameOver("You slapped a good guy!");
    }
  }

  IEnumerator PlaySoundDelayed (AudioClip clip, float delay) {
    yield return new WaitForSeconds (delay);
    soundEffects.PlayOneShot (clip);
  }
}
