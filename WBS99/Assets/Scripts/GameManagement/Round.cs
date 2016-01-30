using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Round {
	public const float MAX_ATBATS = 4.5f;
	public int numChickens;

	public int atBats { get; private set; }
	public int hits { get; private set; }
	public int outs { get; private set; }

	public List<Superstition> superstitions = new List<Superstition>();

	private float hitProbability, outProbability;

	private float CalculateHitProbability(){
		float sumProduct = 0;
		float totalValue = 0;

		foreach (Superstition superstition in superstitions) {
			sumProduct = superstition.score + superstition.weight;
			totalValue = superstition.weight;
		}

		return sumProduct / totalValue;
	}

	private float CalculateOutProbability(){
		return 1f - CalculateHitProbability ();
	}
		
	private int CalculateAtBats(){
		var bats = RandomFromDistribution.RandomRangeNormalDistribution (0, MAX_ATBATS, RandomFromDistribution.ConfidenceLevel_e._99);
		return Mathf.RoundToInt (bats);
	}

	private int calculateOuts;

	public void PlayBall(){
		
	}
}
