﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour {

	Animator anim;
	Rigidbody rb;
	string[] tags = new string[]{"TargetCharacter", "EnemyCharacter"};
	public GameObject[] waypoints;
	int currentWP = 0;
	float accuracyWP;
	NavMeshAgent agent;

	void Awake () {
		anim = GetComponent<Animator> ();
		tag = tags [Random.Range (0, tags.Length)];
		accuracyWP = Random.Range (2.0f, 5.0f);
	}

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		Move();
	}

	void Update () {
		if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuracyWP) {
	  		currentWP++;
		}
		agent.destination = waypoints[currentWP].transform.position;
	}

	void Move() {
    
		if (tag == "TargetCharacter") {
			anim.SetFloat("Forward", 1f);
		} else if (tag == "EnemyCharacter") {
			anim.SetFloat ("Drunk", 1f);
		}
    
	}

	void OnAnimatorMove () {
		Vector3 position = anim.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Rose") {
	  		anim.SetTrigger ("getRose");
	  		anim.SetBool ("hasRose", true);
		}
	}
}
