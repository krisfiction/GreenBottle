using System;
using System.Collections.Generic;
using System.Text;
using GreenBottle.Characters;
using GreenBottle.Characters.Monsters;


namespace GreenBottle.Generator
{
    public static partial class Generate
    {
        public static Monster Monster()
        {

            switch (random.Next(1, 5))
            {
                case 1:
                    Rat rat = new Rat();
                    //rat.HealthMax = rat.Health = rat.SetHealth(); //moved to Rat.cs
                    return rat;
                case 2:
                    Skeleton skeleton = new Skeleton();
                    skeleton.HealthMax = skeleton.Health = skeleton.SetHealth();
                    return skeleton;
                case 3:
                    Goblin goblin = new Goblin();
                    goblin.HealthMax = goblin.Health = goblin.SetHealth();
                    return goblin;
                case 4:
                    Troll troll = new Troll();
                    troll.HealthMax = troll.Health = troll.SetHealth();
                    return troll;
            }

            return null;
        }
    }
}
