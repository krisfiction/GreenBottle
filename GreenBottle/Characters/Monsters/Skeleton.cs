using System;
using System.Collections.Generic;
using System.Text;

namespace GreenBottle.Characters.Monsters
{
    internal class Skeleton : Monster
    {
        public Skeleton()
        {
            Name = "Skeleton";
            Icon = "S";
            HealthLow = 104;
            HealthHigh = 108;
            Gold = 10;
            WeaponDamageLow = 10;
            WeaponDamageHigh = 22;
        }
    }
}
