using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.View
{
    public class ViewLiderHelper
    {
        public int GetNumberSpriteLider(int indexLider,int Mood)
        {
            return (indexLider * 8) + Mood;
        }
        
    }
}
