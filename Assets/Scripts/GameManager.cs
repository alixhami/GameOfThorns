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
  float maxMusicVolume = 0.3f;
  float musicFadeSpeed = 0.02f;
  public AudioClip seriousMusic;
  public AudioClip hopefulMusic;

  public GameObject gameSelectPillars;
  public GameCard[] gameCards;

  void Awake () {
    music.volume = maxMusicVolume;
    music.clip = hopefulMusic;
    music.Play();
  }

  void Start() {
    foreach (var gameCard in gameCards) {
      gameCard.TriggerGame += GameCard_TriggerGame;
    }
      
    targetPractice.GameOver += GameOver;
    limoGame.GameOver += GameOver;
  }

  private void GameCard_TriggerGame (string gameName) {
    playingGame = true;
    switch (gameName) {
      case "LimoTimed":
        ChangeMusic(hopefulMusic);
        limoGame.Play (false);
        break;
      case "LimoSurvival":
        ChangeMusic(hopefulMusic);
        limoGame.Play (true);
        break;
      case "TargetPractice":
        ChangeMusic(seriousMusic);
        limoGame.Hide ();
        targetPractice.Play();
        break;
    }
    gameSelectPillars.SetActive(false);
    menu.HideMenus();
  }

  void ChangeMusic (AudioClip track) {
    if (music.clip.name != track.name) {
      StartCoroutine(ChangeTracks(track));
    }
  }

  IEnumerator ChangeTracks (AudioClip track) {
    float audioVolume = music.volume;

    while (music.volume >= musicFadeSpeed) {
      audioVolume -= musicFadeSpeed;
      music.volume = audioVolume;
      yield return new WaitForSeconds(0.01f);
    }

    music.Stop();

    music.clip = track;

    music.Play();
    music.volume = 0f;
    audioVolume = music.volume;

    while (music.volume < maxMusicVolume) {
      audioVolume += musicFadeSpeed;
      music.volume = audioVolume;
      yield return new WaitForSeconds(0.01f);
    }
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
