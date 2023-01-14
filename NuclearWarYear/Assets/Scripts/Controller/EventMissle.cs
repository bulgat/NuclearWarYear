using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMissle : IEventSend
{
    public int Id { set; get; }
    public int FlagId { set; get; }
    public int CityId { set; get; }
    public EventMissle(int FlagId,int MissleId)
    {
        this.FlagId = FlagId;
        this.Id = MissleId;
    }
}
