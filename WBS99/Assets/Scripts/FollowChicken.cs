using UnityEngine;
using System.Collections;

public class FollowChicken : MonoBehaviour {
	public Transform target;
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = target.position;
	}
}
