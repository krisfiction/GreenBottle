using System;
using System.Collections.Generic;
using System.Text;
using GreenBottle.Items;
using GreenBottle.Items.Scrolls;

namespace GreenBottle.Generator
{
    public static partial class Generate
    {
        public static Scroll Scroll()
        {
            switch (1)
            {
                case 1:
                    Teleportation teleportation = new Teleportation();
                    return teleportation;
            }
            //return null;
        }
    }
}
