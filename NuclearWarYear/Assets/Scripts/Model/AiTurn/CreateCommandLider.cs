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
        public List<CommandLider> CommandOneLider(
            CountryLider lider,
            Action ResetAction,
            List<CountryLider> CountryLiderList,
            List<CityModel> TownList,
            int _flagIdPlayer,
            int FlagIdPlayer,
            int CountYear,
            MainModel mainModel)
        {
            GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

            CityModel myCity = lider.GetFirstCityHelper();

            IncidentAttack incidentAttack = null;

            if (lider.ReleaseCommandList != null)
            {
                var list = mainModel.GetCommandLiderList(mainModel.CountYear - 1, lider.FlagId);

                Debug.Log("0600  _Create L " + list.First().GetNameCommand());
                Debug.Log("0601  _Create L " + list.Count);
                Debug.Log("0602  Create L " + lider.ReleaseCommandList.Count);
                Debug.Log("0603  Create name = " + lider.ReleaseCommandList.First().Name);
                Debug.Log("0604  CreateCommand " + actionNameCommand );
                incidentAttack = new ChangeIncident().MutationIncidentCommand(
                    lider,
                    actionNameCommand,
                    CountYear,
                    mainModel);

                if (incidentAttack != null)
                {
                    actionNameCommand = incidentAttack.TypeEvent;
                }
                Debug.Log("0605  CreateCommand " + actionNameCommand + "  SEC = " + incidentAttack?.SecondIncident);
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
            CityModel targetTownCity = new TargetHelper().GetRandomCity(
                    TownList,
                    lider,
                    fiendLider1.FlagId,
                    true);

            new TargetHelper()
                .SetTargetBuilding(
                CountryLiderList,
                fiendLider1,
                true,
                myCity,
                targetTownCity
                );

            TargetCityModel targetCityModel
                = new TargetCityModel(targetTownCity, myCity, fiendLider1);

            if (lider.FlagId != _flagIdPlayer)
            {

                lider.SetTargetCity(targetCityModel);
            }

            // Счастливая карта!
            CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                lider.FlagId != FlagIdPlayer, lider, CountYear);

            Debug.Log("0606  CreateCommand "+ actionNameCommand + "  SEC = "+ incidentAttack?.SecondIncident);

            CommandLider commandLider = new CommandLider(
                actionNameCommand,
                fiendLider1,
                CountYear,
                targetCityModel,
                lider.FlagId,
                incidentAttack?.SecondIncident
                );
            ResetAction();

            List<CommandLider> commandLidersList = new ActionCommandHelper().CreateAction(
                CountryLiderList,
                TownList,
                FlagIdPlayer,
                commandLider,
                lider,
                CountYear,
                fiendLider1,
                 commandLiderFortune);

            return commandLidersList;
        }
        
    }
}
