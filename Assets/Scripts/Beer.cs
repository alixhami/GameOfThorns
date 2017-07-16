using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour {

	public AudioClip hitGroundSound;
	public AudioClip hitBeerSound;
	AudioSource audioSource;

	bool hasDropped = false;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Beer") {
			audioSource.PlayOneShot (hitBeerSound);
		} else if (other.gameObject.isStatic && !hasDropped) {
			audioSource.PlayOneShot (hitGroundSound);
			hasDropped = true;
		}
	}

}
