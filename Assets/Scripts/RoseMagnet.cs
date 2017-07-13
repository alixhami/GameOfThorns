using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMagnet : MonoBehaviour {

	public GameObject character;

	void OnTriggerEnter (Collider c) {

		if (c.gameObject.tag == "Rose" && !character.GetComponent<Movement>().receivedRose ) {
  			c.transform.SetParent(transform);
  			c.transform.GetComponent<Rigidbody>().isKinematic = true;
			character.GetComponent<Movement> ().GetRose ();
  		}
  	}

}
