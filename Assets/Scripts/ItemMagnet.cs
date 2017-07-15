using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour {

	public GameObject character;
	public Transform roseTarget;
	public float roseSpeed = 0.1f;
	private Movement characterMovement;

	void Awake () {
		characterMovement = character.GetComponent<Movement> ();
	}

	void OnTriggerEnter (Collider c) {

		if (c.gameObject.tag == "Rose" && !characterMovement.receivedRose) {
			c.transform.SetParent (transform);
			c.transform.GetComponent<Rigidbody> ().isKinematic = true;
			StopAllCoroutines ();
			StartCoroutine (moveRoseToTarget (c.transform));

			characterMovement.GetRose ();

		} else if (c.gameObject.tag == "Beer") {
			Destroy (c.gameObject);

			characterMovement.GetBeer ();
		}
	}

	IEnumerator moveRoseToTarget (Transform roseTransform) {
		for (int i = 0; i < 1000; i++) {
			roseTransform.position = Vector3.Lerp (roseTransform.position, roseTarget.position, roseSpeed);
			roseTransform.rotation = Quaternion.Lerp (roseTransform.rotation, roseTarget.rotation, roseSpeed);
			yield return new WaitForSeconds (0.01f);
		}
	}



}
