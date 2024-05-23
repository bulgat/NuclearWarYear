using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.turnEvent
{
    public class TurnEventExecute
    {
        public string MessageSecond { get; private set; }
        public int NegativeMood { get; private set; }
        public bool Ammunition { get; private set; }

        public bool ChangePopulation { get; private set; }
        public bool Random { get; private set; }
        public bool RemoveDefenceWeapon { get; private set; }
        public bool Airport { get; private set; }
        public bool Revert { get; private set; }
        public bool ShowFiend { get; private set; }
        public TurnEventExecute(string messageSecond, int negativeMood, bool ammunition,bool changePopulation,
            bool random, bool removeDefenceWeapon,bool airport, bool showFiend) { 
            this.MessageSecond = messageSecond;
            this.NegativeMood = negativeMood;
            this.Ammunition = ammunition;
            this.ChangePopulation = changePopulation;
            this.Random = random;
            this.RemoveDefenceWeapon = removeDefenceWeapon;
            this.Airport = airport;
            this.ShowFiend = showFiend;
        }
    }
}
