using Assets.Scripts.Model.param;
using Assets.Scripts.Model.turnEvent;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
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

            if (lider.GetStackCommandLider(CountYear) != null)
            {
                Debug.Log(lider.Name +"ChangePopulation = " + GetMessageDictionary(CommandIncident.Name).ChangePopulation + "   _   lider   flag = " + CommandIncident.Name);

                CityModel cityModelTarget = lider.GetCommandLiderFirst(CountYear)._TargetCity.TargetCity;
                //Enemy lider.


                foreach (var itemExecute in GlobalParam.MessageDictionary)
                {


                    if (CommandIncident.Name == itemExecute.Key)
                    {

                        if (itemExecute.Key == GlobalParam.TypeEvent.Missle)
                        {
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                            lider.RemoveMissle();
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }
                        if (itemExecute.Key == GlobalParam.TypeEvent.Bomber)
                        {
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                            lider.RemoveBomber();
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }

                        if (GetMessageDictionary(itemExecute.Key).RemoveDefenceWeapon ||
                            GetMessageDictionary(itemExecute.Key).Airport)
                        {
                            if (GetMessageDictionary(itemExecute.Key).Airport)
                            {
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                            }
                            else
                            {
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                                lider.RemoveDefenceWeapon();
                            }
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }


                        //int UnDamage = 0;

                        CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider,CountYear).TargetCity;

                        CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);

                        //if (GetMessageDictionary(itemExecute.Key).ChangePopulation)
                        //{
                        //UnDamage = AddAndRemovePopulation(cityModelTarget, liderCityMy, lider, GetMessageDictionary(itemExecute.Key).Random, TownList);
                        //Debug.Log(itemExecute.Key+"    __ _____ _____ UnDamage = " + UnDamage);
                        //}
                        string report = CommandIncident.GetMessage() + CommandIncident.GetDamage();
                        lider.GetCommandLiderFirst(CountYear).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, GetMessageDictionary(itemExecute.Key).NegativeMood);

                        if (GetMessageDictionary(itemExecute.Key).MessageSecond != null)
                        {
                            report += GetMessageDictionary(itemExecute.Key).MessageSecond + lider.GetCommandLiderFirst(CountYear).LiderFiend.Name;

                        }
                        if (GetMessageDictionary(itemExecute.Key).Ammunition)
                        {
                            lider.AddMissle(lider.GetCommandLiderFirst(CountYear).GetMissle());
                            Debug.Log(" --------------- ##  Lider  m  =" + lider.GetCommandLiderFirst(CountYear)._reportProducedWeaponList);
                            List<string> reportProducedWeaponList = lider.GetCommandLiderFirst(CountYear)._reportProducedWeaponList;
                            report = string.Join(", ", reportProducedWeaponList.ToArray());
                        }

                        message = lider.SetEventTotalMessageTurn(report, itemExecute.Key);
                        //lider.SetCommandRealise(lider.GetCommandLiderFirst());
                        if (GlobalParam.TypeEvent.Propaganda == itemExecute.Key ||
                        GlobalParam.TypeEvent.Defectors == itemExecute.Key)
                        {

                            CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(), liderCityMy, cityFiend, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }
                        bool doubleCity = itemExecute.Key == GlobalParam.TypeEvent.Propaganda || itemExecute.Key == GlobalParam.TypeEvent.Defectors;
                        
                        CommandIncident.SetReleaseMessage(new StateAddPopulation(message, -CommandIncident.GetDamage(), liderCityMy, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                        lider.SetCommandRealise(CommandIncident);
                        return CommandIncident;

                    }
                }

                int damageAttackCount = 0;
                // attack bomber
                if (lider.GetCommandLiderFirst(CountYear).GetVisibleAttackBomber())
                {
                    if (enemylider.GetCommandLiderFirst(CountYear).GetDefence())
                    {
                        //dead bomber
                        lider.RemoveBomber();
                    }
                    else
                    {
                        //bool explode0;
                        if (lider.GetCommandLiderFirst(CountYear)._TargetCity != null)
                        {
                            if (lider.GetCommandLiderFirst(CountYear).GetAttackBomber() != null)
                            {
                                damageAttackCount = lider.GetCommandLiderFirst(CountYear).GetAttackBomber().GetDamage();

                            }
                        }


                    }

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage() + damageAttackCount + " у " + lider.GetCommandLiderFirst(CountYear).LiderFiend.Name, lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                    lider.GetCommandLiderFirst(CountYear).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                        GetMessageDictionary(lider.GetCommandLiderFirst(CountYear).GetNameCommandFirst()).ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    return CommandIncident;
                }

                // attack Missle
                if (lider.GetCommandLiderFirst(CountYear).GetNameExecute(GlobalParam.TypeEvent.AttackMissle))
                {

                    if (enemylider.GetCommandLiderFirst(CountYear).GetDefence() == false)
                    {
                        if (lider.GetCommandLiderFirst(CountYear)._TargetCity != null)
                        {

                            if (lider.GetCommandLiderFirst(CountYear).GetAttackMissle() != null)
                            {
                                damageAttackCount = lider.GetCommandLiderFirst(CountYear).GetAttackMissle().GetDamage();

                            }
                        }

                    }
                    lider.RemoveMissle();

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst(CountYear).GetIncident().GetMessage() + damageAttackCount +
                        " у " + lider.GetCommandLiderFirst(CountYear).LiderFiend.Name,
                        lider.GetCommandLiderFirst(CountYear).GetIncident().GetName());
                    lider.GetStackCommandLider(CountYear).First().LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                        GetMessageDictionary(lider.GetCommandLiderFirst(CountYear).GetNameCommandFirst()).ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    return CommandIncident;
                }

            }

            Debug.Log(" Mi  message  =" + message);

            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, null),
                GetMessageDictionary(lider.GetCommandLiderFirst(CountYear).GetNameCommandFirst()).ShowFiend);
            lider.SetCommandRealise(CommandIncident);
            return CommandIncident;
        }

        public int GetRandomDamageCity(bool RandomAndUnRevert, CityModel cityModelTarget)
        {
            int UnDamage = UnityEngine.Random.Range(1, 9);
            UnDamage = RandomAndUnRevert ? UnDamage : 9;

            if (cityModelTarget != null)
            {
                if (UnDamage > cityModelTarget.GetPopulation())
                {
                    UnDamage = cityModelTarget.GetPopulation();
                }
            }
            return UnDamage;
        }
        private int AddAndRemovePopulation(CityModel cityModelTarget, CityModel cityModelLider, CountryLider lider,
            bool RandomAndUnRevert, List<CityModel> TownList)
        {

            int UnDamage = GetRandomDamageCity(RandomAndUnRevert, cityModelTarget);


            return UnDamage;
        }
        public CityModel GetTargetCityEvent(bool RandomAndUnRevert, CityModel cityFiend, CityModel cityModelLider)
        {
            if (RandomAndUnRevert)
            {
                return cityFiend;

            }
            return cityModelLider;
        }
        private TurnEventExecute GetMessageDictionary(GlobalParam.TypeEvent key)
        {

            return GlobalParam.MessageDictionary[key];

        }
    }
}
