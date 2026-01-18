using Assets.Scripts;
using Assets.Scripts.Model;
using Assets.Scripts.Model.AiTurn;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model.testReport;
using Assets.Scripts.Model.turnEvent;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEditor.VersionControl;
using UnityEngine;
using static Assets.Scripts.Model.param.GlobalParam;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class MainModel
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	public List<CityModel> TownList;
	private List<int> FlagIdPlayerList { get; set; }
	public bool EndGame;
	private int CityIncrementId;
	private int CurrenPlayerFlag { set; get; }
	public int CountYear { private set; get; }
	public List<CommandLider> MainStackCommandLiderList { private set; get; }
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
		return this.TownList.Where(a => a.GetPopulation() > 0).ToList();
	}
	public List<CountryLider> GetCountryLiderList() {
		return this.CountryLiderList;
	}
	public List<CountryLider> GetFiendCountryLiderList()
	{
		return this.CountryLiderList.Where(a => a.FlagId != this.GetCurrentPlayerFlag()).ToList();
	}
	public void SetPropagandPlayer(int FlagId) {
		int futureYear = CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
		CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
			FlagId);
		ResetAction();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);

		List<CommandLider> сommandLiderList = new ActionCommandHelper().CreateAction(
			CountryLiderList,
			TownList,
			this.GetCurrenPlayer().FlagId,
			commandLider,
			this.GetCurrenPlayer(),
			futureYear,
			countryLider.FiendLider,
			commandLiderFortune);


		MainStackCommandLiderList.AddRange(сommandLiderList);


	}
	public void SetBuildingPlayer(int FlagId) {
		int futureYear = CountYear + 1;


		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
		CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Build,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
			FlagId);

		ResetAction();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				false, countryLider, CountYear);

		MainStackCommandLiderList.AddRange(
			new ActionCommandHelper().CreateAction(
			CountryLiderList,
			TownList,
			FlagId,
			commandLider,
			this.GetCurrenPlayer(),
			futureYear,
			countryLider.FiendLider,
			commandLiderFortune));


	}
	public void SetDefencePlayer(int FlagId) {
		int futureYear = CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
		CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Defence,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
			FlagId);
		ResetAction();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				countryLider.FlagId != GetCurrentPlayerFlag(), countryLider, CountYear);

		MainStackCommandLiderList.AddRange(new ActionCommandHelper().CreateAction(CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId,
			commandLider,
			this.GetCurrenPlayer(),
			futureYear,
			countryLider.FiendLider,
			commandLiderFortune));

	}
	public void SetCommandIncident(int FlagId, TypeEvent nameEvent)
	{

		int futureYear = CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);

		CityModel enemyTownCity = this.GetTownList().Where(a => a.FlagId != FlagId).FirstOrDefault();
		CityModel myCity = this.GetTownList().Where(a => a.FlagId == FlagId).FirstOrDefault();

		CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(
			this.CountryLiderList,
			enemyTownCity.FlagId);

		List<CommandLider> commandLiderList = new List<CommandLider>();
		CommandLider commandLider = new CommandLider(
			nameEvent,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, enemyliderPlayer),
			FlagId);

		commandLiderList.Add(commandLider);

		MainStackCommandLiderList.AddRange(commandLiderList);
		countryLider.SetCommandRealise(commandLider.IncidentCommand);

		new CreateCommandLider().CommandOneLider(
			countryLider,
			ResetAction,
			CountryLiderList,
			TownList,
			this.GetCurrenPlayer().FlagId,
			this.GetCurrenPlayer().FlagId,
			futureYear,
			this);
	}

	public void SetMisslePlayer(int FlagId, TypeEvent nameEvent) {
		SetCommandIncident(FlagId, nameEvent);
	}
	
	public void SetBomberPlayer(int FlagId, TypeEvent nameEvent) {

		SetCommandIncident(FlagId, nameEvent);
	}

	public void SetLiderTargetPlayer(int FlagId) {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.FlagIdAttack = FlagId;
	}

	public void SetWarheadMethodPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, this.GetCurrenPlayer().FlagId);
		countryLider.GetBomber().SetDamage(countryLider.GetBomber().GetDamage());
		countryLider.GetMissleFirst().SetDamage(countryLider.GetMissleFirst().GetDamage());

	}

	public Incident SatisfyOneLiderTurn(int FlagId, Incident CommandIncident)
	{
        CountryLider lider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        return new MainSetTurnLider().SatisfyEventOneLiderTurn(lider, CountryLiderList,
			TownList, CommandIncident, CountYear, this);
	}
	public CommandLider GetCommandLider(int CountYear, int FlagId)
	{

		return this.MainStackCommandLiderList.FirstOrDefault(a => a.IncidentCommand.Year == CountYear && a.LiderId == FlagId);
	}
	public List<CommandLider> GetCommandLiderList(int CountYear, int FlagId)
	{
		return this.MainStackCommandLiderList.Where(a => a.IncidentCommand.Year == CountYear && a.LiderId == FlagId).ToList();
	}

	private CityModel GetGuaranteeEnemyCity(int? CityId, int FlagId)
	{
		CityModel enemyTownCity = this.GetTownList().FirstOrDefault(a => a.GetId() == CityId);
		if (enemyTownCity == null)
		{
			enemyTownCity = this.GetTownList().FirstOrDefault(a => a.FlagId != FlagId);
		}
		return enemyTownCity;
	}

	public void SelectCityEnemyTargetPlayer(int? CityId, int FlagId) {

		CityModel selectCityTarget = null;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(this.CountryLiderList, FlagId);


		CityModel enemyTownCity = GetGuaranteeEnemyCity(CityId, FlagId);
		CityModel myCity = this.GetTownList().FirstOrDefault(a => a.FlagId == FlagId);


		CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

		selectCityTarget = enemyTownCity;


		this.MainStackCommandLiderList.AddRange(new List<CommandLider>() {
			new CommandLider(GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			CountYear,
			new TargetCityModel(enemyTownCity,myCity, enemyliderPlayer),
			FlagId) });

		CommandLider command0 = GetCommandLider(
			CountYear,
			countryLider.FlagId);

		countryLider.SetTargetCity(new TargetCityModel(enemyTownCity, myCity, enemyliderPlayer));



		if (this.GetCurrenPlayer().FlagId != selectCityTarget.FlagId) {

		} else {
			// auto Set attack

			CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, this.GetCurrenPlayer().FlagId);
			CityModel targetCityPlayer = new TargetHelper().GetRandomCity(TownList, countryLider, this.GetCurrenPlayer().FlagId, false);

			new TargetHelper().SetTargetBuilding(
			   CountryLiderList, fiendLider1, true, myCity, targetCityPlayer);

			countryLider.SetTargetCity(new TargetCityModel(targetCityPlayer, myCity, fiendLider1));
		}


	}
	public void ResetSelectCityEnemyTargetPlayer() {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.ResetTargetCity();

	}
	public void ResetAction() {


		CheckVictory checkVictory = new CheckVictory(CountryLiderList, this.TownList);
		EndGame = checkVictory.GetEndGame();

	}
	public void ReleasePopulationEvent(Incident CommandIncident)
	{
		new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.MyCity, CommandIncident.PopulationEvent.MyPopulation);
		new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.FiendCity, CommandIncident.PopulationEvent.FiendPopulation);
	}
	public List<IWeapon> GetEternalWeapon()
	{

		List<IWeapon> missleList = new List<IWeapon>();
		missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Build));
		missleList.Add(new DictionaryEssence().GetIncident(GlobalParam.TypeEvent.Propaganda));
		return missleList;
	}
	public List<Incident> GetCurrentWeapon()
	{

		return this.GetLiderOne(this.CurrenPlayerFlag).GetAllWeapon();
	}
	public void TurnAi()
	{
        Debug.Log("0055    CommandIncident   CountYear = " + CountYear);

        CountYear++;
		

		new AICreateCommand().EstimationCreateCommandAiAll(
			ResetAction,
			CountryLiderList,
		   GetTownList(),
		   GetCurrenFlagPlayer(),
		   GetCurrenFlagPlayer(),
		   CountYear,
		   this);

        //economic
        foreach (CountryLider lider in CountryLiderList)
        {

            foreach (CommandLider commandLider in GetCommandLiderList(CountYear, lider.FlagId))
            {
					lider.RemoveWeapon(commandLider.IncidentCommand.Name);
            }

        }

        CountryLider countryLider = this.GetLiderOne(this.CurrenPlayerFlag);
		countryLider.DoneMoveMade(true);

	}
	public TurnFinally TurnFinality(){
        TurnFinally turnFinally = new TurnFinally();
        StringBuilder printMessage = new StringBuilder();

        CountryLider liderPlayerOne = new LiderHelperOne().GetLiderOne(this.CountryLiderList, GetCurrenFlagPlayer());
		CommandLider commandLider = GetCommandLider(CountYear, liderPlayerOne.FlagId);

        if (commandLider != null)
        {

            if (commandLider.GetVisibleMissle())
            {

                var cityTarget = liderPlayerOne.TargetCitySelectPlayer;
                if (cityTarget == null)
                {
                    printMessage.Append("\n Ready. Not target for missle. Select Target!");
                }
                else
                {
                    printMessage.Append("\n Ready. Select target for missle");
                }
				turnFinally.TypeAttack = 1;
				turnFinally.Attack = true;
				turnFinally.Missle = true;
                turnFinally.OldIncident = commandLider.IncidentCommand;
            }
 
            if (commandLider.GetVisibleBomber())
            {
                var cityTarget = liderPlayerOne.TargetCitySelectPlayer;
                if (cityTarget == null)
                {

                    printMessage.Append("\n not target. Select Target!");
                }
                else
                {

                    printMessage.Append("\n select target bomber");
                }

				turnFinally.TypeAttack = 0;
                turnFinally.Attack = true;
                turnFinally.Missle = false;
                turnFinally.OldIncident = commandLider.IncidentCommand;
            }
        }
        turnFinally.Message = printMessage.ToString();

        if (turnFinally.Attack)
        {
            new CreateAttackMissle().SetAttackMisslePlayer(this,GetCurrenFlagPlayer(), turnFinally);
        }


        return turnFinally;
    }

	public string GetAllMessageTurn(bool debug=false)
	{
        var text = "";
        foreach (CountryLider lider in this.CountryLiderList)
        {
            
			foreach (CommandLider commandLider in GetCommandLiderList(CountYear, lider.FlagId))
			{
                text += "\n" + commandLider.IncidentCommand.FullMessage(lider);
            }

        }

        return text;
    }
}
