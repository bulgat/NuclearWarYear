using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class MainTurnLider
    {
		Dictionary<string,string> MessageDictionary;

		public void SatisfyEventOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList, List<CityModel> TownList)
		{
            MessageDictionary = new Dictionary<string, string>();
			MessageDictionary.Add("RocketRich", "Богатые и Маск постороили ракету на Луну ");
            MessageDictionary.Add("Baby", "Бэбибум ");
            MessageDictionary.Add("Ufo", "Ufo инопланитяне прибыли в город ");
            MessageDictionary.Add("Defectors", "Перебежчики сбежали ");
            MessageDictionary.Add("Build", "Производство вооружения ");
            MessageDictionary.Add("Propaganda", "Под воздействием пропаганды, население мигрировало ");
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

				if (lider.GetCommandLider().VisibleEventList["RocketRich"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary["RocketRich"] + UnDamage, "RocketRich");
				}
				if (lider.GetCommandLider().VisibleEventList["Baby"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary["Baby"] + UnDamage, "Baby");
				}
				if (lider.GetCommandLider().VisibleEventList["Ufo"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary["Ufo"] + UnDamage, "Ufo");
				}
				if (lider.GetCommandLider().VisibleEventList["Defectors"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalMessageTurn(MessageDictionary["Defectors"] + UnDamage, "Defectors");
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}

				if (lider.GetCommandLider().VisibleEventList["Build"])
				{
					// building ammunition
					lider.AddMissle(lider.GetCommandLider().GetMissle());

					List<string> reportProducedWeaponList = lider.GetCommandLider().GetReportProducedWeaponList();
					string resultProducedWeapon = string.Join(", ", reportProducedWeaponList.ToArray());

					lider.SetEventTotalMessageTurn(MessageDictionary["Build"] + resultProducedWeapon, "Build");
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}



				//propogation
				if (lider.GetCommandLider().VisibleEventList["Propaganda"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, true, TownList);

					lider.SetEventTotalMessageTurn(MessageDictionary["Propaganda"] + UnDamage + " сбежав от " + lider.GetCommandLider().GetTargetLider().GetName(), "Propaganda");
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 50);
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
                    

                    lider.SetEventTotalMessageTurn(MessageDictionary["AttackBomber"] + lider.GetCommandLider().GetAttackBomber().GetDamage() + " у " + lider.GetCommandLider().GetTargetLider().GetName(), "AttackBomber");
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}
				if (lider.GetCommandLider().VisibleEventList["Missle"])
				{
					lider.MissleId = lider.GetCommandLider().GetSizeIdMissle();
				}
				// attack Missle
				if (lider.GetCommandLider().VisibleEventList["AttackMissle"])
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

					lider.SetEventTotalMessageTurn(MessageDictionary["AttackMissle"] + lider.GetCommandLider().GetAttackMissle().GetDamage() + " у " + lider.GetCommandLider().GetTargetLider().GetName(), "AttackMissle");
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}

				if (lider.GetCommandLider().VisibleEventList["Defence"])
				{
					lider.SetEventTotalMessageTurn(MessageDictionary["Defence"], "Defence");
					lider.RemoveDefenceWeapon();
				}

				if (lider.GetCommandLider().VisibleEventList["Airport"])
				{
					lider.SetEventTotalMessageTurn(MessageDictionary["Airport"], "Airport");
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
