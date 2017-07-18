using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limo : MonoBehaviour {

  public AudioClip drivingSound;
  public AudioClip stoppingSound;
  public AudioClip doorOpenSound;
  AudioSource audioSource;

  void Awake () {
    audioSource = GetComponent<AudioSource>();
  }

  public void PlayDrivingSound () {
    audioSource.volume = 0.1f;
    audioSource.PlayOneShot(drivingSound);
  }

  public void PlayCarStopSound () {
    audioSource.volume = 0.3f;
    audioSource.Stop();
    audioSource.PlayOneShot(stoppingSound);
  }

  public void PlayDoorOpenSound () {
    audioSource.PlayOneShot(doorOpenSound);
  }

}
