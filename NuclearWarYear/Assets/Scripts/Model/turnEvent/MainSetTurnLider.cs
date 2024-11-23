using Assets.Scripts.Model.param;
using Assets.Scripts.Model.turnEvent;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Progress;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts.Model
{
    public class MainSetTurnLider
    {

        public Incident SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList,
            List<CityModel> TownList, Incident CommandIncident,int CountYear)
        {
            string message = "";

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
            CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider,CountYear);
CityModel cityModelTarget = lider.GetCommandLiderOne(CountYear)._TargetCity.TargetCity;

            if (lider.GetStackCommandLider(CountYear) != null)
            {
                Debug.Log(lider.Name +"ChangePopulation = " + GetMessageDictionary(CommandIncident.Name).ChangePopulation + "   _   lider   flag = " + CommandIncident.Name);

                
                //Enemy lider.

                TurnEventExecute commandIncidentName = GlobalParam.MessageDictionary[CommandIncident.Name];

                Debug.Log(" res = " + commandIncidentName);

                //foreach (var itemExecute in GlobalParam.MessageDictionary)
                //{
                    TurnEventExecute turnEventExecute = GlobalParam.MessageDictionary[CommandIncident.Name];

                message = new CreateSimpleIncident().CreateMessageIncident(turnEventExecute, lider,
                       ref CommandIncident,  CountYear,  enemylider, TownList);
                //}

                message = new CreateBomberIncident().CreateAttackBomber(lider, CountYear, enemylider,
            ref CommandIncident, cityModelTarget);

                message = new CreateMissleIncident().CreateAttackMissle(lider, CountYear,enemylider,
            ref CommandIncident, cityModelTarget);

                

            }

            Debug.Log(" Mi  message  =" + message);

            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), cityModelTarget, enemylider),
                GetMessageDictionary(lider.GetCommandLiderOne(CountYear).GetNameCommandFirst()).ShowFiend);
            lider.SetCommandRealise(CommandIncident);

            
            
            if(CommandIncident.PopulationEvent==null)
            {
                Debug.Log(" 0002  countryLid FiendLider  = ");
                throw new Exception("error MainSetTurnLider");
            }


            return CommandIncident;
        }

        
        
        


        private TurnEventExecute GetMessageDictionary(GlobalParam.TypeEvent key)
        {

            return GlobalParam.MessageDictionary[key];

        }
    }
}
