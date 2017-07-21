using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour {

  public GameManager game;
  float runningTime;
  bool running = false;
     
  public Text text;

  public void Hide () {
    gameObject.SetActive (false);
  }

  public void StartTiming () {
    running = true;
  }

  public void ResetTime () {
    runningTime = 0f;
    text.text = Mathf.Round(runningTime).ToString();
    gameObject.SetActive(true);
  }

  public void StopTime () {
    running = false;
  }

  void Update () {
    if (running) {
      runningTime += Time.deltaTime;
      text.text = Mathf.Round(runningTime).ToString();
    }
  }
}
