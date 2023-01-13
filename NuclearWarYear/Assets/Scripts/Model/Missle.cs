using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Weapon
{

	public Missle(string name, int size, int damage, DictionaryMissle.TypeWeapon Type) {
		Name=name;
		Size=size;
		Damage=damage;
		this.Type = Type;
	}
}
