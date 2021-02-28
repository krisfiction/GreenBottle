using System.Collections.Generic;
using Console = SadConsole.Console;

namespace GreenBottle
{
    public static class ActivityLog
    {
        private static readonly List<string> Log = new List<string>();

        public static void AddToLog(string _input)
        {
            Log.Add(_input);
        }

        public static void ClearLog()
        {
            Log.Clear();
        }

        public static void Display(Console _console)
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