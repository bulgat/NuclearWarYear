using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGameEngine
{
    public CityModel GetCityRandomFlagId(List<CityModel> TownList, CountryLider Lider,
        int FlagIdOwnerAI,
        bool AI)
    {
        Debug.Log("____ mo  = ");
        Debug.Log(" @@@@  -- Lider =  " + Lider.FlagId);
        Lider._RelationShip.GetHighlyHatredLider();

        List<CityModel> TargetCityList = new List<CityModel>();
        foreach (CityModel townCity in TownList)
        {

            if (AI)
            {
                if (townCity.FlagId != FlagIdOwnerAI)
                {
                    if (townCity.GetPopulation() > 0)
                    {
                        TargetCityList.Add(townCity);
                    }
                }
            }
            else
            {
                //select lider attack


                if (townCity.FlagId == Lider.FlagIdAttack)
                {
                    if (townCity.GetPopulation() > 0)
                    {
                        TargetCityList.Add(townCity);

                    }

                }
            }
        }
        int indexTownBomber = Random.Range(0, TargetCityList.Count);
        CityModel target = TargetCityList[indexTownBomber];

        return target;
    }
}
