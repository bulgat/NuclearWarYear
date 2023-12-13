using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : Weapon,IWeapon
{
   public Defence(string name, int size, int damage, DictionaryEssence.TypeWeapon Type,int id)
	{
		this.Id = id;
		this.Name = name;
		this.Size = size;
		this.Damage = damage;
		this.Type = Type;
	}
	public int GetId()
	{
		return this.Id;
	}
	public string GetName()
	{
		return this.Name;
	}
	public DictionaryEssence.TypeWeapon GetTypeWeapon()
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
}
