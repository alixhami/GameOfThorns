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
	string[] tags = new string[]{"TargetCharacter", "DrunkCharacter"};
	public bool goodGuy;
	public bool receivedRose;

	public Transform playerArea;
	public Transform mansionDoor;
	public Transform towardElimination;
	public Transform eliminationSpot;
	public Transform currentDestination;

	float accuracyWP;
	NavMeshAgent agent;

	public Scoring scoring;

	AudioSource audioSource;
	public AudioClip sipSound;
	public AudioClip burpSound;
	public AudioClip refreshedSound;
	public AudioClip roseHitSound;

	void Awake () {
		anim = GetComponent<Animator> ();
		tag = tags [Random.Range (0, tags.Length)];
		goodGuy = tag == "TargetCharacter";
		accuracyWP = 1.0f;
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		currentDestination = playerArea;
		Move();
	}

	void Update () {
		if (Vector3.Distance (currentDestination.transform.position, transform.position) < accuracyWP) {
			if (currentDestination == playerArea) {
				currentDestination = (receivedRose || !goodGuy) ? mansionDoor : towardElimination;
			} else if (currentDestination == towardElimination) {
				currentDestination = eliminationSpot;
			}

		}
		agent.destination = currentDestination.transform.position;
	}

	void Move() {
		anim.SetFloat("Forward", 1f);
	}

	void OnAnimatorMove () {
		Vector3 position = anim.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}

	public void GetRose () {
		audioSource.PlayOneShot(roseHitSound);
		anim.SetTrigger ("getRose");
		anim.SetBool ("hasRose", true);

		if (goodGuy && !receivedRose) {
			scoring.AddPoints (1);
		} else if (!receivedRose) {
			scoring.AddPoints (-1);
		}

		receivedRose = true;
		currentDestination = mansionDoor;
	}

	public void GetBeer () {
		audioSource.PlayOneShot(sipSound);
		StartCoroutine (BeerReaction());

		if (!goodGuy) {
			anim.SetFloat ("Drunk", 1f);
		}
	}

	IEnumerator BeerReaction () {
		yield return new WaitForSeconds (0.8f);
		audioSource.PlayOneShot (goodGuy ? refreshedSound : burpSound);
	}

}
