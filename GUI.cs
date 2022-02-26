namespace KasTAS;

// GUI Functions ~
public static class GUI
{
    public static void FormatConsole()
    {
        Console.Title = "KasTAS";
        Console.SetWindowSize(Global.WindowWidth, Global.WindowHeight);
        Console.Clear();
        Console.SetBufferSize(Global.WindowWidth, Global.WindowHeight);
    }
    public static void FormatConsoleExpBuffer()
    {
        Console.SetWindowSize(Global.WindowWidth, Global.WindowHeight);
        Console.SetBufferSize(Global.WindowWidth, 2500);
    }
    public static void DrawSpacer()
    {
        Writer.WriteLine(" ");
    }
    public static void DrawUpperBarSpacer()
    {
        Writer.WriteLine("▀".PadRight(Console.BufferWidth - 1, Global.charUpperBar), ConsoleColor.DarkGray);
    }
    public static void DrawLowerBarSpacer()
    {
        Writer.WriteLine("▄".PadRight(Console.BufferWidth - 1, Global.charBar), ConsoleColor.DarkGray);
    }
    public static void DrawClearBuffer()
    {
        Console.SetCursorPosition(0, Global.WindowHeight - 2);
        Writer.WriteLine(" ".PadRight(Console.BufferWidth - 1, Global.charSpace));
    }
    public static void DrawTitle()
    {
        Console.Clear();
        Writer.Write(" ♦ Kas", ConsoleColor.White, ConsoleColor.DarkBlue);
        Writer.Write("TAS", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
        Writer.Write(" ", ConsoleColor.White, ConsoleColor.DarkBlue);
        Writer.Write(Global.Version, ConsoleColor.Gray, ConsoleColor.DarkBlue);
        Writer.Write(" 2022(C)", ConsoleColor.DarkGray, ConsoleColor.DarkBlue);
        Writer.Write("                             ", ConsoleColor.White, ConsoleColor.DarkBlue);
        string twitchText = Global.showTwitchTitle ? "www.twitch.tv/karstskarn" : "                        ";
        Writer.Write(twitchText, ConsoleColor.DarkMagenta, ConsoleColor.DarkBlue);
        (int curLeft, _) = Console.GetCursorPosition();
        Writer.WriteLine(" ".PadRight(Console.BufferWidth - curLeft - 1, Global.charSpace), ConsoleColor.White, ConsoleColor.DarkBlue);
        DrawUpperBarSpacer();
    }
    public static void DrawMainMenu()
    {
        const ConsoleColor darkGray = ConsoleColor.DarkGray;
        Writer.Write("  ▬ ", darkGray);
        Writer.Write("1");
        Writer.Write(". ", darkGray);
        Writer.Write("C");
        Writer.WriteLine("onfigure the keys.", darkGray);

        Writer.Write("  ▬ ", darkGray);
        Writer.Write("2");
        Writer.Write(". ", darkGray);
        Writer.Write("T");
        Writer.WriteLine("est the keys.", darkGray);

        Writer.Write("  ▬ ", darkGray);
        Writer.Write("3");
        Writer.Write(". ", darkGray);
        Writer.Write("R");
        Writer.WriteLine("ead a KSOAutoScript file.", darkGray);

        Writer.Write("  ▬ ", darkGray);
        Writer.Write("4");
        Writer.Write(". ", darkGray);
        Writer.Write("E");
        Writer.WriteLine("xecute a KSOAutoScript file.", darkGray);

        DrawSpacer();
        Writer.Write(" Current key mode is [");
        Writer.Write(Global.SysTarget, ConsoleColor.Cyan);
        Writer.WriteLine("]");
        Writer.Write(" Extended key mode is [");
        if (Global.flagExtMode)
            Writer.Write("ENABLED", ConsoleColor.Green);
        else
            Writer.Write("DISABLED", ConsoleColor.Cyan);
        Writer.WriteLine("]");

        Writer.Write("  ▬ ", darkGray);
        Writer.Write("5");
        Writer.Write(". ", darkGray);
        Writer.Write("M");
        Writer.WriteLine("anage output modes.", darkGray);

        DrawSpacer();
        Writer.Write(" You can write directly any");
        Writer.Write(" <.kas> ", ConsoleColor.Cyan);
        Writer.WriteLine("file in the prompt");
        Writer.WriteLine(" and will be executed regardless of the menu you are in.");

        DrawSpacer();
        Writer.Write(" Press ", darkGray);
        Writer.Write("CTRL + C");
        Writer.Write(" or ", darkGray);
        Writer.Write("CTRL + BREAK");
        Writer.WriteLine(" to force the executable shutdown.", darkGray);
        DrawSpacer();
    }
    public static void DrawCreditHint()
    {
        Console.SetCursorPosition(0, Global.WindowHeight - 4);
        Writer.Write(" Type $Credit for use info.", ConsoleColor.DarkGray);
    }
    public static void DrawSubtitle(string title)
    {
        Writer.Write(" ");
        Writer.Write("♦", ConsoleColor.Black, ConsoleColor.White);
        Writer.WriteLine(" " + title);
    }
    public static void DrawReadMenu()
    {
        DrawTitle();
        DrawSubtitle("KSOAutoScript Visualizer");
        DrawSpacer();
        Writer.WriteLine(" Here you can read a KSOAutoScript file and check its syntax", ConsoleColor.DarkGray);
        Writer.WriteLine(" integrity.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("1");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("L");
        Writer.WriteLine("oad a file.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("2");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E", ConsoleColor.Blue);
        Writer.WriteLine("xit.", ConsoleColor.DarkGray);
        DrawSpacer();
    }
    public static void DrawExecuteMenu()
    {
        DrawTitle();
        DrawSubtitle("KSOAutoScript Execution");
        DrawSpacer();
        Writer.WriteLine(" Here you can execute a KSOAutoScript.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("1");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("L");
        Writer.WriteLine("oad a file.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("2");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E", ConsoleColor.Blue);
        Writer.WriteLine("xit.", ConsoleColor.DarkGray);
        DrawSpacer();
    }
    public static void DrawTestMenu()
    {
        FormatConsole();
        DrawTitle();
        DrawSubtitle("Virtual Keyboard Keys Test");
        DrawSpacer();
        Writer.WriteLine(" This function will test the VKeys.", ConsoleColor.DarkGray);
        Writer.WriteLine(" Make sure you got your Software/Game window active.", ConsoleColor.DarkGray);
        Writer.Write(" You will have a delay of ", ConsoleColor.DarkGray);
        Writer.Write(Convert.ToString(Global.testReadyTime));
        Writer.WriteLine(" ms before the test actually starts.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.WriteLine(" Select the test type: ", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("1");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("I");
        Writer.WriteLine("ncremental.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("2");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("C");
        Writer.WriteLine("ontinous.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("3");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("F");
        Writer.WriteLine("rame.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("4");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("H");
        Writer.WriteLine("old push.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("5");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E", ConsoleColor.Blue);
        Writer.WriteLine("xit test mode.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.WriteLine(" Note: All tests are performed with the GB buttons only.");
        DrawSpacer();
    }
    public static void DrawConfigMenu()
    {
        FormatConsole();
        DrawTitle();
        DrawSubtitle("Keyboard Keys Configuration");
        DrawSpacer();
        Writer.Write(" Each Key will be held for ", ConsoleColor.DarkGray);
        Writer.Write(Global.configHoldTime.ToString());
        Writer.Write(" ms.");
        Writer.Write(" You will have ", ConsoleColor.DarkGray);
        Writer.Write(Global.configReadyTime.ToString());
        Writer.Write(" ms");
        Writer.Write(" before every keypress. ", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write(" Select the keys to be configured one by one: ", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("0");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("U");
        Writer.Write("P.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_UP + "}", ConsoleColor.Green);
        Writer.Write("                   ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("6");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("A");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_A + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("1");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("D");
        Writer.Write("OWN.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_DO + "}", ConsoleColor.Green);
        Writer.Write("                 ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("7");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("B");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_B + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("2");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("L");
        Writer.Write("EFT.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_LE + "}", ConsoleColor.Green);
        Writer.Write("                 ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("8");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("X");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_X + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("3");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("R");
        Writer.Write("IGHT.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_RI + "}", ConsoleColor.Green);
        Writer.Write("                ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("9");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("Y");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_Y + "}", ConsoleColor.Green);
        //GUI.DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("4");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("ST");
        Writer.Write("ART.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_ST + "}", ConsoleColor.Green);
        Writer.Write("                ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("11");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("BL");
        Writer.Write(" - Button Left.", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_BL + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("5");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("SE");
        Writer.Write("LECT.", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_SE + "}", ConsoleColor.Green);
        Writer.Write("               ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("12");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("BR");
        Writer.Write(" - Button Right.", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_BR + "}", ConsoleColor.Green);
        // Extra Keys
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("13");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E0");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_E0 + "}", ConsoleColor.Green);
        Writer.Write("                  ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("18");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E5");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_E5 + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("14");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E1");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_E1 + "}", ConsoleColor.Green);
        Writer.Write("                  ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("19");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E6");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_E6 + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("15");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E2");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_E2 + "}", ConsoleColor.Green);
        Writer.Write("                  ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("20");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E7");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_E7 + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("16");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E3");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_E3 + "}", ConsoleColor.Green);
        Writer.Write("                  ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("21");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E8");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_E8 + "}", ConsoleColor.Green);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("17");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E4");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.Write(" {" + Global.VB_E4 + "}", ConsoleColor.Green);
        Writer.Write("                  ");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("22");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E9");
        Writer.Write(".", ConsoleColor.DarkGray);
        Writer.WriteLine(" {" + Global.VB_E9 + "}", ConsoleColor.Green);
        //GUI.DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("10");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E", ConsoleColor.Blue);
        Writer.WriteLine("xit Configuration Mode.", ConsoleColor.DarkGray);
        DrawSpacer();
    }
    public static void DrawTargetMenu()
    {
        DrawTitle();
        DrawSubtitle("Output modes configuration");
        DrawSpacer();
        Writer.WriteLine(" Here you can switch between the keyboard output modes.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.WriteLine(" Basic buttons output mode.");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("1");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("G");
        Writer.WriteLine("B.", ConsoleColor.DarkGray);
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("2");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("S");
        Writer.WriteLine("NES.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.WriteLine(" Extended keys mode.");
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("3");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("T");
        Writer.WriteLine("oggle extended mode.", ConsoleColor.DarkGray);
        DrawSpacer();
        Writer.Write("  ▬ ", ConsoleColor.DarkGray);
        Writer.Write("4");
        Writer.Write(". ", ConsoleColor.DarkGray);
        Writer.Write("E", ConsoleColor.Blue);
        Writer.WriteLine("xit.", ConsoleColor.DarkGray);
        DrawSpacer();
    }
}