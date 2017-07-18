using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public Scoring scoring;
  public PlayerAlerts playerAlerts;
  public AudioSource music;
  public AudioSource soundEffects;

  public AudioClip smoochSound;
  public AudioClip badAlertSound;

  int villainsSnuckInCount;
  int villainsWithRosesCount;
  int villainsSlappedCount;
  int goodGuysWithRosesCount;
  int goodGuysSlappedCount;

  public void VillainSneaksIn () {
    playerAlerts.displayNegativeAlert("A villain snuck in!");
    soundEffects.PlayOneShot(badAlertSound);
    scoring.ChangeVillainCount(1);
    villainsSnuckInCount ++;
  }

  public void VillainGetsRose () {
    StartCoroutine (PlaySoundDelayed(badAlertSound, 0.8f));
    villainsWithRosesCount ++;
    scoring.ChangeVillainCount(1);
  }

  public void VillainSlapped (bool hasRose) {
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
    StartCoroutine (PlaySoundDelayed(smoochSound, 0.8f));
    scoring.ChangeGoodGuyCount(1);
  }

  public void GoodGuySlapped (bool hasRose) {
    StartCoroutine (PlaySoundDelayed(badAlertSound, 0.8f));
    soundEffects.PlayOneShot (badAlertSound);
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
