using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class BuildWeapon
    {
        private List<string> _reportProducedWeaponList;
    
    public List<IWeapon> AddLiderBuildWeaponSwithAction()
    {
        List<IWeapon> resultList = new List<IWeapon>();
        List<bool> randomAddWeaponList = new List<bool>();
        this._reportProducedWeaponList = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            randomAddWeaponList.Add(false);
            if ((int)UnityEngine.Random.Range(0.0f, 2.0f) == 1)
            {
                randomAddWeaponList[i] = true;
            }
        }
        if (randomAddWeaponList[0])
        {
            resultList.Add(new DictionaryEssence().GetMissle(1) );
            this._reportProducedWeaponList.Add("ракета Light");
        }
        if (randomAddWeaponList[1])
        {
            resultList.Add(new DictionaryEssence().GetMissle(2));
            this._reportProducedWeaponList.Add("ракета Medium");
        }
        if (randomAddWeaponList[2])
        {
            resultList.Add(new DictionaryEssence().GetMissle(3));
            this._reportProducedWeaponList.Add("ракета Heavy");
        }
        if (randomAddWeaponList[3])
        {
            resultList.Add(new DictionaryEssence().GetMissle(4));
            this._reportProducedWeaponList.Add("ракета S Heavy");
        }
        if (randomAddWeaponList[4])
        {
            resultList.Add(new DictionaryEssence().GetBomber(4));
            this._reportProducedWeaponList.Add("бомбардировщик");
        }

        if (randomAddWeaponList[5])
        {
            resultList.Add(new DictionaryEssence().GetDefenceWeapon(6));
            this._reportProducedWeaponList.Add("Противоракеты");
        }
        
        return resultList;
    }
    public List<string>  GetReportProducedWeaponList() {
        return this._reportProducedWeaponList;
    }
}

