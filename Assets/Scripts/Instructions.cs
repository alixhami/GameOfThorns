using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

  public GameObject limoTimedInstructions;
  public GameObject limoSurvivalInstructions;
  public GameObject TargetPracticeInstructions;

  string currentGameName;
  public event System.Action<string> TriggerGameStart = delegate { };

  public void ShowInstructions(string gameName) {
    gameObject.SetActive (true);
    currentGameName = gameName;
    switch (currentGameName) {
    case "LimoTimed":
      limoTimedInstructions.SetActive (true);
      break;
    case "LimoSurvival":
      limoSurvivalInstructions.SetActive (true);
      break;
    case "TargetPractice":
      TargetPracticeInstructions.SetActive (true);
      break;
    }
  }

  void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == "Rose") {
      Destroy (other.gameObject);
      TriggerGameStart (currentGameName);
      currentGameName = null;
      HideAll ();
    }
  }

  void HideAll () {
    gameObject.SetActive (false);
    limoTimedInstructions.SetActive (false);
    limoSurvivalInstructions.SetActive (false);
    TargetPracticeInstructions.SetActive (false);
  }
}
