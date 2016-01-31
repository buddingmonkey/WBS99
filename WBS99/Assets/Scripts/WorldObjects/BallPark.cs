using UnityEngine;
using System.Collections;

public class BallPark : MonoBehaviour {

	[SerializeField]
	private GameController gameController;

	void Start() {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			gameController.PlayBall ();
		}
	}
}
