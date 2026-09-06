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
        // Только живые лидеры могут быть целью (у мёртвых нет городов).
        List<RelationMood> relationMoodListSort = this.RelationMoodList
            .Where(a => a.Lider.FlagId != FlagId && !a.Lider.GetDead())
            .OrderBy(a => a.Mood)
            .ToList();

        if (relationMoodListSort.Count <= 0)
        {
            return null;
        }

        int curveRandom = CurveRandom();
        if (curveRandom >= relationMoodListSort.Count)
        {
            curveRandom = relationMoodListSort.Count - 1;
        }

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
