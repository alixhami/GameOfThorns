using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : MonoBehaviour {

    public string gameName;
    public event System.Action<string> TriggerGame = delegate { };

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "GameController") {
            TriggerGame(gameName);
        }
    }

}
