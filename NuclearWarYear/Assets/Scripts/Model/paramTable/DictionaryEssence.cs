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


        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 0,"Ракеты приведены в готовность",new DamageParam(10,0)),
        new Incident(GlobalParam.TypeEvent.HeavyMissle, GlobalParam.TypeEvent.Missle, 1, "Ракеты приведены в готовность",new DamageParam(40,0)),
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 2, "Ракеты приведены в готовность",new DamageParam(50,0)),
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 3, "Ракеты приведены в готовность",new DamageParam(100, 0)),

        new Incident(GlobalParam.TypeEvent.Bomber, GlobalParam.TypeEvent.Bomber, 4,"Бомбардировщики приведены в готовность",new DamageParam(10,4)),
        new Incident(GlobalParam.TypeEvent.HeavyBomber, GlobalParam.TypeEvent.Bomber, 5, "Бомбардировщики приведены в готовность",new DamageParam(50,5)),

        new Incident(GlobalParam.TypeEvent.Defence, GlobalParam.TypeEvent.Defence, 6, "Защитные системы приведены в готовность",new DamageParam(0,6)),
        new Incident(GlobalParam.TypeEvent.HeavyDefence, GlobalParam.TypeEvent.Defence, 7, "Защитные системы приведены в готовность",new DamageParam(0, 6)),

        new Incident(GlobalParam.TypeEvent.Propaganda, GlobalParam.TypeEvent.Defence, 9, "Под воздействием пропаганды, население сбежало к ",new DamageParam(3,10)),

        new Incident(GlobalParam.TypeEvent.Ufo, GlobalParam.TypeEvent.Incident, 10, "Ufo инопланитяне прибыли в город +",new DamageParam(5,12)),
        new Incident(GlobalParam.TypeEvent.Baby, GlobalParam.TypeEvent.Incident, 11, "Бэбибум прибавка населения +",new DamageParam(5, 13)),
        new Incident(GlobalParam.TypeEvent.RocketRich, GlobalParam.TypeEvent.Incident, 12, "Богатые граждвне с помощью Маска постороили ракету на Луну ",new DamageParam(5, 14)),
        new Incident(GlobalParam.TypeEvent.CrazyCow, GlobalParam.TypeEvent.Incident, 13,"Ковид-сумашедшествие от коров",new DamageParam(5, 14)),

        new Incident(GlobalParam.TypeEvent.Build, GlobalParam.TypeEvent.Incident, 14, "Производство вооружения ",new DamageParam(0, 6)),
        new Incident(GlobalParam.TypeEvent.AttackBomber, GlobalParam.TypeEvent.Incident, 15, "От ядерного взрыва с бомбардировщика население уменьшилось на ",new DamageParam(1,10)),
        new Incident(GlobalParam.TypeEvent.AttackMissle, GlobalParam.TypeEvent.Incident, 16, "От ядерного взрыва ракеты население уменьшилось на ",new DamageParam(1,15)),
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Incident, 17, "Ракеты приведены в готовность",new DamageParam(0, 0)),

        new Incident(GlobalParam.TypeEvent.Defectors, GlobalParam.TypeEvent.Incident, 18, "Перебежчики сбежали ",new DamageParam(5, 17)),

        new Incident(GlobalParam.TypeEvent.Airport, GlobalParam.TypeEvent.Defence, 19, "Бомбардировщики приведены в готовность",new DamageParam(0, 4))
    };
     
	public int GetIdEventName(GlobalParam.TypeEvent Name)
	{
        
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }
    public List<Weapon> GetIdTypeEventList(GlobalParam.TypeEvent type)
    {

        return allEssenceList.Where(a => a.Type == type).ToList();

    }

    public Incident GetIncident(int Id)
    {
        
        return allEssenceList.FirstOrDefault(a => a.Id == Id) as Incident;
    }
    public Incident GetIncident(GlobalParam.TypeEvent type)
    {
        
        return allEssenceList.FirstOrDefault(a => a.Name == type) as Incident;
    }
    public Incident BuildIncident(GlobalParam.TypeEvent Name,int Year)
    {
   
        Incident incident = allEssenceList.FirstOrDefault(a => a.Name == Name) as Incident;
        incident.SetYear(Year);
        incident.MutationDamage();
        return incident;
    }
}
