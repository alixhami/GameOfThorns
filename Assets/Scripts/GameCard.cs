using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : MonoBehaviour {

  public string gameName;
  public event System.Action<string> TriggerGame = delegate { };
  Collider collider;

  void Awake () {
    collider = GetComponent<Collider>();
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "GameController" || other.gameObject.tag == "Rose") {
      TriggerGame(gameName);
    }
  }

  public void EnableCollider () {
    collider.enabled = true;
  }

  public void DisableCollider () {
    collider.enabled = false;
  }

}
