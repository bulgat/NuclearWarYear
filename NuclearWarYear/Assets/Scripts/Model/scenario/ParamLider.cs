using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.scenario
{
    public class ParamLider
    {
        public List<ScenarioLider> ScenarioLider_ar;
        public ParamLider(int flagIdPlayer) {
            ScenarioLider_ar = new List<ScenarioLider>();
            ScenarioLider_ar.Add(new ScenarioLider(1, "Путин",0));
            ScenarioLider_ar.Add(new ScenarioLider(2, "Зеленс",1));
            ScenarioLider_ar.Add(new ScenarioLider(3, "Байден",2));
            ScenarioLider_ar.Add(new ScenarioLider(4, "Си Цзип",3));
            ScenarioLider_ar.Add(new ScenarioLider(flagIdPlayer, "Zed",4));
            ScenarioLider_ar.Add(new ScenarioLider(5, "Zedee", 4));
        }
        
    }
}
