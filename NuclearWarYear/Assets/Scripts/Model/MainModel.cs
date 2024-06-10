using Assets.Scripts;
using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model.turnEvent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	private int CountYear;
	public MainModel(List<GameObject> countryLiderPropagandaBuildingList) {
		InitModel(countryLiderPropagandaBuildingList);
	}

	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList) {



		this.CountryLiderPropagandaBuildingList = countryLiderPropagandaBuildingList;
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

		this.CountryLiderList = new List<CountryLider>();

		ParamLider paramLider = new ParamLider();


		//List<ScenarioLider> scenarioLider_ar = paramLider.ScenarioLider_ar;

		this.CountryLiderList = new List<CountryLider>();
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() { new DictionaryEssence().GetIncident(1), new DictionaryEssence().GetIncident(4) }, CountryLiderPropagandaBuildingList[0], TownList, GlobalParam.ParamLiderList[0], 1));
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() { new DictionaryEssence().GetIncident(1), new DictionaryEssence().GetIncident(4) }, CountryLiderPropagandaBuildingList[1], TownList, GlobalParam.ParamLiderList[1], 2));
		this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() { new DictionaryEssence().GetIncident(1), new DictionaryEssence().GetIncident(4) }, CountryLiderPropagandaBuildingList[2], TownList, GlobalParam.ParamLiderList[2], 3));


		if (SettingPlayer.TwoPlayerGame)
		{
			this.CountryLiderList.Add(new CountryLider(true, new List<IWeapon>() { new DictionaryEssence().GetIncident(1), new DictionaryEssence().GetIncident(4) }, CountryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
		} else
		{
			this.CountryLiderList.Add(new CountryLider(false, new List<IWeapon>() { new DictionaryEssence().GetIncident(1), new DictionaryEssence().GetIncident(4) }, CountryLiderPropagandaBuildingList[3], TownList, GlobalParam.ParamLiderList[3], 4));
		}

		this.CountryLiderList.Add(new CountryLider(true, new List<IWeapon>() { new DictionaryEssence().BuildIncident(GlobalParam.TypeEvent.Missle,CountYear),
            new DictionaryEssence().BuildIncident(GlobalParam.TypeEvent.HeavyMissle, CountYear),
			new DictionaryEssence().GetIncident (4),
			new DictionaryEssence().GetIncident(5),
			new DictionaryEssence().GetIncident(6),
			new DictionaryEssence().GetIncident(7)},
			CountryLiderPropagandaBuildingList[4], TownList, GlobalParam.ParamLiderList[4], 5));

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
			Lider._RelationShip.InitRelationContry(this.CountryLiderList);

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
	private int GetIncrementCityId()
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

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.Propaganda, this.GetCurrenPlayer().FlagId, 0, CountYear));


	}
	public void SetBuildingPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.Build, this.GetCurrenPlayer().FlagId, 0, CountYear));

		new AICreateCommand().EstimationSetCommandAi(ResetAction,
			CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetDefencePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.Defence, this.GetCurrenPlayer().FlagId, 0, CountYear));

		new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetCommandIncident(int FlagId, TypeEvent nameEvent)
	{
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		List<CommandLider> commandLiderList = new List<CommandLider>();
		CommandLider commandLider = new CommandLider(nameEvent,CountYear);
			//= new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.Missle, this.GetCurrenPlayer().FlagId, MissleId);
        commandLiderList.Add(commandLider);

        countryLider.SetCommandLider(commandLiderList);
		countryLider.SetCommandRealise(commandLider.GetIncident());

        Debug.Log(commandLiderList.Count + "  & == "+ commandLider.GetIncident().GetName() + " SetMisslePlayer = " + GlobalParam.TypeEvent.Missle);
        Debug.Log(nameEvent+"   countryLider = " + countryLider.ReleaseCommandList.Count );
		new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}

	public void SetMisslePlayer(int FlagId, TypeEvent nameEvent) {
		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackMisslePlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.AttackMissle, this.GetCurrenPlayer().FlagId, 0, CountYear));

		new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
	}
	public void SetBomberPlayer(int FlagId, TypeEvent nameEvent) {
		/*
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.Bomber, this.GetCurrenPlayer().FlagId, BomberId));

		new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId);
        */
		SetCommandIncident(FlagId, nameEvent);
    }
	public void SetAttackBomberPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		countryLider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, GlobalParam.TypeEvent.AttackBomber, this.GetCurrenPlayer().FlagId, 0,CountYear));

		new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList, TownList, this.GetCurrenPlayer().FlagId, this.GetCurrenPlayer().FlagId, CountYear);
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
			new MainSetTurnLider().SatisfyEventOneLiderTurn(lider.FlagId, this.CountryLiderList, this.TownList, lider.GetCommandLiderFirst().GetIncident());
		}
	}
	public Incident SatisfyOneLiderTurn(int FlagId, Incident CommandIncident)
	{
		return new MainSetTurnLider().SatisfyEventOneLiderTurn(FlagId, CountryLiderList, TownList, CommandIncident);
	}


	public void SelectCityEnemyTargetPlayer(int CityId,int LiderFlagId) {
		CityModel selectCityTarget = null;
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, LiderFlagId);

        CityModel enemyTownCity = this.TownList.FirstOrDefault(a=>a.GetId() == CityId);

        CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, LiderFlagId);

 
				selectCityTarget = enemyTownCity;

                liderPlayer.GetCommandLiderFirst().SetTargetCity(new TargetCityModel(enemyTownCity, enemyliderPlayer));
                liderPlayer.SetTargetCitySelectPlayer(new TargetCityModel(enemyTownCity, enemyliderPlayer));



		if (this.GetCurrenPlayer().FlagId != selectCityTarget.FlagId) {

		} else {
            // auto Set attack
            CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, this.GetCurrenPlayer().FlagId);
            CityModel targetCityPlayer = new TargetHelper().GetTargetRandom(CountryLiderList, this.GetCurrenPlayer().FlagId, false, TownList, liderPlayer, fiendLider1);

            liderPlayer.SetTargetCitySelectPlayer(new TargetCityModel(targetCityPlayer, fiendLider1));
        }


	}
	public void ResetSelectCityEnemyTargetPlayer(int CityId) {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.SetTargetCitySelectPlayer(null);
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
		missleList.Add(new DictionaryEssence().GetIncident(8));
		missleList.Add(new DictionaryEssence().GetIncident(9));
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
        new AICreateCommand().EstimationSetCommandAi(ResetAction, CountryLiderList,
		   GetTownList(), GetCurrenFlagPlayer(), GetCurrenFlagPlayer(), CountYear);
	}
	public string GetAllMessageTurn()
	{
        var text = "";
        foreach (CountryLider lider in this.CountryLiderList)
        {

            foreach (CommandLider commandLider in lider.GetCommandLider())
            {

                //StartCoroutine(TurnOneLider(lider, indexLiderTime, commandLider.GetIncident()));
                //indexLiderTime++;
                text += "\n"+commandLider.GetIncident().FullMessage(lider);
            }

        }
		return text;
    }
}
