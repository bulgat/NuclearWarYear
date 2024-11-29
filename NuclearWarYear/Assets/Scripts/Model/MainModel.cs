using Assets.Scripts;
using Assets.Scripts.Model;
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

public class MainModel
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	public List<CityModel> TownList;
	private List<int> FlagIdPlayerList { get; set; }
	public bool _endGame;
	private int CityIncrementId;
	private int CurrenPlayerFlag { set; get; }
	public int CountYear { private set; get; }
	public MainModel(List<GameObject> countryLiderPropagandaBuildingList) {
		InitModel(countryLiderPropagandaBuildingList);
	}

	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList) {

        BindCity bindCity = new BindCity();
		this.TownList = bindCity.GetBindCity(this);

        this.CountryLiderPropagandaBuildingList = countryLiderPropagandaBuildingList;
		/*
		this.TownList = new List<CityModel>();
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
		*/
		//this.CountryLiderList = new List<CountryLider>();

        this.CountryLiderList = new BindLider().GetBindLider(this.TownList, CountYear, CountryLiderPropagandaBuildingList);

        //ParamLider paramLider = new ParamLider();

        //this.CountryLiderList = new List<CountryLider>();
		/*
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() 
		{ new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) 
		},
			CountryLiderPropagandaBuildingList[0], TownList, GlobalParam.ParamLiderList[0], 1));
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() 
		{
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) 
		}, CountryLiderPropagandaBuildingList[1], TownList, GlobalParam.ParamLiderList[1], 2));
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() {
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) 
		}, CountryLiderPropagandaBuildingList[2], TownList, GlobalParam.ParamLiderList[2], 3));


		if (SettingPlayer.TwoPlayerGame)
		{
			this.CountryLiderList.Add(new CountryLider(true, new List<IWeapon>() { new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
				new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) }, CountryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
		} else
		{
			this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() { new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Missle),
				new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Bomber) }, CountryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
		}

		this.CountryLiderList.Add(new CountryLider(true, new List<IWeapon>() { new DictionaryEssence().BuildIncident(GlobalParam.TypeEvent.Missle,CountYear),
            new DictionaryEssence().BuildIncident(GlobalParam.TypeEvent.HeavyMissle, CountYear),
			new DictionaryEssence().GetIncident (GlobalParam.TypeEvent.Bomber),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.HeavyBomber),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Defence),
			new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Defence)},
			CountryLiderPropagandaBuildingList[4], TownList, GlobalParam.ParamLiderList[4], 5));
		*/

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
	public void DoneMoveMadeCurrentPlayer()
	{
		CountryLider countryLider = this.GetLiderOne(this.CurrenPlayerFlag);
		countryLider.DoneMoveMade(true);

	}
	public void ResetDoneMoveAll()
	{
		foreach (var item in this.CountryLiderList)
		{
			item.DoneMoveMade(false);
		}
	}

	public List<CityModel> GetTownList() {
		return this.TownList;
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

        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, countryLider.FiendLider));
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        countryLider.AddCommandLiderList(new SwitchActionHelper().SwitchAction(
			CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider,
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, commandLiderFortune));


	}
	public void SetBuildingPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Build,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, countryLider.FiendLider));
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        countryLider.AddCommandLiderList(new SwitchActionHelper().SwitchAction( CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider, 
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, 
			commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction,
			CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetDefencePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.Defence,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, countryLider.FiendLider));
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
                countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        countryLider.AddCommandLiderList(new SwitchActionHelper().SwitchAction(CountryLiderList, TownList, 
			this.GetCurrenPlayer().FlagId, commandLider,
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider,  commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetCommandIncident(int FlagId, TypeEvent nameEvent)
	{
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != FlagId).FirstOrDefault();
        CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

        List<CommandLider> commandLiderList = new List<CommandLider>();
		CommandLider commandLider = new CommandLider(nameEvent,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, enemyliderPlayer));

        commandLiderList.Add(commandLider);

        countryLider.AddCommandLiderList(commandLiderList);
		countryLider.SetCommandRealise(commandLider.GetIncident());

        new AICreateCommand().SetCommandOneLider(countryLider,ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
    }

	public void SetMisslePlayer(int FlagId, TypeEvent nameEvent) {
		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackMisslePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != countryLider.FiendLider.FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.AttackMissle,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,new TargetCityModel(enemyTownCity, countryLider.FiendLider));
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
               countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        countryLider.AddCommandLiderList(new SwitchActionHelper().SwitchAction(CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider, 
			this.GetCurrenPlayer(), CountYear,
			countryLider.FiendLider, commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetBomberPlayer(int FlagId, TypeEvent nameEvent) {

		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackBomberPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        CityModel enemyTownCity = this.TownList.Where(a => a.FlagId != countryLider.FiendLider.FlagId).FirstOrDefault();

        CommandLider commandLider = new CommandLider(GlobalParam.TypeEvent.AttackBomber,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear, new TargetCityModel(enemyTownCity,countryLider.FiendLider));
        ResetAction();
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
               countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);
        countryLider.AddCommandLiderList(new SwitchActionHelper().SwitchAction( CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId, commandLider,
            this.GetCurrenPlayer(), CountYear,
            countryLider.FiendLider, commandLiderFortune));

		new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetLiderTargetPlayer(int FlagId) {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.FlagIdAttack = FlagId;
	}
	//WarheadMethod
	public void SetWarheadMethodPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, this.GetCurrenPlayer().FlagId);
		countryLider.GetBomber().SetDamage(countryLider.GetBomber().GetDamage());
		countryLider.GetMissle().SetDamage(countryLider.GetMissle().GetDamage());

	}
	public void TotalTurn(int FlagId) {

		foreach (CountryLider lider in this.CountryLiderList) {
			new MainSetTurnLider().SatisfyEventOneLiderTurn(lider.FlagId, this.CountryLiderList, this.TownList, lider.GetCommandLiderOne(CountYear).GetIncident(), CountYear);
		}
	}
	public Incident SatisfyOneLiderTurn(int FlagId, Incident CommandIncident)
	{
		return new MainSetTurnLider().SatisfyEventOneLiderTurn(FlagId, CountryLiderList, TownList, CommandIncident, CountYear);
	}


	public void SelectCityEnemyTargetPlayer(int? CityId,int LiderFlagId) {
		CityModel selectCityTarget = null;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(this.CountryLiderList, LiderFlagId);

        CityModel enemyTownCity = this.TownList.Where(a=>a.FlagId!=LiderFlagId).FirstOrDefault();

        CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

 
				selectCityTarget = enemyTownCity;
  
        countryLider.AddCommandLiderList(new List<CommandLider>() { new CommandLider(GlobalParam.TypeEvent.Propaganda, countryLider._RelationFeind.GetHighlyHatredLiderRandom(), CountYear, new TargetCityModel(enemyTownCity, enemyliderPlayer)) });

        CommandLider command = countryLider.GetCommandLiderOne(CountYear);

        //countryLider.GetCommandLiderOne(CountYear).SetTargetCity(new TargetCityModel(enemyTownCity, enemyliderPlayer));
        countryLider.SetTargetCity(new TargetCityModel(enemyTownCity, enemyliderPlayer));



		if (this.GetCurrenPlayer().FlagId != selectCityTarget.FlagId) {

		} else {
            // auto Set attack
            
            CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, this.GetCurrenPlayer().FlagId);
            CityModel targetCityPlayer = new TargetHelper().GetTargetRandom(CountryLiderList, this.GetCurrenPlayer().FlagId, false, TownList, countryLider, fiendLider1);

            countryLider.SetTargetCity(new TargetCityModel(targetCityPlayer, fiendLider1));
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
	public List<IWeapon> GetStaticWeapon()
	{
		List<IWeapon> missleList = new List<IWeapon>();
		missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Build));
		missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Defence));
		return missleList;
	}
	public List<IWeapon> GetCurrentWeapon()
	{
		List<IWeapon> missleList = new List<IWeapon>();
		missleList.AddRange(GetCurrenPlayer().GetDefenceWeapon());
		missleList.AddRange(GetCurrenPlayer().GetMissleList());
		return missleList;
	}
    public void TurnAi()
	{
		CountYear++;
        new AICreateCommand().EstimationSetCommandAiAll(ResetAction, CountryLiderList,
		   GetTownList(), GetCurrenFlagPlayer(), GetCurrenFlagPlayer(), CountYear);
	}
	public string GetAllMessageTurn()
	{
        var text = "";
        foreach (CountryLider lider in this.CountryLiderList)
        {

            foreach (CommandLider commandLider in lider.GetStackCommandLider(CountYear))
            {

                //StartCoroutine(TurnOneLider(lider, indexLiderTime, commandLider.GetIncident()));
                //indexLiderTime++;
                text += "\n"+commandLider.GetIncident().FullMessage(lider);
            }

        }
		return text;
    }
}
