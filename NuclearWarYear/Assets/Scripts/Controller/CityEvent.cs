﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEvent :IEventSend
{
	public int Id { set; get; }
	public int FlagId { set; get; }
	public int CityId { set; get; }

	public CityEvent(int cityId) {
		this.CityId = cityId;
	}
}
