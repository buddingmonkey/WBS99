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
}
