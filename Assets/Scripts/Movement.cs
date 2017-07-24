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
	string[] suitorTypes = new string[]{"Good", "Drunk"};
  public string suitorType;
	public bool goodGuy;
	public bool receivedRose;
  public ItemMagnet chest;

  public Transform towardPlayer;
	public Transform playerArea;
	public Transform mansionDoor;
	public Transform towardElimination;
	public Transform eliminationSpot;
	public Transform currentDestination;

	float accuracyWP;
	NavMeshAgent agent;
  
	public LimoGame game;

	AudioSource audioSource;
	public AudioClip sipSound;
	public AudioClip burpSound;
	public AudioClip refreshedSound;
	public AudioClip roseHitSound;

	void Awake () {
		anim = GetComponent<Animator> ();
    agent = GetComponent<NavMeshAgent> ();
		suitorType = suitorTypes [Random.Range (0, suitorTypes.Length)];
		goodGuy = suitorType == "Good";
    anim.SetBool ("Villain", !goodGuy);
		accuracyWP = 1.0f;
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
    chest.GotRose += GetRose;
    chest.GotBeer += GetBeer;

    towardPlayer = game.towardPlayer;
    playerArea = game.playerArea;
    mansionDoor = game.mansionDoor;
    towardElimination = game.towardElimination;
    eliminationSpot = game.eliminationSpot;

		currentDestination = towardPlayer;
		Move();
	}

	void Update () {
    if (Vector3.Distance (currentDestination.transform.position, transform.position) < accuracyWP) {
      if (currentDestination == towardPlayer) {
        currentDestination = playerArea;
      } else if (currentDestination == playerArea) {
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
			game.GoodGuyGetsRose();
		} else if (!receivedRose) {
			game.VillainGetsRose();
		}

		receivedRose = true;
		currentDestination = mansionDoor;
	}

	public void GetBeer () {
		audioSource.PlayOneShot(sipSound);
		StartCoroutine (BeerReaction());

		if (suitorType == "Drunk") {
			anim.SetFloat ("Drunk", 1f);
		}
	}

	IEnumerator BeerReaction () {
		yield return new WaitForSeconds (0.8f);
		audioSource.PlayOneShot (suitorType == "Drunk" ? burpSound : refreshedSound);
	}

	public void GetSlapped () {
		Destroy(gameObject);
	}

}
