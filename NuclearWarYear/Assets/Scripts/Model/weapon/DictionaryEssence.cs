using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class DictionaryEssence 
{
	public enum TypeWeapon { Missle,Bomber,Defence,Incident }
    public enum TypeEvent { Building, Bomber , HeavyBomber, Defence, Propaganda, Industry, Ufo, Baby, RocketRich, CrazyCow, Defectors, Missle,
        AttackBomber,AttackMissle,Airport, AttackAirport
    }
	private List<Weapon> allEssenceList { get; }




	public DictionaryEssence() {
        allEssenceList = new List<Weapon>();

        allEssenceList.Add(new Incident(TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 0,""));
		allEssenceList.Add(new Incident(TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 1, ""));
        allEssenceList.Add(new Incident(TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 2, ""));
        allEssenceList.Add(new Incident(TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 3, ""));

		allEssenceList.Add(new Incident(TypeEvent.Bomber.ToString(), DictionaryEssence.TypeWeapon.Bomber, 4,"Бомбардировщики приведены в готовность",4));
        allEssenceList.Add(new Incident(TypeEvent.HeavyBomber.ToString(), DictionaryEssence.TypeWeapon.Bomber, 5, "Бомбардировщики приведены в готовность"));

        allEssenceList.Add(new Incident(TypeEvent.Defence.ToString(), DictionaryEssence.TypeWeapon.Defence, 6, "Защитные системы приведены в готовность",6));
        allEssenceList.Add(new Incident(TypeEvent.Defence.ToString(), DictionaryEssence.TypeWeapon.Defence, 7, "Защитные системы приведены в готовность", 6));

        allEssenceList.Add(new Incident(TypeEvent.Propaganda.ToString(), DictionaryEssence.TypeWeapon.Defence, 9, "Под воздействием пропаганды, население мигрировало ",9));

        allEssenceList.Add(new Incident(TypeEvent.Industry.ToString(), DictionaryEssence.TypeWeapon.Defence, 8, "Производство вооружения ",8));

        allEssenceList.Add(new Incident(TypeEvent.Ufo.ToString(), DictionaryEssence.TypeWeapon.Incident, 10, "Ufo инопланитяне прибыли в город ",11));
        allEssenceList.Add(new Incident(TypeEvent.Baby.ToString(), DictionaryEssence.TypeWeapon.Incident, 11, "Бэбибум ", 12));
        allEssenceList.Add(new Incident(TypeEvent.RocketRich.ToString(), DictionaryEssence.TypeWeapon.Incident, 12, "Богатые и Маск постороили ракету на Луну ",13));
        allEssenceList.Add(new Incident(TypeEvent.CrazyCow.ToString(), DictionaryEssence.TypeWeapon.Incident, 13,"Ковид-сумашедшествие от коров",14));

        allEssenceList.Add(new Incident(TypeEvent.Building.ToString(), DictionaryEssence.TypeWeapon.Incident, 14, "Производство вооружения ", 8));
        allEssenceList.Add(new Incident(TypeEvent.AttackBomber.ToString(), DictionaryEssence.TypeWeapon.Incident, 15, "От ядерного взрыва с бомбардировщика население уменьшилось на ",10));
        allEssenceList.Add(new Incident(TypeEvent.AttackMissle.ToString(), DictionaryEssence.TypeWeapon.Incident, 16, "От ядерного взрыва ракеты население уменьшилось на ",15));
        allEssenceList.Add(new Incident(TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Incident, 17, "Ракеты приведены в готовность"));

        allEssenceList.Add(new Incident(TypeEvent.Defectors.ToString(), DictionaryEssence.TypeWeapon.Incident, 18, "Перебежчики сбежали ",16));

        allEssenceList.Add(new Incident(TypeEvent.Airport.ToString(), DictionaryEssence.TypeWeapon.Defence, 19, "Бомбардировщики приведены в готовность", 4));
    }

	public int GetIdEvent(string Name)
	{
        
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }

	//public Missle GetMissle (int Id){

       // return this.allEssenceList.Where(a=>a.Id== Id).FirstOrDefault() as Missle;

    //}
    /*
	public Bomber GetBomber (int Id){
        return this.allEssenceList.Where(a => a.Id == Id).FirstOrDefault() as Bomber;

	}
    */
    /*
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
    */
    public Incident GetIncident(int Id)
    {
        
        return this.allEssenceList.Where(a => a.Id == Id).FirstOrDefault() as Incident;
    }
    public Incident BuildIncident(string Name)
    {
        
        return this.allEssenceList.Where(a => a.Name == Name).FirstOrDefault() as Incident;
    }
}
