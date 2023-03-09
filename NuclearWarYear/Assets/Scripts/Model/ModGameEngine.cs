using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGameEngine
{
    public CityModel GetCityRandomFlagId(List<CityModel> TownList, CountryLider Lider,
        int FlagIdOwnerAI,
        bool AI)
    {

        CountryLider countryLiderVictim = Lider._RelationShip.GetHighlyHatredLiderRandom();
        List<CityModel> TargetCityListVictim= countryLiderVictim.GetOwnTownListLiderFilterPopulation();

        List<CityModel> TargetCityList = new List<CityModel>();
        if (AI)
        {

            TargetCityList = TargetCityListVictim;


        } else
        {

            foreach (CityModel townCity in TownList)
            {
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
