using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
	public int Id;
	public GlobalParam.TypeEvent Name;
	public int Size;
	public int Damage { protected set; get; }
	public GlobalParam.TypeEvent Type;
    public int IdImage;
    protected string Message;
	public int Uid;
	public EventFortuneIncident Fortune;
}
