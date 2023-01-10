using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryMissle 
{
	public Missle GetMissle (int Id){

		if(Id==2) {
			return new Missle("Medium", 2, 10);
		}
		if(Id==3) {
			return new Missle("Heavy", 3, 15);
		}
		if(Id==4) {
			return new Missle("SuperHeavy", 4, 20);
		}
		// id = 1
		return new Missle("Light", 1, 40);
	}
	public Bomber GetBomber (int Id){
		return new Bomber("Bomber", 1, 5);
	}
	//
	public Warhead GetWarhead (int Id){
		return new Warhead("Warhead", 1, 25);
	}
	public Defence GetDefenceWeapon()
	{
		return new Defence("Defence", 1, 1);
	}
}
