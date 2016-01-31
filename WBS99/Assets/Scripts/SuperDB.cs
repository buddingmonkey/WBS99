using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CityInfo{
	public string cityName;
	public List<Superstition> super;
}

public class SuperDB : ScriptableObject {
	public List<CityInfo> cities = new List<CityInfo>();
	public List<Superstition> superstitions = new List<Superstition>();
}
