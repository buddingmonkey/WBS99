using UnityEngine;
using System.Collections;

public class Baseball : MonoBehaviour {

	public GameController gameController { get; set; }

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip hit;

	[SerializeField]
	private AudioClip pickup;

	// Use this for initialization
	void Awake () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			DestroySelf ();

			if (Random.Range (1, 2) == 1) {
				audioSource.PlayOneShot (hit);
			} else {
				audioSource.PlayOneShot	(pickup);
			}

			// tell game controller
			gameController.GotBaseball(gameObject.name);
		}
	}

	public void DestroySelf () {
		this.GetComponent<Renderer> ().enabled = false;
		Destroy(gameObject, pickup.length);
	}

}


