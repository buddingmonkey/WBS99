using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Round {
	public const float MAX_ATBATS = 9;

	// Buncha hard coded stuff...
	public int numChickens = 0;
	public float lastChicken = 0;
	public int numEnemies = 0;
	public float lastEnemy = 0;
	public int numBeers = 0;
	// LAST BEER DEFAULTS TO INFINITY BECAUSE THERE NEVER IS A LAST BEER
	public float lastBeer = Mathf.Infinity;
	public int numCows = 0;
	public float lastCow = 0;
	public int numBalls = 0;
	public float lastBall = 0;
	public string letters = "";
	public float lastLetter = 0;

	public List<char> lettersCollected = new List<char> ();

	public float timeToStadium;

	public int atBats { get; private set; }
	public int hits { get; private set; }
	public int outs { get; private set; }

	public List<Superstition> superstitions = new List<Superstition>();

	public double hitProbability { get; private set; } 
	public double outProbability { get; private set; }

	public Round() {
		foreach (var s in GameMetrics.Instance.GetSuperstitions()) {
			superstitions.Add (new Superstition (s));
		}
	}

	private double CalculateHitProbability(){
		float sumProduct = 0;
		float totalValue = 0;

		foreach (Superstition superstition in superstitions) {
			sumProduct += superstition.score * superstition.weight;
			totalValue += superstition.weight;
		}

		return sumProduct / totalValue;
	}

	private double CalculateOutProbability(){
		return 1.0 - CalculateHitProbability ();
	}
		
	private int CalculateAtBats(){
		var bats = RandomFromDistribution.RandomRangeNormalDistribution (0, MAX_ATBATS, RandomFromDistribution.ConfidenceLevel_e._80);
		return Mathf.Max(1, Mathf.RoundToInt (bats));
	}

	//=ROUND(NORMINV(0.99*E11,B2/2,B2/6),0)
	private int CalculateHits(){
		if (hitProbability <= 0) {
			hitProbability = 0.0001f;
		} else if (hitProbability >= 1) {
			hitProbability = 1 - 0.0001f;
		}

		if (atBats == 0) {
			return 0;
		}
		return Mathf.RoundToInt((float) NormInv.NormsInv(.99 * hitProbability, (double)atBats/2.0, (double) atBats/ 6.0));
	}

	private void UpdateSuperstitions() {
		foreach (var s in superstitions) {
			s.UpdateFromRound (this);
		}
	}

	public void PlayBall(){
		UpdateSuperstitions ();

		hitProbability = CalculateHitProbability ();
		outProbability = 1 - hitProbability;
		atBats = CalculateAtBats ();

		// HAX
		hits = Mathf.Max(1, CalculateHits ());

		outs = atBats - hits;

		GameMetrics.Instance.totalHits += hits;
		GameMetrics.Instance.totalAtBats += atBats;
		GameMetrics.Instance.lastRound = this;
	}
}
