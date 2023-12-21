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
		Dictionary<string,string> MessageDictionary;

		public void SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList, List<CityModel> TownList)
		{
            MessageDictionary = new Dictionary<string, string>();
			MessageDictionary.Add(DictionaryEssence.TypeEvent.RocketRich.ToString(), "Богатые и Маск постороили ракету на Луну ");
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Baby.ToString(), "Бэбибум ");
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Ufo.ToString(), "Ufo инопланитяне прибыли в город ");
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Defectors.ToString(), "Перебежчики сбежали ");
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Building.ToString(), "Производство вооружения ");
            MessageDictionary.Add(DictionaryEssence.TypeEvent.Propaganda.ToString(), "Под воздействием пропаганды, население мигрировало ");
            MessageDictionary.Add("AttackBomber", "От ядерного взрыва с бомбардировщика население уменьшилось на ");
            MessageDictionary.Add("AttackMissle", "От ядерного взрыва ракеты население уменьшилось на ");
            MessageDictionary.Add("Defence", "Защитные системы приведены в готовность");
            MessageDictionary.Add("Airport", "Бомбардировщики приведены в готовность");

            

            CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

			if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLider().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.RocketRich.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.RocketRich.ToString()] + UnDamage, DictionaryEssence.TypeEvent.RocketRich.ToString());
				}
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Baby.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Baby.ToString()] + UnDamage, DictionaryEssence.TypeEvent.Baby.ToString());
				}
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Ufo.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Ufo.ToString()] + UnDamage, DictionaryEssence.TypeEvent.Ufo.ToString());
				}
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Defectors.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Defectors.ToString()] + UnDamage, DictionaryEssence.TypeEvent.Defectors.ToString());
                    Debug.Log("     Lider = " + lider.GetCommandLider().LiderFiend);
                    lider.GetCommandLider().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}

				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Building.ToString()])
				{
					// building ammunition
					lider.AddMissle(lider.GetCommandLider().GetMissle());

					List<string> reportProducedWeaponList = lider.GetCommandLider().GetReportProducedWeaponList();
					string resultProducedWeapon = string.Join(", ", reportProducedWeaponList.ToArray());

					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Building.ToString()] + resultProducedWeapon, DictionaryEssence.TypeEvent.Building.ToString());
					lider.GetCommandLider().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}



				//propogation
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Propaganda.ToString()])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, true, TownList);

                    Debug.Log(MessageDictionary["Propaganda"]+" Lider = " + lider + " &&&&&&&&&&   Weapon== "+ lider.GetCommandLider());
                    Debug.Log( "  = Contro = " + lider.GetCommandLider().LiderFiend + "   "  );
                    
                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Propaganda.ToString()] + UnDamage + " сбежав от " + lider.GetCommandLider().LiderFiend.GetName(), DictionaryEssence.TypeEvent.Propaganda.ToString());
					lider.GetCommandLider().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 50);
				}
				// attack bomber
				if (lider.GetCommandLider().GetVisibleAttackBomber())
				{
					if (enemylider.GetCommandLider().GetDefence())
					{


						lider.RemoveBomber();
					}
					else
					{
						//bool explode0;
						if (lider.GetCommandLider().GetTargetCity() != null)
						{
							if (lider.GetCommandLider().GetAttackBomber() != null)
							{
								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackBomber().GetDamage(), true);
							}
						}


					}
                    

                    lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.AttackBomber.ToString()] + lider.GetCommandLider().GetAttackBomber().GetDamage() + " у " + lider.GetCommandLider().LiderFiend.GetName(), DictionaryEssence.TypeEvent.AttackBomber.ToString());
					lider.GetCommandLider().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()])
				{
					lider.MissleId = lider.GetCommandLider().GetSizeIdMissle();
				}
				// attack Missle
				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()])
				{

					if (enemylider.GetCommandLider().GetDefence())
					{


					}
					else
					{
						if (lider.GetCommandLider().GetTargetCity() != null)
						{

							if (lider.GetCommandLider().GetAttackMissle() != null)
							{

								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackMissle().GetDamage(), true);
							}
						}

					}
					lider.RemoveMissle();

					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.AttackMissle.ToString()] + lider.GetCommandLider().GetAttackMissle().GetDamage() + " у " + lider.GetCommandLider().LiderFiend.GetName(), DictionaryEssence.TypeEvent.AttackMissle.ToString());
					lider.GetCommandLider().LiderFiend._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}

				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Defence.ToString()])
				{
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Defence.ToString()], DictionaryEssence.TypeEvent.Defence.ToString());
					lider.RemoveDefenceWeapon();
				}

				if (lider.GetCommandLider().VisibleEventList[DictionaryEssence.TypeEvent.Airport.ToString()])
				{
					lider.SetEventTotalMessageTurn(MessageDictionary[DictionaryEssence.TypeEvent.Airport.ToString()], DictionaryEssence.TypeEvent.Airport.ToString());
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
