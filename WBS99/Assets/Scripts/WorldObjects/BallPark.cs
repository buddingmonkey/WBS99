using UnityEngine;
using System.Collections;

public class BallPark : MonoBehaviour {

	[SerializeField]
	private GameController gameController;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			gameController.PlayBall ();
		}
	}
}
