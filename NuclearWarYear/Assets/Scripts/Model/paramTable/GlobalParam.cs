using Assets.Scripts.Model.paramTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.param
{
    public class GlobalParam
    {
        public static int StartTurnIdImage = 18;
        public static int StartTurnIdFlag = 5;
        public enum ActionCommand { Propaganda, Defence, Defectors, Ufo, Baby, RocketRich, CrazyCow, Build, AttackMissle, AttackBomber, Bomber, Missle }

        public static List<EventFortuneIncident> EventFortuneIncidentList= new List<EventFortuneIncident>()
        {
            new EventFortuneIncident(ActionCommand.Defectors,30),
            new EventFortuneIncident(ActionCommand.Ufo,32),
            new EventFortuneIncident(ActionCommand.Baby,32),
            new EventFortuneIncident(ActionCommand.RocketRich, 30),
            new EventFortuneIncident(ActionCommand.CrazyCow,30)
        };



    }
}
