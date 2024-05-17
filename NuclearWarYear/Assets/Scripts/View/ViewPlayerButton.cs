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

            //EventController eventController = new EventController(Controller.Command.Propaganda, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            //menuScript._controller.SendCommand(eventController);
            menuScript._controller.Propaganda(_mainModel.GetCurrenFlagPlayer());
        }
    }
}
