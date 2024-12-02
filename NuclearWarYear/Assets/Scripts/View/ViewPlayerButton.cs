using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.View
{
    public class ViewPlayerButton
    {
        public void SetPropagand(MenuScript menuScript,int FlagId, MainModel _mainModel)
        {
            menuScript._controller.Propaganda(_mainModel.GetCurrenFlagPlayer());
        }
    }
}
