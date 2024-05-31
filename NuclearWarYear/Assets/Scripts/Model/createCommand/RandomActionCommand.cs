using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.createCommand
{
    public class RandomActionCommand
    {
        public GlobalParam.TypeEvent GetRandomActionCommand()
        {
            List<GlobalParam.TypeEvent> nameCommandList = new List<GlobalParam.TypeEvent>() 
            {
                GlobalParam.TypeEvent.Propaganda,
                GlobalParam.TypeEvent.Build,
                GlobalParam.TypeEvent.Defence,
                GlobalParam.TypeEvent.Missle,
                GlobalParam.TypeEvent.Bomber
            };
            int indexCommand = UnityEngine.Random.Range(0, nameCommandList.Count);
            return nameCommandList[indexCommand];
        }
    }
}
