using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour {

	public AudioClip hitSound;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.isStatic) {
			audioSource.PlayOneShot (hitSound);
		}
	}
}
