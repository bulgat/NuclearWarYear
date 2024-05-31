using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class DictionaryEssence 
{
    public enum TypeWeapon { Missle, Bomber, Defence, Incident }

    private static List<Weapon> allEssenceList = new List<Weapon>() {


        new Incident(GlobalParam.TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 0,"Ракеты приведены в готовность",10,0),
        new Incident(GlobalParam.TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 1, "Ракеты приведены в готовность",20,0),
        new Incident(GlobalParam.TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 2, "Ракеты приведены в готовность",50,0),
        new Incident(GlobalParam.TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Missle, 3, "Ракеты приведены в готовность",100, 0),

        new Incident(GlobalParam.TypeEvent.Bomber.ToString(), DictionaryEssence.TypeWeapon.Bomber, 4,"Бомбардировщики приведены в готовность",10,4),
        new Incident(GlobalParam.TypeEvent.HeavyBomber.ToString(), DictionaryEssence.TypeWeapon.Bomber, 5, "Бомбардировщики приведены в готовность",50,5),

        new Incident(GlobalParam.TypeEvent.Defence.ToString(), DictionaryEssence.TypeWeapon.Defence, 6, "Защитные системы приведены в готовность",0,6),
        new Incident(GlobalParam.TypeEvent.Defence.ToString(), DictionaryEssence.TypeWeapon.Defence, 7, "Защитные системы приведены в готовность",0, 6),

        new Incident(GlobalParam.TypeEvent.Propaganda.ToString(), DictionaryEssence.TypeWeapon.Defence, 9, "Под воздействием пропаганды, население мигрировало ",0,9),

        new Incident(GlobalParam.TypeEvent.Industry.ToString(), DictionaryEssence.TypeWeapon.Defence, 8, "Производство вооружения ",0,8),

        new Incident(GlobalParam.TypeEvent.Ufo.ToString(), DictionaryEssence.TypeWeapon.Incident, 10, "Ufo инопланитяне прибыли в город +",0,11),
        new Incident(GlobalParam.TypeEvent.Baby.ToString(), DictionaryEssence.TypeWeapon.Incident, 11, "Бэбибум прибавка населения +",0, 12),
        new Incident(GlobalParam.TypeEvent.RocketRich.ToString(), DictionaryEssence.TypeWeapon.Incident, 12, "Богатые и Маск постороили ракету на Луну ",0,13),
        new Incident(GlobalParam.TypeEvent.CrazyCow.ToString(), DictionaryEssence.TypeWeapon.Incident, 13,"Ковид-сумашедшествие от коров",0,14),

        new Incident(GlobalParam.TypeEvent.Build.ToString(), DictionaryEssence.TypeWeapon.Incident, 14, "Производство вооружения ",0, 8),
        new Incident(GlobalParam.TypeEvent.AttackBomber.ToString(), DictionaryEssence.TypeWeapon.Incident, 15, "От ядерного взрыва с бомбардировщика население уменьшилось на ",1,10),
        new Incident(GlobalParam.TypeEvent.AttackMissle.ToString(), DictionaryEssence.TypeWeapon.Incident, 16, "От ядерного взрыва ракеты население уменьшилось на ",1,15),
        new Incident(GlobalParam.TypeEvent.Missle.ToString(), DictionaryEssence.TypeWeapon.Incident, 17, "Ракеты приведены в готовность",0, 0),

        new Incident(GlobalParam.TypeEvent.Defectors.ToString(), DictionaryEssence.TypeWeapon.Incident, 18, "Перебежчики сбежали ",0, 16),

        new Incident(GlobalParam.TypeEvent.Airport.ToString(), DictionaryEssence.TypeWeapon.Defence, 19, "Бомбардировщики приведены в готовность",0, 4)
    };
     
	public int GetIdEvent(string Name)
	{
        
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }


    public Incident GetIncident(int Id)
    {
        
        return allEssenceList.FirstOrDefault(a => a.Id == Id) as Incident;
    }
    public Incident BuildIncident(string Name)
    {
        Incident incident = allEssenceList.FirstOrDefault(a => a.Name == Name) as Incident;
        Debug.Log("Name ="+ Name);
        incident.MutationDamage();
        return incident;
    }
}
