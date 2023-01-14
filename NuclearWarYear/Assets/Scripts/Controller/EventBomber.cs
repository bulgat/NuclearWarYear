using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBomber : IEventSend
{
    public int Id { set; get; }
    public int FlagId { set; get; }
    public int CityId { set; get; }
    public EventBomber(int FlagId, int BomberId)
    {
        this.FlagId = FlagId;
        this.Id = BomberId;
    }
}
