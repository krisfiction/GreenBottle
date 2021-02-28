
using SadConsole;

namespace GreenBottle
{
    public class Stats
    {
        private int row = 0;
        public void Display(Console _console)
        {
            _console.Clear();
            row = 0;
            _console.Print(1, row++, "Stats:");
        }
    }
}
