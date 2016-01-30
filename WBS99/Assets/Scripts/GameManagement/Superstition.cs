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
	public enum Type {
		Chicken,
		Beer,
		Baseballs
	}
	public Type type;

	[HideInInspector]
	public int value;

	public SuperMetrics metric;

	[HideInInspector]
	public int maxValue;

	public float score {
		get {
			return (float)value / (float)maxValue;
		}
		set { }
	}

	public float weight;
	public string trackedObjectTag;
	
	public void UpdateFromRound(Round round) {
		switch (type) {
		case Type.Chicken:
			value = round.numChickens;
			break;

		case Type.Beer:
			value = round.numBeers;
			break;

		case Type.Baseballs:
			return;

		default:
			return;
		}
	}

	public bool IsMet() {

		// TODO improve this as needed!

		if (value >= maxValue) {
			return true;
		}

		return false;
	}

	public string DisplayName() {
		switch (type) {
		case Type.Chicken:
			return string.Format ("Eat {0} Chickens", maxValue);
		case Type.Beer:
			return string.Format ("Drink {0} Beers", maxValue);
		case Type.Baseballs:
			return string.Format ("Field {0} Balls", maxValue);
		default:
			return "Unknown Superstition";
		}
	}

	private static Superstition Create(Type type, int maxValue) {
		var s = new Superstition ();
		s.type = type;
		s.maxValue = maxValue;
		s.value = 0;
		return s;
	}

	public static Superstition CreateChicken(int numRequired) {
		var s = Create (Type.Chicken, numRequired);

		// TODO set these correctly
		s.score = 0.5f;
		s.weight = 0.5f;
		return s;
	}
}
