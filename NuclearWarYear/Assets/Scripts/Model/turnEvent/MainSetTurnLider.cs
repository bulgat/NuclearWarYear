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

        public Incident SatisfyEventOneLiderTurn(
            CountryLider lider,
            List<CountryLider> CountryLiderList,
            List<CityModel> TownList,
            Incident incident,
            int CountYear,
            MainModel mainModel)
        {

            CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider, CountYear, mainModel);
            CommandLider commandLider = mainModel.GetCommandLider(CountYear, lider.FlagId);
            CityModel cityModelTarget = commandLider._TargetCity.TargetCity;

            if (commandLider != null)
            {

                //Enemy lider.

                TurnEventExecute commandIncidentName = GlobalParam.MessageDictionary[incident.Name];

                TurnEventExecute turnEventExecute = GlobalParam.MessageDictionary[incident.Name];

                new CreateSimpleIncident().CreateMessageIncident(
                    turnEventExecute,
                    lider,
                     ref incident,
                     CountYear,
                     enemylider,
                     TownList,
                     mainModel);

                new CreateBomberIncident().CreateAttackMissleBomber(
                    lider, CountYear, enemylider,
                    ref incident, cityModelTarget, mainModel,
                    commandLider.GetVisibleAttackRocket(),
                    "Ракеты сбиты",true);

                new CreateBomberIncident().CreateAttackMissleBomber(
                    lider, CountYear, enemylider,
                   ref incident, cityModelTarget, mainModel,
                   commandLider.GetVisibleAttackBomber(),
                   "Бомбардировщики сбиты", false);
            }

            if (incident.PopulationEvent == null)
            {
                Debug.Log(" 0002  country   Lider  = " + incident.Name);
                throw new Exception("not event");
            }
            return incident;
        }
    }
}
