using System;
using System.Collections.Generic;
using System.Text;

namespace GreenBottle.Characters.Monsters
{
    internal class Troll : Monster
    {
        public Troll()
        {
            Name = "Troll";
            Icon = "T";
            HealthLow = 25;
            HealthHigh = 52;
            Gold = 10;
            WeaponDamageLow = 18;
            WeaponDamageHigh = 22;
        }
    }
}
