using Assets.Scripts.Model.turnEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts.Model
{
    public class MainTurnLider
    {
		Dictionary<string, TurnEventExecute> MessageDictionary;

		public void SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList, List<CityModel> TownList)
		{
            MessageDictionary = new Dictionary<string, TurnEventExecute>();
			MessageDictionary.Add(DictionaryEssence.TypeEvent.RocketRich.ToString(), new TurnEventExecute("Богатые и Маск постороили ракету на Луну ",null,0,false,true,false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Baby.ToString(), new TurnEventExecute("Бэбибум ", null, 0,false, true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Ufo.ToString(), new TurnEventExecute("Ufo инопланитяне прибыли в город ", null, 0, false,true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Defectors.ToString(), new TurnEventExecute("Перебежчики сбежали ", null, 5,false, true, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Building.ToString(), new TurnEventExecute("Производство вооружения ", null, 5, true, false, false,false, false));
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Propaganda.ToString(), new TurnEventExecute("Под воздействием пропаганды, население мигрировало ",
				" сбежав от ", 5, false, true,true,false, false));
            MessageDictionary.Add("AttackBomber", new TurnEventExecute("От ядерного взрыва с бомбардировщика население уменьшилось на ", null, 0, false, true, false,false, false));
            MessageDictionary.Add("AttackMissle", new TurnEventExecute("От ядерного взрыва ракеты население уменьшилось на ", null, 0,false, true, false,false, false));
            MessageDictionary.Add("Defence", new TurnEventExecute("Защитные системы приведены в готовность", null, 0, false, true, false,true, false));
            MessageDictionary.Add("Airport", new TurnEventExecute("Бомбардировщики приведены в готовность", null, 0, false, true, false,false, true));

            

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

			if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLiderFirst().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				foreach(var item in MessageDictionary)
				{
                    Debug.Log("   E   " + item.Key + " = Bomber   ___tim > ");
					if (lider.GetCommandLiderFirst().VisibleEventList[item.Key])
					{
						

						if (MessageDictionary[item.Key].RemoveDefenceWeapon || MessageDictionary[item.Key].Airport)
						{
							if (MessageDictionary[item.Key].Airport)
							{
                                lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Airport.ToString()].Message, DictionaryEssence.TypeEvent.Airport.ToString());
                            }
							else
							{
								lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Defence.ToString()].Message, DictionaryEssence.TypeEvent.Defence.ToString());
								lider.RemoveDefenceWeapon();
							}

						}
						else
						{

							int UnDamage = 0;// AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
							if (MessageDictionary[item.Key].ChangePopulation)
							{
								UnDamage = AddAndRemovePopulation(cityModelTarget, lider, MessageDictionary[item.Key].Random, TownList);
							}
							string report = MessageDictionary[item.Key].Message + UnDamage;
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
							lider.SetEventTotalMessageTurn(report, item.Key);
						}
                    }
                }

                /*
				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.RocketRich.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.RocketRich.ToString()] + UnDamage, DictionaryEssence.TypeEvent.RocketRich.ToString());
				}
				
				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Baby.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Baby.ToString()] + UnDamage, DictionaryEssence.TypeEvent.Baby.ToString());
				}

				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Ufo.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Ufo.ToString()] + UnDamage, DictionaryEssence.TypeEvent.Ufo.ToString());
				}

				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Defectors.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Defectors.ToString()].Message + UnDamage, DictionaryEssence.TypeEvent.Defectors.ToString());
                    Debug.Log("     Lider = " + lider.GetCommandLiderFirst().LiderFiend);
                    lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}

				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Building.ToString()])
				{
					// building ammunition
					lider.AddMissle(lider.GetCommandLiderFirst().GetMissle());

					List<string> reportProducedWeaponList = lider.GetCommandLiderFirst().GetReportProducedWeaponList();
					string resultProducedWeapon = string.Join(", ", reportProducedWeaponList.ToArray());

					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Building.ToString()] + resultProducedWeapon, DictionaryEssence.TypeEvent.Building.ToString());
					lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}



				//propogation
				if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Propaganda.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, true, TownList);

                    Debug.Log(MessageDictionary["Propaganda"]+" Lider = " + lider + " &&&&&&&&&&   Weapon== "+ lider.GetCommandLider());
                    Debug.Log( "  = Contro = " + lider.GetCommandLiderFirst().LiderFiend + "   "  );
                    
                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Propaganda.ToString()].Message + UnDamage + " сбежав от " + lider.GetCommandLiderFirst().LiderFiend.GetName(), DictionaryEssence.TypeEvent.Propaganda.ToString());
					lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 50);
				}

                if (lider.GetCommandLider().First().VisibleEventList[DictionaryEssence.TypeEvent.Defence.ToString()])
                {
                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Defence.ToString()].Message, DictionaryEssence.TypeEvent.Defence.ToString());
                    lider.RemoveDefenceWeapon();
                }

                if (lider.GetCommandLider().First().VisibleEventList[DictionaryEssence.TypeEvent.Airport.ToString()])
                {
                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Airport.ToString()].Message, DictionaryEssence.TypeEvent.Airport.ToString());
                }
*/

             

                // attack bomber
                if (lider.GetCommandLiderFirst().GetVisibleAttackBomber())
				{
					if (enemylider.GetCommandLiderFirst().GetDefence())
					{


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
                    

                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.AttackBomber.ToString()].Message + lider.GetCommandLiderFirst().GetAttackBomber().GetDamage() + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(), DictionaryEssence.TypeEvent.AttackBomber.ToString());
					lider.GetCommandLiderFirst().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}
                if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()])
                {
                    lider.MissleId = lider.GetCommandLiderFirst().GetSizeIdMissle();
                }
                // attack Missle
                if (lider.GetCommandLiderFirst().VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()])
				{

					if (enemylider.GetCommandLiderFirst().GetDefence())
					{


					}
					else
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

					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.AttackMissle.ToString()].Message + lider.GetCommandLiderFirst().GetAttackMissle().GetDamage() + " у " + lider.GetCommandLiderFirst().LiderFiend.GetName(), DictionaryEssence.TypeEvent.AttackMissle.ToString());
					lider.GetCommandLider().First().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}

			


			}

			BuildingCentral buildingCentralLider = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();

		}
		private int AddAndRemovePopulation(CityModel cityModelTarget, CountryLider lider, 
			bool RandomAndUnRevert, List<CityModel> TownList)
		{
			List<CityModel> liderCityList = new CityHelperList().GetListCityFlagId(TownList, lider.FlagId);
			int indexTown = UnityEngine.Random.Range(0, liderCityList.Count);


			CityModel liderCityMy = liderCityList[indexTown];



			int UnDamage = UnityEngine.Random.Range(0, 9);
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
