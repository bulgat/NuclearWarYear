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
		this.CountryLiderList.Add(new CountryLider(1,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),new DictionaryMissle().GetWarhead (1),CountryLiderPropagandaBuildingList[0],TownList,"Путин"));
		this.CountryLiderList.Add(new CountryLider(2,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),new DictionaryMissle().GetWarhead (1),CountryLiderPropagandaBuildingList[1],TownList,"Зеленский"));
		this.CountryLiderList.Add(new CountryLider(3,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),new DictionaryMissle().GetWarhead (1),CountryLiderPropagandaBuildingList[2],TownList, "Байден"));
		this.CountryLiderList.Add(new CountryLider(4,false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),new DictionaryMissle().GetWarhead (1),CountryLiderPropagandaBuildingList[3],TownList, "Си Цзипин"));
		this.CountryLiderList.Add(new CountryLider(flagIdPlayer, true,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),new DictionaryMissle().GetWarhead (1),CountryLiderPropagandaBuildingList[4],TownList, "Игрок"));
		
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

		Debug.Log(" SetPropagandPlayer = "+ FlagId);

		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Propaganda",_flagIdPlayer));

		//new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, _flagIdPlayer, _flagIdPlayer);
	}
	public void SetBuildingPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Building",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetDefencePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Defence",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetMisslePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Missle",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetAttackMisslePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"AttackMissle",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetBomberPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"Bomber",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetAttackBomberPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, _flagIdPlayer,"AttackBomber",_flagIdPlayer));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,_flagIdPlayer, _flagIdPlayer);
	}
	public void SetLiderTargetPlayer(int FlagId){
		CountryLiderList[4].FlagIdAttack =FlagId;
	}
	//WarheadMethod
	public void SetWarheadMethodPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,_flagIdPlayer);
		countryLider.GetBomber().Damage =countryLider.GetWarhead().Damage;
		countryLider.GetMissle().Damage =countryLider.GetWarhead().Damage;
		countryLider.RemoveWarhead();
		//GetEnergeLevelPercentWarhead();
	}
	public void ReconTotalTurn(int FlagId){
			
			// realize command.
		foreach(CountryLider lider in CountryLiderList){
			bool explode=false;
			if(lider.GetCommandLider ()!=null) {
				
				
				// building ammunition
				lider.AddMissle(lider.GetCommandLider ().GetMissle());
				lider.AddBomber(lider.GetCommandLider ().GetBomber());
				lider.AddWarhead(lider.GetCommandLider ().GetWarhead());
				//Enemy lider.
				CountryLider enemylider = new LiderHelper().GetLiderEnemy(CountryLiderList,lider);
				if(enemylider!=null){

				}

				//propogation
				if(lider.GetCommandLider ().VisibleProp){
					
					List<CityModel> liderCityList = new CityHelperList().GetListCityFlagId(TownList,lider.FlagId);
					int indexTown = UnityEngine.Random.Range(0, liderCityList.Count);
					
					
					//City liderCityMy = liderCityList[indexTown];
					CityModel liderCityMy = liderCityList[indexTown];



					int UnDamage =UnityEngine.Random.Range(0, 9);
					if(lider.GetCommandLider ().GetTargetCity()!=null){
						if(UnDamage>lider.GetCommandLider ().GetTargetCity().GetPopulation()) {
							UnDamage =lider.GetCommandLider ().GetTargetCity().GetPopulation();
						}
					}
					// add population
					int population = liderCityMy.GetPopulation() + UnDamage;
					liderCityMy.SetFuturePopulation(population);
					liderCityMy.SetPresentlyPopulation();
					// remove population Fiend.
					explode = new DamagePopulationHelper().SetDamagePopulation(lider,UnDamage,false);
					if (lider.GetCommandLider ().GetTargetCity()!=null){
						////lider.GetCommandLider ().GetTargetCity().SetVisible(explode);
					}
				}
				// attack bomber
				if(lider.GetCommandLider ().VisibleAttackBomber){
					if(enemylider.GetCommandLider ().GetDefence()){
				
						/////lider.GetCommandLider().GetTargetCity().SetVisibleShild(true);
						lider.RemoveBomber();
					} else{
						//bool explode0;
						if (lider.GetCommandLider ().GetTargetCity()!=null){
							if(lider.GetCommandLider ().GetAttackBomber()!=null){
								explode = new DamagePopulationHelper().SetDamagePopulation(lider,lider.GetCommandLider ().GetAttackBomber().Damage,true);
							}
						}
						/////lider.GetCommandLider ().GetTargetCity().SetVisible(explode);
						
					}
				}
				// attack Missle
				if(lider.GetCommandLider ().VisibleAttackMissle){
					
					if(enemylider.GetCommandLider ().GetDefence()){
						
						////lider.GetCommandLider().GetTargetCity().SetVisibleShild(true);
					} else {
						if (lider.GetCommandLider ().GetTargetCity()!=null){
							if (lider.GetCommandLider ().GetAttackMissle()!=null){
								explode = new DamagePopulationHelper().SetDamagePopulation(lider,lider.GetCommandLider ().GetAttackMissle().Damage,true);
							}
						}
						////lider.GetCommandLider ().GetTargetCity().SetVisible(explode);
					}
					lider.RemoveMissle();
					
					
				}
			} else {
		
			}
			
			lider.ChangeTurn();
			
	
			BuildingCentral buildingCentralLider = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
			buildingCentralLider.ResetStateObject();
		}
	}
	public void SelectCityEnemyTargetPlayer(int CityId) {
		CityModel selectCityTarget=null;
		
		foreach(CityModel townCity in TownList){
			 //City townCity = city.GetComponent<City>();
			 
			 if(townCity.GetId() == CityId) {
				 selectCityTarget = townCity;
				 CountryLiderList[4].GetCommandLider().SetTargetCity(townCity);
				
				 CountryLiderList[4].SetTargetCitySelectPlayer(townCity);
			 }
			  
		 }
		
		 if(_flagIdPlayer != selectCityTarget.FlagId){

		} else {
			// auto Set attack
			CityModel targetCityPlayer = new TargetHelper().GetTarget(CountryLiderList,_flagIdPlayer,false,true,TownList,_flagIdPlayer);
			//City targetCityPlayerTown = targetCityPlayer.GetComponent<City>();
			
			CountryLiderList[4].SetTargetCitySelectPlayer(null);
			
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
