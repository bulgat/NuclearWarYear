using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationMood 
{
    public int Mood;
    public int FlagId;
    public RelationMood(int FlagId) {
        this.FlagId = FlagId;
        this.Mood = 100;
    }
    public void SetNegativeMood(int NegativeMood)
    {
        this.Mood -= NegativeMood;
        if (this.Mood < 0)
        {
            this.Mood = 0;
        }
    }
}
