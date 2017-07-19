using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public GameManager game;
	public GameObject startScreen;
	public GameObject gameOverScreen;
  public GameObject limoGameInstructions;

	void Start () {
		//Invoke("StartTimedLimoGame", 3f); 
	}

  public void ShowInstructions () {
    if (!limoGameInstructions.activeSelf) {
      HideMenus ();
      gameObject.SetActive (true);
      limoGameInstructions.SetActive (true);
    }
  }

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Rose") {
			StartTimedLimoGame(); 
		}
	}

	void StartTimedLimoGame () {
		HideMenus ();
		game.StartTimedLimoGame();
	}

	public void DisplayGameOverMenu () {
    //Invoke("StartTimedLimoGame", 3f); 
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
