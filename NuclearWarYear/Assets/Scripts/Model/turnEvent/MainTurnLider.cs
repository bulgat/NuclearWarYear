using Assets.Scripts.Model.turnEvent;
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
		Dictionary<string, TurnEventExecute> MessageDictionary;

		public string SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList, List<CityModel> TownList, Incident CommandIncident)
		{
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

			string message="";

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
            Debug.Log(CommandIncident.Name+"    = &&&&    We " + CommandIncident .Id+ " = ____________________________________"+ lider.GetCommandLider().Count);
            if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLiderFirst().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				foreach (var item in MessageDictionary)
				{
					
					
                    if (CommandIncident.Name ==item.Key)
					{

                        if (item.Key=="Missle")
						{
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveMissle();
							return message;
                        }
                        if (item.Key == "Bomber")
                        {
                            lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                            message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            lider.RemoveBomber();
                            return message;
                        }

                        if (MessageDictionary[item.Key].RemoveDefenceWeapon || MessageDictionary[item.Key].Airport)
						{
							if (MessageDictionary[item.Key].Airport)
							{
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
                            }
							else
							{
                                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage(), lider.GetCommandLiderFirst().GetIncident().GetName());
								lider.RemoveDefenceWeapon();
							}
                            return message;
                        }
						else
						{
                            Debug.Log("T _  > " + item.Key+ "   ChangePopulation = " + MessageDictionary[item.Key].ChangePopulation);

                            int UnDamage = 0;// AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
							if (MessageDictionary[item.Key].ChangePopulation)
							{
								UnDamage = AddAndRemovePopulation(cityModelTarget, lider, MessageDictionary[item.Key].Random, TownList);
							}
							string report = CommandIncident.GetMessage() + UnDamage;
							lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, MessageDictionary[item.Key].NegativeMood);

							if (MessageDictionary[item.Key].MessageSecond != null)
							{
								report += MessageDictionary[item.Key].MessageSecond + lider.GetCommandLiderFirst().LiderFiend.GetName();

							}
							if (MessageDictionary[item.Key].Ammunition)
							{
								lider.AddMissle(lider.GetCommandLiderFirst().GetMissle());
								List<string> reportProducedWeaponList = lider.GetCommandLiderFirst().GetReportProducedWeaponList();
								report = string.Join(", ", reportProducedWeaponList.ToArray());
							}

                            message = lider.SetEventTotalMessageTurn(report, item.Key);
							lider.SetCommandRealise(lider.GetCommandLiderFirst());
                            Debug.Log( "ount  message = " + message);
                            return message;
						}
                    }
                }
                

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
								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLiderFirst().GetAttackBomber().GetDamage(), true);
							}
						}


					}

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + lider.GetCommandLiderFirst().GetAttackBomber().GetDamage() + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(), lider.GetCommandLiderFirst().GetIncident().GetName());
					lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
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

								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLiderFirst().GetAttackMissle().GetDamage(), true);
							}
						}

					}
					lider.RemoveMissle();

                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderFirst().GetIncident().GetMessage() + lider.GetCommandLiderFirst().GetAttackMissle().GetDamage() + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(),
                        lider.GetCommandLiderFirst().GetIncident().GetName());
					lider.GetCommandLider().First().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}

			}

            //BuildingCentral buildingCentralLider = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
            Debug.Log(" Mi  message  =" + message);
            return message;
		}
		private int AddAndRemovePopulation(CityModel cityModelTarget, CountryLider lider, 
			bool RandomAndUnRevert, List<CityModel> TownList)
		{
			List<CityModel> liderCityList = new CityHelperList().GetListCityFlagId(TownList, lider.FlagId);
			int indexTown = UnityEngine.Random.Range(0, liderCityList.Count);


			CityModel liderCityMy = liderCityList[indexTown];



			int UnDamage = UnityEngine.Random.Range(1, 9);
			UnDamage = RandomAndUnRevert ? UnDamage : 9;

			if (cityModelTarget != null)
			{
				if (UnDamage > cityModelTarget.GetPopulation())
				{
					UnDamage = cityModelTarget.GetPopulation();
				}
			}

			CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider);
			// add population
			if (RandomAndUnRevert)
			{
				AddPopulationCity(liderCityMy, UnDamage, cityFiend);
			}
			else
			{
				AddPopulationCity(cityFiend, UnDamage, liderCityMy);
			}

			return UnDamage;
		}
		private void AddPopulationCity(CityModel liderCityMy, int UnDamage, CityModel cityFiend)
		{
			int population = liderCityMy.GetPopulation() + UnDamage;
			liderCityMy.SetFuturePopulation(population);
			liderCityMy.SetPresentlyPopulation();

			// remove population Fiend.
			new DamagePopulationHelper().SetDamagePopulation(cityFiend, UnDamage, false);
		}
	}
}
