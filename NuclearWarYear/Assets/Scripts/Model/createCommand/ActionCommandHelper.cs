using Assets.Scripts.Model;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEngine.ParticleSystem;
public class ActionCommandHelper
{


    public List<CommandLider> CreateAction(
        List<CountryLider> CountryLiderList,
        List<CityModel> TownList,
        int FlagIdPlayer,
        CommandLider commandLider,
        CountryLider countryLider,
        int Year,
        CountryLider fiendLider1,
        CommandLider commandLiderFortune)
    {

        TargetCityModel targetCityModel = countryLider.TargetCitySelectPlayer;
        List<CommandLider> commandLiderList = new List<CommandLider>();

        if (countryLider.FlagId != FlagIdPlayer)
        {
            if (commandLider.GetNameCommand() == GlobalParam.TypeEvent.Defence)
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    commandLider = new CommandLider(GlobalParam.TypeEvent.Propaganda,
                        countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                        Year, targetCityModel, countryLider);
                }
            }

            AiAddTargetCity(targetCityModel, commandLider, fiendLider1);

        }
        else
        {
            if (commandLider._TargetCity == null)
            {
                
                AiAddTargetCity(targetCityModel, commandLider, fiendLider1);
            }
        }
        new SwichFullCommand().TreatmentCommand(
            commandLider.GetNameCommand(),
            commandLider, 
            countryLider.FlagId,
            countryLider.FlagId != FlagIdPlayer,
            TownList,
            CountryLiderList,
            countryLider);
        Debug.Log("0811  - GetDamagePo GetNameFiendLider  futureYe  = " + commandLider);

        Debug.Log("0812  b  CountYear = " + CountryLiderList);
        Debug.Log("0813 A AttackBomber turnFinally.OldIncident = " + targetCityModel.TargetCity + " LiderList = "  );

        commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCityModel.TargetCity.FlagId).FirstOrDefault());

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {

            commandLiderList.Add(commandLiderFortune);
        }

        return commandLiderList;
    }
    private void AiAddTargetCity(TargetCityModel targetCityModel, CommandLider commandLider, CountryLider enemyLider)
    {
        commandLider.SetTargetLider(enemyLider);
    }
    

}
