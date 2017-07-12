using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoses : MonoBehaviour {

	public GameObject rose;
	private SteamVR_TrackedController controller;
	bool holdingRose = false;
	private GameObject currentRose;

	void Start () {
		controller = GetComponentInParent<SteamVR_TrackedController> ();
		controller.TriggerClicked += SpawnRose;
		controller.TriggerUnclicked += DropRose;
	}

	void SpawnRose (object sender, ClickedEventArgs e) {
		//transform == gameObject.transform == gameObject.GetComponent<Transform>()

		if (rose && !holdingRose) {
			GameObject newRose = Instantiate(rose, transform.position, transform.rotation)as GameObject;
			newRose.transform.SetParent(transform);
			currentRose = newRose;
			holdingRose = true;
		}
	}

	void DropRose(object sender, ClickedEventArgs e) {

		if (holdingRose) {
			currentRose.GetComponent<Rigidbody> ().isKinematic = false;
			currentRose.transform.parent = null;
			holdingRose = false;
		}
	
	}
}
