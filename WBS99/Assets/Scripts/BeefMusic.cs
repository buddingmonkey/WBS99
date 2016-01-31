using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

public class BeefMusic : MonoBehaviour {
	[SerializeField]
	private AudioClip moo;
	[SerializeField]
	private AudioClip step;

	[SerializeField]
	private Transform cowModel;
	// Use this for initialization

	[SerializeField]
	private AudioSource audioSource;

	void Start(){
		BounceBounce ();
	}

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

	void BounceBounce (){
		cowModel.ZKlocalPositionTo (new Vector3 (0, Random.Range (.4f, 2f), 0), Random.Range (.25f, .5f))
			.setLoops (LoopType.PingPong, 1)
			.setCompletionHandler ((ITween<Vector3> obj) => {
				BounceBounce ();
			})
			.setEaseType (EaseType.QuadOut)
			.start ();
	}
}
