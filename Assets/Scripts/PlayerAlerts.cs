using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAlerts : MonoBehaviour {

	public TextMeshPro text;
	Color32 positiveAlertColor = new Color32(159, 30, 11, 139);
	Color32 negativeAlertColor = new Color32(68, 3, 13, 203);
  
	float messageLifetime = 4f;

	void Awake () {
		gameObject.SetActive(false);
	}

	public void displayPositiveAlert(string message) {
		text.color = positiveAlertColor;
		text.text = message;
		displayTimedMessage();
	}

	public void displayNegativeAlert(string message) {
		text.color = negativeAlertColor;
		text.text = message;
		displayTimedMessage();
	}

	void displayTimedMessage () {
    CancelInvoke();
		gameObject.SetActive(true);
		Invoke("hideMessage", messageLifetime);
	}

	// TODO messages are turning each other off when they appear too close together
	public void hideMessage () {
		gameObject.SetActive(false);
	}

}
