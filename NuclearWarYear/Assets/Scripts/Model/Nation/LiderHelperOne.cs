using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Linq;

public class LiderHelperOne 
{
	public CountryLider GetLiderOne(List<CountryLider> CountryLiderList,int FlagId) {

		return CountryLiderList.Where(a=>a.FlagId==FlagId).FirstOrDefault();
	}
}
