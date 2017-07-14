using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMagnet : MonoBehaviour {

	public GameObject character;
  public Transform roseTarget;
  public float roseSpeed = 0.1f;

	void OnTriggerEnter (Collider c) {

		if (c.gameObject.tag == "Rose" && !character.GetComponent<Movement>().receivedRose ) {
  			c.transform.SetParent(transform);
  			c.transform.GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        StartCoroutine(moveRoseToTarget(c.transform));

			  character.GetComponent<Movement> ().GetRose ();
  	 }
  }

  IEnumerator moveRoseToTarget (Transform roseTransform) {
    for (int i = 0; i < 1000; i++) {
      roseTransform.position = Vector3.Lerp (roseTransform.position, roseTarget.position, roseSpeed);
      yield return new WaitForSeconds (0.01f);
    }
  }



}
