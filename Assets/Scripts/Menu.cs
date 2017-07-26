using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour {

	public GameObject startScreen;
	public GameObject gameOverScreen;
  public TextMeshPro gameOverScore;
  public TextMeshPro gameOverFeedback;

	public void DisplayGameOverMenu (string score, string feedback) {
    gameOverScore.text = score;
    gameOverFeedback.text = feedback;
    gameOverScreen.SetActive(true);
	}

	public void HideMenus() {
		startScreen.SetActive(false);
		gameOverScreen.SetActive(false);
	}

}
