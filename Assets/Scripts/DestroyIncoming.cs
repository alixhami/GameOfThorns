using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIncoming : MonoBehaviour {

  public bool isMansionEntrance;
  public LimoGame game;

  void OnTriggerEnter (Collider other) {

    GameObject parentObject = other.gameObject.transform.root.gameObject;

    if (parentObject.tag == "Suitor" && isMansionEntrance) {

      Movement suitorMovement = parentObject.GetComponent<Movement>();
      if (!suitorMovement.goodGuy && !suitorMovement.receivedRose) {
        suitorMovement.receivedRose = true;
        game.VillainSneaksIn();
      }
    }

    Destroy(parentObject);

  }

}
