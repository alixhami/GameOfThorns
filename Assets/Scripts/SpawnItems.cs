using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems: MonoBehaviour {

	private SteamVR_TrackedController controller;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;
	public Rigidbody attachPoint;
  public GameObject item;

	private bool holdingItem = false;
	private GameObject currentItem;
	private Rigidbody currentItemRigidbody;

  public bool targetPractice;

	void Awake () {
		controller = GetComponent<SteamVR_TrackedController> ();
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		controller.TriggerClicked += SpawnItem;
		controller.TriggerUnclicked += ThrowItem;
	}

	void SpawnItem (object sender, ClickedEventArgs e) {

		if (item && !holdingItem) {
			GameObject newItem = Instantiate(item, transform.position, transform.rotation)as GameObject;
			newItem.transform.position = attachPoint.transform.position;

			joint = newItem.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;

			currentItem = newItem;
			currentItemRigidbody = newItem.GetComponent<Rigidbody> ();
			holdingItem = true;
		}
	}

	void ThrowItem(object sender, ClickedEventArgs e) {

		if (holdingItem) {

			Object.DestroyImmediate(joint);
			joint = null;
			holdingItem = false;

      if (currentItem) {
				var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
				if (origin != null) {
					currentItemRigidbody.velocity = origin.TransformVector(device.velocity);
					currentItemRigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
				} else {
					currentItemRigidbody.velocity = device.velocity;
					currentItemRigidbody.angularVelocity = device.angularVelocity;
				}

				currentItemRigidbody.maxAngularVelocity = currentItemRigidbody.angularVelocity.magnitude;
			}
		}
	}
}
