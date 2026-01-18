using Assets.Scripts.Model.createCommand;
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
    internal class CreateAttackMissle
    {
        public void SetAttackMisslePlayer(MainModel mainModel, int FlagId, TurnFinally turnFinally)
        {
            int futureYear = mainModel.CountYear + 1;
            CommandLider commandLider = null;
            CountryLider countryLider = new LiderHelperOne().GetLiderOne(mainModel.CountryLiderList, FlagId);
            CityModel enemyTownCity = countryLider.TargetCitySelectPlayer.TargetCity;
            CityModel myCity = mainModel.GetTownList().Where(a => a.FlagId == countryLider.FiendLider.FlagId).FirstOrDefault();

            CommandLider commandLiderFortune = null;
            if (turnFinally.Missle)
            {
                Debug.Log("_0811  - GetDamagePo GetNameFiendLider  futureYear = " + futureYear);

                Debug.Log("_0809 BAMB!  ** ti   - " + enemyTownCity.Name);

                commandLider = new CommandLider(
                    GlobalParam.TypeEvent.AttackMissle,
                    countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                    futureYear,
                    new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
                    FlagId,
                    turnFinally.OldIncident
                    );

                //mainModel.ResetAction();
                /*
                                commandLiderFortune = new CreateFortune().FortuneEvent(
                                       countryLider.FlagId != mainModel.GetCurrentPlayerFlag(),
                                       countryLider,
                                       futureYear);
                                Debug.Log("_0810  b L = " + mainModel.MainStackCommandLiderList?.Count);

                                mainModel.MainStackCommandLiderList.AddRange(
                                    new ActionCommandHelper().CreateAction(
                                        mainModel.CountryLiderList,
                                        mainModel.TownList,
                                        mainModel.GetCurrenPlayer().FlagId,
                                        commandLider,
                                        mainModel.GetCurrenPlayer(),
                                        futureYear,
                                        countryLider.FiendLider,
                                        commandLiderFortune)
                                    );
                                */

                Debug.Log("_0812  bui = " + commandLider.IncidentCommand.Name + " CountYear = " + mainModel.CountYear);

            }
            else
            {
                Debug.Log("0814 Add AttackBomber  com  LiderList = " + mainModel.GetCommandLiderList(mainModel.CountYear + 1, FlagId).Count);
                //CountryLider countryLider = new LiderHelperOne().GetLiderOne(mainModel.CountryLiderList, FlagId);
                //CityModel enemyTownCity = countryLider.TargetCitySelectPlayer.TargetCity;

                //CityModel myCity = mainModel.GetTownList().Where(a => a.FlagId == countryLider.FiendLider.FlagId).FirstOrDefault();

                commandLider = new CommandLider(
                    GlobalParam.TypeEvent.AttackBomber,
                    countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                    futureYear,
                    new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
                    FlagId,
                    turnFinally.OldIncident
                    );
                commandLider.IncidentCommand.SetSecondIncident(turnFinally.OldIncident);

            }
            mainModel.ResetAction();

            commandLiderFortune = new CreateFortune().FortuneEvent(
                   countryLider.FlagId != mainModel.GetCurrentPlayerFlag(),
                   countryLider,
                   futureYear);

            mainModel.MainStackCommandLiderList.AddRange(new ActionCommandHelper().CreateAction(
                mainModel.CountryLiderList,
                mainModel.TownList,
                mainModel.GetCurrenPlayer().FlagId,
                commandLider,
                mainModel.GetCurrenPlayer(),
                futureYear,
                countryLider.FiendLider,
                commandLiderFortune));
            commandLider.IncidentCommand.SetSecondIncident(turnFinally.OldIncident);
        }
    }
}
