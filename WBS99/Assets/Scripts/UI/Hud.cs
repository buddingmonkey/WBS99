using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public GameController gameController { get; set; }

	[SerializeField]
	private Text uiNumChickens;
	[SerializeField]
	private Text uiNumBeers;
	[SerializeField]
	private Text uiNumBalls;
	[SerializeField]
	private Text uiNumBeef;

	// Use this for initialization
	void Start () {
		if (gameController == null) {
			gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Round round = gameController.GetCurrentRound();
		if (round.numChickens.ToString() != uiNumChickens.text) {
			uiNumChickens.text = round.numChickens.ToString ();
		}
		if (round.numBeers.ToString() != uiNumBeers.text) {
			uiNumBeers.text = round.numBeers.ToString ();
		}
		if (round.numBalls.ToString() != uiNumBalls.text) {
			uiNumBalls.text = round.numBalls.ToString ();
		}
		if (round.numCows.ToString() != uiNumBeef.text) {
			uiNumBeef.text = round.numCows.ToString ();
		}
	}
}
