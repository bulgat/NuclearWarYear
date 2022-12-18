using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentralModel 
{
	private CityModel _targetBomber;
	public void SetTargetBomber(CityModel target)
	{
		// Vector3
		this._targetBomber = target;

	}
	public CityModel GetTargetBomber()
	{
		// Vector3
		return this._targetBomber;

	}
}
