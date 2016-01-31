using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsPanel : MonoBehaviour {

	[SerializeField]
	private Text[] labels;

	[SerializeField]
	private Text[] values;

	[SerializeField]
	private Text pressAKey;

	[SerializeField]
	private Image bg;

	private int nItems;
	private IEnumerator blinkEnumerator;
	private bool allowContinue;

	public GameController gameController { get; set; }

	// Use this for initialization
	void Start () {
		ClearResults ();
		pressAKey.enabled = false;
		bg.enabled = false;
	}

	public void Update() {
		if (allowContinue && Input.anyKeyDown) {
			Debug.Log ("any key pressed");
			enabled = false;
			gameController.NextRound ();
		}
	}

	public void AddItem(string label, string value) {
		if (nItems > labels.Length - 1) {
			Debug.Log ("Too Many Labels!");
			return;
		}
		labels [nItems].text = label;
		values [nItems].text = value;
		nItems++;
	}

	public void ShowResults() {
		Debug.Log ("Show results " + nItems);

		bg.enabled = true;
		StartCoroutine (ShowLabels ());
	}

	private void ClearResults() {
		bg.enabled = false;
		nItems = 0;
		foreach (Text t in labels) {
			t.enabled = false;
		}
		foreach (Text t in values) {
			t.enabled = false;
		}
		if (blinkEnumerator != null) {
			StopCoroutine (blinkEnumerator);
			blinkEnumerator = null;
		}
		allowContinue = false;
	}

	public IEnumerator ShowLabels() {
		for (int i = 0; i < nItems; i++) {
			labels [i].enabled = true;
			yield return new WaitForSeconds (0.5f);
			values [i].enabled = true;
			yield return new WaitForSeconds (0.25f);
		}
		yield return new WaitForSeconds (2);
		StartCoroutine (blinkEnumerator = BlinkPressAKey());
		allowContinue = true;
	}

	private IEnumerator BlinkPressAKey() {
		while (true) {
			pressAKey.enabled = true;
			yield return new WaitForSeconds (0.5f);
			pressAKey.enabled = false;
			yield return new WaitForSeconds (0.5f);
		}
	}

	// Generate screens

	public void generateRoundResults(Round round) {
		Debug.Log ("gen results");
		ClearResults ();

		foreach (var s in round.superstitions) {
			AddItem (s.DisplayName (), s.IsMet () ? "Met" : "Missed");
		}
		AddItem ("", "");
		AddItem ("Game Results:", "");
		AddItem ("At Bats", round.atBats.ToString());
		AddItem ("Hits", round.hits.ToString());
		AddItem ("Outs", round.outs.ToString());
		AddItem ("Current Batting Average", string.Format("{0:.000}", GameMetrics.Instance.battingAverage));

		ShowResults ();
	}
}
