﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	void Start () {
		var exp = GetComponent<ParticleSystem> ();
		exp.Play ();
		Destroy (gameObject, exp.duration);
	}

}
