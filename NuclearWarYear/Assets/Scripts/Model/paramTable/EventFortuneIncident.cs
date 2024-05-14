using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.paramTable
{
    public class EventFortuneIncident
    {
        public GlobalParam.ActionCommand Name { get; private set; }
        public int Random {  get; private set; }
        public EventFortuneIncident(GlobalParam.ActionCommand name, int random)
        {
            this.Name = name;
            this.Random = random;
        }
    }
}
