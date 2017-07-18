using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

	int positivePoints;
	int negativePoints;
	public Text positivePointDisplay;
	public Text negativePointDisplay;

  void Start () {
    UpdateScores();
  }

	public void AddPoints (int points) {
		if (points < 0) {
			negativePoints += Mathf.Abs (points);
		} else {
			positivePoints += points;
		}

		UpdateScores ();
	}

	void UpdateScores () {
		positivePointDisplay.text = positivePoints.ToString();
		negativePointDisplay.text = negativePoints.ToString();
	}
}
