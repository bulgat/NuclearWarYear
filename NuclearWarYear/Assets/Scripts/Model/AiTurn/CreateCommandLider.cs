using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.AiTurn
{
    public class CreateCommandLider
    {
        public List<CommandLider> CommandOneLider(CountryLider lider, Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear, MainModel mainModel)
        {
            GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

            CityModel myCity = lider.GetFirstCityHelper();



            if (lider.ReleaseCommandList != null)
            {

                actionNameCommand = new ChangeIncident().ChangeIncidentCommand(lider, actionNameCommand, CountYear, mainModel);
            }

            if (actionNameCommand == GlobalParam.TypeEvent.None)
            {
                if (lider.FlagId != FlagIdPlayer)
                {
                    actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
                }
                else
                {
                    actionNameCommand = GlobalParam.TypeEvent.Propaganda;
                }
            }

            CountryLider fiendLider1 = lider._RelationFeind.GetHighlyHatredLiderRandom();

            var randTarget = new TargetHelper()
                .GetTargetRandom(
                CountryLiderList,
                fiendLider1,
                true,
                new TargetHelper().GetRandomCity(
                    TownList,
                    lider, 
                    fiendLider1.FlagId,
                    true));

            TargetCityModel targetCityModel
                = new TargetCityModel(randTarget, myCity, fiendLider1);

            if (lider.FlagId != _flagIdPlayer)
            {

                lider.SetTargetCity(targetCityModel);
            }

            // Счастливая карта!
            CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                lider.FlagId != FlagIdPlayer, lider, CountYear);



            CommandLider commandLider = new CommandLider(actionNameCommand, fiendLider1, CountYear, targetCityModel, lider.FlagId);
            ResetAction();
            List<CommandLider> commandLidersList = new SwitchActionHelper().SwitchAction(CountryLiderList,
                TownList, FlagIdPlayer,
                commandLider, lider,
                CountYear, fiendLider1,
                 commandLiderFortune);

            return commandLidersList;
        }
        
    }
}
