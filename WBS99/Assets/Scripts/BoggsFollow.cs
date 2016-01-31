using UnityEngine;
using System.Collections;

public class BoggsFollow : MonoBehaviour {
	public Transform target {
		get {
			return _target;
		}
		set {
			_target = value;
		}
	}
	private Transform _target;

	private Vector3 offset = new Vector3 (-30, 42.5f, -30) * 0.65f;

	void Start(){
		
	}

	// Update is called once per frame
	void LateUpdate () {
		if (target == null) {
			return;
		}
		var currentOffset = transform.position - target.position;

		currentOffset.z = 0;

		this.transform.position = target.TransformPoint(offset);
	}
}
