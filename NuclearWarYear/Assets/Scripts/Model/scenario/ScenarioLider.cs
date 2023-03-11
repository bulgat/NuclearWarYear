using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.scenario
{
    public class ScenarioLider
    {
  
        public string Name { set; get; }
        public int FlagId { set; get; }
        public int GraphicId { get; }
        public ScenarioLider(int flagId,string name,int graphicId)
        {
            this.Name = name;
            this.FlagId = flagId;
            this.GraphicId = graphicId;
        }

    }
}
