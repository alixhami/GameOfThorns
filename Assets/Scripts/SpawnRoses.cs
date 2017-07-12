using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoses : MonoBehaviour {

	public GameObject rose;
	private SteamVR_TrackedController controller;

	void Start () {
		controller = GetComponentInParent<SteamVR_TrackedController> ();
		controller.TriggerClicked += SpawnRose;
	}

	void SpawnRose (object sender, ClickedEventArgs e) {
		//transform == gameObject.transform == gameObject.GetComponent<Transform>()

		if (rose) {
			GameObject newRose = Instantiate(rose, transform.position, transform.rotation)as GameObject;
			newRose.transform.SetParent(transform);
		}
	}
}
