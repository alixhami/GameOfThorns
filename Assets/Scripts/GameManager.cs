using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager gameManagerInstance;

	public Timer timer;
	public Scoring scoring;
	public PlayerAlerts playerAlerts;
	public GameObject limo;
	public Menu menu;

  bool playingGame = false;

	public Transform playerArea;
	public Transform mansionDoor;
	public Transform towardElimination;
	public Transform eliminationSpot;

	public AudioSource music;
	public AudioSource soundEffects;

	public AudioClip badAlertSound;
	public AudioClip slapSound;

	int villainsSnuckInCount;
	int villainsWithRosesCount;
	int villainsSlappedCount;
	int goodGuysWithRosesCount;
	int goodGuysSlappedCount;

	public void StartTimedLimoGame () {
    playingGame = true;
    scoring.ResetScores();
    scoring.transform.gameObject.SetActive(true);
		timer.SetTimer(90f);
		GameObject newLimo = Instantiate(limo);
		newLimo.transform.Find("Spawner").GetComponent<Spawner>().game = this;
	}

  public void ShowInstructions () {
    if (!playingGame) {
      menu.ShowInstructions ();
    }
  }

	public void StartTimer () {
		timer.StartCountdown();
	}

	public void GameOver () {
    playingGame = false;
		DestroyAllWithTag("Prop");
		DestroyAllWithTag("Suitor");

		timer.hideTimer ();
		playerAlerts.hideMessage ();
		menu.DisplayGameOverMenu();
	}

	void DestroyAllWithTag (string tag) {
		GameObject[] targets = GameObject.FindGameObjectsWithTag (tag);

		for(var i = 0 ; i < targets.Length ; i ++) {
		  Destroy(targets[i]);
		}
	}

	public void VillainSneaksIn () {
		playerAlerts.displayNegativeAlert("A villain snuck in!");
		soundEffects.PlayOneShot(badAlertSound);
		scoring.ChangeVillainCount(1);
		villainsSnuckInCount ++;
	}

	public void VillainGetsRose () {
		StartCoroutine (PlaySoundDelayed(badAlertSound, 0.2f));
		playerAlerts.displayNegativeAlert("He's a villain!");
		villainsWithRosesCount ++;
		scoring.ChangeVillainCount(1);
	}

	public void VillainSlapped (bool hasRose) {
		soundEffects.PlayOneShot (slapSound);
		//playerAlerts.displayPositiveAlert("Eliminated a villain!");
		// need cheering sound
		villainsSlappedCount ++;
		if (hasRose) {
			villainsWithRosesCount --;
			scoring.ChangeVillainCount(-1);
		}
	}

	public void GoodGuyInHouse () {
		goodGuysWithRosesCount ++;
	}

	public void GoodGuyGetsRose () {
		//playerAlerts.displayPositiveAlert("What a catch!");
		scoring.ChangeGoodGuyCount(1);
	}

	public void GoodGuySlapped (bool hasRose) {
		soundEffects.PlayOneShot (slapSound);
		playerAlerts.displayNegativeAlert("He was a good guy!");
		StartCoroutine (PlaySoundDelayed(badAlertSound, 0.2f));
		goodGuysSlappedCount ++;
		if (hasRose) {
			scoring.ChangeGoodGuyCount (-1);
		}
	}

	IEnumerator PlaySoundDelayed (AudioClip clip, float delay) {
		yield return new WaitForSeconds (delay);
		soundEffects.PlayOneShot (clip);
	}
}
