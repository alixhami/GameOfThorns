using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {

	public bool hapticFlag = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
		if (hapticFlag) {
			device.TriggerHapticPulse (500);
		}
	}

	void OnTriggerEnter(Collider other) {
		// handles haptic feedback from object collisions
		if (other.gameObject.tag != "Beer" && other.gameObject.tag != "Rose") {
			hapticFlag = true;	
		}

		// handles slap
		if (other.gameObject.tag == "Face") {
      GameObject character = other.gameObject.transform.root.gameObject;
      character.GetComponent<Movement>().GetSlapped();
			

			float pulseIntensity = Mathf.Clamp(device.velocity.magnitude/6f, 0f, 1f);
			float pulseDuration = Mathf.Clamp (pulseIntensity, 0f, 0.3f);
			StartCoroutine (TimedVibration (pulseDuration, pulseIntensity));

			// must set haptic flag to false because trigger won't exit after object is deleted
			hapticFlag = false;
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
