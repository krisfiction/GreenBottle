namespace GreenBottle
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Icon { get; set; }
        public bool IsDoor { get; set; }
        public bool IsWall { get; set; }
        public bool IsWalkable { get; set; }
        public bool IsHallway { get; set; }
        public bool IsMonster { get; set; }
        public bool IsItem { get; set; }

        public Tile(int _x, int _y, string _icon, bool _isdoor, bool _iswall, bool _iswalkable, bool _ishallway, bool _ismonster, bool _isitem)
        {
            X = _x;
            Y = _y;
            Icon = _icon;
            IsDoor = _isdoor;
            IsWall = _iswall;
            IsWalkable = _iswalkable;
            IsHallway = _ishallway;
            IsMonster = _ismonster;
            IsItem = _isitem;
        }

        public string DisplayIcon()
        {
            return Icon;
        }
    }
}
