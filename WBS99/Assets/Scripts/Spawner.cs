using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	[SerializeField]
	private Transform objectPrefab;

	[SerializeField]
	private int initialPoolSize = 1;

	private Stack<Transform> objectPool = new Stack<Transform> ();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < initialPoolSize; i++) {
			InstantiateObject ();
		}
	}
	
	public Transform Spawn(){
		if (objectPool.Count < 1) {
			InstantiateObject();
		}

		var t = objectPool.Pop ();
		t.position = this.transform.position;
		t.rotation = this.transform.rotation;

		return t;
	}

	public void DestroyObject(Transform t){
		t.gameObject.SetActive (false);
		objectPool.Push (t);
	}

	private Transform InstantiateObject(){
		Transform t = Instantiate (objectPrefab);
		t.parent = this.transform;
		objectPool.Push (t);
		return t;
	}
}
