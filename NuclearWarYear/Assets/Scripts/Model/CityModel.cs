﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityModel 
{
    public int FlagId;
    private int _population;
    private int _futurePopulation;
    private int Id;
    public int CountryId { get; }
    public CityModel(int countryId, int id) {
        this.CountryId = countryId;
        _population = 40;
        _futurePopulation = _population;
        this.Id = id;
        
    }
 
    public int GetId()
    {
        return this.Id;
    }
    public int GetPopulation()
    {
        return _population;
    }
    public void SetFuturePopulation(int FuturePopulation)
    {
        _futurePopulation = FuturePopulation;

    }
    public void SetPresentlyPopulation()
    {
        _population = _futurePopulation;

    }
}
