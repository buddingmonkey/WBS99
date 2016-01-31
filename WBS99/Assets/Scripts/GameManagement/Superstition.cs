﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SuperMetrics{
	pickup,
	time
}

[System.Serializable]
public class Superstition {
	public enum Type {
		Chicken,
		Beer,
		Baseballs,
		Cows,
		Letters,
		Time
	}
	public Type type;

	[HideInInspector]
	public int value;

	[HideInInspector]
	public int checkValue;

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
	public string letters;

	public Superstition() {
	}

	public Superstition(Superstition s) {
		this.type = s.type;
		this.metric = s.metric;
		this.letters = s.letters;
		this.weight = s.weight;
	}
	
	public void UpdateFromRound(Round round) {
		switch (type) {
		case Type.Chicken:
			if (metric == SuperMetrics.pickup) {
				value = round.numChickens % 10 == checkValue ? 1 : 0;
			}
			break;

		case Type.Beer:
			if (metric == SuperMetrics.pickup) {
				value = round.numBeers % 10 == checkValue ? 1 : 0;
			}
			break;

		case Type.Baseballs:
			if (metric == SuperMetrics.pickup) {
				value = round.numBalls % 10 == checkValue ? 1 : 0;
			}
			break;

		case Type.Cows:
			if (metric == SuperMetrics.pickup) {
				value = round.numCows % 10 == checkValue ? 1 : 0;
			}
			break;

		default:
			value = 0;
			maxValue = 1;
			break;
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
			if (maxValue == 1) {
				return string.Format ("Eat a Chicken", maxValue);
			} else {
				return string.Format ("Eat {0} Chickens", maxValue);
			}
		case Type.Beer:
			return string.Format ("Drink {0} Beers", maxValue);
		case Type.Baseballs:
			return string.Format ("Field {0} Balls", maxValue);
		case Type.Cows:
			return string.Format ("Hit {0} Road Beef", maxValue);
		case Type.Letters:
			return string.Format ("Spell {0}", letters);
		default:
			return "Unknown Superstition";
		}
	}

	public string NonSpecificDisplayName() {
		switch (type) {
		case Type.Chicken:
			if (maxValue == 1) {
				return string.Format ("Eat a Chicken");
			} else {
				return string.Format ("Eat ?? Chickens");
			}
		case Type.Beer:
			return string.Format ("Drink ?? Beers");
		case Type.Baseballs:
			return string.Format ("Field ?? Balls");
		case Type.Cows:
			return string.Format ("Hit ?? Road Beef");
		case Type.Letters:
			return string.Format ("Spell ????");
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
