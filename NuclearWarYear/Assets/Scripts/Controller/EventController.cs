using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController 
{
	public Controller.Command NameCommand {set;get;}
	public int FlagId {set;get;}
	public int CityId {set;get;}
	public EventController() {
		
	}
	public EventController(Controller.Command nameCommand,int FlagId) {
		this.NameCommand = nameCommand;
		this.FlagId = FlagId;
	}

}

