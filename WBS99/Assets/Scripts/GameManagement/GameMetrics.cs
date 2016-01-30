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

	public int hits;
	public int atBats;

	public float battingAverage {
		get {
			return (float)hits / (float)atBats;
		}
		set { }
	}

	public Dictionary<string, int> collectedItemTotals = new Dictionary<string, int>();

	public string level;
}
