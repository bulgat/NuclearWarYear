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
		TurnSatisfyOneLider,
		SelectCityEnemyTargetPlayer,
		ResetSelectCityEnemyTargetPlayer
	}
	public Controller(MainModel MainModel) {
		_mainModel = MainModel;
	}
	public void SendCommand(EventController eventController) {
		
		if (eventController.NameCommand == Command.Propaganda){

			_mainModel.SetPropagandPlayer(eventController.EventSend.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Building){

			_mainModel.SetBuildingPlayer(eventController.EventSend.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Defence){

			_mainModel.SetDefencePlayer(eventController.EventSend.FlagId);
			
		}
		if (eventController.NameCommand == Command.Missle){
			_mainModel.SetMisslePlayer(eventController.EventSend.FlagId, eventController.EventSend.Id);
			
		}
		if (eventController.NameCommand == Command.AttackMissle){
			_mainModel.SetAttackMisslePlayer(eventController.EventSend.FlagId);
			
		}
		if (eventController.NameCommand == Command.Bomber){
	
			_mainModel.SetBomberPlayer(eventController.EventSend.FlagId, eventController.EventSend.Id);
			
		}
		if (eventController.NameCommand == Command.AttackBomber){
			
			_mainModel.SetAttackBomberPlayer(eventController.EventSend.FlagId);
			
		}
		if (eventController.NameCommand == Command.LiderTargetPlayer){
	
			_mainModel.SetLiderTargetPlayer(eventController.EventSend.FlagId);

		}
		if (eventController.NameCommand == Command.Warhead){
	
			_mainModel.SetWarheadMethodPlayer(eventController.EventSend.FlagId);

		}
		if (eventController.NameCommand == Command.TotalTurn){

			_mainModel.ReconTotalTurn(eventController.EventSend.FlagId);
			
		}
		//TurnSatisfyOneLider
		if (eventController.NameCommand == Command.TurnSatisfyOneLider)
		{

			_mainModel.SatisfyOneLiderTurn(eventController.EventSend.FlagId);

		}

		if (eventController.NameCommand == Command.SelectCityEnemyTargetPlayer){

			_mainModel.SelectCityEnemyTargetPlayer(eventController.EventSend.CityId);
			//_mainModel
		}
		//ResetSelectCityEnemyTargetPlayer
		if (eventController.NameCommand == Command.ResetSelectCityEnemyTargetPlayer){

			_mainModel.ResetSelectCityEnemyTargetPlayer(eventController.EventSend.CityId);
			
		}
	}

}
