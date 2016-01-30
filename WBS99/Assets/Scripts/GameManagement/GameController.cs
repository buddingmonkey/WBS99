using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public enum GameState{
	Flying,
	Baseball,
	City,
	Pausied,
	GameOver
}

public class GameController : MonoBehaviour {

	[SerializeField]
	private Spawner playerSpawner;

	private Transform player;

	[SerializeField]
	private List<Spawner> enemySpawners;

	[SerializeField]
	private List<Spawner> collectibleSpawners;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private Camera mainCamera;

	public GameState gameState { get; private set; }

	private Round currentRound;

	// Use this for initialization
	void Start () {
		NextRound ();
	}

	void NextRound(){
		gameState = GameState.Flying;
		currentRound = new Round ();

		/* Example code for adding a supersitition
		var s = new Superstition ();
		s.maxValue = 2;
		s.value = 1;
		s.score = .5f;
		s.weight = .5f;

		currentRound.superstitions.Add (s);
		*/
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Flying) {
			gameState = GameState.City;
			SpawnPlayer ();
		}
	}

	private void SpawnPlayer(){
		player = playerSpawner.Spawn ();
		player.GetComponent<PlayerController> ().cameraTransform = mainCamera.transform;
		smoothFollow.enabled = true;
		smoothFollow.target = player;
	}

	public void PlayBall(){
		// Do some baseball shit in here
		Debug.Log("BASEBALL!");

		smoothFollow.enabled = false;

		playerSpawner.DestroyObject(player);

		gameState = GameState.Flying;
	}

	public void GotChicken() {
		Debug.Log ("Got a Chicken");
	}

	public void HitEnemy() {
		Debug.Log ("Hit a Enemy");
	}
}
