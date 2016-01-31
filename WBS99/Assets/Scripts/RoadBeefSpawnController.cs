using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadBeefSpawnController : MonoBehaviour {

	public GameObject RoadBeef;
	private GameObject player;
	int poolSize = 5;
	private Stack<Transform> beefPool = new Stack<Transform> ();
	List<Vector3> spawnLocations = new List<Vector3>();

	public GameController gameController;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();

		GameObject[] spawnGOs = GameObject.FindGameObjectsWithTag("BeefSpawnLocation");
		for(int i = 0; i< spawnGOs.Length; i++)
		{
			//Debug.Log (spawnGOs[i].transform.position);
			spawnLocations.Add(spawnGOs[i].transform.position);
		}

		Vector3 locationOfSpawner = FindClosestSpawnLocation();
		for (int i = 0; i < poolSize; i++)
		{
			Spawn ();
		}
	}

	public Transform Spawn(){
		Vector3 locationOfSpawner = FindClosestSpawnLocation();
		if (beefPool.Count < 1) {
			InstantiateObject(locationOfSpawner);
		}

		var t = beefPool.Pop ();
		t.rotation = this.transform.rotation;
		t.position = locationOfSpawner;
		t.gameObject.SetActive (true);

		return t;
	}
	public void DestroyObject(Transform t){
		t.gameObject.SetActive (false);
		beefPool.Push (t);
		//Prime31.ZestKit.CameraShakeTween (Camera.main);
		StartCoroutine(WaitThenCreateNewBeef());
	}

	private Transform InstantiateObject(Vector3 locationOfSpawner){
		Transform t = (Transform)Instantiate (RoadBeef.transform, locationOfSpawner, Quaternion.identity);
		//t.parent = this.transform;
		t.position = locationOfSpawner;
		beefPool.Push (t);
		return t;
	}

	IEnumerator WaitThenCreateNewBeef()
	{
		yield return new WaitForSeconds(2);
		Transform t = Spawn();
		t.gameObject.SetActive(true);
	}


	private Vector3 FindClosestSpawnLocation(){
		float tempDistance;
		float maxDistance = 10000;
		Vector3 closestSpawnerLocation = new Vector3();

		for(int i = 0; i < spawnLocations.Count; i++)
		{
			//Debug.Log ("ASKLDJLAKS");
			tempDistance = Vector3.Distance(player.transform.position, spawnLocations[i]);
			if(tempDistance < maxDistance)
			{
				maxDistance = tempDistance;
				closestSpawnerLocation = spawnLocations[i];
			}
		}
		return closestSpawnerLocation;
	}
}
