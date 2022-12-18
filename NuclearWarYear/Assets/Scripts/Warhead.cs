using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warhead 
{
	public string Name;
	public int Size;
	public int Damage;
	
	public Warhead(string name, int size, int damage) {
		Name=name;
		Size=size;
		Damage=damage;
	}
}
