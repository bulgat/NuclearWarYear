﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.Nation
{
    public class IncidentEvent
    {
        public string NameEvent { get; private set; }
        public int IdEvent { get; private set; }
        public string EventMessage { get;set;}
        public IncidentEvent(string Name) {
            this.NameEvent = Name;
            this.IdEvent = new DictionaryEssence().GetIdEvent(Name);
        }
        
    }
}