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
            Debug.Log("8109  - FIEND Command  tDamagePo GetNameFiendLider  futu    = " + commandLider.GetNameCommand());
            AiAddTargetCity(commandLider, fiendLider1);

            


            new SwichFullCommand().TreatmentCommand(
                commandLider,
                countryLider,
                true,
                TownList,
                CountryLiderList,
                countryLider);

            
            if (commandLider.GetNameCommand() == GlobalParam.TypeEvent.Missle)
            {
                
                if (countryLider.GetMissleCount() > 0)
                {
                    Debug.Log("8110  A   miss = " + countryLider.Name);
                    Debug.Log("8111   Year = " + countryLider.GetMissleFirst().GetName());
                    Debug.Log("8112     AttackBomb  = " + string.Join(",", countryLider.WeaponList.Select(a => a.Name)) + " L = " + countryLider.GetMissleCount());

                    commandLider.SetNameCommand(countryLider.GetMissleFirst().GetName());
                    Debug.Log("8113  A Bomb  remove SECOND = " + commandLider.GetNameCommand());
                }
                else
                {
                    commandLider.SetNameCommand(new RandomActionCommand().GetRandomNeutralCommand());
                }
            }
            if (commandLider.GetNameCommand() == GlobalParam.TypeEvent.Bomber)
            {
                if (countryLider.GetBomberCount() > 0)
                {
                    commandLider.SetNameCommand(countryLider.GetBomberFirst().GetName());
                } else
                {
                    commandLider.SetNameCommand(new RandomActionCommand().GetRandomNeutralCommand());
                }
            }
            if (commandLider.GetNameCommand() == GlobalParam.TypeEvent.Defence)
            {
                if (countryLider.GetDefenceWeapon().Count() > 0)
                {
                    commandLider.SetNameCommand(countryLider.GetDefenceFirst().GetName());
                }
                else
                {
                    commandLider.SetNameCommand(new RandomActionCommand().GetRandomNeutralCommand());
                }
            }
        }
        else
        {
            if (commandLider._TargetCity == null)
            {
                
                AiAddTargetCity(commandLider, fiendLider1);
            }
            new SwichFullCommand().TreatmentCommand(
                commandLider, 
                countryLider,
                false,
                TownList,
                CountryLiderList,
                countryLider);
        }



        

        if (targetCityModel?.TargetCity != null)
        {
            commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCityModel.TargetCity.FlagId).FirstOrDefault());
        }
        else
        {
            // У лидера-цели не осталось живых городов — атаковать некого,
            // превращаем ход в безобидную пропаганду, чтобы не зависнуть.
            commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
        }

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {

            commandLiderList.Add(commandLiderFortune);
        }

        return commandLiderList;
    }
    private void AiAddTargetCity(CommandLider commandLider, CountryLider enemyLider)
    {
        commandLider.SetTargetLider(enemyLider);
    }
    

}
