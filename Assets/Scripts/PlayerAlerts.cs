using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAlerts : MonoBehaviour {

	public TextMeshPro text;
	Color32 positiveAlertColor = new Color32(81, 65, 2, 200);
	Color32 negativeAlertColor = new Color32(41, 5, 10, 200);
  
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
    
	public void hideMessage () {
		gameObject.SetActive(false);
	}

}
