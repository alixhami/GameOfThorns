using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroinHit : MonoBehaviour {

  public string gameName;
  public event System.Action TriggerGroinHit = delegate { };

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Rose" || other.gameObject.tag == "Beer") {
      TriggerGroinHit();
    }
  }

}
