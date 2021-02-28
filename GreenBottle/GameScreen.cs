using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using System;
using Console = SadConsole.Console;
using GreenBottle.Characters;
using GreenBottle.Characters.Monsters;
using System.Collections.Generic;
using GreenBottle.Generator;
using GreenBottle.Items;
using GreenBottle.Items.Potions;
using GreenBottle.Items.Scrolls;

namespace GreenBottle
{
    public class GameScreen : ContainerConsole
    {
        public Console MapConsole { get; }
        public const int mapConsoleWidth = 110;
        public const int mapConsoleHeight = 35;
        public const int mapConsolePOSx = 0;
        public const int mapConsolePOSy = 0;

        public Console ActivityLogConsole { get; }
        public const int ActivityLogConsoleWidth = 110;
        public const int ActivityLogConsoleHeight = 10;
        public const int ActivityLogConsolePOSx = 0;
        public const int ActivityLogConsolePOSy = 35;

        public Console StatsConsole { get; }
        public const int StatsConsoleWidth = 30;
        public const int StatsConsoleHeight = 35;
        public const int StatsConsolePOSx = 110;
        public const int StatsConsolePOSy = 0;

        public DungeonMap DungeonMap;
        //public ActivityLog ActivityLog;
        public Stats Stats;

        public Player Player;

        public Cell PlayerGlyph { get; set; }

        // public Point MyPoisition = new Point(4, 5); // testing

        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;

        private readonly Random random = new Random();
        
        private static List<Monster> activeMonsters = new List<Monster>();
        private static List<Item> activeItems = new List<Item>();

        public Point PlayerPosition //? redo this or get rid of it
        {
            get => _playerPosition;
            private set
            {
                // set boundry for player
                if (value.X < 0 || value.X >= MapConsole.Width || value.Y < 0 || value.Y >= MapConsole.Height)
                {
                    return;
                }

                // Restore map cell
                _playerPositionMapGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Move player
                _playerPosition = value;
                // Save map cell
                _playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Draw player
                PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Redraw the map
                MapConsole.IsDirty = true;
            }
        }

        public GameScreen()
        {
            DungeonMap = new DungeonMap();
            //ActivityLog = new ActivityLog(); //made static
            Stats = new Stats(); //? make static as well
            Player = new Player();

            // Setup map
            MapConsole = new Console(mapConsoleWidth, mapConsoleHeight) //size of window
            {
                Position = new Point(mapConsolePOSx, mapConsolePOSy), //position of window
                DefaultBackground = Color.Transparent,
                Parent = this
            };

            // Setup Log
            ActivityLogConsole = new Console(ActivityLogConsoleWidth, ActivityLogConsoleHeight) //size on window
            {
                Position = new Point(ActivityLogConsolePOSx, ActivityLogConsolePOSy), //position of window
                DefaultBackground = Color.Black,
                DefaultForeground = Color.White,
                Parent = this
            };

            // Setup Stats
            StatsConsole = new Console(StatsConsoleWidth, StatsConsoleHeight) //size on window = 30, 35
            {
                Position = new Point(StatsConsolePOSx, StatsConsolePOSy), //position of window = 110, 0
                DefaultBackground = Color.Black,
                Parent = this
            };

            StartGame();

        }

        public void StartGame()
        {
            //CaveMap caveMap = new CaveMap();

            //caveMap.Initialize(40);
            //caveMap.Filter();
            //caveMap.Filter();
            //caveMap.MakeBoarder();

            //caveMap.Display(MapConsole);

            //DungeonMap dungeonMap = new DungeonMap();

            DungeonMap.Initialize();
            //dungeonMap.CreateOneRoom();



            //START monster coode
            List<Monster> activeMonsters = new List<Monster>();

            //generate 5 monsters
            for (int i = 0; i < 5; i++)
            {
                activeMonsters.Add(Generate.Monster());
            }

            //add monsters to map
            for (int i = 0; i < activeMonsters.Count; i++)
            {
                var (x, y) = DungeonMap.PlaceMonster(activeMonsters[i]);

                activeMonsters[i].X = x;
                activeMonsters[i].Y = y;
            }
            //END monster code


            //START item code
            for (int i = 0; i < 3; i++)
            {
                activeItems.Add(Generate.Potion());
                activeItems.Add(Generate.Scroll());
            }
            //add items to map
            for (int i = 0; i < activeItems.Count; i++)
            {
                var (x, y) = DungeonMap.PlaceItem(activeItems[i]);

                activeItems[i].X = x;
                activeItems[i].Y = y;
            }
            //END item code

            //START inventory code
            Inventory inventory = new Inventory();
            inventory.Initialize();
            //END inventory code

            UpdateDisplay();

            CreatePlayer();

            //UpdateDisplay();
        }






        public void CreatePlayer()
        {
            // Setup player
            PlayerGlyph = new Cell(Color.Red, Color.Black, 64); // 64 = @

            //_playerPosition = new Point(random.Next(1, mapConsoleWidth), random.Next(1, mapConsoleHeight));
            _playerPosition = RandomPosition();

            _playerPositionMapGlyph = new Cell();
            _playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);


            Player.Name = "Tunk";
            Player.HPMax = 100;
            Player.HP = 100;
            Player.X = _playerPosition.X;
            Player.Y = _playerPosition.Y;
            

        }

        //check if random starting position is walkable and not a hallway
        public Point RandomPosition()
        {
            bool _isWalkable;
            bool _isHallway;
            Point _point;

            do
            {
                do
                {
                    _point.X = random.Next(0, mapConsoleWidth - 1);
                    _point.Y = random.Next(0, mapConsoleHeight - 1);
                    _isHallway = DungeonMap.IsHallway(_point.X, _point.Y);
                    _isWalkable = DungeonMap.IsWalkable(_point.X, _point.Y);
                } while (_isHallway);
            } while (!_isWalkable);
            //} while (_isHallway && !_isWalkable); // broken

            //MapConsole.Print(1, 1, $"isHallway: {_isHallway}, isWalkable: {_isWalkable}"); //testing

            return _point;
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            Point newPlayerPosition = PlayerPosition;

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                //DungeonMap.Initialize();
                //DungeonMap.Display(MapConsole);

                //newPlayerPosition = RandomPosition(); // seems to be woring but leave old map glyph in place of previous spot


                StartGame();
                newPlayerPosition = RandomPosition(); // seems to be woring but leave old map glyph in place of previous spot
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.F6))
            {
                newPlayerPosition = RandomPosition();
            }

            // movement keys
            //? move IsWalkable to out of specific map, then pass map along with X, Y cordinates (DungeonMap and CaveMap)
            //? create ActiveMap variable and send DungeonMap and CaveMap to it
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad8))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.North;
                    ActivityLog.AddToLog("You move North.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad2))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.South;
                    ActivityLog.AddToLog("You move South.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad4))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y))
                {
                    newPlayerPosition += SadConsole.Directions.West;
                    ActivityLog.AddToLog("You move West.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad6))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y))
                {
                    newPlayerPosition += SadConsole.Directions.East;
                    ActivityLog.AddToLog("You move East.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad7))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.NorthWest;
                    ActivityLog.AddToLog("You move NorthWest.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad9))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.NorthEast;
                    ActivityLog.AddToLog("You move NorthEast.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad1))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.SouthWest;
                    ActivityLog.AddToLog("You move SouthWest.");
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad3))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.SouthEast;
                    ActivityLog.AddToLog("You move SouthEast.");
                }
            }


            if(info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.G))
            {
                for (int i = 0; i < activeItems.Count; i++)
                {
                    if (activeItems[i].X == _playerPosition.X && activeItems[i].Y == _playerPosition.Y)
                    {
                        ActivityLog.AddToLog("you pick up " + activeItems[i].Name);

                        if (activeItems[i].Type == "Potion")
                        {
                            Inventory.PotionInventory.Add((Health)activeItems[i]);
                        }
                        if (activeItems[i].Type == "Scroll")
                        {
                            Inventory.ScrollInventory.Add((Teleportation)activeItems[i]);
                        }

                        activeItems.RemoveAt(i); // remove item from active list

                        DungeonMap.ProcessItemTile(Player); //set tile.IsItem to false
                    }
                }
            }




            //DungeonMap.LightRadius(MapConsole, _playerPosition.X, _playerPosition.Y); //testing

            if (newPlayerPosition != PlayerPosition)
            {
                PlayerPosition = newPlayerPosition;

                Player.X = newPlayerPosition.X;
                Player.Y = newPlayerPosition.Y;
                
                return true;
            }

            //UpdateDisplay();
            ActivityLog.Display(ActivityLogConsole);

            return false;
        }

        public void UpdateDisplay()
        {
            DungeonMap.Display(MapConsole);
            ActivityLog.Display(ActivityLogConsole);
            Stats.Display(StatsConsole);
        }
    }
}