using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

  float timeLeft;
  bool countingDown = false;
  public event System.Action OutOfTime = delegate { };
     
  public TextMeshPro text;

  public void Hide () {
    gameObject.SetActive (false);
  }

  public void SetTimer (float seconds) {
    text.text = null;
    gameObject.SetActive(true);
    timeLeft = seconds;
  }

  public void StartCountdown () {
    countingDown = true;
  }

  void Update () {
    if (countingDown) {
      timeLeft -= Time.deltaTime;

      if (timeLeft <= 10) {
        text.text = Mathf.Round(timeLeft).ToString();
      }

      if(timeLeft <= 0) {
        countingDown = false;
        timeLeft = 0;
        Hide ();
        OutOfTime ();
      }
    }
  }
}
