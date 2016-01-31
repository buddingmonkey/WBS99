using UnityEngine;
using System.Collections;

public class Letter : MonoBehaviour {

	public GameController gameController { get; set; }

	// Use this for initialization
	void Awake () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			DestroySelf ();

			// tell game controller
			gameController.GotLetter(gameObject.name[0]);
		}
	}

	public void DestroySelf () {
		Destroy(gameObject);
	}

}


