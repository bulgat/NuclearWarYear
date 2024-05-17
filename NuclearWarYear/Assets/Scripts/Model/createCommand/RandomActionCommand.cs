using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.createCommand
{
    public class RandomActionCommand
    {
        public string GetRandomActionCommand()
        {
            List<string> nameCommandList = new List<string>() {
            "Propaganda",
            "Building",
            "Defence",
            "Missle",
            "Bomber"
        };
            int indexCommand = UnityEngine.Random.Range(0, nameCommandList.Count);
            return nameCommandList[indexCommand];
        }
    }
}
