using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class BuildWeapon
    {
        private List<string> _reportProducedWeaponList;
    /*
        public List<string>  AddLiderBuildWeapon(CountryLider lider)
        {
            List<bool> randomAddWeaponList = new List<bool>();
            List<string> reportProducedWeaponList = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                randomAddWeaponList.Add(false);
                if ((int)UnityEngine.Random.Range(0.0f, 3.0f) == 1)
                {
                  randomAddWeaponList[i]=true;
                }
            }
            if (randomAddWeaponList[0])
            {
                lider.AddMissle(lider.GetCommandLider().GetMissle());
                reportProducedWeaponList.Add("ракета");
            }
            if (randomAddWeaponList[1])
            {
                lider.AddBomber(lider.GetCommandLider().GetBomber());
                reportProducedWeaponList.Add("бомбардировщик");
            }

            if (randomAddWeaponList[3])
            {
                lider.AddDefenceWeapon(lider.GetCommandLider().GetDefenceWeapon());
                reportProducedWeaponList.Add("Противоракеты");
            }

            return reportProducedWeaponList;
        }*/
    public List<Weapon> AddLiderBuildWeaponSwithAction()
    {
        List<Weapon> resultList = new List<Weapon>();
        List<bool> randomAddWeaponList = new List<bool>();
        this._reportProducedWeaponList = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            randomAddWeaponList.Add(false);
            if ((int)UnityEngine.Random.Range(0.0f, 3.0f) == 1)
            {
                randomAddWeaponList[i] = true;
            }
        }
        if (randomAddWeaponList[0])
        {
            resultList.Add(new DictionaryMissle().GetMissle(1) );
            this._reportProducedWeaponList.Add("ракета Light");
        }
        if (randomAddWeaponList[1])
        {
            resultList.Add(new DictionaryMissle().GetMissle(2));
            this._reportProducedWeaponList.Add("ракета Medium");
        }
        if (randomAddWeaponList[2])
        {
            resultList.Add(new DictionaryMissle().GetMissle(3));
            this._reportProducedWeaponList.Add("ракета Heavy");
        }
        if (randomAddWeaponList[3])
        {
            resultList.Add(new DictionaryMissle().GetMissle(4));
            this._reportProducedWeaponList.Add("ракета S Heavy");
        }
        if (randomAddWeaponList[4])
        {
            resultList.Add(new DictionaryMissle().GetBomber(1));
            this._reportProducedWeaponList.Add("бомбардировщик");
        }

        if (randomAddWeaponList[5])
        {
            resultList.Add(new DictionaryMissle().GetDefenceWeapon(1));
            this._reportProducedWeaponList.Add("Противоракеты");
        }
        
        return resultList;
    }
    public List<string>  GetReportProducedWeaponList() {
        return this._reportProducedWeaponList;
    }
}

