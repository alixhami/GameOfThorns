using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour {

	public int villainCount;
	public int goodGuyCount;
	public TextMeshPro villainCountDisplay;
	public TextMeshPro goodGuyCountDisplay;

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

  public void Show () {
    gameObject.SetActive (true);
  }

  public void Hide () {
    gameObject.SetActive(false);
  }

	public void ResetScores () {
		villainCount = 0;
		goodGuyCount = 0;
		UpdateScores ();
	}
}

