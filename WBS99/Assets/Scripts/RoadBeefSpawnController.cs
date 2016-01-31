using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadBeefSpawnController : MonoBehaviour {

	public GameObject RoadBeef;
	private Stack<Transform> beefPool = new Stack<Transform> ();
	public GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
		InstantiateObject ();
		InstantiateObject ();
		InstantiateObject ();
		InstantiateObject ();
		InstantiateObject ();
	}

//	public Transform Spawn(){
//		if (beefPool.Count < 1) {
//			InstantiateObject();
//		}
//
//		var t = beefPool.Pop ();
//		t.rotation = this.transform.rotation;
//		t.position = this.transform.position;
//
//		return t;
//	}
	public void DestroyObject(Transform t){
		t.gameObject.SetActive (false);
		beefPool.Pop ();
		//StartCoroutine(WaitThenCreateNewEnemy());
	}

	private Transform InstantiateObject(){
		Transform t = Instantiate (RoadBeef.transform);
		t.parent = this.transform;
		t.position = transform.position;
		beefPool.Push (t);
		return t;
	}

//		IEnumerator WaitThenCreateNewEnemy()
//		{
//			yield return new WaitForSeconds(2);
//			//Vector3 locationOfSpawner = FindClosestSpawnLocation();
//			Transform t = Spawn();
//			t.gameObject.SetActive(true);
//		}
}
