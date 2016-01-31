using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ResultsPanel : MonoBehaviour {

	[SerializeField]
	private Text[] titles;

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

	private Action callback;

	// Use this for initialization
	void Awake () {
		ClearResults ();
		pressAKey.enabled = false;
		bg.enabled = false;
	}

	public void Update() {
		if (allowContinue && Input.anyKeyDown) {
			Debug.Log ("any key pressed");
			ClearResults ();
			callback ();
		}
	}

	public void AddTitle(string title) {
		if (nItems > labels.Length - 1) {
			Debug.Log ("Too Many Labels!");
			return;
		}
		titles [nItems].text = title;
		labels [nItems].text = "";
		values [nItems].text = "";
		nItems++;
	}

	public void AddItem(string label, string value) {
		if (nItems > labels.Length - 1) {
			Debug.Log ("Too Many Labels!");
			return;
		}
		titles [nItems].text = "";
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
		foreach (Text t in titles) {
			t.enabled = false;
		}
		foreach (Text t in labels) {
			t.enabled = false;
		}
		foreach (Text t in values) {
			t.enabled = false;
		}
		if (blinkEnumerator != null) {
			StopCoroutine (blinkEnumerator);
			blinkEnumerator = null;
			pressAKey.enabled = false;
		}
		allowContinue = false;
	}

	public IEnumerator ShowLabels() {
		for (int i = 0; i < nItems; i++) {
			if (titles [i].text.Length > 0) {
				titles [i].enabled = true;
				yield return new WaitForSeconds (0.75f);
			}
			if (labels [i].text.Length > 0) {
				labels [i].enabled = true;
				yield return new WaitForSeconds (0.5f);
			}
			if (values [i].text.Length > 0) {
				values [i].enabled = true;
				yield return new WaitForSeconds (0.25f);
			}
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

	public void generateRoundResults(Round round, Action callback) {
		this.callback = callback;
		ClearResults ();

		foreach (var s in round.superstitions) {
			AddItem (s.DisplayName (), s.IsMet () ? "Met" : "Missed");
		}
		AddTitle ("");
		AddTitle ("Game Results:");
		AddItem ("At Bats", round.atBats.ToString());
		AddItem ("Hits", round.hits.ToString());
		AddItem ("Outs", round.outs.ToString());
		AddItem ("Current Batting Average", string.Format("{0:.000}", GameMetrics.Instance.battingAverage));

		ShowResults ();
	}

	public void generateSuperstitionsList(Action callback) {
		this.callback = callback;
		ClearResults ();
		AddTitle ("Pre-game superstitions for " + GameMetrics.Instance.cityName);
		AddTitle ("");
		foreach (var s in GameMetrics.Instance.GetSuperstitions()) {
			AddTitle (s.DisplayName());
		}
		ShowResults ();
	}
}
