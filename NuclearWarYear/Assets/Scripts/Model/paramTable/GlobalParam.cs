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
        public static bool TestMode = false;
        public static int StartTurnIdImage = 18;
        public static int StartTurnIdFlag = 5;

        public static List<TypeEvent> GroupMissleList = new List<TypeEvent>()
        {
            TypeEvent.Missle,TypeEvent.HeavyMissle
        };

        public static List<TypeEvent> GroupBomberList = new List<TypeEvent>()
        {
            TypeEvent.Bomber,TypeEvent.HeavyBomber
        };

        public enum TypeEvent
        {
            Build, Bomber, HeavyBomber, Defence, HeavyDefence,Propaganda, Ufo, Baby, RocketRich,
            CrazyCow, Defectors, Missle, HeavyMissle,
            AttackBomber, AttackMissle, Airport, AttackAirport, Incident,None
        }

        public enum Scene
        {
            MenuStart, Victory, GameScene
        }

        public static Dictionary<GlobalParam.TypeEvent, TurnEventExecute> MessageDictionary = 
            new Dictionary<GlobalParam.TypeEvent, TurnEventExecute>() {
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
            {TypeEvent.HeavyMissle, new TurnEventExecute( null, 0, false, true, false, false, true,false) },
            {TypeEvent.Bomber, new TurnEventExecute( null, 0, false, true, false, false, true,false) } ,
            {TypeEvent.HeavyBomber, new TurnEventExecute( null, 0, false, true, false, false, true,false) }
        };

        public static List<ScenarioLider> ParamLiderList = new List<ScenarioLider>()
        {

            new ScenarioLider(1, "Путин", 0, new List<string>(){"Moscow","Peterburg","Vladivostok","Novosibirsk","Sevastopol"}),
            new ScenarioLider(2, "Зеленс", 1, new List<string>(){"Kiev","Odessa","Lvov","Harkov","Nikolaev"}),
            new ScenarioLider(3, "Байден", 2, new List<string>(){"Vashington","New-York","Los angeles","Chicago","Houston"}),
            new ScenarioLider(4, "Си Цзип", 3, new List<string>(){"Pekin","Shanghai","Chongqing","Beijing","Chengdu"}),
            new ScenarioLider(5, "Цезарь", 5, new List < string >() { "Rome", "Konstantinopol", "Alexandria", "Siracuse", "Efes" }),
            new ScenarioLider(6, "Сталин", 4, new List < string >() { "Moscow", "Peterburg", "Vladivostok", "Novosibirsk", "Sevastopol" })
        };
    }
}
