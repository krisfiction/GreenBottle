using System;
using System.Collections.Generic;
using System.Text;
using GreenBottle.Items;
using GreenBottle.Items.Potions;

namespace GreenBottle.Generator
{
    public static partial class Generate
    {
        public static Potion Potion()
        {
            switch (1)
            {
                case 1:
                    Health health = new Health();
                    return health;
            }
            //return null;
        }
    }
}