using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using System;
using Console = SadConsole.Console;

namespace GreenBottle
{
    public class GameScreen : ContainerConsole
    {
        public Console MapConsole { get; }
        public const int mapConsoleWidth = 110;
        public const int mapConsoleHeight = 35;
        public const int mapConsolePOSx = 0;
        public const int mapConsolePOSy = 0;

        public Console ActivityLogWindow { get; }
        public const int ActivityLogWindowWidth = 110;
        public const int ActivityLogWindowHeight = 10;
        public const int ActivityLogWindowPOSx = 0;
        public const int ActivityLogWindowPOSy = 35;

        public Cell PlayerGlyph { get; set; }

        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;

        // public static CaveMap CaveMap { get; }
        // public static DungeonMap DungeonMap { get; }
        public DungeonMap DungeonMap;

        private readonly Random random = new Random();

        public Point PlayerPosition
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

            // Setup map
            MapConsole = new Console(mapConsoleWidth, mapConsoleHeight) //size of window
            {
                Position = new Point(mapConsolePOSx, mapConsolePOSy), //position of window
                DefaultBackground = Color.Transparent,
                Parent = this
            };

            // Setup Log
            ActivityLogWindow = new Console(ActivityLogWindowWidth, ActivityLogWindowHeight) //size on window
            {
                Position = new Point(ActivityLogWindowPOSx, ActivityLogWindowPOSy), //position of window
                DefaultBackground = Color.DarkGray,
                Parent = this
            };

            //CaveMap caveMap = new CaveMap();

            //caveMap.Initialize(40);
            //caveMap.Filter();
            //caveMap.Filter();
            //caveMap.MakeBoarder();

            //caveMap.Display(MapConsole);

            //DungeonMap dungeonMap = new DungeonMap();

            DungeonMap.Initialize();
            //dungeonMap.CreateOneRoom();

            
            DungeonMap.Display(MapConsole);

            CreatePlayer();
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
                DungeonMap.Initialize();
                DungeonMap.Display(MapConsole);

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
                    DungeonMap.LightRadius(MapConsole, _playerPosition.X, _playerPosition.Y); //todo needs work
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad2))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.South;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad4))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y))
                {
                    newPlayerPosition += SadConsole.Directions.West;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad6))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y))
                {
                    newPlayerPosition += SadConsole.Directions.East;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad7))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.NorthWest;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad9))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.NorthEast;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad1))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X - 1, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.SouthWest;
                }
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad3))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X + 1, newPlayerPosition.Y + 1))
                {
                    newPlayerPosition += SadConsole.Directions.SouthEast;
                }
            }

            DungeonMap.LightRadius(MapConsole, _playerPosition.X, _playerPosition.Y);

            if (newPlayerPosition != PlayerPosition)
            {
                PlayerPosition = newPlayerPosition;
                return true;
            }

            return false;
        }
    }
}