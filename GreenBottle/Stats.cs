
using SadConsole;

namespace GreenBottle
{
    public class Stats
    {
        public void Display(Console _console)
        {
            int row = 0;

            _console.Clear();
            _console.Print(1, row++, "Stats:");
        }
    }
}
