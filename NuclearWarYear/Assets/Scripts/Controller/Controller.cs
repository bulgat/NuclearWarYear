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
		ResetSelectCityEnemyTargetPlayer
	}
	public Controller(MainModel MainModel) {
		_mainModel = MainModel;
	}
	public void SendCommand(EventController eventController) {
		
		if (eventController.NameCommand == Command.Propaganda){

			_mainModel.SetPropagandPlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Building){

			_mainModel.SetBuildingPlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Defence){

			_mainModel.SetDefencePlayer(eventController.FlagId);
			
		}
		if (eventController.NameCommand == Command.Missle){
			_mainModel.SetMisslePlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.AttackMissle){
			_mainModel.SetAttackMisslePlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Bomber){
	
			_mainModel.SetBomberPlayer(eventController.FlagId);
			
		}
		if (eventController.NameCommand == Command.AttackBomber){
			
			_mainModel.SetAttackBomberPlayer(eventController.FlagId);
			
		}
		if (eventController.NameCommand == Command.LiderTargetPlayer){
	
			_mainModel.SetLiderTargetPlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.Warhead){
	
			_mainModel.SetWarheadMethodPlayer(eventController.FlagId);
			//_mainModel
		}
		if (eventController.NameCommand == Command.TotalTurn){

			_mainModel.ReconTotalTurn(eventController.FlagId);
			
		}
		if (eventController.NameCommand == Command.SelectCityEnemyTargetPlayer){

			_mainModel.SelectCityEnemyTargetPlayer(eventController.CityId);
			//_mainModel
		}
		//ResetSelectCityEnemyTargetPlayer
		if (eventController.NameCommand == Command.ResetSelectCityEnemyTargetPlayer){

			_mainModel.ResetSelectCityEnemyTargetPlayer(eventController.CityId);
			
		}
	}

}
