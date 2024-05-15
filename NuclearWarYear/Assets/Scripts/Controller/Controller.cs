using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller 
{
	private MainModel _mainModel;
	public enum Command
	{
		Propaganda,
		Building,
		Defence,
		Missle,
		Bomber,
		AttackBomber,
		AttackMissle,
		LiderTargetPlayer,
		Warhead,
		TotalTurn,
		SelectCityEnemyTargetPlayer,
		ResetSelectCityEnemyTargetPlayer,
		DoneMoveMadeCurrentPlayer,
		ChangeCurrentPlayer
	}
	public Controller(MainModel MainModel) {
		_mainModel = MainModel;
	}
	public void SendCommand(EventController eventController) {

        if (eventController.NameCommand == Command.ChangeCurrentPlayer)
		{
			_mainModel.ChangeCurrentPlayer();
			return;
		}
		if (eventController.NameCommand == Command.DoneMoveMadeCurrentPlayer)
		{
			_mainModel.DoneMoveMadeCurrentPlayer();
			return;
		}
		
		if (eventController.NameCommand == Command.Propaganda){

			_mainModel.SetPropagandPlayer(eventController.EventSend.FlagId);
			return;
			//_mainModel
		}
		if (eventController.NameCommand == Command.Building){

			_mainModel.SetBuildingPlayer(eventController.EventSend.FlagId);
			return;
			//_mainModel
		}
		if (eventController.NameCommand == Command.Defence){

			_mainModel.SetDefencePlayer(eventController.EventSend.FlagId);
			return;

		}
		/*
		if (eventController.NameCommand == Command.Missle){
			_mainModel.SetMisslePlayer(eventController.EventSend.FlagId, eventController.EventSend.Id);
			return;
		}
		*/
		if (eventController.NameCommand == Command.AttackMissle){
			_mainModel.SetAttackMisslePlayer(eventController.EventSend.FlagId);
			return;
		}
		if (eventController.NameCommand == Command.Bomber){
	
			_mainModel.SetBomberPlayer(eventController.EventSend.FlagId, eventController.EventSend.Id);
			return;
		}
		if (eventController.NameCommand == Command.AttackBomber){
			
			_mainModel.SetAttackBomberPlayer(eventController.EventSend.FlagId);
			return;
		}
		if (eventController.NameCommand == Command.LiderTargetPlayer){
	
			_mainModel.SetLiderTargetPlayer(eventController.EventSend.FlagId);
			return;
		}
		if (eventController.NameCommand == Command.Warhead){
	
			_mainModel.SetWarheadMethodPlayer(eventController.EventSend.FlagId);
			return;
		}
		if (eventController.NameCommand == Command.TotalTurn){

			_mainModel.TotalTurn(eventController.EventSend.FlagId);
			return;
		}
		if (eventController.NameCommand == Command.SelectCityEnemyTargetPlayer){

			_mainModel.SelectCityEnemyTargetPlayer(eventController.EventSend.CityId);
			return;
		}
		//ResetSelectCityEnemyTargetPlayer
		if (eventController.NameCommand == Command.ResetSelectCityEnemyTargetPlayer){

			_mainModel.ResetSelectCityEnemyTargetPlayer(eventController.EventSend.CityId);
			return;
		}
		throw new System.Exception("Not Command controller"); 
	}
	public void SetMissle(int FlagId,int Id)
	{
            _mainModel.SetMisslePlayer(FlagId, Id);
            return;
    }
	public void TurnCreateCommand()
	{
        new AICreateCommand().EstimationSetCommandAi(_mainModel.ResetAction, _mainModel.CountryLiderList,
           _mainModel.GetTownList(), _mainModel.GetCurrenFlagPlayer(), _mainModel.GetCurrenFlagPlayer());
    }
    public Incident TurnSatisfyOneLider(int flagId, Incident CommandIncident)
	{
			return _mainModel.SatisfyOneLiderTurn(flagId, CommandIncident);	
	}
    public void ReleasePopulationEvent(Incident CommandIncident)
    {
		_mainModel.ReleasePopulationEvent(CommandIncident);
    }
}
