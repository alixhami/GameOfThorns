using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticTouch : MonoBehaviour {

	public bool hapticFlag = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
		if (hapticFlag) {
			device.TriggerHapticPulse (500);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Beer" && other.gameObject.tag != "Rose") {
			hapticFlag = true;	
		}
	}

	void OnTriggerExit() {
		hapticFlag = false;
	}
}
