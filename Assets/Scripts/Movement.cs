using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	Animator anim;
	Rigidbody rb;
	string[] tags = new string[]{"TargetCharacter", "EnemyCharacter"};
	bool goodGuy;
	public bool receivedRose;

	public GameObject[] waypoints;
	int currentWP = 0;
	float accuracyWP;
	NavMeshAgent agent;

	public Scoring scoring;

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
			goodGuy = true;
			anim.SetFloat("Forward", 1f);
		} else if (tag == "EnemyCharacter") {
			goodGuy = false;
			anim.SetFloat ("Drunk", 1f);
		}
    
	}

	void OnAnimatorMove () {
		Vector3 position = anim.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}

	public void GetRose () {
		anim.SetTrigger ("getRose");
		anim.SetBool ("hasRose", true);

		if (goodGuy && !receivedRose) {
			scoring.AddPoints (1);
		} else if (!receivedRose) {
			scoring.AddPoints (-1);
		}

		receivedRose = true;
	}

}
