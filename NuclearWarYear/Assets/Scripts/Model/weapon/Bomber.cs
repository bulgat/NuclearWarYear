using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber: Weapon,IWeapon
{

	
	public Bomber(string name, int size, int damage, DictionaryEssence.TypeWeapon Type,int id, int IdImage = 0) {
		this.Id = id;
		Name=name;
		Size=size;
		Damage=damage;
		this.Type = Type;
		this.IdImage=IdImage;
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
