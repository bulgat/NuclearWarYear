using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    private MainModel _mainModel;

    public Controller(MainModel MainModel)
    {
        _mainModel = MainModel;
    }

    public void LiderTargetPlayer(int FlagId)
    {
        _mainModel.SetLiderTargetPlayer(FlagId);
    }
    public void Defence(CountryLider FlagId)
    {
        _mainModel.SetDefencePlayer(FlagId);
    }

    public void Building(CountryLider FlagId)
    {
        _mainModel.SetBuildingPlayer(FlagId);

    }

    public void Propaganda(CountryLider FlagId)
    {
        _mainModel.SetPropagandPlayer(FlagId);
    }

    public void SelectCityEnemyTargetPlayer(int? CityId, int FlagId)
    {
        _mainModel.SelectCityEnemyTargetPlayer(CityId, FlagId);
    }

    public void ResetSelectCityEnemyTargetPlayer()
    {
        _mainModel.ResetSelectCityEnemyTargetPlayer();
    }


    public void ChangeCurrentPlayer()
    {
        _mainModel.ChangeCurrentPlayer();
    }
  
    public void SetMissle(CountryLider FlagId, GlobalParam.TypeEvent nameEvent)
    {
        _mainModel.SetMisslePlayer(FlagId, nameEvent);
        return;
    }
    public void SetBomber(CountryLider FlagId, GlobalParam.TypeEvent nameEvent)
    {
        _mainModel.SetBomberPlayer(FlagId, nameEvent);
        return;
    }
    public void TurnAi()
    {
        _mainModel.TurnAi();
    }
    public TurnFinally TurnFinality()
    {
        return _mainModel.TurnFinality();
    }

    public Incident TurnSatisfyOneLider(CountryLider lider, Incident CommandIncident)
    {
        return _mainModel.SatisfyOneLiderTurn(lider, CommandIncident);
    }
    public void ReleasePopulationEvent(Incident CommandIncident)
    {
        _mainModel.ReleasePopulationEvent(CommandIncident);
    }
}
