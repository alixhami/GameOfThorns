using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

	int positivePoints;
	int negativePoints;
	public Text positivePointDisplay;
	public Text negativePointDisplay;

	void Update () {
		positivePointDisplay.text = positivePoints.ToString();
		negativePointDisplay.text = negativePoints.ToString();
	}
}
