using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour {

	public AudioClip hitSound;
  public bool pinned = false;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
    Invoke ("SelfDestruct", 20f);
	}

  void Update() {
    if (pinned) {
      CancelInvoke();
    }
  }

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.isStatic) {
			audioSource.PlayOneShot (hitSound);
		}
	}

  void SelfDestruct () {
    Destroy (gameObject);
  }
}
