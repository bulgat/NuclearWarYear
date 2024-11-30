using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelationShip {
    private List<CountryLider> _CountryLiderList;
    List<RelationMood> RelationMoodList;
    int FlagId;

    public RelationShip(int liderFlagId)
    {
        FlagId = liderFlagId;
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
    public void SetNegativeMood(int flagId,int NegativeMood)
    {
        
        RelationMood relationLider = this.RelationMoodList.Where(a => a.Lider.FlagId == flagId).FirstOrDefault();
        relationLider.SetNegativeMood(NegativeMood);
       
    }
    public CountryLider GetHighlyHatredLiderRandom()
    {
        List<RelationMood> relationMoodListSort = this.RelationMoodList.Where(a=>a.Lider.FlagId!= FlagId).OrderBy(a => a.Mood).ToList();
        int curveRandom = CurveRandom();
        
        return relationMoodListSort[curveRandom].Lider;  
    }
    int CurveRandom()
    {
        int[] probabilities = { 20, 50, 100, 200 };
        int number = (int)UnityEngine.Random.Range(0.0f, 200.0f);
        int resultIndex = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (number < probabilities[i])
            {
                resultIndex = i;
                break;
            }
        }
        return 0;
    }
}
