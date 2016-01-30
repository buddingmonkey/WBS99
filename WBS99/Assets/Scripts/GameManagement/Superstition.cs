using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Superstition {
	public int value;
	public int maxValue;
	public float score {
		get {
			return (float)value / (float)maxValue;
		}
		set { }
	}
	public float weight;
}
