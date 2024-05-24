using Assets.Scripts.Model.paramTable;
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
        public enum ActionCommand { Propaganda, Defence, Defectors, Ufo, Baby, RocketRich, CrazyCow, Build, AttackMissle, AttackBomber, Bomber, Missle }

        public static List<EventFortuneIncident> EventFortuneIncidentList= new List<EventFortuneIncident>()
        {
            new EventFortuneIncident(ActionCommand.Defectors,30),
            new EventFortuneIncident(ActionCommand.Ufo,32),
            new EventFortuneIncident(ActionCommand.Baby,32),
            new EventFortuneIncident(ActionCommand.RocketRich, 30),
            new EventFortuneIncident(ActionCommand.CrazyCow,30)
        };
        public static Dictionary<string, TurnEventExecute> MessageDictionary = new Dictionary<string, TurnEventExecute>() {
            { DictionaryEssence.TypeEvent.RocketRich.ToString(), new TurnEventExecute(null,0,false,true,false,false, false,false)},
            { DictionaryEssence.TypeEvent.Baby.ToString(), new TurnEventExecute( null, 0,false, true, false,false, false,false) },
            {DictionaryEssence.TypeEvent.Ufo.ToString(), new TurnEventExecute( null, 0, false,true, false,false, false,false) },
            {DictionaryEssence.TypeEvent.CrazyCow.ToString(), new TurnEventExecute( null, 0, false,true, false,false, false,false) },
            {DictionaryEssence.TypeEvent.Defectors.ToString(), new TurnEventExecute( null, 5,false, true, false,false, false,true) },
            {DictionaryEssence.TypeEvent.Building.ToString(), new TurnEventExecute(null, 5, true, false, false,false, false,false) },
            {DictionaryEssence.TypeEvent.Propaganda.ToString(), new TurnEventExecute(
                " сбежав от ", 5, false, true,true,false, false,true)},
            { DictionaryEssence.TypeEvent.AttackBomber.ToString(), new TurnEventExecute(null, 0, false, true, false,false, false,true) },
            { DictionaryEssence.TypeEvent.AttackMissle.ToString(), new TurnEventExecute( null, 0,false, true, false,false, false,true) },
            {DictionaryEssence.TypeEvent.Defence.ToString(), new TurnEventExecute( null, 0, false, true, false,true, false,false) },
            {DictionaryEssence.TypeEvent.Airport.ToString(), new TurnEventExecute( null, 0, false, true, false,false, true,false) },
            {DictionaryEssence.TypeEvent.Missle.ToString(), new TurnEventExecute( null, 0, false, true, false, false, true,false) },
            {DictionaryEssence.TypeEvent.Bomber.ToString(), new TurnEventExecute( null, 0, false, true, false, false, true,false) } };


    }
}
