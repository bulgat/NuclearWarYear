using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationMood 
{
    public int Mood;
    public CountryLider Lider;
    public RelationMood(CountryLider lider) {
        this.Lider = lider;
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
