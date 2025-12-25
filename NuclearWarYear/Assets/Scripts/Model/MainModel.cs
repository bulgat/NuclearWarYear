using Assets.Scripts;
using Assets.Scripts.Model;
using Assets.Scripts.Model.AiTurn;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model.turnEvent;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using static Assets.Scripts.Model.param.GlobalParam;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class MainModel
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	private List<CityModel> TownList;
	private List<int> FlagIdPlayerList { get; set; }
	public bool _endGame;
	private int CityIncrementId;
	private int CurrenPlayerFlag { set; get; }
	public int CountYear { private set; get; }
    public List<CommandLider> MainStackCommandLiderList { set; get; }
    public MainModel(List<GameObject> countryLiderPropagandaBuildingList) {
		InitModel(countryLiderPropagandaBuildingList);
        MainStackCommandLiderList = new List<CommandLider>();

    }

	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList) {

        BindCity bindCity = new BindCity();
		this.TownList = bindCity.GetBindCity(this);

        this.CountryLiderPropagandaBuildingList = countryLiderPropagandaBuildingList;
		
		

        this.CountryLiderList = new BindLider().GetBindLider(this.TownList, CountYear, CountryLiderPropagandaBuildingList);


		this.FlagIdPlayerList = new List<int>();
		foreach (var item in this.CountryLiderList)
		{
			if (item.Player)
			{
				this.FlagIdPlayerList.Add(item.FlagId);

			}
		}
		this.CurrenPlayerFlag = this.FlagIdPlayerList[0];

		LiderCountryHelper.Init(this.CountryLiderList);

		foreach (var Lider in this.CountryLiderList)
		{
			Lider._RelationFeind.InitRelationContry(this.CountryLiderList);

		}
	}
	public bool EveryonePlayerWent()
	{
		foreach (CountryLider lider in this.CountryLiderList)
		{

			if (this.FlagIdPlayerList.Contains(lider.FlagId))
			{

				if (lider.MoveMade == false)
				{
					return false;
				}

			}
		}

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
		if (this.FlagIdPlayerList.Count > 1)
		{
			var index = this.FlagIdPlayerList.FindIndex(a => a == this.CurrenPlayerFlag);
			index++;
			if (index >= this.FlagIdPlayerList.Count)
			{
				this.CurrenPlayerFlag = this.FlagIdPlayerList[0];
				return;
			}
			this.CurrenPlayerFlag = this.FlagIdPlayerList[index];


		}


	}
	public int GetCurrentPlayerFlag()
	{
		return this.CurrenPlayerFlag;

	}
	public int GetIncrementCityId()
	{
		return this.CityIncrementId++;
	}
	public void ResetDoneMoveAll()
	{
		foreach (var item in this.CountryLiderList)
		{
			item.DoneMoveMade(false);
		}
	}

	public List<CityModel> GetTownList() {
		return this.TownList.Where(a=>a.GetPopulation()>0).ToList();
	}
	public List<CountryLider> GetCountryLiderList() {
		return this.CountryLiderList;
	}
	public List<CountryLider> GetFiendCountryLiderList()
	{
		return this.CountryLiderList.Where(a => a.FlagId != this.GetCurrentPlayerFlag()).ToList();
	}
	public void SetPropagandPlayer(int FlagId) {

		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),FlagId);
        ResetAction();

        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);

		List<CommandLider> сommandLiderList = new SwitchActionHelper().SwitchAction(
			CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider,
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, commandLiderFortune);

        Debug.Log("@@@I  - GetDamagePopulati  GetNameFiendLider() L = " + сommandLiderList.Count);

        MainStackCommandLiderList.AddRange(сommandLiderList);


    }
	public void SetBuildingPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Build,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider), FlagId);
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        MainStackCommandLiderList.AddRange(new SwitchActionHelper().SwitchAction( CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider, 
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, 
			commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction,
			CountryLiderList, TownList, this.GetCurrenPlayer().FlagId,
			this.GetCurrenPlayer().FlagId, CountYear, this);
	}
	public void SetDefencePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Defence,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider), FlagId);
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        MainStackCommandLiderList.AddRange(new SwitchActionHelper().SwitchAction(CountryLiderList, TownList, 
			this.GetCurrenPlayer().FlagId, commandLider,
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider,  commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList,
			TownList, this.GetCurrenPlayer().FlagId,
			this.GetCurrenPlayer().FlagId, CountYear, this);
	}
	public void SetCommandIncident(int FlagId, TypeEvent nameEvent)
	{
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

        CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

        List<CommandLider> commandLiderList = new List<CommandLider>();
		CommandLider commandLider = new CommandLider(nameEvent,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, myCity, enemyliderPlayer), FlagId);

        commandLiderList.Add(commandLider);

        MainStackCommandLiderList.AddRange(commandLiderList);
		countryLider.SetCommandRealise(commandLider.GetIncident());

        new CreateCommandLider().CommandOneLider(countryLider,ResetAction,
			CountryLiderList, TownList, this.GetCurrenPlayer().FlagId,
			this.GetCurrenPlayer().FlagId, CountYear, this);
    }

	public void SetMisslePlayer(int FlagId, TypeEvent nameEvent) {
		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackMisslePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        
		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != countryLider.FiendLider.FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == countryLider.FiendLider.FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.AttackMissle,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider), FlagId);
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
               countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        MainStackCommandLiderList.AddRange(new SwitchActionHelper().SwitchAction(CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider, 
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList,
			TownList, this.GetCurrenPlayer().FlagId,
			this.GetCurrenPlayer().FlagId, CountYear, this);
	}
	public void SetBomberPlayer(int FlagId, TypeEvent nameEvent) {

		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackBomberPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        
		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != countryLider.FiendLider.FlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == countryLider.FiendLider.FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.AttackBomber,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear, new TargetCityModel(enemyTownCity,myCity, countryLider.FiendLider), FlagId);
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
               countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        MainStackCommandLiderList.AddRange(new SwitchActionHelper().SwitchAction( CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider,
            this.GetCurrenPlayer(), CountYear,
            countryLider.FiendLider, commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, 
			CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, 
			this.GetCurrenPlayer().FlagId, CountYear,this);
	}
	public void SetLiderTargetPlayer(int FlagId) {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.FlagIdAttack = FlagId;
	}
	//WarheadMethod
	public void SetWarheadMethodPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, this.GetCurrenPlayer().FlagId);
		countryLider.GetBomber().SetDamage(countryLider.GetBomber().GetDamage());
		countryLider.GetMissleFirst().SetDamage(countryLider.GetMissleFirst().GetDamage());

	}
	public void TotalTurn(int FlagId) {

		foreach (CountryLider lider in this.CountryLiderList) {
            new MainSetTurnLider().SatisfyEventOneLiderTurn(lider.FlagId,
				this.CountryLiderList,
				this.TownList,
                GetCommandLider(CountYear, lider.FlagId).GetIncident(), CountYear,this);
        }
	}
	public Incident SatisfyOneLiderTurn(int FlagId, Incident CommandIncident)
	{
		return new MainSetTurnLider().SatisfyEventOneLiderTurn(FlagId, CountryLiderList,
			TownList, CommandIncident, CountYear,this);
	}
	public CommandLider GetCommandLider(int CountYear,int FlagId)
	{

           return this.MainStackCommandLiderList.FirstOrDefault(a => a.GetIncident().Year == CountYear && a.LiderId ==FlagId);
    }
    public List<CommandLider> GetCommandLiderList(int CountYear, int FlagId)
    {

        return this.MainStackCommandLiderList.Where(a => a.GetIncident().Year == CountYear && a.LiderId == FlagId).ToList();
    }

    public void SelectCityEnemyTargetPlayer(int? CityId,int LiderFlagId) {
		CityModel selectCityTarget = null;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(this.CountryLiderList, LiderFlagId);

        CityModel enemyTownCity = this.GetTownList().Where(a=>a.FlagId!=LiderFlagId).FirstOrDefault();
        CityModel myCity = this.GetTownList().Where(a => a.FlagId == LiderFlagId).FirstOrDefault();

        CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

 
				selectCityTarget = enemyTownCity;


        this.MainStackCommandLiderList.AddRange(new List<CommandLider>() {
            new CommandLider(GlobalParam.TypeEvent.Propaganda,
            countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
            CountYear, new TargetCityModel(enemyTownCity,myCity, enemyliderPlayer),LiderFlagId) });

        CommandLider command0 = GetCommandLider(CountYear, countryLider.FlagId);

        countryLider.SetTargetCity(new TargetCityModel(enemyTownCity, myCity, enemyliderPlayer));



		if (this.GetCurrenPlayer().FlagId != selectCityTarget.FlagId) {

		} else {
            // auto Set attack
            
            CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, this.GetCurrenPlayer().FlagId);
			CityModel targetCityPlayer = new TargetHelper().GetRandomCity(TownList, countryLider, this.GetCurrenPlayer().FlagId, false);

             new TargetHelper().SetTargetBuilding(
				CountryLiderList, fiendLider1,true, myCity, targetCityPlayer);

            countryLider.SetTargetCity(new TargetCityModel(targetCityPlayer, myCity, fiendLider1));
        }


	}
	public void ResetSelectCityEnemyTargetPlayer() {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
        liderPlayer.ResetTargetCity();

    }
	public void ResetAction() {


		CheckVictory checkVictory = new CheckVictory(CountryLiderList, this.TownList);
		_endGame = checkVictory.GetEndGame();

	}
	public void ReleasePopulationEvent(Incident CommandIncident)
	{
        new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.MyCity, CommandIncident.PopulationEvent.MyPopulation);
		new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.FiendCity, CommandIncident.PopulationEvent.FiendPopulation);
	}
	public List<IWeapon> GetEternalWeapon()
	{
        //this.GetLiderOne(this.CurrenPlayerFlag).GetAllMissle();


        List<IWeapon> missleList = new List<IWeapon>();
        missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Build));
        missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Propaganda));
        return missleList;
	}
	public List<IWeapon> GetCurrentWeapon()
	{
		//List<IWeapon> missleList = new List<IWeapon>();
		//missleList.AddRange(GetCurrenPlayer().GetDefenceWeapon());
		//missleList.AddRange(GetCurrenPlayer().GetMissleList());
		return this.GetLiderOne(this.CurrenPlayerFlag).GetAllMissle();
	}
    public void TurnAi()
	{
		CountYear++;
        new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList,
		   GetTownList(), GetCurrenFlagPlayer(), GetCurrenFlagPlayer(), CountYear,this);

        CountryLider countryLider = this.GetLiderOne(this.CurrenPlayerFlag);
        countryLider.DoneMoveMade(true);

    }
	public string GetAllMessageTurn(bool debug=false)
	{
        var text = "";
        foreach (CountryLider lider in this.CountryLiderList)
        {
            
			foreach (CommandLider commandLider in GetCommandLiderList(CountYear, lider.FlagId))
			{
                text += "\n" + commandLider.GetIncident().FullMessage(lider);
            }

        }
        Debug.Log("  in   = ");
        foreach (CountryLider lider in this.CountryLiderList)
        {
            foreach (CommandLider commandLider in GetCommandLiderList(CountYear, lider.FlagId))
			{
				Debug.Log(CountYear+" Town =  " + commandLider.GetIncident().Year+"  uid = "+ commandLider.GetIncident().Uid);
			}
        }


        return text;
    }
}
