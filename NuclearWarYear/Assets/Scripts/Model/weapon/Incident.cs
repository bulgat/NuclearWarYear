using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Incident: Weapon,IWeapon
{
    private static int UnicId = 0;
	
	public Incident(string name, DictionaryEssence.TypeWeapon type, int id, string message,int IdImage=0) {
		this.Name=name;
		this.Id = id;
		this.IdImage = IdImage;
        this.Type = type;
        this.Damage = 0;
        this.Message = message;
        this.Uid = UnicId++;
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
    public Incident Copy()
    {
        return this.MemberwiseClone() as Incident;
    }
}
