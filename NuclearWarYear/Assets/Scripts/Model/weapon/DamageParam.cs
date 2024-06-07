using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public class DamageParam
    {
        public int Damage {  get; private set; }
        public int IdImage { get; private set; }
        public DamageParam(int damage,int idImage) {
            this.Damage = damage;
            this.IdImage = idImage;
        }
    }
}
