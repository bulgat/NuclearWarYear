using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DictionaryEssence 
{
	public enum TypeWeapon { Missle,Bomber,Defence,Incident }
	private List<Weapon> allEssenceList { get; }

	public DictionaryEssence() {
        allEssenceList = new List<Weapon>();

        allEssenceList.Add(new Missle("Light", 1, 40, DictionaryEssence.TypeWeapon.Missle, 0));
		allEssenceList.Add(new Missle("Medium", 2, 10, DictionaryEssence.TypeWeapon.Missle, 1));
        allEssenceList.Add(new Missle("Heavy", 3, 15, DictionaryEssence.TypeWeapon.Missle, 2));
        allEssenceList.Add(new Missle("SuperHeavy", 4, 20, DictionaryEssence.TypeWeapon.Missle, 3));

		allEssenceList.Add(new Bomber("Bomber", 1, 5, DictionaryEssence.TypeWeapon.Bomber, 4));
        allEssenceList.Add(new Bomber("HeavyBomber", 2, 10, DictionaryEssence.TypeWeapon.Bomber, 5));

        allEssenceList.Add(new Defence("Defence", 1, 1, DictionaryEssence.TypeWeapon.Defence, 6,6));
        allEssenceList.Add(new Defence("HeavyDefence", 2, 10, DictionaryEssence.TypeWeapon.Defence, 7));

        allEssenceList.Add(new Defence("Propaganda", 1, 1, DictionaryEssence.TypeWeapon.Defence, 9,9));

        allEssenceList.Add(new Defence("Industry", 1, 1, DictionaryEssence.TypeWeapon.Defence, 8));

        allEssenceList.Add(new Incident("Ufo", DictionaryEssence.TypeWeapon.Incident, 10,11));
        allEssenceList.Add(new Incident("Baby", DictionaryEssence.TypeWeapon.Incident, 11,12));
        allEssenceList.Add(new Incident("RocketRich", DictionaryEssence.TypeWeapon.Incident, 12,13));
        allEssenceList.Add(new Incident("CrazyCow", DictionaryEssence.TypeWeapon.Incident, 13,14));

        allEssenceList.Add(new Incident("Building", DictionaryEssence.TypeWeapon.Incident, 14,8));
        allEssenceList.Add(new Incident("AttackBomber", DictionaryEssence.TypeWeapon.Incident, 15,10));
        allEssenceList.Add(new Incident("AttackMissle", DictionaryEssence.TypeWeapon.Incident, 16,15));
        allEssenceList.Add(new Incident("Missle", DictionaryEssence.TypeWeapon.Incident, 17));

        allEssenceList.Add(new Incident("Defectors", DictionaryEssence.TypeWeapon.Incident, 18,16));
        

    }
	public int GetIdEvent(string Name)
	{
        Debug.Log(  "   ---   = " + Name);
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }

	public Missle GetMissle (int Id){

        return this.allEssenceList.Where(a=>a.Id== Id).FirstOrDefault() as Missle;

    }
	public Bomber GetBomber (int Id){
        return this.allEssenceList.Where(a => a.Id == Id).FirstOrDefault() as Bomber;

	}

	public Defence GetDefenceWeapon(int Id)
	{
        return this.allEssenceList.Where(a => a.Id == Id).FirstOrDefault() as Defence;

	}
	public Defence GetPropaganda()
	{
        return this.allEssenceList.Where(a => a.Id == 9).FirstOrDefault() as Defence;

	}
	public Defence GetIndustry()
	{
        return this.allEssenceList.Where(a => a.Id == 8).FirstOrDefault() as Defence;
	}
    public Incident GetIncident(int Id)
    {
        return this.allEssenceList.Where(a => a.Id == 8).FirstOrDefault() as Incident;

    }
}
