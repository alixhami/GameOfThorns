using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitorFace : MonoBehaviour {

  public GameObject character;
  private Movement characterMovement;

  void Awake () {
    characterMovement = character.GetComponent<Movement> ();
  }

  void OnTriggerEnter (Collider c) {

    if (c.gameObject.tag == "Beer") {
      Destroy (c.gameObject);

      characterMovement.GetBeer ();
    }
  }
}
