using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelationShip {
   // public int Mood { set; get; }
    private List<CountryLider> _CountryLiderList;
    List<RelationMood> RelationMoodList;

    public RelationShip()
    {
        //Mood = 100;
    }
    public void InitRelationContry(List<CountryLider> CountryLiderList)
    {
        this._CountryLiderList = CountryLiderList;
        this.RelationMoodList = new List<RelationMood>();
        foreach (var lider in CountryLiderList)
        {
            this.RelationMoodList.Add(new RelationMood(lider));
        }
    }
    public int GetMood(int FlafId)
    {
        
        RelationMood relationLider = this.RelationMoodList.Where(a => a.Lider.FlagId == FlafId).FirstOrDefault();
        
        return relationLider.Mood;
    }
    public void SetNegativeMood(int FlagId,int NegativeMood)
    {
        RelationMood relationLider = this.RelationMoodList.Where(a => a.Lider.FlagId == FlagId).FirstOrDefault();
        relationLider.SetNegativeMood(NegativeMood);
       
    }
    public void GetHighlyHatredLider()
    {
        List<RelationMood> relationMoodListSort = this.RelationMoodList.OrderBy(a => a.Mood).ToList();
        Debug.Log( "   ---- U    [  " + relationMoodListSort[0].Mood + "   ]  FlafId = " + relationMoodListSort[1].Mood+" = " + relationMoodListSort[2].Mood+" = "+ relationMoodListSort[3].Mood);
    }
}
