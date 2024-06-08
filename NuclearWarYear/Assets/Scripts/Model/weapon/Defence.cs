using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : Weapon,IWeapon
{
   public Defence(GlobalParam.TypeEvent name, int size, int damage, GlobalParam.TypeEvent Type,int id, int IdImage = 0)
	{
		this.Id = id;
		this.Name = name;
		this.Size = size;
		this.Damage = damage;
		this.Type = Type;
		this.IdImage = IdImage;
	}
	public int GetId()
	{
		return this.Id;
	}
	public GlobalParam.TypeEvent GetName()
	{
		return this.Name;
	}
	public GlobalParam.TypeEvent GetTypeWeapon()
	{
		return this.Type;

	}
	public int GetSize()
	{
		return this.Size;
	}
	public int GetDamage()
	{
		return this.Damage;

	}
	public void SetDamage(int damage)
	{
		this.Damage = damage;

	}

    public string GetMessage()
    {
        throw new System.NotImplementedException();
    }
}
