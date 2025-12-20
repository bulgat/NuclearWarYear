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

                actionNameCommand = ChangeIncidentCommand(lider, actionNameCommand, CountYear, mainModel);
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

        private GlobalParam.TypeEvent ChangeIncidentCommand(CountryLider lider, GlobalParam.TypeEvent actionNameCommand, int countYear, MainModel mainModel)
        {
            //auto command player
            var type = lider.ReleaseCommandList?.FirstOrDefault();



            if (type == null)
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;

            }
            else
            {
                actionNameCommand = type.GetName();

                var lastYeatCommandList0 = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear).ToList();
                var lastYeatCommandList = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear - 1).ToList();

                foreach (var item in lastYeatCommandList)
                {

                    if (item.Type == GlobalParam.TypeEvent.Missle)
                    {
                        item.SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);

                        return GlobalParam.TypeEvent.AttackMissle;
                    }
                    if (item.Type == GlobalParam.TypeEvent.Bomber)
                    {
                        item.SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);

                        return GlobalParam.TypeEvent.AttackBomber;
                    }

                }

            }
            return actionNameCommand;
        }
    }
}
