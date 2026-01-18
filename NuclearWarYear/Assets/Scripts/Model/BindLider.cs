using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Assets.Scripts.Model
{
    public class BindLider
    {
        public BindLider() { }
        public List<CountryLider> GetBindLider(
            List<CityModel> TownList, 
            int CountYear,
            List<GameObject> countryLiderPropagandaBuildingList) 
        {

            List<CountryLider>  CountryLiderList = new List<CountryLider>();
            CountryLiderList.Add(new CountryLider(false, new List<Incident>()
        { new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber)
        },
            countryLiderPropagandaBuildingList[0], TownList, GlobalParam.ParamLiderList[0], 1));
            CountryLiderList.Add(new CountryLider(false, new List<Incident>()
        {
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber)
        }, countryLiderPropagandaBuildingList[1], TownList, GlobalParam.ParamLiderList[1], 2));
            CountryLiderList.Add(new CountryLider(false, new List<Incident>() {
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber)
        }, countryLiderPropagandaBuildingList[2], TownList, GlobalParam.ParamLiderList[2], 3));


            if (SettingPlayer.TwoPlayerGame)
            {
                CountryLiderList.Add(new CountryLider(true, new List<Incident>() { new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
                new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) }, countryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
            }
            else
            {
                CountryLiderList.Add(new CountryLider(false, new List<Incident>() { 
                    new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
                new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) }, countryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
            }

            CountryLiderList.Add(new CountryLider(true, new List<Incident>() { new DictionaryEssence().BuildIncident(
                GlobalParam.TypeEvent.Missle,CountYear),
                
            new DictionaryEssence().BuildIncident(GlobalParam.TypeEvent.HeavyMissle, CountYear),
            new DictionaryEssence().GetIncident (GlobalParam.TypeEvent.Bomber),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.HeavyBomber),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Defence),
            new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Defence)
                },
                countryLiderPropagandaBuildingList[4], TownList, GlobalParam.ParamLiderList[4], 5));
            return CountryLiderList;
        }
    }
}
