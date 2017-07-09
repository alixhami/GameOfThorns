using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIncoming : MonoBehaviour {

  void OnTriggerEnter(Collider other) {
    Destroy(other.gameObject);
  }

}
