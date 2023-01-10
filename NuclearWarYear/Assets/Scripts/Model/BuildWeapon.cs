using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class BuildWeapon
    {
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
        }

    }

