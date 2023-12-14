using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incident: Weapon
{
	
	
	public Incident(string name, DictionaryEssence.TypeWeapon Type, int id, int IdImage=0) {
		this.Name=name;
		this.Id = id;
		this.IdImage = IdImage;
	}
}
