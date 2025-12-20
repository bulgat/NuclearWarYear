using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentralModel 
{
	private CityModel _targetBomber;
	public BuildingCentralModel(CityModel target)
	{
		if (target == null) {
			throw new Exception("Exception Target bomber not null! ");
		}
		this._targetBomber = target;
	}
	public CityModel GetTargetBomber()
	{
		return this._targetBomber;
	}
}
