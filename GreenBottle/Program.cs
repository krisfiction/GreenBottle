using SadConsole;

namespace GreenBottle
{
    public static class Program
    {
        public const int WindowWidth = 140;
        public const int WindowHeight = 45;

        public static void Main()
        {
            // Setup the engine and create the main window.
            Game.Create(WindowWidth, WindowHeight);

            // Hook the start event so we can add consoles to the system.
            Game.OnInitialize = Init;

            // Start the game.
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            Global.CurrentScreen = new GameScreen
            {
                IsFocused = true
            };
        }
    }
}