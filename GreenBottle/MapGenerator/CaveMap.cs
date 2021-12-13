using System;
using Console = SadConsole.Console;

namespace GreenBottle
{
    public class CaveMap
    {
        private const string WallIcon = "#";
        private const string FloorIcon = ".";

        private const int MapSizeX = 110;
        private const int MapSizeY = 35;

        private int passes = 0; //? new name for better understanding

        private static readonly Tile[,] GameMap = new Tile[MapSizeX, MapSizeY];

        private static readonly Tile[,] TempMap = new Tile[MapSizeX, MapSizeY];

        private static readonly Random random = new Random();



        public void MakeTileFloor(string _map, int _x, int _y)
        {
            if (_map == "Temp")
            {
                TempMap[_x, _y] = new Tile(_x, _y, FloorIcon, false, false, true, false, false, false);
            }

            if (_map == "Game")
            {
                GameMap[_x, _y] = new Tile(_x, _y, FloorIcon, false, false, true, false, false, false);
            }
        }

        public void MakeTileWall(string _map, int _x, int _y)
        {
            if (_map == "Temp")
            {
                TempMap[_x, _y] = new Tile(_x, _y, WallIcon, false, true, false, false, false, false);
            }

            if (_map == "Game")
            {
                GameMap[_x, _y] = new Tile(_x, _y, WallIcon, false, true, false, false, false, false);
            }
        }



        public int Initialize(int _wallChance)
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    MakeTileWall("Temp", x, y); //fill TempMap with all walls


                    if (random.Next(1, 101) <= _wallChance)
                    {
                        MakeTileWall("Game", x, y); //set tile to wall
                    }
                    else
                    {
                        MakeTileFloor("Game", x, y); //set tile to floor
                    }

                }
            }

            MakeBoarder();

            passes = 0;
            return passes;
        }

        //? make a method that checks for 1 floor tile surrounded by 8 wall tiles and fill in in, seperate from filter() ?

        public void SplitX()
        {
            //! not a full X

            // mapsizeX 110
            // mapsizeY 35

            // mapsizeX / mapsizeY = yShift - how many tiles to move over

            int x = 0;
            int y = 0;
            const int yShift = MapSizeX / MapSizeY;

            do
            {
                MakeTileFloor("Temp", x, y);
                x++;
                //y++;
                y += yShift;
            } while (y <= MapSizeY - 1);
        }

        public void SplitPlus()
        {
            // split map in have on both X and Y axis - like a + symbol
            const int xOne = MapSizeX / 2;
            const int yOne = MapSizeY / 2;

            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    if (x == xOne || y == yOne)
                    {
                        MakeTileFloor("Temp", x, y);
                    }
                }
            }
        }

        public void SplitIntoQuarters() //? different name
        {
            // seed map with three lines across X and Y axis to help prevent unattached caverns
            //? try a X and a + instead

            const int xOne = MapSizeX / 4;
            const int xTwo = xOne * 2;
            const int xThree = xOne * 3;

            const int yOne = MapSizeY / 4;
            const int yTwo = yOne * 2;
            const int yThree = yOne * 3;

            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    if (x == xOne || x == xTwo || x == xThree || y == yOne || y == yTwo || y == yThree)
                    {
                        MakeTileFloor("Temp", x, y);
                    }
                }
            }
        }

        public void MakeBoarder()
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    if (x == 0 || y == 0 || x == MapSizeX - 1 || y == MapSizeY - 1) // create a border of all walls
                    {
                        MakeTileWall("Game", x, y);
                    }
                }
            }
        }

        public int Filter()
        {
            for (int x = 1; x <= MapSizeX - 2; x++) //skip outer wall
            {
                for (int y = 1; y <= MapSizeY - 2; y++) //skip outer wall
                {
                    Tile TempTile = (Tile)TempMap[x, y];

                    int WallCount = GetWallCount(x, y);

                    if (WallCount >= 5) //set as wall
                    {
                        TempTile.Icon = WallIcon;
                        TempTile.IsWall = true;
                        TempTile.IsWalkable = false;
                    }
                    else if (WallCount <= 3) //set as floor
                    {
                        TempTile.Icon = FloorIcon;
                        TempTile.IsWall = false;
                        TempTile.IsWalkable = true;
                    }
                }
            }

            //TODO set these to accept which map to modify GameMap / TempMap
            SplitIntoQuarters();
            //SplitX();
            //SplitPlus();
            //RemoveSingleTiles();
            MakeBoarder();

            passes++;
            CopyMap(TempMap, GameMap); //? move this
            return passes;
        }

        public void RemoveSingleTiles()
        {
            //? to remove any single tiles that may have been creater with filter() without running filter again so only single tiles are fixed
            //? not sure if this is even needed

            //? move to be activated by a keypress for testing

            bool _repeat = false;
            do
            {
                for (int x = 1; x <= MapSizeX - 2; x++) //skip outer wall
                {
                    for (int y = 1; y <= MapSizeY - 2; y++) //skip outer wall
                    {
                        if (!TempMap[x, y].IsWall)
                        {
                            int _wallCount = GetWallCount(x, y);
                            if (_wallCount == 8)
                            {
                                MakeTileWall("Temp", x, y);

                                //! slows the game down to a crawl - may not be set right
                                _repeat = true;
                            }
                        }
                    }
                }
            } while (_repeat);
        }

        public static int GetWallCount(int LOCx, int LOCy)
        {
            int _wallCount = 0;

            for (int x = (LOCx - 1); x <= (LOCx + 1); x++)
            {
                for (int y = (LOCy - 1); y <= (LOCy + 1); y++)
                {
                    if (!(x == LOCx && y == LOCy)) //dont count itself
                    {
                        if (GameMap[x, y].IsWall)
                            _wallCount++;
                    }
                }
            }
            return _wallCount;
        }

        public static void CopyMap(Tile[,] oldMap, Tile[,] newMap)
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    newMap[x, y] = oldMap[x, y];
                }
            }
        }

        public void Display(Console console)
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    Tile tile = GameMap[x, y];
                    console.Print(x, y, tile.Icon);
                }
            }
        }

        public static bool IsWalkable(int x, int y)
        {
            Tile tile = GameMap[x, y];
            return tile.IsWalkable;
        }
    }
}