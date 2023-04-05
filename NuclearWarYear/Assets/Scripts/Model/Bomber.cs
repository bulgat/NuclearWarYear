using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber: Weapon,IWeapon
{

	
	public Bomber(string name, int size, int damage, DictionaryMissle.TypeWeapon Type) {
		Name=name;
		Size=size;
		Damage=damage;
		this.Type = Type;
	}
	public DictionaryMissle.TypeWeapon GetTypeWeapon()
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
