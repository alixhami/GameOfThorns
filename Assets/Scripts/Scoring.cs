using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

	int villainCount;
	int goodGuyCount;
	public Text villainCountDisplay;
	public Text goodGuyCountDisplay;

	void Start () {
		UpdateScores();
	}

	public void ChangeVillainCount (int num) {
		villainCount += num;
		UpdateScores ();
	}

	public void ChangeGoodGuyCount (int num) {
		goodGuyCount += num;
		UpdateScores();
	}

	void UpdateScores () {
		goodGuyCountDisplay.text = goodGuyCount.ToString();
		villainCountDisplay.text = villainCount.ToString();
	}

	public void ResetScores () {
		villainCount = 0;
		goodGuyCount = 0;
		UpdateScores ();
	}
}

