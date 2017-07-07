using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour {

	Animator anim;
	Rigidbody rb;

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Rose") {
			anim.SetTrigger ("getRose");
		}
	}

	void Awake () {
		anim = GetComponent<Animator> ();
		Move();
	}

	void Move() {
		print (gameObject.tag);
		anim.SetFloat ("Forward", 1f);
		if (gameObject.tag == "TargetCharacter") {
			anim.SetFloat("Forward", 1f);
		} else if (gameObject.tag == "EnemyCharacter") {
			anim.SetFloat ("Drunk", 1f);
		}

	}
}
