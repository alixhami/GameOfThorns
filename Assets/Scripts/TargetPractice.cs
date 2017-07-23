using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : MonoBehaviour {

  public TargetSuitor[] suitors;
  public Timer timer;
  public event System.Action<string> GameOver = delegate { };

  float timeLimit = 90f;

  void Start () {
    timer.OutOfTime += Timer_OutOfTime;
  }

  public void Play () {

    foreach (var suitor in suitors) {
      suitor.Reset ();
    }

    gameObject.SetActive (true);
    timer.SetTimer(90f);
    timer.StartCountdown ();
  }

  void Timer_OutOfTime () {
    gameObject.SetActive (false);

    GameOver ("You're out of time! Insert Stats here...");
  }
}
