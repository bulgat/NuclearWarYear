using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    private MainModel _mainModel;
    /*
	public enum Command
	{
		//Bomber,
		//AttackMissle,
		//LiderTargetPlayer,
		//Warhead,
		//TotalTurn,
	}*/
    public Controller(MainModel MainModel)
    {
        _mainModel = MainModel;
    }
    //public void SendCommand(EventController eventController) {
    /*
		if (eventController.NameCommand == Command.AttackMissle){
			_mainModel.SetAttackMisslePlayer(eventController.EventSend.FlagId);
			return;
		}
	*/
    /*
    if (eventController.NameCommand == Command.Bomber){

        _mainModel.SetBomberPlayer(eventController.EventSend.FlagId, eventController.EventSend.Id);
        return;
    }
    */
    /*
    if (eventController.NameCommand == Command.LiderTargetPlayer){

        _mainModel.SetLiderTargetPlayer(eventController.EventSend.FlagId);
        return;
    }*/
    /*
    if (eventController.NameCommand == Command.Warhead){

        _mainModel.SetWarheadMethodPlayer(eventController.EventSend.FlagId);
        return;
    }*/
    /*
    if (eventController.NameCommand == Command.TotalTurn){

        _mainModel.TotalTurn(eventController.EventSend.FlagId);
        return;
    }
    */
    //throw new System.Exception("Not Command controller"); 
    //}
    /*
    public void TotalTurn(int FlagId)
	{
        _mainModel.TotalTurn(FlagId);
    }*/
    public void AttackMissle(int FlagId)
    {
        _mainModel.SetAttackMisslePlayer(FlagId);
    }
    public void LiderTargetPlayer(int FlagId)
    {
        _mainModel.SetLiderTargetPlayer(FlagId);
    }
    public void Defence(int FlagId)
    {
        _mainModel.SetDefencePlayer(FlagId);
    }

    public void Building(int FlagId)
    {
        _mainModel.SetBuildingPlayer(FlagId);

    }

    public void Propaganda(int FlagId)
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

    public void AttackBomber(int FlagId)
    {
        _mainModel.SetAttackBomberPlayer(FlagId);

    }


    public void ChangeCurrentPlayer()
    {
        _mainModel.ChangeCurrentPlayer();
    }
    /*
    public void DoneMoveMadeCurrentPlayer()
	{

            _mainModel.DoneMoveMadeCurrentPlayer();

    }
	*/
    public void SetMissle(int FlagId, GlobalParam.TypeEvent nameEvent)
    {
        _mainModel.SetMisslePlayer(FlagId, nameEvent);
        return;
    }
    public void SetBomber(int FlagId, GlobalParam.TypeEvent nameEvent)
    {
        _mainModel.SetBomberPlayer(FlagId, nameEvent);
        return;
    }
    public void TurnAi()
    {
        _mainModel.TurnAi();
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
