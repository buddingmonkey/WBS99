using UnityEngine;
using System.Collections;

public class Beer : MonoBehaviour {

	public GameController gameController { get; set; }

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip gulp;

	[SerializeField]
	private AudioClip bottle;

	// Use this for initialization
	void Awake () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			DestroySelf ();

			if (Random.Range (1, 2) == 1) {
				audioSource.PlayOneShot (gulp);
			} else {
				audioSource.PlayOneShot	(bottle);
			}

			// tell game controller
			gameController.GotBeer(gameObject.name);
		}
	}

	public void DestroySelf () {
		this.GetComponent<Renderer> ().enabled = false;
		Destroy(gameObject, bottle.length);
	}

}


