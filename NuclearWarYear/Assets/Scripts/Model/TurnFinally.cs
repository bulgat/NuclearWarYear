using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class TurnFinally
    {
        public string Message { get; set; } = "";
        public int TypeAttack { get; set; }
        public bool Attack { get; set; }

        public bool Missle { get; set; }
        public Incident OldIncident { get; set; }
    }
}
