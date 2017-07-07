using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Transform crosshair;
	public GameObject projectile;
	private SteamVR_TrackedController controller;

	void Start () {
		controller = GetComponentInParent<SteamVR_TrackedController> ();
		controller.TriggerClicked += Shoot;
	}

	void Shoot (object sender, ClickedEventArgs e) {
		//transform == gameObject.transform == gameObject.GetComponent<Transform>()
		if (projectile) {
		  GameObject newProjectile = Instantiate(projectile, crosshair.gameObject.transform.position, transform.rotation)as GameObject;
		  newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.VelocityChange);
		}
	}
}
