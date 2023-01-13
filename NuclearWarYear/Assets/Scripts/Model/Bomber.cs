using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber: Weapon
{

	
	public Bomber(string name, int size, int damage, DictionaryMissle.TypeWeapon Type) {
		Name=name;
		Size=size;
		Damage=damage;
		this.Type = Type;
	}
}
