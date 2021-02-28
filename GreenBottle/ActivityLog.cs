using System.Collections.Generic;
using Console = SadConsole.Console;

namespace GreenBottle
{
    public class ActivityLog
    {
        private readonly List<string> Log = new List<string>();

        public void AddToLog(string _input)
        {
            Log.Add(_input);
        }

        public void ClearLog()
        {
            Log.Clear();
        }

        public void Display(Console _console)
        {
            int _row = 0;

            _console.Clear();

            if (Log.Count > 10)
            {
                for (int i = Log.Count - 10; i < Log.Count; i++)
                {
                    _console.Print(0, _row++, Log[i]);
                }
            }
            else
            {
                for (int i = 0; i <= Log.Count - 1; i++)
                {
                    _console.Print(0, _row++, Log[i]);
                }
            }

            _console.IsDirty = true;
        }
    }
}