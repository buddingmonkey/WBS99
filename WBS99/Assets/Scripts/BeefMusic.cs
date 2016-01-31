using UnityEngine;
using System.Collections;

public class BeefMusic : MonoBehaviour {
	[SerializeField]
	private AudioClip moo;
	[SerializeField]
	private AudioClip step;
	// Use this for initialization

	[SerializeField]
	private AudioSource audioSource;

	void Update(){
		if (!audioSource.isPlaying ) {
			if (Random.Range (0, 4) == 0) {
				audioSource.clip = moo;
			} else {
				audioSource.clip = step;
			}
			audioSource.Play ();
		}
	}
}
