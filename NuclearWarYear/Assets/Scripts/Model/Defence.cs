using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : Weapon
{
   public Defence(string name, int size, int damage)
	{
		this.Name = name;
		this.Size = size;
		this.Damage = damage;
	}
}
