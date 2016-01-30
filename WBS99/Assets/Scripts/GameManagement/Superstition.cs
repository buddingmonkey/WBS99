using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SuperMetrics{
	pickup,
	totalTime,
	pickupTime
}

[System.Serializable]
public class Superstition {
	[HideInInspector]
	public int value;

	public SuperMetrics metric;

	public int maxValue;

	public float score {
		get {
			return (float)value / (float)maxValue;
		}
		set { }
	}

	public float weight;
	public string trackedObjectTag;
}
