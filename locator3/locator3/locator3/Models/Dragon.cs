using System;
using System.Collections.Generic;
using System.Text;

namespace locator3.Models
{
    class Dragon
    {
        public string Image { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Armour { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string SpecialAttack { get; set; }

        public Dragon()
        {
            SpecialAttack = null;
        }

    }
}
