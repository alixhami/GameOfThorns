using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Limo : MonoBehaviour {

  public AudioClip drivingSound;
  public AudioClip stoppingSound;
  public AudioClip doorOpenSound;
  AudioSource audioSource;
  public Spawner spawner;

  public float spawnInterval;
  public float slowSpawnInterval = 7f;
  public float mediumSpawnInterval = 6f;
  public float fastSpawnInterval = 4f;

  NavMeshAgent agent;
  public Transform target;
  float accuracyWP = 0.5f;
  bool driving = true;

  Animator anim;

  void Awake () {
    agent = GetComponent<NavMeshAgent> ();
    anim = GetComponent<Animator> ();
    audioSource = GetComponent<AudioSource>();
  }

  void Start () {
    agent.destination = target.position;
    PlayDrivingSound();
  }

  void Update () {
    if (driving && Vector3.Distance (target.position, transform.position) < accuracyWP) {
      PlayCarStopSound();
      driving = false;
      Invoke("OpenDoor", 2f);
    }
  }

  public void OpenDoor () {
    anim.SetTrigger("OpenDoor");
    spawner.StartSpawning(spawnInterval);
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
