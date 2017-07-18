using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public Scoring scoring;
  public PlayerAlerts playerAlerts;

  int villainsSnuckInCount;
  int villainsWithRosesCount;
  int goodGuysWithRosesCount;

  public void villainSneaksIn () {
    playerAlerts.displayNegativeAlert("A villain snuck in!");
    scoring.AddPoints(-1);
    villainsSnuckInCount += 1;
  }
}
