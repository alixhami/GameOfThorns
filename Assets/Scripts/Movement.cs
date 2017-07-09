using System.Collections;
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
  public Transform goal;

	void Awake () {
    anim = GetComponent<Animator> ();
    tag = tags [Random.Range (0, tags.Length)];
	}

  void Start () {
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    agent.destination = goal.position;

    Move();
  }

	void Move() {
    
		if (tag == "TargetCharacter") {
			anim.SetFloat("Forward", 1f);
		} else if (tag == "EnemyCharacter") {
			anim.SetFloat ("Drunk", 1f);
		}
    
	}

  void OnCollisionEnter (Collision c) {
    if (c.gameObject.tag == "Rose") {
      anim.SetTrigger ("getRose");
    }
  }
}
