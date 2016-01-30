using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;

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

	[SerializeField]
	private SuperDB superDB;

	[SerializeField]
	private ResultsPanel resultsPanel;

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

		currentRound.superstitions.Add (Superstition.CreateChicken (1));
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
		currentRound.PlayBall ();

		smoothFollow.enabled = false;

		playerSpawner.DestroyObject(player);

		if (GameMetrics.Instance.totalHits > GameConstants.winningHits) {
			Debug.Log ("Winner");
		} else if (GameMetrics.Instance.battingAverage < GameConstants.losingAverage && GameMetrics.Instance.battingAverage > .005f) {
			SceneManager.LoadSceneAsync ("GameOver");
		} else {
			resultsPanel.generateRoundResults (currentRound);
		}
	}

	public void GotChicken(string name) {
		currentRound.numChickens++;
		Debug.Log ("Got a Chicken");
	}

	public void HitEnemy() {
		currentRound.numEnemies++;
		Debug.Log ("Hit a Enemy");
	}

	public void GenerateSupersititions(){
		var round = GameMetrics.Instance.lastRound;
		if (round == null) {
			Debug.LogError ("No last round logged! Probably called this function before PlayBall()");
			return;
		}
	}
}
