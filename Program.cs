// ---------------------------------------------------
// Code: KasTAS.cs
// Version: 0.0.94
// Author: Karst Skarn / Owain#3593 (Discord)
// Date: 12-01-2022 (Begin) / 21-01-2022 (0.0.94)
// Description: C# executable able to read from a custom TAS-Script file and execute keyboard patterns.
// Comments: It should technically work with any kind of emulator/game since it always applies those keys to the active window.
//           Based on WinKeys by Stefan Stranger.
// ---------------------------------------------------

namespace KasTAS;

public static class Program
{
    public static void Main()
    {
        while (true)
        {
            GUI.FormatConsole();
            ExecutionFunctions.LoadConfig();
            ExecutionFunctions.LoadKeyConfig();
            ExecutionFunctions.LoadLastHalt();
            GC.Collect();
            GUI.DrawTitle();
            GUI.DrawMainMenu();
            GUI.DrawCreditHint();
            Global.flagFirstRun = false;
            Global.tmpUserInput = ExecutionFunctions.StaticPrompt();
            switch (Global.tmpUserInput.ToLower())
            {
                case "1":
                case "c":
                    ExecutionFunctions.ConfigKeys();
                    break;
                case "2":
                case "t":
                    ExecutionFunctions.TestKeys();
                    break;
                case "3":
                case "r":
                    ExecutionFunctions.ReadKAS();
                    break;
                case "4":
                case "e":
                    ExecutionFunctions.ExecuteKAS();
                    break;
                case "5":
                case "m":
                    Global.SysConfig = true;
                    ExecutionFunctions.ChangeTarget();
                    break;
                case "$credit":
                    ExecutionFunctions.PrintCredits();
                    break;
            }


        }
    }
}