using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlerts : MonoBehaviour {

  Text text;
  Color positiveAlertColor = new Color(201, 168, 34, 255);
  Color negativeAlertColor = new Color(154, 38, 55, 255);

  void Awake () {
    text = GetComponent<Text>();
  }

  public void displayPositiveAlert(string message) {
    text.color = positiveAlertColor;
    text.text = message;
  }

  public void displayNegativeAlert(string message) {
    text.color = negativeAlertColor;
    text.text = message;
  }


}
