using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

  public GameManager game;
  public GameObject startScreen;
  public GameObject gameOverScreen;

  void Start () {
    //Invoke("StartTimedLimoGame", 3f); 
  }

  void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == "Rose") {
      StartTimedLimoGame(); 
    }
  }

  void StartTimedLimoGame () {
    gameObject.SetActive(false);
    startScreen.SetActive(false);
    gameOverScreen.SetActive(false);
    game.StartTimedLimoGame();
  }

  public void DisplayGameOverMenu () {
    gameOverScreen.SetActive(true);
  }


}
