using SadConsole;
using Console = SadConsole.Console;

namespace GreenBottle
{
    public class SpellWindow
    {
        public void Display(Console _console)
        {
            int row = 0;
            _console.Clear();

            _console.Print(1, row, "Spells:");
            row++;
            row++;

            _console.Print(1, row, "a) Heal");
            row++;
            row++;

            _console.Print(1, row, "more spells");
            row++;
            row++;

            _console.Print(1, row, "more spells");


        }
    }
}
