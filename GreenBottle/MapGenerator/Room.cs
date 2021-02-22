namespace GreenBottle
{
    internal class Room
    {
        public int Number { get; set; }

        public int POSx { get; set; }
        public int POSy { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Room(int _number, int _posx, int _posy, int _height, int _width)
        {
            Number = _number;
            POSx = _posx;
            POSy = _posy;
            Height = _height;
            Width = _width;
        }
    }
}