using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public interface IWeapon
    {
        public DictionaryMissle.TypeWeapon GetTypeWeapon();
        public int GetSize();
        public int GetDamage();
        public void SetDamage(int damage);
    }

}
