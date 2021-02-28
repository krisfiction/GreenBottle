using System;
using System.Collections.Generic;
using System.Text;

namespace GreenBottle.Characters.Monsters
{
    internal class Rat : Monster
    {
        public Rat()
        {
            Name = "Rat";
            Icon = "R";
            HealthLow = 10;
            HealthHigh = 15;
            Gold = 10;
            WeaponDamageLow = 2;
            WeaponDamageHigh = 10;

            HealthMax = Health = SetHealth();
        }
    }
}
