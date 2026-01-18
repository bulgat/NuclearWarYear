using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

public class Incident : Weapon, IWeapon
{
    public int UnicalId = 0;
    public string ReleaseMessage { get; private set; }
    public string PrepareMessage { get; private set; }
    public int ReleasePopulation { get; private set; }
    public PopulationEvent PopulationEvent { get; private set; }
    private bool ShowLider { get; set; }
    public int Year {private set; get;}
    public bool ExplodeNuclear { private set; get; }
    public Incident SecondIncident { private set; get; }
    public EventFortuneIncident Fortune { private set; get; }
    public Incident(
        GlobalParam.TypeEvent name,
        GlobalParam.TypeEvent type,
        int id,
        string message,
        DamageParam damageParam,
        bool explodeNuclear,
        EventFortuneIncident fortune,
        string prepareMessage
        ) 
    {
		this.Name=name;
		this.Id = id;
		this.IdImage = damageParam.IdImage;
        this.Type = type;
        this.Damage = damageParam.Damage;
        this.Message = message;
        this.ExplodeNuclear = explodeNuclear;
        this.Fortune = fortune;
        this.PrepareMessage = prepareMessage;
        this.UnicalId = DictionaryEssence.GetUnicalId();
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
       

        string message = MutationMessage();
        if (this.PopulationEvent == null)
        {
            return message;
        }
        int population = Mathf.Max(Mathf.Abs(this.PopulationEvent.MyPopulation), Mathf.Abs(this.PopulationEvent.FiendPopulation));

        return $"{lider.Name}   : {message} {(population>0? ":"+population:"")} {GetNameFiendLider()}.";
    }
    string MutationMessage()
    {
        Debug.Log("0600  message incident = " + GetMessage()+ " PopulationEvent = "+ PopulationEvent+" name = "+Name);
        if (PopulationEvent?.MyCity == null && PopulationEvent?.FiendCity == null)
        {
            return GetMessage();
        }
        if (PopulationEvent?.MyCity != null && PopulationEvent?.FiendCity != null)
        {
            return String.Format(GetMessage(), PopulationEvent.MyCity.Name, PopulationEvent.FiendCity.Name);
        }
        if (PopulationEvent?.MyCity != null) {
            return String.Format(GetMessage(), PopulationEvent.MyCity.Name, "");
        }
        if (PopulationEvent?.FiendCity != null)
        {
            Debug.Log("0601"+this.Name+" message incident = " + GetMessage());
            return String.Format(GetMessage(), PopulationEvent?.FiendCity.Name);
        }
        return String.Format(GetMessage(), "");
    }
    public void SetMessage(string newMessage)
    {
        Debug.Log("0602  id = "+Id+" NEW message incident = " + newMessage);
        this.Message = newMessage;
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
   
    public int GetYear()
    {
        return this.Year;
    }
    public void SetYear(int value)
    {
        this.Year = value;
        this.Uid = UnicalId++;
    }
    public Incident Copy()
    {
        return this.MemberwiseClone() as Incident;
    }
    public void SetReleaseMessage(bool showFiend)
    {
        this.ShowLider = showFiend;
    }
    public void SetPopulationEvent(PopulationEvent statePopulationEvent)
    {
        this.PopulationEvent = statePopulationEvent;

    }


    public int GetImageId()
    {
        return this.IdImage;
    }
    public void SetSecondIncident(Incident secondIncident)
    {
        SecondIncident = secondIncident;
    }
}
