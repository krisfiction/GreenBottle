using System;
using System.Collections.Generic;
using System.Text;
using GreenBottle.Characters;

namespace GreenBottle.Items.Potions
{
    public class Health : Potion
    {
        //public Health(int _x, int _y)
        public Health()
        {
            Name = "Health Potion"; //? remove potion from name
            Icon = "!"; //? move to be set in Potion.cs
            Type = "Potion";

            // X = _x; //! will this work - set position at creation 
            // Y = _y;
        }

        public static bool Cast(Player player)
        {
            if (player.HealthMax == player.Health) // you are at max HP
            {
                //Console.Clear();
               // Console.WriteLine("You are already as max health!");
               // ActivityLog.AddToLog("You are already as max health!");
                return false;
            }
            else
            {
                int oldHP = player.Health;
                int tooHeal = Convert.ToInt32((player.HealthMax / 100) * 60); // Convert.ToInt32 to prevent half numbers like 59.3 hp


                player.Health = Math.Min(player.HealthMax, player.Health + tooHeal); // compare 2 int's and return the lowest
                // above replaces below
                //if ((player.Health + tooHeal) > player.HealthMax) // if the potion will heal over max hp
                //{
                //    player.Health = player.HealthMax; // set hp to max
                //}
                //else // if potion will not over heal
                //{
                //    player.Health += tooHeal; // add heal amount to current hp
                //}

                //Console.Clear();
               // Console.WriteLine("you have used a 60% heal potion, you gained " + (player.Health - oldHP) + "hp,  you are now at " + player.Health + "hp");
                //ActivityLog.AddToLog("you have used a 60% heal potion, you gained " + (player.Health - oldHP) + "hp,  you are now at " + player.Health + "hp");

                return true;
            }
        }
    }
}
