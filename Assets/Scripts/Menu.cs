using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public GameManager game;
	public GameObject startScreen;
	public GameObject gameOverScreen;
  public GameObject limoGameInstructions;
   
  public void ShowInstructions () {
    if (!limoGameInstructions.activeSelf) {
      HideMenus ();
      gameObject.SetActive (true);
      limoGameInstructions.SetActive (true);
    }
  }

	void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == "Rose") {
      StartTimedLimoGame (); 
    } else if (other.gameObject.tag == "Beer") {
      StartSurvivalLimoGame();
    }
	}

	void StartTimedLimoGame () {
		HideMenus ();
		game.StartTimedLimoGame();
	}

  void StartSurvivalLimoGame () {
    HideMenus ();
    game.StartSurvivalLimoGame();
  }

	public void DisplayGameOverMenu () {
		gameOverScreen.SetActive(true);
		gameObject.SetActive (true);
	}

	void HideMenus() {
		gameObject.SetActive(false);
		startScreen.SetActive(false);
    limoGameInstructions.SetActive (false);
		gameOverScreen.SetActive(false);
	}


}
