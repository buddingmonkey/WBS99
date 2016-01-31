using UnityEngine;
using System.Collections;

public class Baseball : MonoBehaviour {

	public GameController gameController { get; set; }

	// Use this for initialization
	void Awake () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			DestroySelf ();

			// tell game controller
			gameController.GotBaseball(gameObject.name);
		}
	}

	public void DestroySelf () {
		Destroy(gameObject);
	}

}


