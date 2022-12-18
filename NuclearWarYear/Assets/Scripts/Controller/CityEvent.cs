using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvent :EventController
{
	
	public CityEvent(Controller.Command nameCommand,int cityId) {
		this.NameCommand = nameCommand;
		this.CityId = cityId;
	}
}
