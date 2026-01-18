using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.createCommand
{
    internal class SwichFullCommand
    {
        public void TreatmentCommand(GlobalParam.TypeEvent actionCommand, CommandLider commandLider,
        int FlagId, bool AIfiend, List<CityModel> TownList,
           List<CountryLider> CountryLiderList, CountryLider countryLider)
        {

            switch (actionCommand)
            {
                case GlobalParam.TypeEvent.Propaganda:
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);


                    var target = countryLider.TargetCitySelectPlayer;


                    countryLider.TargetCitySelectPlayer.TargetCity = new ModGameEngine().GetCityRandomFlagId(TownList, countryLider.FiendLider, FlagId, AIfiend);
                    break;
                case GlobalParam.TypeEvent.Build:
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Build, true);
                    BuildWeapon buildWeapon = new BuildWeapon();
                    commandLider.AddMissle(buildWeapon.AddLiderBuildWeaponSwithAction());
                    commandLider.AddReportProducedWeaponList(buildWeapon.GetReportProducedWeaponList());
                    break;
                case GlobalParam.TypeEvent.Defence:
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Defence, true);
                    break;
                case GlobalParam.TypeEvent.Missle:
                    commandLider.SetVisibleMissle(true);
                    break;
                case GlobalParam.TypeEvent.Bomber:
                    commandLider.SetVisibleBomber(true);
                    break;
                case GlobalParam.TypeEvent.AttackBomber:
                    if (countryLider.TargetCitySelectPlayer == null)
                    {
                        commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                    }
                    else
                    {
                        commandLider.SetVisibleAttackBomber(true);
                        commandLider.SetTargetLider(countryLider.TargetCitySelectPlayer.EnemyLider);
                        commandLider.SetAttackBomber(countryLider.GetBomber());
                    }
                    break;
                case GlobalParam.TypeEvent.AttackMissle:
                    if (countryLider.TargetCitySelectPlayer == null)
                    {
                        commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                    }
                    else
                    {
                        commandLider.SetVisibleEventList(GlobalParam.TypeEvent.AttackMissle, true);
                        commandLider.SetTargetLider(countryLider.TargetCitySelectPlayer.EnemyLider);
                        commandLider.SetAttackMissle(countryLider.GetMissleFirst());
                    }
                    break;
                default:
                    //print ("Incorrect intelligence level.");
                    break;
            }
        }
    }
}
