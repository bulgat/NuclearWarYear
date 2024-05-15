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
		//Dictionary<string, TurnEventExecute> MessageDictionary;

		public Incident SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList,
			List<CityModel> TownList, Incident CommandIncident)
		{
			/*
            MessageDictionary = new Dictionary<string, TurnEventExecute>();
			MessageDictionary.Add(DictionaryEssence.TypeEvent.RocketRich.ToString(), new TurnEventExecute(null,0,false,true,false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Baby.ToString(), new TurnEventExecute( null, 0,false, true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Ufo.ToString(), new TurnEventExecute( null, 0, false,true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Defectors.ToString(), new TurnEventExecute( null, 5,false, true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Building.ToString(), new TurnEventExecute(null, 5, true, false, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Propaganda.ToString(), new TurnEventExecute(
				" сбежав от ", 5, false, true,true,false, false));
            MessageDictionary.Add("AttackBomber", new TurnEventExecute(null, 0, false, true, false,false, false));
            MessageDictionary.Add("AttackMissle", new TurnEventExecute( null, 0,false, true, false,false, false));
            MessageDictionary.Add("Defence", new TurnEventExecute( null, 0, false, true, false,true, false));
            MessageDictionary.Add("Airport", new TurnEventExecute( null, 0, false, true, false,false, true));
            MessageDictionary.Add("Missle", new TurnEventExecute( null, 0, false, true, false, false, true));
            MessageDictionary.Add("Bomber", new TurnEventExecute( null, 0, false, true, false, false, true));
			*/
			string message="";

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
            
            if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLiderFirst().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				foreach (var item in GlobalParam.MessageDictionary)
				{
					
					
                    if (CommandIncident.Name ==item.Key)
					{

                        if (item.Key=="Missle")
						{
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveMissle();
							//CommandIncident.SetReleaseMessage(message, 0, null,null,false);
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message,0,null));
                            return CommandIncident;
                        }
                        if (item.Key == "Bomber")
                        {
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveBomber();
                            //CommandIncident.SetReleaseMessage(message, 0, null, null, false);
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null));
                            return CommandIncident;
                        }

                        if (GlobalParam.MessageDictionary[item.Key].RemoveDefenceWeapon || GlobalParam.MessageDictionary[item.Key].Airport)
						{
							if (GlobalParam.MessageDictionary[item.Key].Airport)
							{
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            }
							else
							{
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
								lider.RemoveDefenceWeapon();
							}
                            //CommandIncident.SetReleaseMessage(message,0,null, null, false);
                            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null));
                            return CommandIncident;
                        }
						else
						{
                            
                            int UnDamage = 0;
							// null;
							CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider);
							//bool attackFiend=false;
							CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);
                            // CityModel liderCityMy = GetTargetCityEvent(bool RandomAndUnRevert, cityFiend, cityModelTarget);
                            if (GlobalParam.MessageDictionary[item.Key].ChangePopulation)
							{
								//attackFiend = true;
                                //liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);
                                UnDamage = AddAndRemovePopulation(cityModelTarget, liderCityMy, lider, GlobalParam.MessageDictionary[item.Key].Random, TownList);
							}
							string report = CommandIncident.GetMessage() + UnDamage;
							lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, GlobalParam.MessageDictionary[item.Key].NegativeMood);

							if (GlobalParam.MessageDictionary[item.Key].MessageSecond != null)
							{
								report += GlobalParam.MessageDictionary[item.Key].MessageSecond + lider.GetCommandLiderFirst().LiderFiend.GetName();

							}
							if (GlobalParam.MessageDictionary[item.Key].Ammunition)
							{
								lider.AddMissle(lider.GetCommandLiderFirst().GetMissle());
								List<string> reportProducedWeaponList = lider.GetCommandLiderFirst().GetReportProducedWeaponList();
								report = string.Join(", ", reportProducedWeaponList.ToArray());
							}
							
                            message = lider.SetEventTotalMessageTurn(report, item.Key);
							lider.SetCommandRealise(lider.GetCommandLiderFirst());
							if (DictionaryEssence.TypeEvent.Propaganda.ToString() == item.Key || DictionaryEssence.TypeEvent.Defectors.ToString() ==item.Key) {
                                CommandIncident.SetReleaseMessage(new StateDragPopulation(message, UnDamage, liderCityMy, cityFiend));
                                return CommandIncident;
                            }
							bool doubleCity = item.Key == DictionaryEssence.TypeEvent.Propaganda.ToString() || item.Key == DictionaryEssence.TypeEvent.Defectors.ToString();
                            //CommandIncident.SetReleaseMessage(message, UnDamage, liderCityMy, cityFiend, doubleCity);
                            
                            CommandIncident.SetReleaseMessage(new StateAddPopulation(message, -UnDamage, liderCityMy));
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

                                //new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), 
								//	damageAttackCount, true);
							}
						}


					}

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + damageAttackCount + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(), lider.GetCommandLiderFirst().GetIncident().GetName());
					lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);

                    //CommandIncident.SetReleaseMessage(message, damageAttackCount, null, cityModelTarget, false);
                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget));
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

                                //new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider),
                               // damageAttackCount, true);
							
							}
						}

					}
					lider.RemoveMissle();

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + damageAttackCount + 
						" у " + lider.GetCommandLiderFirst().LiderFiend.GetName(),
                        lider.GetCommandLiderFirst().GetIncident().GetName());
					lider.GetCommandLider().First().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);

                    //CommandIncident.SetReleaseMessage(message, damageAttackCount, null, cityModelTarget, false);
                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget));
                    return CommandIncident;
                }

			}

            //BuildingCentral buildingCentralLider = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
            Debug.Log(" Mi  message  =" + message);
            //CommandIncident.SetReleaseMessage(message, 0, null, null, false);
            CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, 0, null));
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
		/*
		public void AddPopulationCity(int UnDamage, CityModel cityFiend)
		{
			//int population = liderCityMy.GetPopulation() + UnDamage;
			//liderCityMy.SetFuturePopulation(population);
			//liderCityMy.SetPresentlyPopulation();

			// remove population Fiend.
			new DamagePopulationHelper().SetDamagePopulation(cityFiend, UnDamage, false);
		}
		*/
	}
}
