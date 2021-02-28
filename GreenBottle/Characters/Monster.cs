using System;
using System.Collections.Generic;
using System.Text;

namespace GreenBottle.Characters
{
    public class Monster : Character
    {
        public static Random random = new Random();

        // values for random health based on high/low - so monsters will have varying health
        public int HealthLow { get; set; }
        public int HealthHigh { get; set; }

        public int SetHealth()
        {
            //Health = random.Next(HealthLow, HealthHigh);
            //HealthMax = Health;

            return random.Next(HealthLow, HealthHigh);
        }

    }
}