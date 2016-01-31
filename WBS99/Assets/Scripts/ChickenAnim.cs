using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

public class ChickenAnim : MonoBehaviour {
	[SerializeField]
	private Transform leftWing;
	[SerializeField]
	private Transform rightWing;
	[SerializeField]
	private Transform leftEye;
	[SerializeField]
	private Transform rightEye;

	// Use this for initialization
	void Start () {
		FlapWings (null);
		CrayEye (leftEye);
		CrayEye (rightEye);
		BounceBounce ();
	}

	void FlapWings(ITween<UnityEngine.Vector3> tween){
		//ZestKit.instance.stopAllTweensWithTarget (rightWing);

		var flapTime = Random.Range (0f, .5f);
		rightWing.ZKlocalEulersTo (new Vector3 (3, -125, 69), flapTime)
			.setFrom (new Vector3(270, 0, 0))
			.setLoops(LoopType.PingPong, 1)
			.setEaseType(EaseType.QuartInOut)
			.start ();
		
		leftWing.ZKlocalEulersTo (new Vector3 (-13, -33, -134), flapTime)
			.setLoops(LoopType.PingPong, 1)
			.setCompletionHandler(FlapWings)
			.setEaseType(EaseType.QuartInOut)
			.start ();
	}

	void CrayEye(Transform t){
		t.ZKlocalEulersTo (new Vector3 (Random.Range(-40f,40f), Random.Range(-40f,40f), Random.Range(-40f, 40f)), Random.Range(0f, .5f))
			.setLoops(LoopType.PingPong, 1)
			.setIsRelative()
			.setCompletionHandler( (ITween<Vector3> obj) => {
				CrayEye(t);
			})
			.setEaseType(EaseType.QuartInOut)
			.start ();
	}

	void BounceBounce (){
		transform.ZKlocalPositionTo (new Vector3 (0, Random.Range (.5f, 2f), 0), Random.Range (.25f, .5f))
			.setLoops (LoopType.PingPong, 1)
			.setCompletionHandler ((ITween<Vector3> obj) => {
				BounceBounce ();
			})
			.setEaseType (EaseType.QuadOut)
			.start ();
	}
}
