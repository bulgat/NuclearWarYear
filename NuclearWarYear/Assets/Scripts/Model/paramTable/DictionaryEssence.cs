using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class DictionaryEssence 
{
    public enum TypeWeapon { Missle, Bomber, Defence, Incident }

    private static List<Weapon> allEssenceList = new List<Weapon>() {


        new Incident(GlobalParam.TypeEvent.Missle, DictionaryEssence.TypeWeapon.Missle, 0,"Ракеты приведены в готовность",new DamageParam(10,0)),
        new Incident(GlobalParam.TypeEvent.Missle, DictionaryEssence.TypeWeapon.Missle, 1, "Ракеты приведены в готовность",new DamageParam(20,0)),
        new Incident(GlobalParam.TypeEvent.Missle, DictionaryEssence.TypeWeapon.Missle, 2, "Ракеты приведены в готовность",new DamageParam(50,0)),
        new Incident(GlobalParam.TypeEvent.Missle, DictionaryEssence.TypeWeapon.Missle, 3, "Ракеты приведены в готовность",new DamageParam(100, 0)),

        new Incident(GlobalParam.TypeEvent.Bomber, DictionaryEssence.TypeWeapon.Bomber, 4,"Бомбардировщики приведены в готовность",new DamageParam(10,4)),
        new Incident(GlobalParam.TypeEvent.HeavyBomber, DictionaryEssence.TypeWeapon.Bomber, 5, "Бомбардировщики приведены в готовность",new DamageParam(50,5)),

        new Incident(GlobalParam.TypeEvent.Defence, DictionaryEssence.TypeWeapon.Defence, 6, "Защитные системы приведены в готовность",new DamageParam(0,6)),
        new Incident(GlobalParam.TypeEvent.Defence, DictionaryEssence.TypeWeapon.Defence, 7, "Защитные системы приведены в готовность",new DamageParam(0, 6)),

        new Incident(GlobalParam.TypeEvent.Propaganda, DictionaryEssence.TypeWeapon.Defence, 9, "Под воздействием пропаганды, население сбежало к ",new DamageParam(0,9)),

        new Incident(GlobalParam.TypeEvent.Industry, DictionaryEssence.TypeWeapon.Defence, 8, "Производство вооружения ",new DamageParam(0, 8)),

        new Incident(GlobalParam.TypeEvent.Ufo, DictionaryEssence.TypeWeapon.Incident, 10, "Ufo инопланитяне прибыли в город +",new DamageParam(-5,11)),
        new Incident(GlobalParam.TypeEvent.Baby, DictionaryEssence.TypeWeapon.Incident, 11, "Бэбибум прибавка населения +",new DamageParam(-5, 12)),
        new Incident(GlobalParam.TypeEvent.RocketRich, DictionaryEssence.TypeWeapon.Incident, 12, "Богатые и Маск постороили ракету на Луну ",new DamageParam(5, 13)),
        new Incident(GlobalParam.TypeEvent.CrazyCow, DictionaryEssence.TypeWeapon.Incident, 13,"Ковид-сумашедшествие от коров",new DamageParam(5, 14)),

        new Incident(GlobalParam.TypeEvent.Build, DictionaryEssence.TypeWeapon.Incident, 14, "Производство вооружения ",new DamageParam(0, 8)),
        new Incident(GlobalParam.TypeEvent.AttackBomber, DictionaryEssence.TypeWeapon.Incident, 15, "От ядерного взрыва с бомбардировщика население уменьшилось на ",new DamageParam(1,10)),
        new Incident(GlobalParam.TypeEvent.AttackMissle, DictionaryEssence.TypeWeapon.Incident, 16, "От ядерного взрыва ракеты население уменьшилось на ",new DamageParam(1,15)),
        new Incident(GlobalParam.TypeEvent.Missle, DictionaryEssence.TypeWeapon.Incident, 17, "Ракеты приведены в готовность",new DamageParam(0, 0)),

        new Incident(GlobalParam.TypeEvent.Defectors, DictionaryEssence.TypeWeapon.Incident, 18, "Перебежчики сбежали ",new DamageParam(5, 16)),

        new Incident(GlobalParam.TypeEvent.Airport, DictionaryEssence.TypeWeapon.Defence, 19, "Бомбардировщики приведены в готовность",new DamageParam(0, 4))
    };
     
	public int GetIdEvent(GlobalParam.TypeEvent Name)
	{
        
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }


    public Incident GetIncident(int Id)
    {
        
        return allEssenceList.FirstOrDefault(a => a.Id == Id) as Incident;
    }
    public Incident BuildIncident(GlobalParam.TypeEvent Name)
    {
        Incident incident = allEssenceList.FirstOrDefault(a => a.Name == Name) as Incident;
        Debug.Log(incident.GetDamage()+"    Name =" + Name);
        incident.MutationDamage();
        return incident;
    }
}
