using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetSuitor : MonoBehaviour {

  string[] names = {
    "Brian F","Brian K","Brian J","Brian P","Chris M","Chris P","Chris H","Chris L",
    "Ryan B","Ryan N","Nick L","Nick Z","Jonathan","Michael","Ben M","Ben T","Ben C",
    "John","Josh","Eric","Mike","James","Kyle"
  };

  string[] normalJobs = {
    "Personal Trainer","Attorney","Pharmaceutical Sales Rep","Financial Analyst", 
    "Firefighter", "Accountant", "Marketing Consultant", "Singer/ Songwriter", "ER Physician",
    "Former Pro Athlete","Veteran"
  };

  string[] weirdJobs = {
    "Tickle Monster", "Chicken Enthusiast", "Whaboom", "Twin", "Jumbotron Operator",
    "Manscaper", "Amateur Sex Coach", "Pizza Entrepreneur", "Aspiring Drummer", "Canadian",
    "Panstapreneur", "Hipster", "Free Spirit", "Dog Lover", "Aspiring Dolphin Trainer"
  };

  bool isJoke;
  string suitorName;
  int age;
  string job;

  bool hasRose = false;

  public Animator anim;
  AudioSource audioSource;
  public AudioClip painSound;
  public AudioClip laughSound;
  public AudioClip celebrationSound;
  public GroinHit groin;
  public ItemMagnet chest;
  public TextMeshPro cardLine1;
  public TextMeshPro cardLine2;

  public event System.Action<bool> CountRose = delegate { };

  void Awake () {
    audioSource = GetComponent<AudioSource> ();
    anim = GetComponent<Animator> ();
    anim.SetFloat ("IdleOffset", Random.value);
  }

	void Start () {
    groin.TriggerGroinHit += HitInGroin;
    chest.GotRose += GetRose;
	}

  void GetRose () {
    hasRose = true;
    CountRose (isJoke);

    if (isJoke) {
      audioSource.PlayOneShot (laughSound);
    } else {
      audioSource.PlayOneShot (celebrationSound);
    }
    anim.SetTrigger ("GetRose");
  }

  void HitInGroin () {
    audioSource.PlayOneShot (painSound);
    anim.SetTrigger ("GroinHit");
  }

  public void Reset () {
    hasRose = false;
    Randomize ();
    chest.ResetRose ();
  }

  void Randomize () {
    suitorName = names [Random.Range (0, names.Length)];
    age = Random.Range (24, 37);

    isJoke = (Random.value < 0.5);
    anim.SetBool ("isJoke", isJoke);

    if (isJoke) {
      job = weirdJobs [Random.Range (0, weirdJobs.Length)];
    } else {
      job = normalJobs [Random.Range (0, normalJobs.Length)];
    }

    cardLine1.text = string.Concat (suitorName, ", ", age);
    cardLine2.text = job;
  }
	
}
