using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

  public GameManager game;
  float timeLeft;
  bool countingDown = false;
     
  public Text text;

  void Awake () {
    text = GetComponent<Text>();
  }

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
        game.GameOver("HEY GUYS");
      }
    }
  }
}
