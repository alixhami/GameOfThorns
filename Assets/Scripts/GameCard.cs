using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : MonoBehaviour {

  public string gameName;
  public event System.Action<string> TriggerGame = delegate { };
  Collider gameCardCollider;

  void Awake () {
    gameCardCollider = GetComponent<Collider>();
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "GameController" || other.gameObject.tag == "Rose") {
      TriggerGame(gameName);
    }
  }

  public void EnableCollider () {
    gameCardCollider.enabled = true;
  }

  public void DisableCollider () {
    gameCardCollider.enabled = false;
  }

}
