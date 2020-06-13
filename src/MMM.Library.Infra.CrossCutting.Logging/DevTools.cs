using System;

namespace MMM.Library.Infra.CrossCutting.Logging
{
    public static class DevTools
    {
        public static void PrintConsoleMessage(string message,
                                                ConsoleColor backgroundColor,
                                                ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
