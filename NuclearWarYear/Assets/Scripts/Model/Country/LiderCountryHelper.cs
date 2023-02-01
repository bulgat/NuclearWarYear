using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LiderCountryHelper 
{
    static List<CountryLider> CountryLiderList;
    public static void Init(List<CountryLider> countryLiderList)
    {
        CountryLiderList = countryLiderList;
    }
    public CountryLider GetLiderWithFlag(int flagId)
    {
        return CountryLiderList.Where(a => a.FlagId == flagId).FirstOrDefault();
    }
    public int GetLiderIndexWithFlag(int flagId)
    {
        CountryLider countryLider = CountryLiderList.Where(a => a.FlagId == flagId).FirstOrDefault();
        return CountryLiderList.IndexOf(countryLider);
    }
}
