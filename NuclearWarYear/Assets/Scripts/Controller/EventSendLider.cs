using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSendLider : IEventSend
{
    public int Id { set; get; }
    public int FlagId { set; get; }
    public int CityId { set; get; }
    public EventSendLider(int FlagId)
    {
        this.FlagId = FlagId;
    }
    
}
