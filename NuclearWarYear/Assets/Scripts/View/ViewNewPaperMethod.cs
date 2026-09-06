using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.View
{
    public class ViewNewPaperMethod: ViewResourcуBase
    {
        public Button ButtonBack;
        public Button ButtonNext;
        private List<string> _messageArchvList;
        private int _page;

        public void Awake()
        {
            Debug.Log("0700 IncidentComma =   name = "  );
            ButtonBack.onClick.AddListener(() => ButtonBackMethod());
            ButtonNext.onClick.AddListener(() => ButtonNextMethod());
        }
        private void ButtonBackMethod()
        {
            Debug.Log("0712 INCIDENT AttackMissle Lid  Target   CommandIncident  S L= " + _messageArchvList.Count);
            if (_page > 0)
            {
                Debug.Log("0713 NuclearExplode    AttackBomber  =   SEC = " + _page);
                _page -= 1;

            }
            Message(_messageArchvList[_page]);
            Debug.Log("0714 Add AttackBomb  Finally.OldIncide  LiderLi = "  + _page);
            Debug.Log("0715  country   Lid   = " + _messageArchvList[_page]);
        }
        private void ButtonNextMethod()
        {
            if (_page < _messageArchvList.Count-1) {
                _page += 1;

            }
            Message(_messageArchvList[_page]);
            Debug.Log("0755    CommandIncident   CountYe  = "  );
        }
        public override void SetMessage(string messageList)
        {

        }
        public void SetMessageList(List<string> messageList)
        {
            _messageArchvList = messageList;
            _page = messageList.Count - 1;
            Debug.Log("0781 DEAD  untYear na  To GetTargetBom   = " + _messageArchvList.Count);
            Message(_messageArchvList[_page]);
        }
        public void Message(string message)
        {
            textPopulation.GetComponent<Text>().text = message;
        }
    }
}
