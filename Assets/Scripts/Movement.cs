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

	public GameObject[] waypoints;
	int currentWP = 0;
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
		accuracyWP = Random.Range (2.0f, 5.0f);
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		Move();
	}

	void Update () {
		if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuracyWP && currentWP < waypoints.Length - 1) {
	  		currentWP++;
		}
		agent.destination = waypoints[currentWP].transform.position;
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
