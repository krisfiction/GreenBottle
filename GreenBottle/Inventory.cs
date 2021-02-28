using System;
using System.Collections.Generic;
using System.Text;
using GreenBottle.Items.Potions;
using GreenBottle.Items.Scrolls;
using GreenBottle.Characters;

namespace GreenBottle
{
    public class Inventory
    {
        public static List<Items.Potion> PotionInventory { get; set; }
        public static List<Items.Scroll> ScrollInventory { get; set; }

        public void Initialize()
        {
            PotionInventory = new List<Items.Potion>();
            ScrollInventory = new List<Items.Scroll>();
        }



        public static void Display(Player player, DungeonMap map, string _type)
        {
            if (_type == "potion")
            {
                if (PotionInventory.Count == 0)
                {
                    Console.WriteLine("Potion Inventory Empty.");
                }
                else
                {
                    Console.WriteLine("Potion Inventory:");
                    Console.WriteLine();

                    //int _lineNumber = 0;
                    char _lineNumber = 'a';
                    foreach (var Potion in PotionInventory)
                    {
                        Console.WriteLine($"{_lineNumber}: {Potion.Name}");
                        _lineNumber++;
                    }
                    if (_type == "potion")
                    {
                        GetInput(player, map, "potion");
                    }

                }
            }
            if (_type == "scroll")
            {
                if (ScrollInventory.Count == 0)
                {
                    Console.WriteLine("Scroll Inventory Empty.");
                }
                else
                {
                    Console.WriteLine("Scroll Inventory:");
                    Console.WriteLine();

                    char _lineNumber = 'a';
                    foreach (var Scroll in ScrollInventory)
                    {
                        Console.WriteLine($"{_lineNumber}: {Scroll.Name}");
                        _lineNumber++;
                    }
                    if (_type == "scroll")
                    {
                        GetInput(player, map, "scroll");
                    }
                }
            }
            if (_type == "all")
            {
                //todo clean up and make useable
                char _lineNumber = 'a';
                foreach (var Scroll in ScrollInventory)
                {
                    Console.WriteLine($"{_lineNumber}: {Scroll.Name}");
                    _lineNumber++;
                }
                foreach (var Potion in PotionInventory)
                {
                    Console.WriteLine($"{_lineNumber}: {Potion.Name}");
                    _lineNumber++;
                }

            }
        }

        public static void GetInput(Player player, DungeonMap map, string _type)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enter Letter:");

            //int i = Convert.ToInt32(Console.ReadLine());
            ConsoleKey cInput = Console.ReadKey(true).Key; //true hides input

            if (cInput == ConsoleKey.Escape)
            {
                return; //? is this the correct way to do this
            }

            int i = (int)cInput - 65; //a == 0

            if (_type == "potion")
            {
                if (PotionInventory[i].Name == "Health Potion" && Health.Cast(player))
                {
                    PotionInventory.RemoveAt(i);
                }
            }
            if (_type == "scroll")
            {
                if (ScrollInventory[i].Name == "Teleportation Scroll")
                {
                    Teleportation.Cast(player, map);
                }

                ScrollInventory.RemoveAt(i);
            }
        }


        public static void Loop(Player player, DungeonMap map, string _type)
        {

            bool _displayInventory = true;
            do
            {
                Display(player, map, _type);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("press 'esc' to return to the map.");


                Console.SetCursorPosition(0, 35);
                //ActivityLog.Display();

                ConsoleKey bInput = Console.ReadKey(true).Key; //true hides input
                if (bInput == ConsoleKey.Escape)
                {
                    _displayInventory = false;
                }


            } while (_displayInventory);


        }

    }
}
