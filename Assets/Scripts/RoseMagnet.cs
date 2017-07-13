using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMagnet : MonoBehaviour {

	void OnTriggerEnter (Collider c) {

		if (c.gameObject.tag == "Rose") {
			c.transform.SetParent(transform);
			c.transform.GetComponent<Rigidbody>().isKinematic = true;
		}
  	}

}
