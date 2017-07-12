using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMagnet : MonoBehaviour {

	void OnTriggerEnter (Collider c) {
		print ("Collided");
		print (c.gameObject.tag);
		if (c.gameObject.tag == "Rose") {
			print ("A ROSE!");
			c.transform.SetParent(transform);
			c.transform.GetComponent<Rigidbody>().isKinematic = true;
		}
  	}

}
