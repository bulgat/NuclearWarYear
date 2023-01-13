using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryMissle 
{
	public enum TypeWeapon { Missle,Bomber,Defence }
	public Missle GetMissle (int Id){

		if(Id==2) {
			return new Missle("Medium", 2, 10, DictionaryMissle.TypeWeapon.Missle);
		}
		if(Id==3) {
			return new Missle("Heavy", 3, 15, DictionaryMissle.TypeWeapon.Missle);
		}
		if(Id==4) {
			return new Missle("SuperHeavy", 4, 20, DictionaryMissle.TypeWeapon.Missle);
		}
		// id = 1
		return new Missle("Light", 1, 40, DictionaryMissle.TypeWeapon.Missle);
	}
	public Bomber GetBomber (int Id){
		if (Id == 2)
		{
			return new Bomber("HeavyBomber", 2, 10, DictionaryMissle.TypeWeapon.Bomber);
		}
		return new Bomber("Bomber", 1, 5, DictionaryMissle.TypeWeapon.Bomber);
	}

	public Defence GetDefenceWeapon(int Id)
	{
		if (Id == 2)
		{
			return new Defence("HeavyDefence", 2, 10, DictionaryMissle.TypeWeapon.Defence);
		}
		return new Defence("Defence", 1, 1, DictionaryMissle.TypeWeapon.Defence);
	}
	
}
