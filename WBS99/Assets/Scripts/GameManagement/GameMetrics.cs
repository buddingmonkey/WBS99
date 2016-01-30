using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMetrics {

#region singleton crap
	private static readonly GameMetrics instance = new GameMetrics();

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static GameMetrics()
	{
	}

	private GameMetrics()
	{
	}

	public static GameMetrics Instance
	{
		get
		{
			return instance;
		}
	}
#endregion

	public int totalHits;
	public int totalAtBats;

	public float battingAverage {
		get {
			if (totalAtBats < 1) {
				return 0;
			}
			return (float)totalHits / (float)totalAtBats;
		}
		set { }
	}

	public Dictionary<string, int> collectedItemTotals = new Dictionary<string, int>();

	public List<Superstition> superstitions = new List<Superstition>();

	public Round lastRound;

	public string level;

	public int cityIndex = 0;
	public int gamesPlayedInCity = 0;
}
