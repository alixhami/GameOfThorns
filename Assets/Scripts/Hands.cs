using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

  public GameManager game;

	public bool hapticFlag = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }

  AudioSource audioSource;
  public AudioClip hitSound;

  public event System.Action<bool, bool> Slap = delegate { };

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
    audioSource = GetComponent<AudioSource> ();
	}

	void Update () {
		if (hapticFlag) {
			device.TriggerHapticPulse (500);
		}
	}

	void OnTriggerEnter(Collider other) {
    
		if (other.gameObject.tag == "Chest") {
			
      hapticFlag = true;	

		} else if (other.gameObject.tag == "GameCard") {
      
      StartCoroutine (TimedVibration (0.15f, 0.5f));
      audioSource.PlayOneShot (hitSound);

    } else if (other.gameObject.tag == "Face") {

      GameObject character = other.gameObject.transform.root.gameObject;
      character.GetComponent<Movement>().GetSlapped();

      bool goodGuy = character.GetComponent<Movement>().goodGuy;
      bool receivedRose = character.GetComponent<Movement>().receivedRose;
      Slap(goodGuy, receivedRose);

			float pulseIntensity = Mathf.Clamp(device.velocity.magnitude/6f, 0f, 1f);
			float pulseDuration = Mathf.Clamp (pulseIntensity, 0f, 0.3f);
			StartCoroutine (TimedVibration (pulseDuration, pulseIntensity));
		
    }
	}

	void OnTriggerExit() {
		hapticFlag = false;
	}

	//length is how long the vibration should go for
	//strength is vibration strength from 0-1
	IEnumerator TimedVibration(float length, float strength) {
		for(float i = 0; i < length; i += Time.deltaTime) {
			device.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}
}
