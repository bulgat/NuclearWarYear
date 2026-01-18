using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.AiTurn
{
    public class IncidentAttack
    {
        public GlobalParam.TypeEvent TypeEvent {  get; set; }
        public Incident SecondIncident { get; set; }
    }
}
