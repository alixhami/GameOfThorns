using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : MonoBehaviour {

  public TargetSuitor[] suitors;
  public Timer timer;
  public event System.Action<string, string> GameOver = delegate { };

  public SpawnItems leftHandSpawner;
  public SpawnItems rightHandSpawner;

  int badCount;
  int goodCount;

  bool playing = false;

  float timeLimit = 90f;

  void Start () {
    timer.OutOfTime += Timer_OutOfTime;

    foreach (var suitor in suitors) {
      suitor.CountRose += Suitor_CountRose;
    }
  }

  void Suitor_CountRose (bool isJoke) {
    if (isJoke) {
      badCount++;
    } else {
      goodCount++;
    }
  }

  public void Play () {
    playing = true;
    gameObject.SetActive (true);
    Reset ();
   
    timer.SetTimer(timeLimit) ;
    timer.StartCountdown ();
  }

  void Reset() {
    goodCount = 0;
    badCount = 0;

    foreach (var suitor in suitors) {
      suitor.Reset ();
    }
  }

  void Timer_OutOfTime () {
    // only runs if the timer is for this game
    if (playing) {
      gameObject.SetActive (false);

      string credibility;
      string feedback = "";
      int totalCount = goodCount + badCount;

      // Calculate Bachelor Credibility %
      if (totalCount == 0) {
        credibility = "N/A";
      } else {
        float percentage = (float)goodCount / totalCount * 100f;
        credibility = string.Concat(Mathf.Round(percentage).ToString (), "%");
      }

      // Determine feedback
      if (goodCount == 0 && badCount == 0) {
        feedback = "You didn't give out a single rose! The production company will be suing you for breach of contract.";  
      } else if (badCount == 0) {
        feedback = "Every bachelor in the house is a catch! You'll probably find love, but the ratings will be crap.";
      } else if (goodCount == 0) {
        feedback = "Every bachelor you chose is a joke! Hope you're ready to hear that kooky xylophone music all season...";
      } else if (badCount > goodCount) {
        feedback = "Most of your bachelors are jokes... Bachelor Nation is going to think you have poor judgment.";
      } else if (goodCount > badCount) {
        feedback = "Most of your bachelors are here for the right reasons! Hopefully you can weed out the weirdos by week 2.";
      } else if (badCount == goodCount) {
        feedback = "Half of your bachelors are jokes! Hopefully your judgment improves in the weeks ahead.";
      }

      playing = false;
      GameOver (string.Concat("Roses Given: ", totalCount, "\nCredibility: ", credibility), feedback);
    }
  }
}
