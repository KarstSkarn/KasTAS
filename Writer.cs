namespace KasTAS;

public static class Writer
{
    public static void WriteLine(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        // Call the write method, but with a newline at the end.
        Write(text + Environment.NewLine, foregroundColor, backgroundColor);
    }
    public static void Write(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        // If no text, don't write anything
        if (text == String.Empty) return;

        // Change color to desired color
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;

        // Write
        Console.Write(text);

        // Reset colors to their defaults
        ResetConsoleColor();
    }
    public static void ResetConsoleColor()
    {
        Console.ResetColor();
    }
}