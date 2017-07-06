using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {

  Animator anim;

  void Awake () {
    anim = GetComponent<Animator> ();
  }

  void Update() {
    Move();
    // react
  }

  void Move() {
    anim.SetFloat("Drunk", 1f);
  }
}
