using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelationShip {
   // public int Mood { set; get; }
    private List<CountryLider> _CountryLiderList;
    List<RelationMood> _RelationMood;

    public RelationShip()
    {
        //Mood = 100;
    }
    public void InitRelationContry(List<CountryLider> CountryLiderList)
    {
        this._CountryLiderList = CountryLiderList;
        this._RelationMood = new List<RelationMood>();
        foreach (var lider in CountryLiderList)
        {
            this._RelationMood.Add(new RelationMood(lider.FlagId));
        }
    }
    public int GetMood(int FlafId)
    {
        
        RelationMood relationLider = this._RelationMood.Where(a => a.FlagId == FlafId).FirstOrDefault();
        Debug.Log(relationLider+"   ---- UFO   [  "+ this._RelationMood .Count+ "   ]  FlafId = " + FlafId);
        return relationLider.Mood;
    }
    public void SetNegativeMood(int FlagId,int NegativeMood)
    {
        RelationMood relationLider = this._RelationMood.Where(a => a.FlagId == FlagId).FirstOrDefault();
        relationLider.SetNegativeMood(NegativeMood);
       
    }
}
