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
    public class MainTurnLider
    {

		public Incident SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList,
			List<CityModel> TownList, Incident CommandIncident)
		{

			string message="";

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
            
            if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLiderFirst().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				foreach (var itemExecute in GlobalParam.MessageDictionary)
				{
					
					
                    if (CommandIncident.Name == itemExecute.Key)
					{

                        if (itemExecute.Key=="Missle")
						{
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveMissle();
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message,0,null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            
							return CommandIncident;
                        }
                        if (itemExecute.Key == "Bomber")
                        {
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveBomber();
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            return CommandIncident;
                        }

                        if (GetMessageDictionary(itemExecute.Key).RemoveDefenceWeapon || GetMessageDictionary(itemExecute.Key).Airport)
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
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            return CommandIncident;
                        }
						else
						{
                            
                            int UnDamage = 0;

							CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider);

							CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);

                            if (GetMessageDictionary(itemExecute.Key).ChangePopulation)
							{
                                UnDamage = AddAndRemovePopulation(cityModelTarget, liderCityMy, lider, GetMessageDictionary(itemExecute.Key).Random, TownList);
							}
							string report = CommandIncident.GetMessage() + UnDamage;
							lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, GetMessageDictionary(itemExecute.Key).NegativeMood);

							if (GetMessageDictionary(itemExecute.Key).MessageSecond != null)
							{
								report += GetMessageDictionary(itemExecute.Key).MessageSecond + lider.GetCommandLiderFirst().LiderFiend.GetName();

							}
							if (GetMessageDictionary(itemExecute.Key).Ammunition)
							{
								lider.AddMissle(lider.GetCommandLiderFirst().GetMissle());
								List<string> reportProducedWeaponList = lider.GetCommandLiderFirst().GetReportProducedWeaponList();
								report = string.Join(", ", reportProducedWeaponList.ToArray());
							}
							
                            message = lider.SetEventTotalMessageTurn(report, itemExecute.Key);
							lider.SetCommandRealise(lider.GetCommandLiderFirst());
							if (DictionaryEssence.TypeEvent.Propaganda.ToString() == itemExecute.Key || DictionaryEssence.TypeEvent.Defectors.ToString() ==itemExecute.Key) {
                                CommandIncident.SetReleaseMessage(new StateDragPopulation(message, UnDamage, liderCityMy, cityFiend, enemylider), GetMessageDictionary(itemExecute.Key).ShowFiend);
                                return CommandIncident;
                            }
							bool doubleCity = itemExecute.Key == DictionaryEssence.TypeEvent.Propaganda.ToString() || itemExecute.Key == DictionaryEssence.TypeEvent.Defectors.ToString();

                            CommandIncident.SetReleaseMessage(new StateAddPopulation(message, -UnDamage, liderCityMy), GetMessageDictionary(itemExecute.Key).ShowFiend);
                            return CommandIncident;
						}
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
						if (lider.GetCommandLiderFirst().GetTargetCity() != null)
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
                    return CommandIncident;
                }

                // attack Missle
                if (lider.GetCommandLiderFirst().GetNameExecute(DictionaryEssence.TypeEvent.AttackMissle.ToString()))
				{

					if (enemylider.GetCommandLiderFirst().GetDefence()==false)
					{
						if (lider.GetCommandLiderFirst().GetTargetCity() != null)
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
                    return CommandIncident;
                }

			}

            Debug.Log(" Mi  message  =" + message);

            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null, null),
                GetMessageDictionary(lider.GetCommandLiderFirst().GetNameCommandFirst()).ShowFiend);
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
		private TurnEventExecute GetMessageDictionary(string key)
		{
			return GlobalParam.MessageDictionary[key];

        }
	}
}
