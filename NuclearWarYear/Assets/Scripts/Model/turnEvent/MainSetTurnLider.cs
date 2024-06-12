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
            List<CityModel> TownList, Incident CommandIncident)
        {

            string message = "";

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
            CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

            if (lider.GetCommandLider() != null)
            {
                Debug.Log("ChangePopulation = " + GetMessageDictionary(CommandIncident.Name).ChangePopulation + "   _   lider   flag = " + CommandIncident.Name);

                CityModel cityModelTarget = lider.GetCommandLiderFirst()._TargetCity.TargetCity;
                //Enemy lider.


                foreach (var itemExecute in GlobalParam.MessageDictionary)
                {


                    if (CommandIncident.Name == itemExecute.Key)
                    {

                        if (itemExecute.Key == GlobalParam.TypeEvent.Missle)
                        {
                            //lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveMissle();
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }
                        if (itemExecute.Key == GlobalParam.TypeEvent.Bomber)
                        {
                            //lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
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
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            }
                            else
                            {
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                                lider.RemoveDefenceWeapon();
                            }
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            lider.SetCommandRealise(CommandIncident);
                            return CommandIncident;
                        }


                        //int UnDamage = 0;

                        CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider).TargetCity;

                        CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);

                        //if (GetMessageDictionary(itemExecute.Key).ChangePopulation)
                        //{
                        //UnDamage = AddAndRemovePopulation(cityModelTarget, liderCityMy, lider, GetMessageDictionary(itemExecute.Key).Random, TownList);
                        //Debug.Log(itemExecute.Key+"    __ _____ _____ UnDamage = " + UnDamage);
                        //}
                        string report = CommandIncident.GetMessage() + CommandIncident.GetDamage();
                        lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, GetMessageDictionary(itemExecute.Key).NegativeMood);

                        if (GetMessageDictionary(itemExecute.Key).MessageSecond != null)
                        {
                            report += GetMessageDictionary(itemExecute.Key).MessageSecond + lider.GetCommandLiderFirst().LiderFiend.GetName();

                        }
                        if (GetMessageDictionary(itemExecute.Key).Ammunition)
                        {
                            lider.AddMissle(lider.GetCommandLiderFirst().GetMissle());
                            Debug.Log(" --------------- ##  Lider  m  =" + lider.GetCommandLiderFirst()._reportProducedWeaponList);
                            List<string> reportProducedWeaponList = lider.GetCommandLiderFirst()._reportProducedWeaponList;
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
                if (lider.GetCommandLiderFirst().GetVisibleAttackBomber())
                {
                    if (enemylider.GetCommandLiderFirst().GetDefence())
                    {
                        //dead bomber
                        lider.RemoveBomber();
                    }
                    else
                    {
                        //bool explode0;
                        if (lider.GetCommandLiderFirst()._TargetCity != null)
                        {
                            if (lider.GetCommandLiderFirst().GetAttackBomber() != null)
                            {
                                damageAttackCount = lider.GetCommandLiderFirst().GetAttackBomber().GetDamage();

                            }
                        }


                    }

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + damageAttackCount + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(), lider.GetCommandLiderFirst().GetIncident().GetName());
                    lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);

                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                        GetMessageDictionary(lider.GetCommandLiderFirst().GetNameCommandFirst()).ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    return CommandIncident;
                }

                // attack Missle
                if (lider.GetCommandLiderFirst().GetNameExecute(GlobalParam.TypeEvent.AttackMissle))
                {

                    if (enemylider.GetCommandLiderFirst().GetDefence() == false)
                    {
                        if (lider.GetCommandLiderFirst()._TargetCity != null)
                        {

                            if (lider.GetCommandLiderFirst().GetAttackMissle() != null)
                            {
                                damageAttackCount = lider.GetCommandLiderFirst().GetAttackMissle().GetDamage();

                            }
                        }

                    }
                    lider.RemoveMissle();

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + damageAttackCount +
                        " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(),
                        lider.GetCommandLiderFirst().GetIncident().GetName());
                    lider.GetCommandLider().First().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);

                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                        GetMessageDictionary(lider.GetCommandLiderFirst().GetNameCommandFirst()).ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    return CommandIncident;
                }

            }

            Debug.Log(" Mi  message  =" + message);

            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, null),
                GetMessageDictionary(lider.GetCommandLiderFirst().GetNameCommandFirst()).ShowFiend);
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
