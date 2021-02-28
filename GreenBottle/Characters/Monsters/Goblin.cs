using System;
using System.Collections.Generic;
using System.Text;

namespace GreenBottle.Characters.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin()
        {
            Name = "Goblin";
            Icon = "G";
            HealthLow = 20;
            HealthHigh = 30;
            Gold = 20;
            WeaponDamageLow = 10;
            WeaponDamageHigh = 25;
        }
    }
}
