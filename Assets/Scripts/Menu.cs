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

	public void DisplayGameOverMenu () {
		gameOverScreen.SetActive(true);
		gameObject.SetActive (true);
	}

	public void HideMenus() {
		gameObject.SetActive(false);
		startScreen.SetActive(false);
        limoGameInstructions.SetActive (false);
		gameOverScreen.SetActive(false);
	}


}
