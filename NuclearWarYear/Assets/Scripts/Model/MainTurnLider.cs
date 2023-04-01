using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class MainTurnLider
    {
		public void SatisfyOneLiderTurn(int FlagId, List<CountryLider> CountryLiderList, List<CityModel> TownList)
		{
			CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
			//CountryLider lider = this.CountryLiderList[FlagId - 1];

			if (lider.GetCommandLider() != null)
			{
				CityModel cityModelTarget = lider.GetCommandLider().GetTargetCity();
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

				if (lider.GetCommandLider().VisibleEventList["RocketRich"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalTurn("Богатые и Маск постороили ракету на Луну " + UnDamage);
				}
				if (lider.GetCommandLider().VisibleEventList["Baby"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalTurn("Бэбибум " + UnDamage);
				}
				if (lider.GetCommandLider().VisibleEventList["Ufo"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalTurn("Ufo инопланитяне прибыли в город " + UnDamage);
				}
				if (lider.GetCommandLider().VisibleEventList["Defectors"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false, TownList);
					lider.SetEventTotalTurn("Перебежчики сбежали " + UnDamage);
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}

				if (lider.GetCommandLider().VisibleEventList["Build"])
				{
					// building ammunition
					lider.AddMissle(lider.GetCommandLider().GetMissle());

					List<string> reportProducedWeaponList = lider.GetCommandLider().GetReportProducedWeaponList();
					string resultProducedWeapon = string.Join(", ", reportProducedWeaponList.ToArray());




					lider.SetEventTotalTurn("Производство вооружения " + resultProducedWeapon);
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
				}



				//propogation
				if (lider.GetCommandLider().VisibleEventList["Prop"])
				{
					int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, true, TownList);

					lider.SetEventTotalTurn("Население увеличилось на " + UnDamage + " сбежав от " + lider.GetCommandLider().GetTargetLider().GetName());
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
								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackBomber().Damage, true);
							}
						}


					}
					lider.SetEventTotalTurn("От ядерного взрыва с бомбардировщика население уменьшилось на " + lider.GetCommandLider().GetAttackBomber().Damage + " у " + lider.GetCommandLider().GetTargetLider().GetName());
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

								new DamagePopulationHelper().SetDamagePopulation(new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackMissle().Damage, true);
							}
						}

					}
					lider.RemoveMissle();

					lider.SetEventTotalTurn("От ядерного взрыва ракеты население уменьшилось на " + lider.GetCommandLider().GetAttackMissle().Damage + " у " + lider.GetCommandLider().GetTargetLider().GetName());
					lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 25);
				}

				if (lider.GetCommandLider().VisibleEventList["Defence"])
				{
					lider.SetEventTotalTurn("Защитные системы приведены в готовность");
					lider.RemoveDefenceWeapon();
				}

				if (lider.GetCommandLider().VisibleEventList["Airport"])
				{
					lider.SetEventTotalTurn("Бомбардировщики приведены в готовность");
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
