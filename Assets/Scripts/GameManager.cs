using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager gameManagerInstance;

	public Menu menu;
  public TargetPractice targetPractice;
  public LimoGame limoGame;

  public bool playingGame = false;

	public AudioSource music;

  public GameObject gameSelectPillars;
  public GameCard[] gameCards;

  void Start() {
    foreach (var gameCard in gameCards) {
      gameCard.TriggerGame += GameCard_TriggerGame;
    }
      
    targetPractice.GameOver += GameOver;
    limoGame.GameOver += GameOver;
  }

  private void GameCard_TriggerGame(string gameName) {
    playingGame = true;
    switch(gameName) {
      case "LimoTimed":
        limoGame.Play (false);
        break;
      case "LimoSurvival":
        limoGame.Play (true);
        break;
      case "TargetPractice":
        limoGame.Hide ();
        targetPractice.Play();
        break;
    }
    gameSelectPillars.SetActive(false);
    menu.HideMenus();
  }

  public void ShowInstructions () {
    if (!playingGame) {
      menu.ShowInstructions ();
    }
  }

  public void GameOver (string score, string feedback) {
    playingGame = false;
    StopAllCoroutines ();
    DestroyAllWithTag("Prop");
    DestroyAllWithTag("Suitor");

    menu.DisplayGameOverMenu(score, feedback);
    gameSelectPillars.SetActive(true);
  }

  void DestroyAllWithTag (string tag) {
    GameObject[] targets = GameObject.FindGameObjectsWithTag (tag);

    for(var i = 0 ; i < targets.Length ; i ++) {
      Destroy(targets[i]);
    }
  }

}
