using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour {

	public GameObject startScreen;
	public GameObject gameOverScreen;
  public TextMeshPro gameOverScore;
  public TextMeshPro gameOverFeedback;
  public GameObject limoGameInstructions;

  public void ShowInstructions () {
    if (!limoGameInstructions.activeSelf) {
      HideMenus ();
      gameObject.SetActive (true);
      limoGameInstructions.SetActive (true);
    }
  }

	public void DisplayGameOverMenu (string score, string feedback) {
    gameOverScore.text = score;
    gameOverFeedback.text = feedback;
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
