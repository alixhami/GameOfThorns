using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour {

	public GameObject character;
	public Transform roseTarget;
	public float roseSpeed = 0.1f;

  bool hasRose = false;
  GameObject rose;
  public event System.Action GotRose = delegate { };
  public event System.Action GotBeer = delegate { };

	void OnTriggerEnter (Collider c) {

		if (c.gameObject.tag == "Rose" && !hasRose) {
      rose = c.gameObject;
      rose.GetComponent<Rose> ().pinned = true;
      rose.transform.SetParent (transform);
			rose.transform.GetComponent<Rigidbody> ().isKinematic = true;
			StopAllCoroutines ();
			StartCoroutine (moveRoseToTarget (rose.transform));

      hasRose = true;
      GotRose ();

		} else if (c.gameObject.tag == "Beer") {
			Destroy (c.gameObject);

      GotBeer ();
		}
	}

	IEnumerator moveRoseToTarget (Transform roseTransform) {
		for (int i = 0; i < 1000; i++) {
			roseTransform.position = Vector3.Lerp (roseTransform.position, roseTarget.position, roseSpeed);
			roseTransform.rotation = Quaternion.Lerp (roseTransform.rotation, roseTarget.rotation, roseSpeed);
			yield return new WaitForSeconds (0.01f);
		}
	}

  public void ResetRose () {
    Destroy (rose);
    hasRose = false;
  }

}
