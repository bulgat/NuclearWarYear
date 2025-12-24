using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using Assets.Scripts.Model.weapon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class DictionaryEssence 
{
    public enum TypeWeapon { Missle, Bomber, Defence, Incident }

    public static List<string> MessagePrepareList = new List<string>()
    {
        "Подготовка ракет",
        "Подготовка бомбандировщиков",
        "Подготовка системы перехвата ракет",
        "Подготовка сенсаций в СМИ",
        "Производство вооружения"
    };
    private static List<Incident> allEssenceList = new List<Incident>() {

        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 0,
            "Ракеты приведены в готовность",new DamageParam(10,0),false, null),
        new Incident(GlobalParam.TypeEvent.HeavyMissle, GlobalParam.TypeEvent.Missle, 1, 
            "Ракеты приведены в готовность",new DamageParam(40,1),false, null),
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 2, 
            "Ракеты приведены в готовность",new DamageParam(50,2), false, null),
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Missle, 3, 
            "Ракеты приведены в готовность",new DamageParam(100, 3), false, null),

        new Incident(GlobalParam.TypeEvent.Bomber, GlobalParam.TypeEvent.Bomber, 4,
            "Бомбардировщики приведены в готовность",new DamageParam(10,4),false, null),
        new Incident(GlobalParam.TypeEvent.HeavyBomber, GlobalParam.TypeEvent.Bomber, 5, 
            "Бомбардировщики приведены в готовность",new DamageParam(50,5), false, null),
        new Incident(GlobalParam.TypeEvent.Defence, GlobalParam.TypeEvent.Defence, 6, 
            "Защитные системы приведены в готовность",new DamageParam(0,7), false, null),
        new Incident(GlobalParam.TypeEvent.HeavyDefence, GlobalParam.TypeEvent.Defence, 7, 
            "Защитные системы приведены в готовность",new DamageParam(0, 6), false, null),

        new Incident(GlobalParam.TypeEvent.Propaganda, GlobalParam.TypeEvent.Defence, 9, 
            "Под воздействием пропаганды, население {0} сбежало в {1}",new DamageParam(3,10), false, null),

        new Incident(GlobalParam.TypeEvent.Ufo, GlobalParam.TypeEvent.Incident, 10, 
            "Ufo инопланитяне прибыли в город +",new DamageParam(5,12), false, new EventFortuneIncident(GlobalParam.TypeEvent.Ufo,32)),
        new Incident(GlobalParam.TypeEvent.Baby, GlobalParam.TypeEvent.Incident, 11, 
            "Бэбибум прибавка населения +",new DamageParam(5, 13), false, new EventFortuneIncident(GlobalParam.TypeEvent.Baby,32)),
        new Incident(GlobalParam.TypeEvent.RocketRich, GlobalParam.TypeEvent.Incident, 12, 
            "Богатые граждане {0} с помощью Маска постороили ракету на Луну ",new DamageParam(5, 14), false,new EventFortuneIncident(GlobalParam.TypeEvent.RocketRich, 30)),
        new Incident(GlobalParam.TypeEvent.CrazyCow, GlobalParam.TypeEvent.Incident, 13,
            "Ковид-безумие сумашедшедших коров, поразило {0} ",new DamageParam(5, 15), false, new EventFortuneIncident(GlobalParam.TypeEvent.CrazyCow,30)),

        new Incident(GlobalParam.TypeEvent.Build, GlobalParam.TypeEvent.Incident, 14, 
            "Производство вооружения ",new DamageParam(0, 6), false, null),

        new Incident(GlobalParam.TypeEvent.AttackBomber, GlobalParam.TypeEvent.Incident, 15, 
            "От ядерного взрыва с бомбардировщика население {0} уменьшилось на ",new DamageParam(10,11),true, null),
        new Incident(GlobalParam.TypeEvent.AttackMissle, GlobalParam.TypeEvent.Incident, 16, 
            "От ядерного взрыва ракеты население {0} уменьшилось на ",new DamageParam(10,16),true, null),
        
        new Incident(GlobalParam.TypeEvent.Missle, GlobalParam.TypeEvent.Incident, 17, 
            "Ракеты приведены в готовность",new DamageParam(0, 0), false, null),

        new Incident(GlobalParam.TypeEvent.Defectors, GlobalParam.TypeEvent.Incident, 18, 
            "Перебежчики сбежали из {0} в {1}",new DamageParam(5, 17), false, new EventFortuneIncident(GlobalParam.TypeEvent.Defectors,30)),
        new Incident(GlobalParam.TypeEvent.Airport, GlobalParam.TypeEvent.Defence, 19, 
            "Бомбардировщики приведены в готовность",new DamageParam(0, 4), false, null)
    };
     
	public int GetIdEventName(GlobalParam.TypeEvent Name)
	{
        
        return allEssenceList.Where(a=>a.Name== Name).FirstOrDefault().IdImage;

    }
    public List<Incident> GetIdTypeEventList(GlobalParam.TypeEvent type)
    {

        return allEssenceList.Where(a => a.Type == type).ToList();

    }

    public Incident GetIncident(int Id)
    {
        
        return (allEssenceList.FirstOrDefault(a => a.Id == Id) as Incident).Copy();
    }
    public Incident GetIncident(GlobalParam.TypeEvent type)
    {
        
        return (allEssenceList.FirstOrDefault(a => a.Name == type) as Incident).Copy();
    }
    public Incident BuildIncident(GlobalParam.TypeEvent Name,int Year)
    {
   
        Incident incident = (allEssenceList.FirstOrDefault(a => a.Name == Name) as Incident).Copy();
        
        incident.SetYear(Year);
        incident.MutationDamage();
        return incident;
    }
    public static List<Incident> GetFortune()
    {
        return allEssenceList.Where(a=>a.Fortune!=null).ToList();
    }

}
