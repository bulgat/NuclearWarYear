using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.turnEvent
{
    internal class TurnEventExecute
    {
        //public string Message { get; private set; }
        public string MessageSecond { get; private set; }
        public int NegativeMood { get; private set; }
        public bool Ammunition { get; private set; }

        public bool ChangePopulation { get; private set; }
        public bool Random { get; private set; }
        public bool RemoveDefenceWeapon { get; private set; }
        public bool Airport { get; private set; }
        public bool Revert { get; private set; }
        public TurnEventExecute(string messageSecond, int negativeMood, bool ammunition,bool changePopulation,
            bool random, bool removeDefenceWeapon,bool airport) { 
            //this.Message = message;
            this.MessageSecond = messageSecond;
            this.NegativeMood = negativeMood;
            this.Ammunition = ammunition;
            this.ChangePopulation = changePopulation;
            this.Random = random;
            this.RemoveDefenceWeapon = removeDefenceWeapon;
            this.Airport = airport;
           // this.Revert = revert;
        }
    }
}
