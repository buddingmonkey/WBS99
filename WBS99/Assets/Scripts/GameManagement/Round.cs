using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Round {
	public const float MAX_ATBATS = 9;
	public int numChickens;

	public int atBats { get; private set; }
	public int hits { get; private set; }
	public int outs { get; private set; }

	public List<Superstition> superstitions = new List<Superstition>();

	private double hitProbability, outProbability;

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
		return Mathf.RoundToInt (bats);
	}

	//=ROUND(NORMINV(0.99*E11,B2/2,B2/6),0)
	private int CalculateHits(){
		if (atBats == 0) {
			return 0;
		}
		return Mathf.RoundToInt((float) NormInv.NormsInv(.99 * hitProbability, (double)atBats/2.0, (double) atBats/ 6.0));
	}

	public void PlayBall(){
		hitProbability = CalculateHitProbability ();
		outProbability = 1 - hitProbability;
		atBats = CalculateAtBats ();
		hits = CalculateHits ();
		outs = atBats - hits;
	}
}
