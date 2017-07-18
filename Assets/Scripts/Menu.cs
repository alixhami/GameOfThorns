using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

  public GameObject limo;

  void Start () {
    // Invoke("StartGame", 5f); 
  }

  void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == "Rose") {
      StartGame(); 
    }
  }

  void StartGame () {
    gameObject.SetActive(false);
    limo.GetComponent<Animator>().SetTrigger("Arrive");
  }
}
