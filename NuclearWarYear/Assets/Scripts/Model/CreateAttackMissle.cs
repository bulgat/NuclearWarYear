using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using System.Linq;
using UnityEngine;

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
            CityModel myCity = mainModel.GetAllTownList().Where(a => a.FlagId == countryLider.FiendLider.FlagId).FirstOrDefault();

            CommandLider commandLiderFortune = null;
            if (turnFinally.Missle)
            {


         
                commandLider = new CommandLider(
                    GlobalParam.TypeEvent.AttackMissle,
                    countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                    futureYear,
                    new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
                    countryLider,
                    turnFinally.OldIncident
                    );


            }
            else
            {

                commandLider = new CommandLider(
                    GlobalParam.TypeEvent.AttackBomber,
                    countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                    futureYear,
                    new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
                    countryLider,
                    turnFinally.OldIncident
                    );
                

            }
            
            commandLider.IncidentCommand.SetSecondIncident(turnFinally.OldIncident);
            mainModel.ResetAction();

            commandLiderFortune = new CreateFortune().FortuneEvent(
                   countryLider.FlagId != mainModel.GetCurrentPlayer().FlagId,
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
