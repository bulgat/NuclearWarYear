using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController 
{
	public Controller.Command NameCommand {set;get;}
	//public int FlagId {set;get;}
	//public int CityId {set;get;}
	public IEventSend EventSend { set; get; }

	public EventController(Controller.Command nameCommand,IEventSend eventSend) {
		this.NameCommand = nameCommand;
		//this.FlagId = eventSendLider.FlagId;
		this.EventSend = eventSend;
	}

}

