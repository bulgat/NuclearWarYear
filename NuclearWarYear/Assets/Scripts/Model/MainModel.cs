using Assets.Scripts;
using Assets.Scripts.Model;
using Assets.Scripts.Model.AiTurn;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using Assets.Scripts.View;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
using static Assets.Scripts.Model.param.GlobalParam;

public class MainModel
{
	public List<CountryLider> CountryLiderList;
	public List<GameObject> CountryLiderPropagandaBuildingList;
	public List<CityModel> TownList;
	private List<CountryLider> FlagIdPlayerList { get; set; }
	public bool EndGame;
	private int CityIncrementId;
	private CountryLider CurrenPlayer { set; get; }
	public GameParam _gameParam = new GameParam(1);
	
	public List<CommandLider> MainStackCommandLiderList { private set; get; }
	public MainModel(List<GameObject> countryLiderPropagandaBuildingList) {
		InitModel(countryLiderPropagandaBuildingList);
		MainStackCommandLiderList = new List<CommandLider>();

	}

	private void InitModel(List<GameObject> countryLiderPropagandaBuildingList) {

		BindCity bindCity = new BindCity();
		this.TownList = bindCity.GetBindCity(this);

		this.CountryLiderPropagandaBuildingList = countryLiderPropagandaBuildingList;

		this.CountryLiderList = new BindLider().GetBindLider(this.TownList, _gameParam.CountYear, CountryLiderPropagandaBuildingList);


		this.FlagIdPlayerList = new List<CountryLider>();
		foreach (var item in this.CountryLiderList)
		{
			if (item.Player)
			{
				this.FlagIdPlayerList.Add(item);

			}
		}
		this.CurrenPlayer = this.FlagIdPlayerList[0];

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

			if (this.FlagIdPlayerList.Contains(lider))
			{

				if (lider.MoveMade == false)
				{
					return false;
				}

			}
		}

		return true;
	}
	public CountryLider GetCurrenFlagPlayer()
	{
		return this.CurrenPlayer;

	}
	public CountryLider GetCurrenPlayer()
	{
		return this.CurrenPlayer;

	}
	public CountryLider GetLiderOne(int FlagId)
	{
		return new LiderHelperOne().GetLiderOne(this.CountryLiderList, FlagId);

	}
	public void ChangeCurrentPlayer()
	{
		if (this.FlagIdPlayerList.Count > 1)
		{
			var index = this.FlagIdPlayerList.FindIndex(a => a == this.CurrenPlayer);
			index++;
			if (index >= this.FlagIdPlayerList.Count)
			{
				this.CurrenPlayer = this.FlagIdPlayerList[0];
				return;
			}
			this.CurrenPlayer = this.FlagIdPlayerList[index];


		}


	}
	public CountryLider GetCurrentPlayer()
	{
		return this.CurrenPlayer;

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

	public List<CityModel> GetAllTownList() {
		return this.TownList.Where(a => a.GetPopulation() > 0).ToList();
	}
	public List<CountryLider> GetCountryLiderList() {
		return this.CountryLiderList;
	}
	public List<CountryLider> GetFiendCountryLiderList()
	{
		return this.CountryLiderList.Where(a => a.FlagId != this.GetCurrentPlayer().FlagId).ToList();
	}
	public void SetPropagandPlayer(CountryLider FlagId) {
		int futureYear = _gameParam.CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId.FlagId);

		CityModel enemyTownCity = this.GetAllTownList().Where(a => a.FlagId != FlagId.FlagId).FirstOrDefault();
		CityModel myCity = this.GetAllTownList().Where(a => a.FlagId == FlagId.FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
            countryLider);
		ResetActionCheckVictory();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				countryLider.FlagId != GetCurrentPlayer().FlagId, countryLider, _gameParam.CountYear);

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
	public void SetBuildingPlayer(CountryLider countryLider) {
		int futureYear = _gameParam.CountYear + 1;


		//CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId.FlagId);

		CityModel enemyTownCity = this.GetAllTownList().Where(a => a.FlagId != countryLider.FlagId).FirstOrDefault();
		CityModel myCity = this.GetAllTownList().Where(a => a.FlagId == countryLider.FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Build,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
            countryLider);

		ResetActionCheckVictory();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				false, countryLider, _gameParam.CountYear);

		MainStackCommandLiderList.AddRange(
			new ActionCommandHelper().CreateAction(
			CountryLiderList,
			TownList,
            countryLider.FlagId,
			commandLider,
			this.GetCurrenPlayer(),
			futureYear,
			countryLider.FiendLider,
			commandLiderFortune));


	}
	public void SetDefencePlayer(CountryLider FlagId) {
		int futureYear = _gameParam.CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId.FlagId);

		CityModel enemyTownCity = this.GetAllTownList().Where(a => a.FlagId != FlagId.FlagId).FirstOrDefault();
		CityModel myCity = this.GetAllTownList().Where(a => a.FlagId == FlagId.FlagId).FirstOrDefault();

		CommandLider commandLider = new CommandLider(
			GlobalParam.TypeEvent.Defence,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, countryLider.FiendLider),
            countryLider);
		ResetActionCheckVictory();

		CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
				countryLider.FlagId != GetCurrentPlayer().FlagId, countryLider, _gameParam.CountYear);

		MainStackCommandLiderList.AddRange(new ActionCommandHelper().CreateAction(CountryLiderList, TownList,
			this.GetCurrenPlayer().FlagId,
			commandLider,
			this.GetCurrenPlayer(),
			futureYear,
			countryLider.FiendLider,
			commandLiderFortune));

	}
	public void SetCommandIncident(CountryLider FlagId, TypeEvent nameEvent)
	{

		int futureYear = _gameParam.CountYear + 1;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId.FlagId);

		CityModel enemyTownCity = this.GetAllTownList().Where(a => a.FlagId != FlagId.FlagId).FirstOrDefault();
		CityModel myCity = this.GetAllTownList().Where(a => a.FlagId == FlagId.FlagId).FirstOrDefault();

		CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(
			this.CountryLiderList,
			enemyTownCity.FlagId);

		List<CommandLider> commandLiderList = new List<CommandLider>();
		CommandLider commandLider = new CommandLider(
			nameEvent,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
			futureYear,
			new TargetCityModel(enemyTownCity, myCity, enemyliderPlayer),
            countryLider);

		commandLiderList.Add(commandLider);

		MainStackCommandLiderList.AddRange(commandLiderList);
		countryLider.SetCommandRealise(commandLider.IncidentCommand);

		ResetActionCheckVictory();


        new CreateCommandLider().GetCommandOneLiderList(
			countryLider,
			CountryLiderList,
			TownList,
			this.GetCurrenPlayer(),
			futureYear,
			this);
	}

	public void SetMisslePlayer(CountryLider FlagId, TypeEvent nameEvent) {
		SetCommandIncident(FlagId, nameEvent);
	}
	
	public void SetBomberPlayer(CountryLider FlagId, TypeEvent nameEvent) {

		SetCommandIncident(FlagId, nameEvent);
	}

	public void SetLiderTargetPlayer(int FlagId) {
		CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, this.GetCurrenPlayer().FlagId);
		liderPlayer.FlagIdAttack = FlagId;
	}

	public void SetWarheadMethodPlayer(int FlagId) {
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, this.GetCurrenPlayer().FlagId);
		countryLider.GetBomberFirst().SetDamage(countryLider.GetBomberFirst().GetDamage());
		countryLider.GetMissleFirst().SetDamage(countryLider.GetMissleFirst().GetDamage());

	}

	public Incident SatisfyOneLiderTurn(CountryLider country, Incident CommandIncident)
	{
        return new MainSetTurnLider().SatisfyEventOneLiderTurn(country, CountryLiderList,
			TownList, CommandIncident, _gameParam.CountYear, this);
	}
	public CommandLider GetCommandLider(int CountYear, CountryLider lider)
	{

		return this.MainStackCommandLiderList.FirstOrDefault(a => a.IncidentCommand.Year == CountYear && a.Lider.FlagId == lider.FlagId);
	}
    public List<int> GetYearLiderCount()
	{
		return this.MainStackCommandLiderList.GroupBy(a => a.IncidentCommand.Year).Select(a=>a.FirstOrDefault().IncidentCommand.Year).ToList();

    }


    public List<CommandLider> GetCommandLiderList(int CountYear, CountryLider FlagId)
	{
		return this.MainStackCommandLiderList.Where(a => a.IncidentCommand.Year == CountYear && a.Lider.FlagId == FlagId.FlagId).ToList();
	}

	private CityModel GetGuaranteeEnemyCity(int? CityId, int FlagId)
	{
		CityModel enemyTownCity = this.GetAllTownList().FirstOrDefault(a => a.GetId() == CityId);
		if (enemyTownCity == null)
		{
			enemyTownCity = this.GetAllTownList().FirstOrDefault(a => a.FlagId != FlagId);
		}
		return enemyTownCity;
	}

	public void SelectCityEnemyTargetPlayer(int? CityId, int FlagId) {

		CityModel selectCityTarget = null;
		CountryLider countryLider = new LiderHelperOne().GetLiderOne(this.CountryLiderList, FlagId);


		CityModel enemyTownCity = GetGuaranteeEnemyCity(CityId, FlagId);
		CityModel myCity = this.GetAllTownList().FirstOrDefault(a => a.FlagId == FlagId);


		CountryLider enemyliderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, enemyTownCity.FlagId);

		selectCityTarget = enemyTownCity;


		this.MainStackCommandLiderList.AddRange(new List<CommandLider>() {
			new CommandLider(GlobalParam.TypeEvent.Propaganda,
			countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
            _gameParam.CountYear,
			new TargetCityModel(enemyTownCity,myCity, enemyliderPlayer),
            countryLider) });

		CommandLider command0 = GetCommandLider(
            _gameParam.CountYear,
			countryLider);

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
	public void ResetActionCheckVictory() {


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

		return this.GetLiderOne(this.CurrenPlayer.FlagId).GetAllWeapon();
	}
	public void TurnAi()
	{

		_gameParam.IncrementYear();
		//CountYear++;
		ResetActionCheckVictory();


        new AICreateCommand().EstimationCreateCommandAiAll(
			CountryLiderList,
		   GetAllTownList(),
		   GetCurrenFlagPlayer(),
           _gameParam.CountYear,
		   this);

		//AddRain Event
		new CreateFalloutRain(this,CountryLiderList);

        //economic
        foreach (CountryLider lider in CountryLiderList)
        {

            foreach (CommandLider commandLider in GetCommandLiderList(_gameParam.CountYear, lider))
            {
					lider.RemoveWeapon(commandLider.IncidentCommand.Name);
            }

        }
		


        CountryLider countryLider = this.CurrenPlayer;
		countryLider.DoneMoveMade(true);

	}

	public bool VisibleCardLaunchWeapon()
	{
        CommandLider commandLider = GetCommandLider(_gameParam.CountYear, GetCurrenFlagPlayer());
		if (commandLider != null)
		{
			return (commandLider.GetVisibleMissle() == true || commandLider.GetVisibleBomber() == true)==false;

        }
		return true;
    }

	public TurnFinally TurnFinality(){
        TurnFinally turnFinally = new TurnFinally();
        StringBuilder printMessage = new StringBuilder();

        CountryLider liderPlayerOne = new LiderHelperOne().GetLiderOne(this.CountryLiderList, GetCurrenFlagPlayer().FlagId);
		CommandLider commandLider = GetCommandLider(_gameParam.CountYear, liderPlayerOne);

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
            new CreateAttackMissle().SetAttackMisslePlayer(this,GetCurrenFlagPlayer().FlagId, turnFinally);
        }


        return turnFinally;
    }

	public List<string> GetAllMessageTurn(bool debug=false)
	{

  


        var textList = new List<string>();
		foreach (var year in GetYearLiderCount())
		{

            var text = "";
			foreach (CountryLider lider in this.CountryLiderList)
			{

				foreach (CommandLider commandLider in GetCommandLiderList(year, lider))
				{
					text += "\n" + commandLider.IncidentCommand.FullMessage(lider);
				}

			}
			textList.Add(text);
		}
        Debug.Log("0980 DEAD BOMB "+ textList.Count);

        return textList;
    }
}
