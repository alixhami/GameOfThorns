using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoses : MonoBehaviour {

	private SteamVR_TrackedController controller;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;
	public Rigidbody attachPoint;
	public GameObject rose;

	private bool holdingRose = false;
	private GameObject currentRose;
	private Rigidbody currentRoseRigidbody;

	void Awake () {
		controller = GetComponent<SteamVR_TrackedController> ();
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		controller.TriggerClicked += SpawnRose;
		controller.TriggerUnclicked += ThrowRose;
	}

	void SpawnRose (object sender, ClickedEventArgs e) {

		if (rose && !holdingRose) {
			GameObject newRose = Instantiate(rose, transform.position, transform.rotation)as GameObject;
			newRose.transform.position = attachPoint.transform.position;
			joint = newRose.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;

			currentRose = newRose;
			currentRoseRigidbody = newRose.GetComponent<Rigidbody> ();
			holdingRose = true;
		}
	}

	void ThrowRose(object sender, ClickedEventArgs e) {

		if (holdingRose) {

			Object.DestroyImmediate(joint);
			joint = null;
			holdingRose = false;
			Object.Destroy(currentRose, 15.0f);

			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
			if (origin != null)
			{
				currentRoseRigidbody.velocity = origin.TransformVector(device.velocity);
				currentRoseRigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				currentRoseRigidbody.velocity = device.velocity;
				currentRoseRigidbody.angularVelocity = device.angularVelocity;
			}

			currentRoseRigidbody.maxAngularVelocity = currentRoseRigidbody.angularVelocity.magnitude;
		}
	
	}
}
