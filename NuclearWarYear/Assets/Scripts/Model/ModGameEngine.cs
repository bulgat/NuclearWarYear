using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGameEngine
{
    public CityModel GetCityRandomFlagId(List<CityModel> TownList, CountryLider Lider,
        int FlagIdOwnerAI,
        bool AI)
    {
        
        CityModel target = null;
        if (Lider != null)
        {
            CountryLider countryLiderVictim = Lider._RelationFeind.GetHighlyHatredLiderRandom();
            List<CityModel> TargetCityListVictim = countryLiderVictim.GetOwnTownListLiderFilterPopulation();

            List<CityModel> TargetCityList = new List<CityModel>();
            if (AI)
            {

                TargetCityList = TargetCityListVictim;


            }
            else
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
            if (TargetCityList.Count > 0)
            {
                Debug.Log("0701  SecondIncident = " + TargetCityList.Count);
                int indexTownBomber = Random.Range(0, TargetCityList.Count);
                Debug.Log("0702  Crea L " + TargetCityList.Count);
                Debug.Log("0703  Crea  name = " + indexTownBomber);
                target = TargetCityList[indexTownBomber];
            }
        }
        return target;
    }
}
