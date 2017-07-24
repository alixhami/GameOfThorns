using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimoGame : MonoBehaviour {

  public Scoring scoring;
  public Stopwatch stopwatch;
  public Timer timer;
  public PlayerAlerts playerAlerts;
  public Hands leftHand;
  public Hands rightHand;

  AudioSource soundEffects;
  public AudioClip badAlertSound;
  public AudioClip slapSound;

  public bool activeStopwatch;
  float timeLimit = 30f;
  public event System.Action<string, string> GameOver = delegate { };

  public GameObject limo;
  bool playing;
  public bool survivalMode;

  float slowSpawnInterval = 7f;
  float fastSpawnInterval = 4f;

  // waypoints to pass to spawner
  public Transform towardPlayer;
  public Transform playerArea;
  public Transform mansionDoor;
  public Transform towardElimination;
  public Transform eliminationSpot;

  public Transform[] limoDestinations;

  void Start () {
    soundEffects = GetComponent<AudioSource> ();
    timer.OutOfTime += Timer_OutOfTime;
    leftHand.Slap += Hands_Slap;
    rightHand.Slap += Hands_Slap;
  }

  public void Play (bool survival) {
    playing = true;
    survivalMode = survival;

    if (!survivalMode) {
      scoring.ResetScores ();
      scoring.Show ();
      timer.SetTimer (timeLimit);
      CreateLimo (fastSpawnInterval, limoDestinations[0]);
    } else {
      stopwatch.ResetTime ();
      StartCoroutine(CreateMultipleLimos(slowSpawnInterval, 37f));
    }
  }

  void CreateLimo (float spawnInterval, Transform destination) {
    GameObject newLimo = Instantiate(limo);

    Limo limoController = newLimo.GetComponent<Limo>();
    limoController.target = destination;
    limoController.spawner.game = this;
    limoController.spawnInterval = spawnInterval;
  }

  IEnumerator CreateMultipleLimos (float spawnInterval, float delay) {
    for (int i = 0; i < limoDestinations.Length; i++) {
      CreateLimo(spawnInterval, limoDestinations[i]);
      yield return new WaitForSeconds(delay);
    }
  }

  public void StartTimer () {
    timer.StartCountdown ();
  }

  public void StartStopwatch () {
    stopwatch.StartTiming ();
  }

  void Timer_OutOfTime () {
    // only runs if the timer is for this game
    if (playing) {
      scoring.Hide ();
      playerAlerts.hideMessage ();

      string purityScore;
      string feedback = "";
      int goodGuyCount = scoring.goodGuyCount;
      int villainCount = scoring.villainCount;

      if (goodGuyCount + villainCount == 0) {
        purityScore = "N/A";
      } else {
        float percentage = (float)goodGuyCount / (goodGuyCount + villainCount) * 100f;
        purityScore = string.Concat(Mathf.Round(percentage).ToString (), "%");
      }

      if (goodGuyCount == 0 && villainCount == 0) {
        feedback = "You didn't give out a single rose! The production company will be suing you for breach of contract.";  
      } else if (villainCount == 0) {
        feedback = "Every bachelor in the house is a catch! You'll probably find love, but the ratings will be crap.";
      } else if (goodGuyCount == 0) {
        feedback = "Nobody in the house is here for the right reasons. This is going to be the most dramatic season yet!";
      } else if (villainCount > goodGuyCount) {
        feedback = "Most of your bachelors are villains... Bachelor Nation is going to think you have poor judgment.";
      } else if (goodGuyCount > villainCount) {
        feedback = "Most of your bachelors are here for the right reasons! Hopefully you can weed out the villains by week 5.";
      } else if (villainCount == goodGuyCount) {
        feedback = "Half of your bachelors are villains! Hopefully your judgment improves in the weeks ahead.";
      }

      playing = false;
      GameOver (string.Concat("Bachelor Purity: ", purityScore), feedback);
    }
  }
    
  void SurvivalGameOver (string feedback) {
    playing = false;
    stopwatch.StopTime ();
    playerAlerts.hideMessage ();
    stopwatch.Hide ();
    StopAllCoroutines ();

    GameOver (string.Concat("Time Survived: ", stopwatch.text.text), feedback);
  }

  public void Hide () {
    scoring.Hide ();
    stopwatch.Hide ();
  }

  void Hands_Slap (bool goodGuy, bool receivedRose) {
    if (goodGuy) {
      GoodGuySlapped(receivedRose);
    } else {
      VillainSlapped(receivedRose);
    }
  }

  public void VillainSneaksIn () {
    playerAlerts.displayNegativeAlert ("A villain snuck in!");
    soundEffects.PlayOneShot (badAlertSound);

    if (survivalMode) {
      SurvivalGameOver("A villain snuck in! Villains aren't classy enough to wait for an invitation.");
    } else {
      scoring.ChangeVillainCount(1);
    }
  }

  public void VillainGetsRose () {
    StartCoroutine (PlaySoundDelayed (badAlertSound, 0.2f));
    playerAlerts.displayNegativeAlert ("He's a villain!");
    scoring.ChangeVillainCount (1);

    if (survivalMode) {
      SurvivalGameOver("You gave a rose to a villain! He just gained 1 million Instagram followers and left you with nothing.");
    }
  }

  public void VillainSlapped (bool hasRose) {
    soundEffects.PlayOneShot (slapSound);
    if (hasRose) {
      scoring.ChangeVillainCount(-1);
    }
  }

  public void GoodGuyGetsRose () {
    scoring.ChangeGoodGuyCount(1);
  }

  public void GoodGuySlapped (bool hasRose) {
    soundEffects.PlayOneShot (slapSound);
    playerAlerts.displayNegativeAlert ("He was a good guy!");
    StartCoroutine (PlaySoundDelayed (badAlertSound, 0.2f));

    if (hasRose) {
      scoring.ChangeGoodGuyCount (-1);
    }

    if (survivalMode) {
      SurvivalGameOver("You slapped a good guy! Bachelor nation is furious that you eliminated such a dreamboat.");
    }
  }

  IEnumerator PlaySoundDelayed (AudioClip clip, float delay) {
    yield return new WaitForSeconds (delay);
    soundEffects.PlayOneShot (clip);
  }
}
