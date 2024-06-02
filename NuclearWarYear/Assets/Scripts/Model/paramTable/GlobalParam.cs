using Assets.Scripts.Model.paramTable;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model.turnEvent;
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
        public enum ActionCommand { Propaganda, Defence, Defectors, Ufo, Baby,
            RocketRich, CrazyCow, Build, AttackMissle, AttackBomber, Bomber, Missle,None }

        public enum TypeEvent
        {
            Build, Bomber, HeavyBomber, Defence, Propaganda, Industry, Ufo, Baby, RocketRich,
            CrazyCow, Defectors, Missle,
            AttackBomber, AttackMissle, Airport, AttackAirport,None
        }

        public static List<EventFortuneIncident> EventFortuneIncidentList= new List<EventFortuneIncident>()
        {
            new EventFortuneIncident(GlobalParam.TypeEvent.Defectors,30),
            new EventFortuneIncident(GlobalParam.TypeEvent.Ufo,32),
            new EventFortuneIncident(GlobalParam.TypeEvent.Baby,32),
            new EventFortuneIncident(GlobalParam.TypeEvent.RocketRich, 30),
            new EventFortuneIncident(GlobalParam.TypeEvent.CrazyCow,30)
        };
        public static Dictionary<GlobalParam.TypeEvent, TurnEventExecute> MessageDictionary = new Dictionary<GlobalParam.TypeEvent, TurnEventExecute>() {
            { TypeEvent.RocketRich, new TurnEventExecute(null,0,false,true,false,false, false,false)},
            { TypeEvent.Baby, new TurnEventExecute( null, 0,false, true, false,false, false,false) },
            {TypeEvent.Ufo, new TurnEventExecute( null, 0, false,true, false,false, false,false) },
            {TypeEvent.CrazyCow, new TurnEventExecute( null, 0, false,true, false,false, false,false) },
            {TypeEvent.Defectors, new TurnEventExecute( null, 5,false, true, false,false, false,true) },
            {TypeEvent.Build, new TurnEventExecute(null, 5, true, false, false,false, false,false) },
            {TypeEvent.Propaganda, new TurnEventExecute(
                " сбежав от ", 5, false, true,true,false, false,true)},
            { TypeEvent.AttackBomber, new TurnEventExecute(null, 0, false, true, false,false, false,true) },
            { TypeEvent.AttackMissle, new TurnEventExecute( null, 0,false, true, false,false, false,true) },
            {TypeEvent.Defence, new TurnEventExecute( null, 0, false, true, false,true, false,false) },
            {TypeEvent.Airport, new TurnEventExecute( null, 0, false, true, false,false, true,false) },
            {TypeEvent.Missle, new TurnEventExecute( null, 0, false, true, false, false, true,false) },
            {TypeEvent.Bomber, new TurnEventExecute( null, 0, false, true, false, false, true,false) } };

        public static List<ScenarioLider> ParamLiderList = new List<ScenarioLider>()
        {

            new ScenarioLider(1, "Путин", 0),
            new ScenarioLider(2, "Зеленс", 1),
            new ScenarioLider(3, "Байден", 2),
            new ScenarioLider(4, "Си Цзип", 3),
            new ScenarioLider(5, "Zed", 4),
            new ScenarioLider(6, "Сталин", 4)
        };
    }
}
