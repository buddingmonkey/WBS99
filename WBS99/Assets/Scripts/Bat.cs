using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {

	void OnTriggerEnter(Collider col){
	Debug.Log(col.gameObject.tag);
	if(col.gameObject.tag == "Enemy")
		{
	Debug.Log("AKLDJHAL");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
