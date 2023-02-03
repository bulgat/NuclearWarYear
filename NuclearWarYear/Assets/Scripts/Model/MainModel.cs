using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModel 
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	public List<CityModel> TownList;
	public int _flagIdPlayer;
	public bool _endGame;
	private int CityIncrementId;
	
	public MainModel(List<GameObject> countryLiderPropagandaBuildingList,int flagIdPlayer) {
		InitModel(countryLiderPropagandaBuildingList,flagIdPlayer);
	}
	
	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList,int flagIdPlayer) {

		

		this.CountryLiderPropagandaBuildingList = countryLiderPropagandaBuildingList;
		this.TownList =  new List<CityModel>();
		this.TownList.Add(new CityModel(1, GetIncrementCityId()));
		this.TownList.Add(new CityModel(1, GetIncrementCityId()));
		this.TownList.Add(new CityModel(1, GetIncrementCityId()));
		this.TownList.Add(new CityModel(1, GetIncrementCityId()));
		this.TownList.Add(new CityModel(1, GetIncrementCityId()));

		this.TownList.Add(new CityModel(2, GetIncrementCityId()));
		this.TownList.Add(new CityModel(2, GetIncrementCityId()));
		this.TownList.Add(new CityModel(2, GetIncrementCityId()));
		this.TownList.Add(new CityModel(2, GetIncrementCityId()));
		this.TownList.Add(new CityModel(2, GetIncrementCityId()));

		this.TownList.Add(new CityModel(3, GetIncrementCityId()));
		this.TownList.Add(new CityModel(3, GetIncrementCityId()));
		this.TownList.Add(new CityModel(3, GetIncrementCityId()));
		this.TownList.Add(new CityModel(3, GetIncrementCityId()));
		this.TownList.Add(new CityModel(3, GetIncrementCityId()));

		this.TownList.Add(new CityModel(4, GetIncrementCityId()));
		this.TownList.Add(new CityModel(4, GetIncrementCityId()));
		this.TownList.Add(new CityModel(4, GetIncrementCityId()));
		this.TownList.Add(new CityModel(4, GetIncrementCityId()));
		this.TownList.Add(new CityModel(4, GetIncrementCityId()));

		this.TownList.Add(new CityModel(5, GetIncrementCityId()));
		this.TownList.Add(new CityModel(5, GetIncrementCityId()));
		this.TownList.Add(new CityModel(5, GetIncrementCityId()));
		this.TownList.Add(new CityModel(5, GetIncrementCityId()));
		this.TownList.Add(new CityModel(5, GetIncrementCityId()));

		this.CountryLiderList = new List<CountryLider>();
		//5
		this._flagIdPlayer = flagIdPlayer;
		this.CountryLiderList = new List<CountryLider>();
		this.CountryLiderList.Add(new CountryLider(1,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[0],TownList,"Путин"));
		this.CountryLiderList.Add(new CountryLider(2,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[1],TownList,"Зеленс"));
		this.CountryLiderList.Add(new CountryLider(3,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[2],TownList, "Байден"));
		this.CountryLiderList.Add(new CountryLider(4,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[3],TownList, "Си Цзип"));
		this.CountryLiderList.Add(new CountryLider(flagIdPlayer, true,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[4],TownList, "Игрок"));

		LiderCountryHelper.Init(this.CountryLiderList);

		foreach (var Lider in this.CountryLiderList)
        {
			Lider._RelationShip.InitRelationContry(this.CountryLiderList);

		}
	}
	private int GetIncrementCityId()
    {
		return this.CityIncrementId++;
    }
	public List<CityModel> GetTownList() {
		return this.TownList;
	}
	public List<CountryLider> GetCountryLiderList() {
		return this.CountryLiderList;
	}
	public void SetPropagandPlayer(int FlagId){

		

		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Propaganda",_flagIdPlayer,0));

		
	}
	public void SetBuildingPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Building",_flagIdPlayer,0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetDefencePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Defence",_flagIdPlayer,0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetMisslePlayer(int FlagId,int MissleId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Missle",_flagIdPlayer, MissleId));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetAttackMisslePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"AttackMissle",_flagIdPlayer,0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetBomberPlayer(int FlagId,int BomberId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Bomber",_flagIdPlayer, BomberId));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetAttackBomberPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"AttackBomber",_flagIdPlayer,0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetLiderTargetPlayer(int FlagId){
		CountryLiderList[4].FlagIdAttack =FlagId;
	}
	//WarheadMethod
	public void SetWarheadMethodPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,_flagIdPlayer);
		countryLider.GetBomber().Damage =countryLider.GetBomber().Damage;
		countryLider.GetMissle().Damage =countryLider.GetMissle().Damage;

	}
	public void ReconTotalTurn(int FlagId){

		Debug.Log("0  ReconTotalTurn  = ");
		

		// realize command.
		foreach (CountryLider lider in CountryLiderList){
			SatisfyOneLiderTurn(lider.FlagId);
			
		}
	}
	public void SatisfyOneLiderTurn(int FlagId)
    {
		CountryLider lider = this.CountryLiderList[FlagId - 1];
		//bool explode = false;
		if (lider.GetCommandLider() != null)
		{
            CityModel cityModelTarget = lider.GetCommandLider().GetTargetCity();
				//Enemy lider.
			CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList, lider);

			if (lider.GetCommandLider().VisibleEventList["RocketRich"])
            {
				int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false);
				lider.SetEventTotalTurn("Богатые и Маск постороили ракету на Луну " + UnDamage);
			}
			if (lider.GetCommandLider().VisibleEventList["Baby"])
			{
                int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false);
                lider.SetEventTotalTurn("Бэбибум " + UnDamage);
            }
            if (lider.GetCommandLider().VisibleEventList["Ufo"])
			{
                int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false);
                lider.SetEventTotalTurn("Ufo инопланитяне прибыли в город "+ UnDamage);
            }
            if (lider.GetCommandLider().VisibleEventList["Defectors"])
			{
                int UnDamage = AddAndRemovePopulation(cityModelTarget, lider, false);
                lider.SetEventTotalTurn("Перебежчики сбежали "+ UnDamage);
				lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
			}
 
            if (lider.GetCommandLider().VisibleEventList["Build"])
            {
				// building ammunition
				lider.AddMissle(lider.GetCommandLider().GetMissle());

				List<string> reportProducedWeaponList = lider.GetCommandLider().GetReportProducedWeaponList();
				string resultProducedWeapon = string.Join(", ", reportProducedWeaponList.ToArray());

				
				

				lider.SetEventTotalTurn("Производство вооружения "+ resultProducedWeapon);
				lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 5);
			}

			

            //propogation
            if (lider.GetCommandLider().VisibleEventList["Prop"])
			{
				int UnDamage = AddAndRemovePopulation(cityModelTarget, lider,true);

				lider.SetEventTotalTurn("Население увеличилось на "+ UnDamage+" сбежав от "+ lider.GetCommandLider().GetTargetLider().GetName());
				lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId,50);
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
							 new DamagePopulationHelper().SetDamagePopulation( new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackBomber().Damage, true);
						}
					}


				}
				lider.SetEventTotalTurn("От ядерного взрыва с бомбардировщика население уменьшилось на "+ lider.GetCommandLider().GetAttackBomber().Damage+" у "+ lider.GetCommandLider().GetTargetLider().GetName());
				lider.GetCommandLider().GetTargetLider()._RelationShip.SetNegativeMood(lider.FlagId, 25);
			}
			if (lider.GetCommandLider().VisibleEventList["Missle"]) {
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
						Debug.Log("0005   mberObje  "+ lider.GetMissleSideId());
						if (lider.GetCommandLider().GetAttackMissle() != null)
						{
                            
                             new DamagePopulationHelper().SetDamagePopulation( new DamagePopulationHelper().GetCityLider(lider), lider.GetCommandLider().GetAttackMissle().Damage, true);
						}
					}

				}
				lider.RemoveMissle();

				lider.SetEventTotalTurn("От ядерного взрыва ракеты население уменьшилось на "+ lider.GetCommandLider().GetAttackMissle().Damage+" у "+ lider.GetCommandLider().GetTargetLider().GetName());
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

		//lider.ChangeTurn();


		BuildingCentral buildingCentralLider = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();

	}
	
	private int AddAndRemovePopulation(CityModel cityModelTarget, CountryLider lider,bool RandomAndUnRevert)
	{
        List<CityModel> liderCityList = new CityHelperList().GetListCityFlagId(TownList, lider.FlagId);
        int indexTown = UnityEngine.Random.Range(0, liderCityList.Count);


        CityModel liderCityMy = liderCityList[indexTown];



        int UnDamage = UnityEngine.Random.Range(0, 9);
        UnDamage = RandomAndUnRevert ? UnDamage:9;

        if (cityModelTarget != null)
        {
            if (UnDamage > cityModelTarget.GetPopulation())
            {
                UnDamage = cityModelTarget.GetPopulation();
            }
        }

		CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider);
		// add population
		if (RandomAndUnRevert) { 
			AddPopulationCity(liderCityMy, UnDamage, cityFiend);
		} else
		{
            AddPopulationCity(cityFiend, UnDamage, liderCityMy);
        }
        

        
        

		return UnDamage;
    }
	private void AddPopulationCity(CityModel liderCityMy,int UnDamage, CityModel cityFiend)
	{
		int population = liderCityMy.GetPopulation() + UnDamage;
        liderCityMy.SetFuturePopulation(population);
        liderCityMy.SetPresentlyPopulation();

		// remove population Fiend.
        new DamagePopulationHelper().SetDamagePopulation(cityFiend, UnDamage, false);
	}
	public void SelectCityEnemyTargetPlayer(int CityId) {
		CityModel selectCityTarget=null;
		CountryLider playerLider = CountryLiderList[4];
		foreach (CityModel townCity in this.TownList){
			
			 if(townCity.GetId() == CityId) {
				 selectCityTarget = townCity;
				playerLider.GetCommandLider().SetTargetCity(townCity);

				playerLider.SetTargetCitySelectPlayer(townCity);
				//Debug.Log(" SetPropag Play  = "+ CountryLiderList[4].GetTargetCitySelectPlayer());
			}
			  
		 }
		
		if(_flagIdPlayer != selectCityTarget.FlagId){

		} else {
			// auto Set attack
			CityModel targetCityPlayer = new TargetHelper().GetTargetRandom(CountryLiderList,_flagIdPlayer,false,TownList, playerLider);
			//Debug.Log("0010  SetTargetCitySelectPlayer="+ targetCityPlayer);
			playerLider.SetTargetCitySelectPlayer(targetCityPlayer);
			
		}
		 
	
	}
	public void ResetSelectCityEnemyTargetPlayer(int CityId) {
		
		CountryLiderList[4].SetTargetCitySelectPlayer(null);
	}
	public void ResetAction(){


		CheckVictory checkVictory=new CheckVictory(CountryLiderList,this.TownList);
		_endGame= checkVictory.GetEndGame();
		
	}
}
