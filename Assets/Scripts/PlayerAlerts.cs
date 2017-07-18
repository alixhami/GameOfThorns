using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlerts : MonoBehaviour {

  Text text;
  Color positiveAlertColor = new Color(201, 168, 34, 255);
  Color negativeAlertColor = new Color(154, 38, 55, 255);

  float messageLifetime = 4f;

  void Awake () {
    gameObject.SetActive(false);
    text = GetComponent<Text>();
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
    gameObject.SetActive(true);
    Invoke("hideMessage", messageLifetime);
  }

  void hideMessage () {
    gameObject.SetActive(false);
  }

}
