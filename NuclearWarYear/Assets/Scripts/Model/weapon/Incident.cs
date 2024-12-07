using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;

public class Incident : Weapon, IWeapon
{
    private static int UnicId = 0;
    public string ReleaseMessage { get; private set; }
    public int ReleasePopulation { get; private set; }
    public PopulationEvent PopulationEvent { get; private set; }
    private bool ShowLider { get; set; }
    public int Year {private set; get;}

    public Incident(GlobalParam.TypeEvent name, GlobalParam.TypeEvent type,
        int id, string message, DamageParam damageParam) {
		this.Name=name;
		this.Id = id;
		this.IdImage = damageParam.IdImage;
        this.Type = type;
        this.Damage = damageParam.Damage;
        this.Message = message;
     }
    public int MutationDamage()
    {
        
        return UnityEngine.Random.Range(1, this.Damage);
    }
    public int GetDamage()
    {
        return this.Damage;
    }

    public int GetId()
    {
        return this.Id;
    }

    public GlobalParam.TypeEvent GetName()
    {
        return this.Name;
    }

    public int GetSize()
    {
        return 1;
    }

    public GlobalParam.TypeEvent GetTypeWeapon()
    {
        return this.Type;
    }
    public void SetTypeWeapon(GlobalParam.TypeEvent valueType)
    {
        this.Type = valueType;
    }
    public void SetDamage(int damage)
    {
        this.Damage = damage;
    }
    public string GetMessage()
    {
        return this.Message;
    }
    public string FullMessage(CountryLider lider)
    {
        
        
        if (String.IsNullOrEmpty(GetDamagePopulation())) {
            return $"{lider.Name}   : {GetMessage()}.";
        }
        return $"{lider.Name}   : {GetMessage()}: {GetDamagePopulation()} от {GetNameFiendLider()}.";
    }
    string GetNameFiendLider() {
        

        if (this.PopulationEvent?.FiendCountryLider == null)
        {
            return "";
        }

        if (GlobalParam.MessageDictionary[this.Name].ShowFiend == false)
        {
            return "";
        }
        
        return this.PopulationEvent.FiendCountryLider.Name; 
    }
    string GetDamagePopulation()
    {
        
        if (this.PopulationEvent == null)
        {
            return "";
        }
        
        int population  = Mathf.Max(Mathf.Abs(this.PopulationEvent.MyPopulation), Mathf.Abs(this.PopulationEvent.FiendPopulation));
        
        if (population > 0)
        {
            return population.ToString();
        }
        return "";

    }
    public int GetYear()
    {
        return this.Year;
    }
    public void SetYear(int value)
    {
        this.Year = value;
        this.Uid = UnicId++;
    }
    public Incident Copy()
    {
        return this.MemberwiseClone() as Incident;
    }
    public void SetReleaseMessage(PopulationEvent statePopulationEvent,bool showFiend)
    {
        
        this.PopulationEvent = statePopulationEvent;
        
        this.ShowLider = showFiend;
    }

    public int GetImageId()
    {
        return this.IdImage;
    }
}
