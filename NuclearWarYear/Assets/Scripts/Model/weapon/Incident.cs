using Assets.Scripts.Model;
using Assets.Scripts.Model.weapon;
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
    public IStatePopulationEvent PopulationEvent { get; private set; }

    public Incident(string name, DictionaryEssence.TypeWeapon type, int id, string message,int damage,int IdImage) {
		this.Name=name;
		this.Id = id;
		this.IdImage = IdImage;
        this.Type = type;
        this.Damage = damage;
        this.Message = message;
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
        return "" + lider.GetName() + "  : " + Name + GetMessage() +": "+ GetDamagePopulation();
    }
    string GetDamagePopulation()
    {
        int population  = Mathf.Max(this.PopulationEvent.GetMyPopulation(), this.PopulationEvent.GetMyPopulation());
        if (population > 0)
        {
            return population.ToString();
        }
        return "";
        //new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.GetMyCity(), CommandIncident.PopulationEvent.GetMyPopulation());
        //new DamagePopulationHelper().SetDamagePopulation(CommandIncident.PopulationEvent.GetFiendCity(), CommandIncident.PopulationEvent.GetFiendPopulation());
    }

    public Incident Copy()
    {
        return this.MemberwiseClone() as Incident;
    }
    public void SetReleaseMessage(IStatePopulationEvent statePopulationEvent)
    {
        this.PopulationEvent = statePopulationEvent;
    }

}
