using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentralModel 
{
	private CityModel _targetBomber;
	public void SetTargetBomber(CityModel target)
	{
		this._targetBomber = target;
	}
	public CityModel GetTargetBomber()
	{
		return this._targetBomber;
	}
}
