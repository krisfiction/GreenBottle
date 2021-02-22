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

        //public int mapConsoleWidth = (int)((Global.RenderWidth / Global.FontDefault.Size.X) * 1.0);
        //public int mapConsoleHeight = (int)((Global.RenderHeight / Global.FontDefault.Size.Y) * 1.0);
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

        public static CaveMap CaveMap { get; }
        public static DungeonMap DungeonMap { get; }

        private readonly Random random = new Random();

        public Point PlayerPosition
        {
            get => _playerPosition;
            private set
            {
                // set boundry for player
                // change value 1 to 0 to make it be the edge of the screen
                //if (value.X < 1 || value.X >= MapConsole.Width - 1 || value.Y < 1 || value.Y >= MapConsole.Height - 1)
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

            DungeonMap dungeonMap = new DungeonMap();

            dungeonMap.Initialize();
            //dungeonMap.CreateOneRoom();

            dungeonMap.Display(MapConsole);




            CreatePlayer();
        }

        public void CreatePlayer()
        {
            // Setup player
            PlayerGlyph = new Cell(Color.Red, Color.Black, 64); // 64 = @

            //_playerPosition = new Point(random.Next(1, mapConsoleWidth), random.Next(1, mapConsoleHeight));
            RandomPosition();

            _playerPositionMapGlyph = new Cell();
            _playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
        }

        //check if random starting position is walkable and not a hallway
        public void RandomPosition()
        {
            int _x;
            int _y;
            bool _isWalkable;
            bool _isHallway;
            do
            {
                _x = random.Next(0, mapConsoleWidth - 1);
                _y = random.Next(0, mapConsoleHeight - 1);
                _isWalkable = DungeonMap.IsWalkable(_x, _y);
                _isHallway = DungeonMap.IsHallway(_x, _y);
            } while (!_isWalkable && !_isHallway);
            _playerPosition = new Point(_x, _y);
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            Point newPlayerPosition = PlayerPosition;

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                //RandomPosition();
                //_playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[PlayerPosition.X, PlayerPosition.Y]);
                //PlayerGlyph.CopyAppearanceTo(MapConsole[newPlayerPosition.X, newPlayerPosition.Y]);
            }

            // movement keys
            //todo add all 8 directions using numpad
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) || info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad8))
            {
                if (DungeonMap.IsWalkable(newPlayerPosition.X, newPlayerPosition.Y - 1))
                {
                    newPlayerPosition += SadConsole.Directions.North;
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

            if (newPlayerPosition != PlayerPosition)
            {
                PlayerPosition = newPlayerPosition;
                return true;
            }

            return false;
        }
    }
}