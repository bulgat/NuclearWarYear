using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;

public class Incident: Weapon,IWeapon
{
    private static int UnicId = 0;
    public string ReleaseMessage { get; private set; }
    public int ReleasePopulation { get; private set; }
    public PopulationEvent PopulationEvent { get; private set; }
    private bool ShowLider { get; set; }

    public Incident(string name, DictionaryEssence.TypeWeapon type, int id, string message,int damage,int IdImage) {
		this.Name=name;
		this.Id = id;
		this.IdImage = IdImage;
        this.Type = type;
        this.Damage = damage;
        this.Message = message;
        this.PopulationEvent = new PopulationEvent();
        this.Uid = UnicId++;

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

    public string GetName()
    {
        return this.Name;
    }

    public int GetSize()
    {
        return 1;
    }

    public DictionaryEssence.TypeWeapon GetTypeWeapon()
    {
        return this.Type;
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
        return "" + lider.GetName() + "  : " + GetMessage() + ": " + GetDamagePopulation() + " * " + GetNameFiendLider();
    }
    string GetNameFiendLider() {
        Debug.Log("  Ima ------------------------------------------------------------------ " + this.Name);
        Debug.Log(this.ShowLider+" Z Z"+ GlobalParam.MessageDictionary[this.Name].ShowFiend + " Z"+ this.Name + " Z lider = "  + this.PopulationEvent.FiendCountryLider);
        if (this.PopulationEvent.FiendCountryLider == null)
        {
            return "";
        }
        if (GlobalParam.MessageDictionary[this.Name].ShowFiend == false)
        {
            return "";
        }
        return this.PopulationEvent.FiendCountryLider.GetName(); 
    }
    string GetDamagePopulation()
    {
        
        if (this.PopulationEvent == null)
        {
            return "";
        }
        int population  = Mathf.Max(this.PopulationEvent.MyPopulation, this.PopulationEvent.MyPopulation);
        if (population > 0)
        {
            return population.ToString();
        }
        return "";

    }

    public Incident Copy()
    {
        return this.MemberwiseClone() as Incident;
    }
    public void SetReleaseMessage(PopulationEvent statePopulationEvent,bool showFiend)
    {
        this.PopulationEvent = statePopulationEvent;
        if (statePopulationEvent.FiendCountryLider == null)
        {
            throw new ArgumentNullException("not lider");
        }
        this.ShowLider = showFiend;
    }

}
