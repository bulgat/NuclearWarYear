using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetModel 
{
	private CityModel _targetBomber;
	public TargetModel(CityModel target)
	{
		//if (target == null) {
		//	throw new Exception("Exception Target bomber not null! ");
		//}
		this._targetBomber = target;
	}
	public CityModel GetTarget()
	{
		return this._targetBomber;
	}
}
