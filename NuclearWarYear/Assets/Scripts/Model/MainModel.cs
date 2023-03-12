using Assets.Scripts.Model.scenario;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainModel 
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	public List<CityModel> TownList;
	private List<int> FlagIdPlayer { get; set; } 
	public bool _endGame;
	private int CityIncrementId;
	private int CurrenPlayerFlag { set; get; }
	public MainModel(List<GameObject> countryLiderPropagandaBuildingList) {
		InitModel(countryLiderPropagandaBuildingList);
	}
	
	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList) {

		//FlagIdPlayer = new List<int>() { 5 };
		//this.FlagIdPlayer.Add(5);

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

		ParamLider paramLider = new ParamLider();
		

		List<ScenarioLider> scenarioLider_ar = paramLider.ScenarioLider_ar;

		this.CountryLiderList = new List<CountryLider>();
		this.CountryLiderList.Add(new CountryLider(false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[0],TownList, scenarioLider_ar[0],1));
		this.CountryLiderList.Add(new CountryLider(false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[1],TownList, scenarioLider_ar[1],2));
		this.CountryLiderList.Add(new CountryLider(false,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[2],TownList, scenarioLider_ar[2],3));
		this.CountryLiderList.Add(new CountryLider(true,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[3],TownList, scenarioLider_ar[3],4));
		this.CountryLiderList.Add(new CountryLider(true,new DictionaryMissle().GetMissle (1),new DictionaryMissle().GetBomber (1),CountryLiderPropagandaBuildingList[4],TownList, scenarioLider_ar[4],5));

		this.FlagIdPlayer = new List<int>();
		foreach (var item in this.CountryLiderList)
        {
			if (item.Player)
            {
				this.FlagIdPlayer.Add(item.FlagId);

			}
        }
		this.CurrenPlayerFlag = this.FlagIdPlayer[0];
		//FlagIdPlayer = new List<int>() { 5, 3 };


		LiderCountryHelper.Init(this.CountryLiderList);

		foreach (var Lider in this.CountryLiderList)
        {
			Lider._RelationShip.InitRelationContry(this.CountryLiderList);

		}
	}
	public bool EveryonePlayerWent()
    {
		foreach(CountryLider lider in this.CountryLiderList)
        {
			
			if (this.FlagIdPlayer.Contains(lider.FlagId))
            {
				Debug.Log("====="+lider.MoveMade+"   ["+ this.FlagIdPlayer[0] +","+ this.FlagIdPlayer[1]+ "] ==" + lider.FlagId);
				if (lider.MoveMade==false)
                {
					return false;
                }

			}
        }
		Debug.Log( "   ----  Coun  = "); 
		return true;
    }
	public int GetCurrenFlagPlayer()
    {
		return this.CurrenPlayerFlag;

	}
	public CountryLider GetCurrenPlayer()
	{
		return this.GetLiderOne(this.CurrenPlayerFlag);

	}
	public CountryLider GetLiderOne(int FlagId)
    {
		return new LiderHelperOne().GetLiderOne(this.CountryLiderList, FlagId);

	}
	public void ChangeCurrentPlayer()
    {
		if (this.FlagIdPlayer.Count > 1)
        {
			var index = this.FlagIdPlayer.FindIndex(a=>a==this.CurrenPlayerFlag);
			index++;
			if(index>= this.FlagIdPlayer.Count)
            {
				this.CurrenPlayerFlag = this.FlagIdPlayer[0];
				return;
			}
			this.CurrenPlayerFlag = this.FlagIdPlayer[index];


		}
		

	}
	public int GetCurrentPlayerFlag()
    {
		return this.CurrenPlayerFlag;

	}
	private int GetIncrementCityId()
    {
		return this.CityIncrementId++;
    }
	public void DoneMoveMadeCurrentPlayer()
    {
		CountryLider countryLider =  this.GetLiderOne(this.CurrenPlayerFlag);
		countryLider.DoneMoveMade();

	}


	public List<CityModel> GetTownList() {
		return this.TownList;
	}
	public List<CountryLider> GetCountryLiderList() {
		return this.CountryLiderList;
	}
    public List<CountryLider> GetFiendCountryLiderList()
    {
        return this.CountryLiderList.Where(a=>a.FlagId!= this.GetCurrentPlayerFlag()).ToList();
    }
    public void SetPropagandPlayer(int FlagId){

		

		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"Propaganda",this.FlagIdPlayer[0],0));

		
	}
	public void SetBuildingPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"Building",this.FlagIdPlayer[0],0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetDefencePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"Defence",this.FlagIdPlayer[0],0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetMisslePlayer(int FlagId,int MissleId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"Missle",this.FlagIdPlayer[0], MissleId));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetAttackMisslePlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"AttackMissle",this.FlagIdPlayer[0],0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetBomberPlayer(int FlagId,int BomberId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"Bomber",this.FlagIdPlayer[0], BomberId));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetAttackBomberPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList, this.FlagIdPlayer[0],"AttackBomber",this.FlagIdPlayer[0],0));
		
		new AICreateCommand().EstimationSetCommandAi(ResetAction,CountryLiderList,TownList,this.FlagIdPlayer[0], this.FlagIdPlayer[0]);
	}
	public void SetLiderTargetPlayer(int FlagId){
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.FlagIdPlayer[0]);
		liderPlayer.FlagIdAttack =FlagId;
	}
	//WarheadMethod
	public void SetWarheadMethodPlayer(int FlagId){
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,this.FlagIdPlayer[0]);
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
		CountryLider lider = new LiderHelperOne().GetLiderOne(this.CountryLiderList, FlagId);
		//CountryLider lider = this.CountryLiderList[FlagId - 1];
	
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
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.FlagIdPlayer[0]);
		//CountryLider playerLider = CountryLiderList[4];
		foreach (CityModel townCity in this.TownList){
			
			 if(townCity.GetId() == CityId) {
				 selectCityTarget = townCity;
				liderPlayer.GetCommandLider().SetTargetCity(townCity);

				liderPlayer.SetTargetCitySelectPlayer(townCity);
				
			}
			  
		 }
		
		if(this.FlagIdPlayer[0] != selectCityTarget.FlagId){

		} else {
			// auto Set attack
			CityModel targetCityPlayer = new TargetHelper().GetTargetRandom(CountryLiderList,this.FlagIdPlayer[0],false,TownList, liderPlayer);

			liderPlayer.SetTargetCitySelectPlayer(targetCityPlayer);
			
		}
		 
	
	}
	public void ResetSelectCityEnemyTargetPlayer(int CityId) {
		CountryLider liderPlayer =new LiderHelperOne().GetLiderOne(this.CountryLiderList,this.FlagIdPlayer[0]);
		liderPlayer.SetTargetCitySelectPlayer(null);
	}
	public void ResetAction(){


		CheckVictory checkVictory=new CheckVictory(CountryLiderList,this.TownList);
		_endGame= checkVictory.GetEndGame();
		
	}
}
