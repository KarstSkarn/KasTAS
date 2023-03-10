namespace KasTAS;

// Execution Functions ~
public class ExecutionFunctions
{
    public enum PV_GROUPStates
    {
        None,
        A,
        B,
        ST,
        SE,
        UP,
        DO,
        LE,
        RI,
        XX,
        YY,
        BL,
        BR,
        E0,
        E1,
        E2,
        E3,
        E4,
        E5,
        E6,
        E7,
        E8,
        E9
    }
    public enum KindOfOPStates
    {
        None,
        CO,
        JT,
        SP,
        JU,
        TI,
        RE,
        EN,
        KS
    }

    private static PV_GROUPStates PV_GROUP = PV_GROUPStates.None;


    private static KindOfOPStates tmpKindOfOP = KindOfOPStates.None;

    private static void LineChanger(string newText, string fileName, int lineToEdit)
    {
        string[] arrLine = File.ReadAllLines(fileName);
        arrLine[lineToEdit - 1] = newText;
        File.WriteAllLines(fileName, arrLine);
    }
    public static void LoadConfig()
    {
        /*
        00 LAST FILE    = NULL
        01 CFG HOLD T   = 7000
        02 CFG READY T  = 3000
        03 TST READY T  = 5000
        04 INCR STRT T  = 1000
        05 INCR REDC T  = 50
        06 TST CONT T   = 300
        07 TST FRAME T  = 33.3333
        08 SYS TARGET   = GB
        09 TIME RES.    = 1
        10 READ PAUSE   = 100
        11 SHOW TW      = true
        */
        if (Directory.Exists("Scripts") == false) Directory.CreateDirectory("Scripts");

        bool tmpFileExists = File.Exists(Global.cfgFile);
        if (!tmpFileExists)
        {
            Global.tmpUserInput = "NULL";
            Console.Clear();
            Writer.WriteLine(" Error: The file <kascfg.ini> is missing or in wrong format.", ConsoleColor.Red);
            Writer.WriteLine(" If you continue beyond this point the preset values will be loaded.", ConsoleColor.Red);
            Writer.WriteLine(" Press [ENTER] to continue.", ConsoleColor.Red);
            Console.ReadLine();
            return;
        }

        int tmpFileSize = File.ReadLines(Global.cfgFile).Count();
        if (tmpFileSize != Global.cfgTotalLines)
        {
            Console.Clear();
            Writer.WriteLine(" Error: The file <kascfg.ini> is missing or in wrong format.", ConsoleColor.Red);
            Writer.WriteLine(" If you continue beyond this point the preset values will be loaded.", ConsoleColor.Red);
            Writer.WriteLine(" Press [ENTER] to continue.", ConsoleColor.Red);
            Console.ReadLine();
            return;
        }

        for (int ii = 0; ii <= (Global.cfgTotalLines - 1); ii++)
        {
            //TODO: Why is this copy pasted so often!?
            string tmpFileData = File.ReadLines(Global.cfgFile).Skip(ii).Take(1).First();

            if ((ii < 0) || (ii > 11)) continue;

            int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
            string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
            //TODO: ideally use some kind of enumeration for these as well
            switch (ii)
            {
                case 0:
                {
                    Global.lastFileLoaded = tmpSubString;
                    break;
                }
                case 1:
                {
                    Global.configHoldTime = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 2:
                {
                    Global.configReadyTime = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 3:
                {
                    Global.testReadyTime = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 4:
                {
                    Global.testIncrementalStartTime = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 5:
                {
                    Global.testIncrementalReduction = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 6:
                {
                    Global.testContinousTime = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 7:
                {
                    Global.testFrameTime = Convert.ToDouble(tmpSubString);
                    break;
                }
                case 8:
                {
                    Global.SysTarget = tmpSubString;
                    break;
                }
                case 9:
                {
                    Global.timeResolution = Convert.ToInt32(tmpSubString);
                    break;
                }
                case 10:
                {
                    Global.tmpReadMaxPause = Convert.ToInt32(tmpSubString);
                    break;
                }
                //TODO: why does this need to be an option
                case 11:
                {
                    Global.showTwitchTitle = Boolean.Parse(tmpSubString);
                    break;
                }
            }
        }
    }
    public static void LoadKeyConfig()
    {
        /*
        00 SYS TARGET   = GB
        01 EXTRA MODE   = false
        01 BUTTON A     = P
        02 BUTTON B     = O
        03 BUTTON START = X
        04 BUTTON SELECT = Z
        05 BUTTON UP    = W
        06 BUTTON DOWN  = S
        07 BUTTON LEFT  = A
        08 BUTTON RIGHT = D
        09 E0           = H
        10 E1           = H
        11 E2           = H
        12 E3           = H
        13 E4           = H
        14 E5           = H
        15 E6           = H
        16 E7           = H
        17 E8           = H
        18 E9           = H
        */
        bool tmpFileExists = File.Exists(Global.cfgKeysFile);
        if (!tmpFileExists)
        {
            Global.tmpUserInput = "NULL";
            Console.Clear();
            Writer.WriteLine(" Error: The file <keydata.ini> is missing or in wrong format.", ConsoleColor.Red);
            Writer.WriteLine(" If you continue beyond this point the preset values will be loaded.", ConsoleColor.Red);
            Writer.WriteLine(" Press [ENTER] to continue.", ConsoleColor.Red);
            Console.ReadLine();
            return;
        }
        int tmpFileSize = File.ReadLines(Global.cfgKeysFile).Count();
        if (tmpFileSize != Global.cfgKeyTotalLines)
        {
            Console.Clear();
            Writer.WriteLine(" Error: The file <keydata.ini> is missing or in wrong format.", ConsoleColor.Red);
            Writer.WriteLine(" If you continue beyond this point the preset values will be loaded.", ConsoleColor.Red);
            Writer.WriteLine(" Press [ENTER] to continue.", ConsoleColor.Red);
            Console.ReadLine();
            return;
        }

        for (int ii = 0; ii <= (Global.cfgKeyTotalLines - 1); ii++)
        {
            string tmpFileData = File.ReadLines(Global.cfgKeysFile).Skip(ii).Take(1).First();
            if ((ii < 0) || (ii > 23)) continue;

            int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
            string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
            switch (ii)
            {
                case 0:
                {
                    Global.SysTarget = tmpSubString;
                    break;
                }
                case 1:
                {
                    Global.flagExtMode = Boolean.Parse(tmpSubString);
                    break;
                }
                //TODO: see todo in virtualKeyboard; use some kind of enumerable for the buttons so i can just do Buttons[i] = tmpSubString
                case 2:
                {
                    Global.VB_A = tmpSubString;
                    break;
                }
                case 3:
                {
                    Global.VB_B = tmpSubString;
                    break;
                }
                case 4:
                {
                    Global.VB_ST = tmpSubString;
                    break;
                }
                case 5:
                {
                    Global.VB_SE = tmpSubString;
                    break;
                }
                case 6:
                {
                    Global.VB_UP = tmpSubString;
                    break;
                }
                case 7:
                {
                    Global.VB_DO = tmpSubString;
                    break;
                }
                case 8:
                {
                    Global.VB_LE = tmpSubString;
                    break;
                }
                case 9:
                {
                    Global.VB_RI = tmpSubString;
                    break;
                }
                case 10:
                {
                    Global.VB_X = tmpSubString;
                    break;
                }
                case 11:
                {
                    Global.VB_Y = tmpSubString;
                    break;
                }
                case 12:
                {
                    Global.VB_BL = tmpSubString;
                    break;
                }
                case 13:
                {
                    Global.VB_BR = tmpSubString;
                    break;
                }
                case 14:
                {
                    Global.VB_E0 = tmpSubString;
                    break;
                }
                case 15:
                {
                    Global.VB_E1 = tmpSubString;
                    break;
                }
                case 16:
                {
                    Global.VB_E2 = tmpSubString;
                    break;
                }
                case 17:
                {
                    Global.VB_E3 = tmpSubString;
                    break;
                }
                case 18:
                {
                    Global.VB_E4 = tmpSubString;
                    break;
                }
                case 19:
                {
                    Global.VB_E5 = tmpSubString;
                    break;
                }
                case 20:
                {
                    Global.VB_E6 = tmpSubString;
                    break;
                }
                case 21:
                {
                    Global.VB_E7 = tmpSubString;
                    break;
                }
                case 22:
                {
                    Global.VB_E8 = tmpSubString;
                    break;
                }
                case 23:
                {
                    Global.VB_E9 = tmpSubString;
                    break;
                }
            }
        }
    }
    public static void LoadLastHalt()
    {
        if (Global.lastFileLoaded == "NULL") return;
        if (!Global.flagFirstRun) return;

        GUI.DrawTitle();
        GUI.DrawSpacer();
        Writer.Write(" Last executed file was <");
        Writer.Write(Global.lastFileLoaded, ConsoleColor.Cyan);
        Writer.WriteLine(">");
        Global.tmpUserInput = "NULL";
        Writer.WriteLine(" Do you want to execute it again?");
        Writer.Write(" Write ", ConsoleColor.DarkGray);
        Writer.Write("[N/n]");
        Writer.Write(" and then press ", ConsoleColor.DarkGray);
        Writer.Write("[ENTER]");
        Writer.Write(" or ", ConsoleColor.DarkGray);
        Writer.Write("leave empty");
        Writer.WriteLine(" to load it again. ", ConsoleColor.DarkGray);
        GUI.DrawSpacer();
        Global.tmpUserInput = StaticPrompt();
        if (Global.tmpUserInput.ToLower() == "n")
        {
            Global.tmpWritePad = "00 LAST FILE    = NULL";
            WriteToConfig(0, Global.tmpWritePad);
            Global.flagFirstRun = false;
            Global.flagLoadLast = false;
            return;
        }

        Global.flagLoadLast = true;
        Global.flagFirstRun = false;
        ExecuteKAS();

    }

    public static void WriteToConfig(int line, string value, bool keyCfg = false)
    {
        int totalLines = keyCfg ? Global.cfgKeyTotalLines : Global.cfgTotalLines;
        string file = keyCfg ? Global.cfgKeysFile : Global.cfgFile;

        const char padChar = '0';
        value = value.PadLeft(2, padChar);
        line += 1;
        if (line > totalLines) line = totalLines;
        if (!File.Exists(file))
        {
            ClearBufferSetCursorPosAndLowerSpacer();
            Writer.Write(" Fatal Error: " + file + " doesn't exist in the executable directory.", ConsoleColor.Red);
            Console.ReadKey();
            return;
        }

        LineChanger(value, file, line);
    }

    public static void DebugHalt()
    {
        // Used only for debug purposes.
        Writer.WriteLine(" ");
        Writer.WriteLine(" [Debug] Execution Halted.", ConsoleColor.Yellow);
        Console.ReadLine();
    }
    public static void ReadHalt()
    {
        Writer.Write(" ▬ ", ConsoleColor.Cyan);
        Writer.WriteLine("End of file.");
    }
    public static void EndExecutionHalt()
    {
        Writer.Write(" ▬ ", ConsoleColor.Cyan);
        Writer.WriteLine(" End of script execution.");
    }
    public static void ExecutionHalt()
    {
        Console.SetCursorPosition(0, Global.WindowHeight - 3);
        GUI.DrawLowerBarSpacer();
        Writer.Write(" ▬ ", ConsoleColor.Cyan);
        Writer.Write("Press ", ConsoleColor.DarkGray);
        Writer.Write("[ENTER]");
        Writer.Write(" to execute the script.", ConsoleColor.DarkGray);
        Console.ReadLine();
    }
    public static void PrintCredits()
    {
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" KasTAS 1.0.98 (C) 2022 by Owain Horton / Karst Skarn [Owain#3593]", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" is licensed under CC BY-NC-SA International 4.0", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" (KasTAS.exe - CC Attribution-NonCommercial-ShareAlike 4.0 International)", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" To view a copy of this license.", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" Visit http://creativecommons.org/licenses/by-nc-sa/4.0/", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" In case of public use (Stream, video recording...)", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" The attribution and mention is strongly encouraged.", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" https://www.twitch.tv/karstskarn", ConsoleColor.Green);
        Console.ReadKey();
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" Thanks!", ConsoleColor.Green);
        Console.ReadKey();
        Global.tmpUserInput = "NULL";
    }
    public static string Prompt()
    {
        Writer.Write(" ► ", ConsoleColor.Cyan);
        Writer.Write("Choose an option [", ConsoleColor.DarkGray);
        Writer.Write("N");
        Writer.Write("|", ConsoleColor.DarkGray);
        Writer.Write("C");
        Writer.Write("]:", ConsoleColor.DarkGray);
        Writer.Write(" ", ConsoleColor.Cyan);
        string userInput = Console.ReadLine();
        //TODO: copy pasted
        if (userInput.Length >= 5)
        {
            Global.tmpUserInput = userInput.Substring(userInput.Length - 4, 4);
            if (Global.tmpUserInput == ".kas")
            {
                Global.flagLoadLast = true;
                Global.lastFileLoaded = userInput;
                Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                WriteToConfig(0, Global.tmpWritePad);
                ExecuteKAS();
            }
        }
        return userInput;
    }
    public static string StaticPrompt()
    {
        ClearBufferSetCursorPosAndLowerSpacer();
        return Prompt();
    }

    //TODO: not used
    public static string NoReturnStaticPrompt()
    {
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" ► ", ConsoleColor.Cyan);
        Writer.Write("Press ", ConsoleColor.DarkGray);
        Writer.Write("[ENTER]");
        Writer.Write(" to continue:", ConsoleColor.DarkGray);
        Writer.Write(" ", ConsoleColor.Cyan);
        string userInput = "NULL";
        Console.ReadLine();
        return userInput;
    }
    public static string FileStaticPrompt()
    {
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.Write(" ► ", ConsoleColor.Cyan);
        Writer.Write("Input ", ConsoleColor.DarkGray);
        Writer.Write("[FILENAME]");
        Writer.Write(" ", ConsoleColor.DarkGray);
        Writer.Write("(Scripts/");
        Writer.Write("...", ConsoleColor.Cyan);
        Writer.Write(")");
        Writer.Write(":", ConsoleColor.DarkGray);
        Writer.Write(" ", ConsoleColor.Cyan);
        string userInput = Console.ReadLine();
        return userInput;
    }
    public static void ChangeTarget()
    {
        GUI.DrawTargetMenu();
        if (!Global.SysConfig) return;

        Global.tmpUserInput = StaticPrompt();
        switch (Global.tmpUserInput.ToLower())
        {
            case "1":
            case "g":
                Global.tmpWritePad = "00 SYS TARGET   = GB";
                WriteToConfig(0, Global.tmpWritePad, true);
                break;
            case "2":
            case "s":
                Global.tmpWritePad = "00 SYS TARGET   = SNES";
                WriteToConfig(0, Global.tmpWritePad, true);
                break;
            case "3":
            case "t":
                Global.tmpWritePad = "01 EXTRA MODE   = " + Global.flagExtMode;
                WriteToConfig(1, Global.tmpWritePad, true);
                break;
            case "4":
            case "e":
                Global.SysConfig = false;
                Global.tmpUserInput = "NULL";
                break;
        }
    }


    private static void ConfigureSingleKey(string button, string buttonName)
    {
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.WriteLine(" Virtual Pressing ["+ buttonName + "] (" + button + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", ConsoleColor.DarkGray);
        Timing.Hold(Global.configReadyTime);
        VirtualKeyboard.VK_Down(Global.VB_UP);
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.WriteLine(" Virtual Key Pressed!", ConsoleColor.Green);
        Timing.Hold(Global.configHoldTime);
        VirtualKeyboard.VK_Up(Global.VB_UP);
        ClearBufferSetCursorPosAndLowerSpacer();
        Writer.WriteLine(" Virtual Key Released!", ConsoleColor.Green);
    }

    public static void ConfigKeys()
    {
        bool isStillInConfigKeysMode = true;
        while (isStillInConfigKeysMode)
        {
            GUI.DrawConfigMenu();
            Global.tmpUserInput = StaticPrompt();
            switch (Global.tmpUserInput.ToLower())
            {
                //TODO: This should get condensed.
                case "0":
                case "u":
                    ConfigureSingleKey(Global.VB_UP, "UP");
                    break;
                case "1":
                case "d":
                    ConfigureSingleKey(Global.VB_DO, "DOWN");
                    break;
                case "2":
                case "l":
                    ConfigureSingleKey(Global.VB_LE, "LEFT");
                    break;
                case "3":
                case "r":
                    ConfigureSingleKey(Global.VB_RI, "RIGHT");
                    break;
                case "4":
                case "st":
                    ConfigureSingleKey(Global.VB_ST, "START");
                    break;
                case "5":
                case "se":
                    ConfigureSingleKey(Global.VB_SE, "SELECT");
                    break;
                case "6":
                case "a":
                    ConfigureSingleKey(Global.VB_A, "A");
                    break;
                case "7":
                case "b":
                    ConfigureSingleKey(Global.VB_UP, "B");
                    break;
                case "8":
                case "x":
                    ConfigureSingleKey(Global.VB_X, "X");
                    break;
                case "9":
                case "y":
                    ConfigureSingleKey(Global.VB_Y, "Y");
                    break;
                case "11":
                case "bl":
                    ConfigureSingleKey(Global.VB_BL, "BL");
                    break;
                case "12":
                case "br":
                    ConfigureSingleKey(Global.VB_BR, "BR");
                    break;
                case "13":
                case "e0":
                    ConfigureSingleKey(Global.VB_E0, "E0");
                    break;
                case "14":
                case "e1":
                    ConfigureSingleKey(Global.VB_E1, "E1");
                    break;
                case "15":
                case "e2":
                    ConfigureSingleKey(Global.VB_E2, "E2");
                    break;
                case "16":
                case "e3":
                    ConfigureSingleKey(Global.VB_E3, "E3");
                    break;
                case "17":
                case "e4":
                    ConfigureSingleKey(Global.VB_E4, "E4");
                    break;
                case "18":
                case "e5":
                    ConfigureSingleKey(Global.VB_E5, "E5");
                    break;
                case "19":
                case "e6":
                    ConfigureSingleKey(Global.VB_E6, "E6");
                    break;
                case "20":
                case "e7":
                    ConfigureSingleKey(Global.VB_E7, "E7");
                    break;
                case "21":
                case "e8":
                    ConfigureSingleKey(Global.VB_E8, "E8");
                    break;
                case "22":
                case "e9":
                    ConfigureSingleKey(Global.VB_E9, "E9");
                    break;

                case "69":
                case "bgb":
                    // This presses the virtual keys in the same order than the BGB Emulator allows them to be configured.
                    // So it solves a little time.
                    ConfigureSingleKey(Global.VB_UP, "UP");
                    ConfigureSingleKey(Global.VB_DO, "DOWN");
                    ConfigureSingleKey(Global.VB_LE, "LEFT");
                    ConfigureSingleKey(Global.VB_RI, "RIGHT");
                    ConfigureSingleKey(Global.VB_ST, "START");
                    ConfigureSingleKey(Global.VB_SE, "SELECT");
                    ConfigureSingleKey(Global.VB_A, "A");
                    ConfigureSingleKey(Global.VB_UP, "B");
                    ConfigureSingleKey(Global.VB_X, "X");
                    ConfigureSingleKey(Global.VB_Y, "Y");
                    ConfigureSingleKey(Global.VB_BL, "BL");
                    ConfigureSingleKey(Global.VB_BR, "BR");
                    ConfigureSingleKey(Global.VB_E0, "E0");
                    ConfigureSingleKey(Global.VB_E1, "E1");
                    ConfigureSingleKey(Global.VB_E2, "E2");
                    ConfigureSingleKey(Global.VB_E3, "E3");
                    ConfigureSingleKey(Global.VB_E4, "E4");
                    ConfigureSingleKey(Global.VB_E5, "E5");
                    ConfigureSingleKey(Global.VB_E6, "E6");
                    ConfigureSingleKey(Global.VB_E7, "E7");
                    ConfigureSingleKey(Global.VB_E8, "E8");
                    ConfigureSingleKey(Global.VB_E9, "E9");
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" All keys for BGB Emulator should be assigned now!");
                    break;
                //Exit
                case "10":
                case "e":
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Exiting Key Configuration Mode...", ConsoleColor.DarkGray);
                    isStillInConfigKeysMode = false;
                    Global.tmpUserInput = "NULL";
                    break;
            }
        }
    }

    private static void TestMainKeysWithHold(int time)
    {
        VirtualKeyboard.VK_Down(Global.VB_A);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_A);
        VirtualKeyboard.VK_Down(Global.VB_B);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_B);
        VirtualKeyboard.VK_Down(Global.VB_ST);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_ST);
        VirtualKeyboard.VK_Down(Global.VB_SE);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_SE);
        VirtualKeyboard.VK_Down(Global.VB_RI);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_RI);
        VirtualKeyboard.VK_Down(Global.VB_DO);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_DO);
        VirtualKeyboard.VK_Down(Global.VB_LE);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_LE);
        VirtualKeyboard.VK_Down(Global.VB_UP);
        Timing.Hold(time);
        VirtualKeyboard.VK_Up(Global.VB_UP);
    }

    public static void TestKeys()
    {
        bool isStillInTestKeysMode = true;
        while (isStillInTestKeysMode)
        {
            GUI.DrawTestMenu();
            Global.tmpUserInput = StaticPrompt();
            switch (Global.tmpUserInput.ToLower())
            {
                case "1":
                case "i":
                {
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Performing Incremental test in " + Convert.ToString(Global.testReadyTime) + " ms...", ConsoleColor.DarkGray);
                    Timing.Hold(Global.testReadyTime);
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Starting Incremental test!", ConsoleColor.Green);
                    int testWorkingTime = Convert.ToInt32(Global.testIncrementalStartTime);
                    for (int ii = 0; ii <= 40; ii++)
                    {
                        TestMainKeysWithHold(testWorkingTime);
                        testWorkingTime -= Global.testIncrementalReduction;
                        if (testWorkingTime <= 30) testWorkingTime = 15;
                    }
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Incremental Test did end!", ConsoleColor.Green);
                    break;
                }
                case "2":
                case "c":
                {
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Performing Continuous test in " + Convert.ToString(Global.testReadyTime) + " ms...", ConsoleColor.DarkGray);
                    Timing.Hold(Global.testReadyTime);
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Starting Continuous test!", ConsoleColor.Green);
                    int testWorkingTime = Global.testContinousTime;
                    for (int ii = 0; ii <= 10; ii++)
                        TestMainKeysWithHold(testWorkingTime);

                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Continuous Test did end!", ConsoleColor.Green);
                    break;
                }
                case "3":
                case "f":
                {
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Performing Frame test in " + Convert.ToString(Global.testReadyTime) + " ms...", ConsoleColor.DarkGray);
                    Timing.Hold(Global.testReadyTime);
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Starting Frame test!", ConsoleColor.Green);
                    int testWorkingTime = Convert.ToInt32(Global.testFrameTime);
                    for (int ii = 0; ii <= 50; ii++)
                        TestMainKeysWithHold(testWorkingTime);

                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Frame Test did end!");
                    break;
                }
                case "4":
                case "h":
                {
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Performing TI.Hold(test in " + Convert.ToString(Global.testReadyTime) + " ms...", ConsoleColor.DarkGray);
                    Timing.Hold(Global.testReadyTime);
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" Starting TI.Hold(test!");
                    int testWorkingTime = Global.testContinousTime;
                    for (int ii = 0; ii <= 10; ii++)
                    {
                        VirtualKeyboard.VK_Down(Global.VB_A);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_B);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_ST);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_SE);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_RI);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_DO);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_LE);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Down(Global.VB_UP);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_A);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_B);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_ST);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_SE);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_RI);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_DO);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_LE);
                        Timing.Hold(testWorkingTime);
                        VirtualKeyboard.VK_Up(Global.VB_UP);
                        Timing.Hold(testWorkingTime);
                    }
                    ClearBufferSetCursorPosAndLowerSpacer();
                    Writer.WriteLine(" TI.Hold(Test did end!", ConsoleColor.Green);
                    break;
                }
                //Exit
                case "5":
                case "e":
                    Writer.WriteLine(" Exiting Test Mode", ConsoleColor.DarkGray);
                    isStillInTestKeysMode = false;
                    Global.tmpUserInput = "NULL";
                    break;
            }
        }
    }

    private static void ClearBufferSetCursorPosAndLowerSpacer()
    {
        GUI.DrawClearBuffer();
        Console.SetCursorPosition(0, Global.WindowHeight - 3);
        GUI.DrawLowerBarSpacer();
    }

    public static void ReadKAS()
    {
        bool ksoAutoScriptReadMode = true;
        bool restartArea = false;
        // Active While End
        while (ksoAutoScriptReadMode)
        {
            GUI.DrawReadMenu();
            if (restartArea) continue;

            Global.tmpUserInput = StaticPrompt();

            switch (Global.tmpUserInput.ToLower())
            {
                case "1":
                case "l":
                {
                    Global.tmpUserInput = FileStaticPrompt();
                    bool tmpFileExists = File.Exists(Global.scriptsDirectory + Global.tmpUserInput);
                    if (!tmpFileExists)
                    {
                        Global.flagLoadLast = false;
                        Global.tmpUserInput = "NULL";
                        return;
                    }
                    FileInfo scriptFileInfo = new FileInfo(Global.scriptsDirectory + Global.tmpUserInput);
                    int tmpFileSize = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Count();
                    long tmpFileWeight = scriptFileInfo.Length;
                    GUI.DrawTitle();
                    GUI.DrawSpacer();
                    Writer.Write(" Filename: ");
                    Writer.WriteLine(Convert.ToString(Global.tmpUserInput), ConsoleColor.Cyan);
                    Writer.Write(" Total Lines: ");
                    Writer.WriteLine(Convert.ToString(tmpFileSize), ConsoleColor.Green);
                    Writer.Write(" File Size: ");
                    Writer.Write(Convert.ToString(tmpFileWeight), ConsoleColor.Green);
                    Writer.WriteLine(" Bytes", ConsoleColor.DarkGray);
                    GUI.DrawSpacer();
                    GUI.FormatConsoleExpBuffer();
                    //                   D        H        | O  V                         | A T     "
                    Global.tmpHeader = " LINE #            | OP VALUE                               ";
                    Global.tmpHeader = Global.tmpHeader.PadRight(Global.WindowWidth - 1, Global.charSpace);
                    Writer.WriteLine(Global.tmpHeader, ConsoleColor.Black, ConsoleColor.White);
                    Writer.ResetConsoleColor();
                    Global.tmpReadErrorFound = 0;
                    int tmpReadModePause = 0;

                    for (int ii = 0; ii <= (tmpFileSize-1); ii++)
                    {
                        Global.scriptCodeValid = false;
                        string tmpLineNumber = Convert.ToString(ii);
                        tmpLineNumber = tmpLineNumber.PadLeft(8, Global.charZero);
                        Writer.Write(" ");
                        Writer.Write(tmpLineNumber);
                        Writer.Write(" ");
                        string tmpHexLineNumber = " ";
                        tmpHexLineNumber = tmpHexLineNumber.PadLeft(8, Global.charSpace);
                        Writer.Write(tmpHexLineNumber, ConsoleColor.DarkGray);
                        Writer.Write(" | ", ConsoleColor.DarkMagenta);
                        string tmpFileData = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Skip(ii).Take(1).First();
                        // Parse first XX of OpCode.
                        string tmpSubString = tmpFileData.Substring(0, 2);
                        tmpKindOfOP = KindOfOPStates.None;
                        Global.PV_RESW = 9;
                        PV_GROUP = PV_GROUPStates.None;
                        switch (Global.tmpSubstring)
                        {
                            case "PB":
                            case "RB":
                            case "RA":
                                tmpKindOfOP = KindOfOPStates.CO;
                                break;
                            case "JT":
                                tmpKindOfOP = KindOfOPStates.JT;
                                break;
                            case "SP":
                            case "FP":
                            case "TP":
                                tmpKindOfOP = KindOfOPStates.SP;
                                break;
                            case "JM":
                                tmpKindOfOP = KindOfOPStates.JU;
                                break;
                            case "HW":
                            case "SW":
                            case "SR":
                            case "SM":
                            case "CM":
                            case "DM":
                                tmpKindOfOP = KindOfOPStates.TI;
                                break;
                            case "RE":
                                tmpKindOfOP = KindOfOPStates.RE;
                                break;
                            case "EN":
                                tmpKindOfOP = KindOfOPStates.EN;
                                break;
                            case "KS":
                                tmpKindOfOP = KindOfOPStates.KS;
                                break;

                            default:
                                Global.scriptCodeValid = false;
                                break;
                        }

                        if (Global.scriptCodeValid)
                        {
                            // Draw on screen the first half of the OpCode.
                            switch (tmpKindOfOP)
                            {
                                case KindOfOPStates.SP:
                                case KindOfOPStates.CO:
                                case KindOfOPStates.KS:
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.Yellow);
                                    break;
                                case KindOfOPStates.JU:
                                case KindOfOPStates.JT:
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.Magenta);
                                    break;
                                case KindOfOPStates.TI:
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.Blue);
                                    break;
                                case KindOfOPStates.RE:
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.DarkMagenta);
                                    break;
                                case KindOfOPStates.EN:
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.White, ConsoleColor.Blue);
                                    break;
                            }
                        }
                        else
                        {
                            // Error: Not a valid OpCode.
                            Global.tmpReadErrorFound++;
                            int tmpLateSubstring = tmpFileData.Length - 2;
                            tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                            Writer.Write(tmpSubString, ConsoleColor.Red);
                        }
                        switch (tmpKindOfOP)
                        {
                            // OpCode is a Key String.
                            case KindOfOPStates.KS:
                            // OpCode is an end OpCode.
                            case KindOfOPStates.EN:
                            // OpCode is a Script Commentary/Rem.
                            case KindOfOPStates.RE:
                            {
                                int tmpLateSubstring = tmpFileData.Length - 2;
                                tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                ConsoleColor color = tmpKindOfOP == KindOfOPStates.KS ? ConsoleColor.DarkGreen : ConsoleColor.DarkMagenta;
                                Writer.Write(tmpSubString, color);
                                break;
                            }
                        }

                        // Parse next 2 XX YY of the OpCode
                        if ((tmpFileData.Length >= 5) && (tmpKindOfOP != KindOfOPStates.RE))
                        {
                            tmpSubString = tmpFileData.Substring(2, 1);
                            if (tmpSubString == " ")
                            {
                                Writer.Write(tmpSubString, ConsoleColor.Yellow);
                            }
                            else
                            {
                                Global.tmpReadErrorFound++;
                                Writer.Write(tmpSubString, ConsoleColor.White, ConsoleColor.Red);
                            }
                            switch (tmpKindOfOP)
                            {
                                case KindOfOPStates.CO:
                                case KindOfOPStates.SP:
                                {
                                    // OpCode is a button control OpCode.
                                    Global.scriptCodeValid = false;
                                    tmpSubString = tmpFileData.Substring(3, 2);
                                    switch (tmpSubString)
                                    {
                                        case "AA":
                                        case "BB":
                                        case "ST":
                                        case "SE":
                                        case "UP":
                                        case "DO":
                                        case "LE":
                                        case "RI":
                                            Global.scriptCodeValid = true;
                                            break;
                                        default:
                                            Global.scriptCodeValid = false;
                                            break;
                                    }
                                    if (Global.SysTarget == "SNES")
                                    {
                                        switch (tmpSubString)
                                        {
                                            case "XX":
                                            case "YY":
                                            case "BL":
                                            case "BR":
                                                Global.scriptCodeValid = true;
                                                break;
                                            default:
                                                Global.scriptCodeValid = false;
                                                break;
                                        }
                                    }
                                    if (Global.flagExtMode)
                                    {
                                        switch (Global.tmpSubstring)
                                        {
                                            case "E0":
                                            case "E1":
                                            case "E2":
                                            case "E3":
                                            case "E4":
                                            case "E5":
                                            case "E6":
                                            case "E7":
                                            case "E8":
                                            case "E9":
                                                Global.scriptCodeValid = true;
                                                break;
                                            default:
                                                Global.scriptCodeValid = false;
                                                break;
                                        }
                                    }
                                    if (Global.scriptCodeValid)
                                    {
                                        Writer.Write(tmpSubString, ConsoleColor.Green);
                                    }
                                    else
                                    {
                                        Global.tmpReadErrorFound++;
                                        Writer.Write(tmpSubString, ConsoleColor.Red);
                                    }
                                    if (tmpFileData.Length >= 5)
                                    {
                                        if ((tmpKindOfOP == KindOfOPStates.CO) || (tmpKindOfOP == KindOfOPStates.SP))
                                        {
                                            int tmpLateSubstring = tmpFileData.Length - 5;
                                            tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                            ConsoleColor color = tmpKindOfOP == KindOfOPStates.CO ? ConsoleColor.Red : ConsoleColor.Cyan;
                                            Writer.Write(tmpSubString, color);
                                        }
                                    }

                                    break;
                                }
                                // OpCode is a Jump OpCode.
                                case KindOfOPStates.JU:
                                // OpCode is a Time/Wait OpCode.
                                case KindOfOPStates.TI:
                                {
                                    int tmpLateSubstring = tmpFileData.Length - 3;
                                    tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                    ConsoleColor color = tmpKindOfOP == KindOfOPStates.JU ? ConsoleColor.White : ConsoleColor.Cyan;
                                    Writer.Write(tmpSubString, color);
                                    break;
                                }
                                // OpCode is a Jump X Times OpCode.
                                case KindOfOPStates.JT:
                                {
                                    // Get how many times it will repeat (Max = 99).
                                    tmpSubString = tmpFileData.Substring(3, 2);
                                    Writer.Write(tmpSubString, ConsoleColor.DarkCyan);
                                    tmpSubString = tmpFileData.Substring(5, 1);
                                    if (tmpSubString == " ")
                                    {
                                        // Is a valid JT OpCode. Draw line on screen.
                                        int tmpLateSubstring = tmpFileData.Length - 5;
                                        tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                        Writer.Write(tmpSubString, ConsoleColor.Cyan);
                                    }
                                    else
                                    {
                                        // Error: Format error on JT OpCode.
                                        Global.tmpReadErrorFound++;
                                        int tmpLateSubstring = tmpFileData.Length - 5;
                                        tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                        Writer.Write(tmpSubString, ConsoleColor.Red);
                                    }
                                    break;
                                }
                            }
                        }
                        else if ((tmpFileData.Length >= 3) && (tmpFileData.Length <= 5) && (tmpKindOfOP != KindOfOPStates.RE))
                        {
                            // Special case for small value OpCodes.
                            switch (tmpKindOfOP)
                            {
                                // OpCode is a Jump OpCode.
                                case KindOfOPStates.JU:
                                // OpCode is a Time/Wait OpCode.
                                case KindOfOPStates.TI:
                                {
                                    int tmpLateSubstring = tmpFileData.Length - 3;
                                    tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                    Writer.Write(" ");
                                    ConsoleColor color = tmpKindOfOP == KindOfOPStates.JU ? ConsoleColor.White : ConsoleColor.Cyan;
                                    Writer.Write(tmpSubString, color);
                                    break;
                                }
                                // OpCode is a Jump X Times OpCode.
                                case KindOfOPStates.JT:
                                {
                                    // Get how many times it will repeat (Max = 99).
                                    tmpSubString = tmpFileData.Substring(3, 2);
                                    Writer.Write(" ");
                                    Writer.Write(tmpSubString, ConsoleColor.DarkCyan);
                                    tmpSubString = tmpFileData.Substring(5, 1);
                                    if (tmpSubString == " ")
                                    {
                                        // Is a valid JT OpCode. Draw line on screen.
                                        Writer.Write(" ");
                                        int tmpLateSubstring = tmpFileData.Length - 6;
                                        tmpSubString = tmpFileData.Substring(6, tmpLateSubstring);
                                        Writer.Write(tmpSubString);
                                    }
                                    else
                                    {
                                        // Error: Format error on JT OpCode.
                                        Global.tmpReadErrorFound++;
                                        Writer.Write(" ");
                                        int tmpLateSubstring = tmpFileData.Length - 6;
                                        tmpSubString = tmpFileData.Substring(6, tmpLateSubstring);
                                        Writer.Write(tmpSubString, ConsoleColor.Red);
                                    }

                                    break;
                                }
                            }
                        }

                        Writer.ResetConsoleColor();
                        Global.tmpPadSpacing = " ".PadRight(80 - Global.tmpFileData.Length - 49);
                        Writer.WriteLine(Global.tmpPadSpacing); // Intro endline
                        tmpReadModePause++;
                        if (tmpReadModePause != Global.tmpReadMaxPause) continue;

                        Writer.Write(" ▬ ", ConsoleColor.Cyan);
                        Writer.Write("                ");
                        Writer.Write("|", ConsoleColor.DarkMagenta);
                        Writer.Write(" Errors found: ");
                        Writer.Write(Convert.ToString(Global.tmpReadErrorFound), Global.tmpReadErrorFound == 0 ? ConsoleColor.Green : ConsoleColor.Red);
                        Writer.Write(" Press ", ConsoleColor.DarkGray);
                        Writer.Write("[ENTER]");
                        Writer.Write(" to continue reading. ", ConsoleColor.DarkGray);
                        Console.ReadLine();
                        tmpReadModePause = 0;
                    }
                    Writer.Write(" Total errors found: ");
                    Writer.WriteLine(Convert.ToString(Global.tmpReadErrorFound), Global.tmpReadErrorFound == 0 ? ConsoleColor.Green : ConsoleColor.Red);
                    ReadHalt();
                    Writer.Write(" Press ", ConsoleColor.DarkGray);
                    Writer.Write("[ENTER]");
                    Writer.Write(" to return to the menu.", ConsoleColor.DarkGray);
                    Writer.Write(" ", ConsoleColor.Black);
                    Console.ReadLine();
                    restartArea = true;
                    GC.Collect();
                    break;
                }

                case "2":
                case "e":
                    ksoAutoScriptReadMode = false;
                    Global.tmpUserInput = "NULL";
                    Writer.Write(" Leaving Read Menu...");
                    break;
            }
        }
    }
    //TODO: readKas and executeKas have ***a lot*** of copy-pasted code.
    public static void ExecuteKAS()
    {
        bool ksoAutoScriptExecuteMode = true;
        Global.restartArea = false;
        while (ksoAutoScriptExecuteMode)
        {
            GUI.DrawExecuteMenu();
            if (Global.restartArea == false)
            {
                Global.tmpUserInput = !Global.flagLoadLast ? StaticPrompt() : "1";
            }
            else
            {
                ExecuteKAS();
            }
            switch (Global.tmpUserInput.ToLower())
            {
                case "1":
                case "l":
                {
                    if (Global.flagLoadLast == false)
                    {
                        Global.tmpUserInput = FileStaticPrompt();
                        Global.lastFileLoaded = Global.tmpUserInput;
                        Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                        WriteToConfig(0, Global.tmpWritePad);
                    }
                    else { Global.tmpUserInput = Global.lastFileLoaded; }
                    bool tmpFileExists = File.Exists(Global.scriptsDirectory + Global.tmpUserInput);
                    if (!tmpFileExists)
                    {
                        Global.flagLoadLast = false;
                        Global.tmpUserInput = "NULL";
                        return;
                    }
                    FileInfo scriptFileInfo = new FileInfo(Global.scriptsDirectory + Global.tmpUserInput);
                    int tmpFileSize = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Count();
                    long tmpFileWeight = scriptFileInfo.Length;
                    GUI.DrawTitle();
                    GUI.DrawSubtitle("KSOAutoScript Execution");
                    GUI.DrawSpacer();
                    Writer.Write(" Filename: ");
                    Writer.WriteLine(Convert.ToString(Global.tmpUserInput), ConsoleColor.Cyan);
                    Writer.Write(" Total Lines: ");
                    Writer.WriteLine(Convert.ToString(tmpFileSize), ConsoleColor.Green);
                    Writer.Write(" File Size: ");
                    Writer.Write(Convert.ToString(tmpFileWeight), ConsoleColor.Green);
                    Writer.WriteLine(" Bytes", ConsoleColor.DarkGray);
                    GUI.DrawSpacer();
                    ExecutionHalt();
                    Console.CursorVisible = false;
                    GC.Collect();
                    GUI.FormatConsoleExpBuffer();
                    GUI.DrawTitle();
                    //                   D        H        | O  V                         | A T     "
                    Global.tmpHeader = " LINE #   LINE @   | OP VALUE                     | ▲ TIME  ";
                    Global.tmpHeader = Global.tmpHeader.PadRight(Global.WindowWidth - 1, Global.charSpace);
                    Writer.WriteLine(Global.tmpHeader, ConsoleColor.Black, ConsoleColor.White);
                    Writer.ResetConsoleColor();
                    if (tmpFileSize > 0)
                    {
                        Timing.timeBeginPeriod(Convert.ToUInt32(Global.timeResolution));
                        // Load the whole file into an array.
                        for (int jj = 0; jj <= (tmpFileSize - 1); jj++)
                        {
                            Global.tmpLoadString = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Skip(jj).Take(1).First();
                            Global.tmpFileArray.Add(Global.tmpLoadString);
                        }
                        // Start Milliseconds Control Counter.
                        Global.tmpMarkerMillis = 0;
                        Global.SW.Start();
                        // Stop in case it was already running from another execution.
                        Global.SW.Stop();
                        Global.SW.Start();
                        Global.SW.Restart();
                        Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                        Global.tmpLineCursor = 0;
                        Global.tmpLineTotal = 0;
                        while (Global.tmpLineCursor <= tmpFileSize)
                        {
                            Global.scriptCodeValid = true;
                            string tmpLineNumber = Convert.ToString(Global.tmpLineCursor);
                            tmpLineNumber = tmpLineNumber.PadLeft(8, Global.charZero);
                            Writer.Write(" " + tmpLineNumber);
                            Writer.Write(" ");
                            string padLineTotal = Convert.ToString(Global.tmpLineTotal);
                            padLineTotal = padLineTotal.PadLeft(8, Global.charZero);
                            Writer.Write(padLineTotal, ConsoleColor.DarkGray);
                            Writer.Write(" | ", ConsoleColor.DarkMagenta);
                            if (Global.tmpLineCursor >= tmpFileSize)
                            {
                                break;
                            }

                            Global.tmpFileData = Global.tmpFileArray[Global.tmpLineCursor];
                            if (Global.tmpFileData.Length > 0)
                            {
                                // Limit max. length of a line.
                                if (Global.tmpFileData.Length >= 30)
                                {
                                    Global.tmpFileData = Global.tmpFileData.Substring(0, 30);
                                }
                                // Parse first XX of OpCode.
                                Global.tmpSubstring = Global.tmpFileData.Substring(0, 2);
                                tmpKindOfOP = KindOfOPStates.None;
                                Global.PV_RESW = 9;
                                PV_GROUP = PV_GROUPStates.None;
                                Global.tmpOPCache = "NULL";
                                switch (Global.tmpSubstring)
                                {
                                    case "PB":
                                    case "RB":
                                    case "RA":
                                        tmpKindOfOP = KindOfOPStates.CO;
                                        break;
                                    case "JT":
                                        tmpKindOfOP = KindOfOPStates.JT;
                                        break;
                                    case "SP":
                                    case "FP":
                                    case "TP":
                                        tmpKindOfOP = KindOfOPStates.SP;
                                        break;
                                    case "JM":
                                        tmpKindOfOP = KindOfOPStates.JU;
                                        break;
                                    case "HW":
                                    case "SW":
                                    case "SR":
                                    case "SM":
                                    case "CM":
                                    case "DM":
                                        tmpKindOfOP = KindOfOPStates.TI;
                                        break;
                                    case "RE":
                                        tmpKindOfOP = KindOfOPStates.RE;
                                        break;
                                    case "EN":
                                        tmpKindOfOP = KindOfOPStates.EN;
                                        break;
                                    case "KS":
                                        tmpKindOfOP = KindOfOPStates.KS;
                                        break;

                                    default:
                                        Global.scriptCodeValid = false;
                                        break;
                                }

                                if (Global.scriptCodeValid)
                                {
                                    // Draw on screen the first half of the OpCode.
                                    switch (tmpKindOfOP)
                                    {
                                        case KindOfOPStates.SP:
                                        case KindOfOPStates.CO:
                                        case KindOfOPStates.KS:
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Yellow);
                                            break;
                                        case KindOfOPStates.JU:
                                        case KindOfOPStates.JT:
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Magenta);
                                            break;
                                        case KindOfOPStates.TI:
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Blue);
                                            break;
                                        case KindOfOPStates.RE:
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.DarkMagenta);
                                            break;
                                        case KindOfOPStates.EN:
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.White, ConsoleColor.Blue);
                                            break;
                                    }
                                    // Save the OP type for further use
                                    Global.tmpOPCache = Global.tmpSubstring;
                                }
                                else
                                {
                                    // Error: Not a valid OpCode.
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.Red);
                                }
                                // Small OPCodes Special Identification Area
                                switch (tmpKindOfOP)
                                {
                                    // OpCode is a Special Key String.
                                    case KindOfOPStates.KS:
                                    // OpCode is a Script Commentary/Rem.
                                    case KindOfOPStates.RE:
                                    // OpCode is an end OpCode.
                                    case KindOfOPStates.EN:
                                    {
                                        int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                        ConsoleColor color = tmpKindOfOP == KindOfOPStates.KS ? ConsoleColor.DarkGreen : ConsoleColor.Magenta;
                                        Writer.Write(Global.tmpSubstring, color);
                                        break;
                                    }
                                }
                                // Small OpCodes Special Drawing
                                if (Global.tmpFileData.Length <= 3)
                                {
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    Writer.Write(Global.tmpSubstring, ConsoleColor.Red);
                                }
                                // Parse next 2 XX YY of the OpCode
                                if ((Global.tmpFileData.Length >= 5) && (tmpKindOfOP != KindOfOPStates.RE) && (tmpKindOfOP != KindOfOPStates.KS))
                                {
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, 1);
                                    if (Global.tmpSubstring == " ")
                                    {
                                        Writer.Write(Global.tmpSubstring, ConsoleColor.Yellow);
                                    }
                                    else
                                    {
                                        // Error: OpCode has a format error.
                                        Writer.Write(Global.tmpSubstring, ConsoleColor.White, ConsoleColor.Red);
                                    }
                                    // Read the buttons and draw them in Control OpCodes.
                                    if ((tmpKindOfOP == KindOfOPStates.CO) || (tmpKindOfOP == KindOfOPStates.SP))
                                    {
                                        // OpCode is a button control OpCode.
                                        Global.scriptCodeValid = true;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        switch (Global.tmpSubstring)
                                        {
                                            case "AA":
                                                PV_GROUP = PV_GROUPStates.A;
                                                break;
                                            case "BB":
                                                PV_GROUP = PV_GROUPStates.B;
                                                break;
                                            case "ST":
                                                PV_GROUP = PV_GROUPStates.ST;
                                                break;
                                            case "SE":
                                                PV_GROUP = PV_GROUPStates.SE;
                                                break;
                                            case "UP":
                                                PV_GROUP = PV_GROUPStates.UP;
                                                break;
                                            case "DO":
                                                PV_GROUP = PV_GROUPStates.DO;
                                                break;
                                            case "LE":
                                                PV_GROUP = PV_GROUPStates.LE;
                                                break;
                                            case "RI":
                                                PV_GROUP = PV_GROUPStates.RI;
                                                break;
                                            default:
                                                Global.scriptCodeValid = false;
                                                break;
                                        }
                                        if (Global.SysTarget == "SNES")
                                        {
                                            switch (Global.tmpSubstring)
                                            {
                                                case "XX":
                                                    PV_GROUP = PV_GROUPStates.XX;
                                                    break;
                                                case "YY":
                                                    PV_GROUP = PV_GROUPStates.YY;
                                                    break;
                                                case "BL":
                                                    PV_GROUP = PV_GROUPStates.BL;
                                                    break;
                                                case "BR":
                                                    PV_GROUP = PV_GROUPStates.BR;
                                                    break;
                                                default:
                                                    Global.scriptCodeValid = false;
                                                    break;
                                            }
                                        }
                                        if (Global.flagExtMode)
                                        {
                                            switch (Global.tmpSubstring)
                                            {
                                                case "E0":
                                                    PV_GROUP = PV_GROUPStates.E0;
                                                    break;
                                                case "E1":
                                                    PV_GROUP = PV_GROUPStates.E1;
                                                    break;
                                                case "E2":
                                                    PV_GROUP = PV_GROUPStates.E2;
                                                    break;
                                                case "E3":
                                                    PV_GROUP = PV_GROUPStates.E3;
                                                    break;
                                                case "E4":
                                                    PV_GROUP = PV_GROUPStates.E4;
                                                    break;
                                                case "E5":
                                                    PV_GROUP = PV_GROUPStates.E5;
                                                    break;
                                                case "E6":
                                                    PV_GROUP = PV_GROUPStates.E6;
                                                    break;
                                                case "E7":
                                                    PV_GROUP = PV_GROUPStates.E7;
                                                    break;
                                                case "E8":
                                                    PV_GROUP = PV_GROUPStates.E8;
                                                    break;
                                                case "E9":
                                                    PV_GROUP = PV_GROUPStates.E9;
                                                    break;
                                                default:
                                                    Global.scriptCodeValid = false;
                                                    break;
                                            }
                                        }
                                        Writer.Write(Global.tmpSubstring, Global.scriptCodeValid ? ConsoleColor.Green : ConsoleColor.Red);
                                        // Draw the rest of an OpCode if is bigger than 5 spaces.
                                        if (Global.tmpFileData.Length >= 5)
                                        {
                                            if (tmpKindOfOP == KindOfOPStates.CO)
                                            {
                                                // Error: A CO type OpCode shouldn't have more characters.
                                                int tmpLateSubstring = Global.tmpFileData.Length - 5;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(5, tmpLateSubstring);
                                                Writer.Write(Global.tmpSubstring, ConsoleColor.Red);
                                            }
                                            if (tmpKindOfOP == KindOfOPStates.SP)
                                            {
                                                // Get SoftPush OpCode time value.
                                                Writer.Write(" ");
                                                int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                                // Save the value for further use.
                                                Global.tmpOPValueCache = Global.tmpSubstring;
                                                Writer.Write(Global.tmpSubstring, ConsoleColor.Cyan);
                                            }
                                        }
                                    }
                                    else if (tmpKindOfOP == KindOfOPStates.JU)
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        Writer.Write(Global.tmpSubstring);
                                    }
                                    else if (tmpKindOfOP == KindOfOPStates.TI)
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        Writer.Write(Global.tmpSubstring, ConsoleColor.Cyan);
                                    }
                                    else if (tmpKindOfOP == KindOfOPStates.JT)
                                    {
                                        // OpCode is a Jump X Times OpCode.
                                        // Get how many times it will repeat (Max = 99).
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        Writer.Write(Global.tmpSubstring, ConsoleColor.DarkCyan);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(5, 1);
                                        if (Global.tmpSubstring == " ")
                                        {
                                            // Is a valid JT OpCode. Draw line on screen.
                                            Writer.Write(" ");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Cyan);
                                        }
                                        else
                                        {
                                            // Error: Format error on JT OpCode.
                                            Writer.Write(" ");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Red);
                                        }
                                    }
                                }
                                else if ((Global.tmpFileData.Length > 3) && (Global.tmpFileData.Length < 5) && (tmpKindOfOP != KindOfOPStates.RE))
                                {
                                    // Special case for small value OpCodes.
                                    switch (tmpKindOfOP)
                                    {
                                        // OpCode is a Jump OpCode.
                                        case KindOfOPStates.JU:
                                        {
                                            int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                            Global.tmpOPValueCache = Global.tmpSubstring;
                                            Writer.Write(" ");
                                            Writer.Write(Global.tmpSubstring);
                                            break;
                                        }
                                        // OpCode is a Time/Wait OpCode.
                                        case KindOfOPStates.TI:
                                        {
                                            int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                            Global.tmpOPValueCache = Global.tmpSubstring;
                                            Writer.Write(" ");
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Cyan);
                                            break;
                                        }
                                        case KindOfOPStates.JT:
                                        {
                                            // OpCode is a Jump X Times OpCode.
                                            // Get how many times it will repeat (Max = 99).
                                            Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                            Writer.Write(" ");
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.DarkCyan);
                                            Global.tmpOPValueCache = Global.tmpSubstring;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(5, 1);

                                            Writer.Write(" ");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            Writer.Write(Global.tmpSubstring, ConsoleColor.Red);
                                            // " " is a valid JT OpCode. Everything else is a format error.
                                            ConsoleColor color = Global.tmpSubstring == " " ? ConsoleColor.White : ConsoleColor.Red;
                                            Writer.Write(Global.tmpSubstring, color);
                                            break;
                                        }
                                    }
                                }
                            }
                            // Draw close bar.
                            Global.tmpWritePad = "| ";
                            Global.tmpWritePad = Global.tmpWritePad.PadLeft((31 - Global.tmpFileData.Length), Global.charSpace);
                            Writer.Write(Global.tmpWritePad, ConsoleColor.DarkMagenta);
                            switch (Global.tmpOPCache)
                            {
                                // Execute read OpCodes.
                                // PB - Push Button.
                                case "PB":
                                {
                                    VirtualKeyboard.VK_Down(PV_GROUP);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                }
                                // RB - Release Button.
                                case "RB":
                                {
                                    VirtualKeyboard.VK_Up(PV_GROUP);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                }
                                // RA - Release all buttons.
                                case "RA":
                                {
                                    VirtualKeyboard.VK_Up(Global.VB_A);
                                    VirtualKeyboard.VK_Up(Global.VB_B);
                                    VirtualKeyboard.VK_Up(Global.VB_ST);
                                    VirtualKeyboard.VK_Up(Global.VB_SE);
                                    VirtualKeyboard.VK_Up(Global.VB_UP);
                                    VirtualKeyboard.VK_Up(Global.VB_DO);
                                    VirtualKeyboard.VK_Up(Global.VB_LE);
                                    VirtualKeyboard.VK_Up(Global.VB_RI);
                                    if (Global.SysTarget == "SNES")
                                    {
                                        VirtualKeyboard.VK_Up(Global.VB_X);
                                        VirtualKeyboard.VK_Up(Global.VB_Y);
                                        VirtualKeyboard.VK_Up(Global.VB_BL);
                                        VirtualKeyboard.VK_Up(Global.VB_BR);
                                    }
                                    if (Global.flagExtMode)
                                    {
                                        VirtualKeyboard.VK_Up(Global.VB_E0);
                                        VirtualKeyboard.VK_Up(Global.VB_E1);
                                        VirtualKeyboard.VK_Up(Global.VB_E2);
                                        VirtualKeyboard.VK_Up(Global.VB_E3);
                                        VirtualKeyboard.VK_Up(Global.VB_E4);
                                        VirtualKeyboard.VK_Up(Global.VB_E5);
                                        VirtualKeyboard.VK_Up(Global.VB_E6);
                                        VirtualKeyboard.VK_Up(Global.VB_E7);
                                        VirtualKeyboard.VK_Up(Global.VB_E8);
                                        VirtualKeyboard.VK_Up(Global.VB_E9);
                                    }
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                }
                                // HW - Hold/Wait a given time.
                                case "HW":
                                    Timing.Hold(Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                // JM - Absolute Jump / Infinite Loop to a given line.
                                case "JM":
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPValueCache);
                                    break;
                                // JT - Jump X Times to a given line.
                                case "JT":
                                {
                                    if (Global.tmpJumpCurrent != Global.tmpLineCursor)
                                    {
                                        Global.tmpJumpTimes = Convert.ToInt32(Global.tmpOPValueCache);
                                        Global.tmpJumpTimes -= 1;
                                        Global.tmpJumpCurrent = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPAuxValueCache);
                                    }
                                    if (Global.tmpJumpCurrent == Global.tmpLineCursor)
                                    {
                                        if (Global.tmpJumpTimes >= 1)
                                        {
                                            Global.tmpJumpTimes -= 1;
                                            Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPAuxValueCache);
                                        }
                                        else
                                        {
                                            Global.tmpLineCursor++;
                                            Global.tmpJumpCurrent = 0;
                                        }
                                    }

                                    break;
                                }
                                // RE - Comment / REM
                                case "RE":
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                // SP - Soft Push a button a given time
                                case "SP":
                                {
                                    VirtualKeyboard.VK_HoldUntilDelay(PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;

                                    break;
                                }
                                // FP - Fast Push a button a given time.
                                case "FP":
                                {
                                    VirtualKeyboard.VK_HoldUntil(PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;

                                    break;
                                }
                                // TP - Turbo-Pushes a button a given time.
                                case "TP":
                                {
                                    VirtualKeyboard.VK_HoldTurbo(PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;

                                    break;
                                }
                                // KS - Attempts to push a Key String.
                                case "KS":
                                    VirtualKeyboard.VK_HoldUntilDelay(Global.tmpOPValueCache, 50);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                    break;
                                // SW - Sync Waits until there's no delay.
                                case "SW":
                                {
                                    if (Global.tmpStackDelay <= Convert.ToInt32(Global.tmpOPValueCache))
                                    {
                                        Global.tmpStackSync = Convert.ToInt32(Global.tmpOPValueCache) - Global.tmpStackDelay;
                                        Global.tmpStackMem = Global.tmpStackDelay;
                                        Timing.Hold(Global.tmpStackSync);
                                        Global.tmpStackDelay -= Global.tmpStackSync;
                                        if (Global.tmpStackDelay < 0) { Global.tmpStackDelay = 0; }
                                        Global.tmpStackStatus = 1;
                                    }
                                    else
                                    {
                                        Timing.Hold(Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpStackDelay -= Convert.ToInt32(Global.tmpOPValueCache);
                                        Global.tmpStackMem = Global.tmpStackDelay;
                                        Global.tmpStackStatus = 2;
                                    }
                                    Global.tmpLineCursor++;
                                    break;
                                }
                                // SR - Resets the Sync Delay Stack.
                                case "SR":
                                    Global.tmpStackDelay = 0;
                                    Global.tmpLineCursor++;
                                    break;
                                // DM - Displays marker time.
                                case "DM":
                                    Global.tmpStackStatus = 3;
                                    Global.tmpLineCursor++;
                                    break;
                                // SM - Sets a new marker and resets the marker counter.
                                case "SM":
                                    Global.tmpMarkerMillis = 0;
                                    Global.tmpLineCursor++;
                                    break;
                                // CM - Waits if the time marker is lower than the value.
                                case "CM":
                                {
                                    if (Global.tmpMarkerMillis < Convert.ToInt32(Global.tmpOPValueCache))
                                    {
                                        Global.tmpMarkerDifference = Convert.ToInt32(Global.tmpOPValueCache) - Global.tmpMarkerMillis;
                                        Timing.Hold(Global.tmpMarkerDifference);
                                        Global.tmpStackStatus = 4;
                                    }
                                    else
                                    {
                                        Global.tmpMarkerDifference = 0;
                                        Global.tmpStackStatus = 5;
                                    }
                                    Global.tmpLineCursor++;
                                    break;
                                }
                                // EN - Ends the script execution.
                                case "EN":
                                    Global.tmpLineCursor = tmpFileSize + 1;
                                    break;
                            }

                            // Calculate the Delta time between OpCodes.
                            if (Global.SW.ElapsedMilliseconds >= 1000000)
                            {
                                Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds) - 1000000;
                                Global.SW.Stop();
                                Global.SW.Start();
                                Global.tmpDeltaMillis = Global.tmpLastMillis;
                            }
                            else
                            {
                                Global.tmpDeltaMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds) - Global.tmpLastMillis;
                                Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                            }
                            // Parse to avoid tmpOPValueCache being not an integer
                            Global.tmpIsParsed = Int32.TryParse(Global.tmpOPValueCache, out _);
                            if (!Global.tmpIsParsed) Global.tmpOPValueCache = "0";

                            switch (Global.tmpOPCache)
                            {
                                // Sync count all delay.
                                case "SP":
                                    Global.tmpStackDelay += Global.tmpDeltaMillis - (Convert.ToInt32(Global.tmpOPValueCache) + 25);
                                    Global.tmpStackException = true;
                                    break;
                                case "KS":
                                    Global.tmpStackDelay += Global.tmpDeltaMillis - (Convert.ToInt32(Global.tmpOPValueCache) + 50);
                                    Global.tmpStackException = true;
                                    break;
                                case "SM":
                                case "DM":
                                case "SR":
                                    Global.tmpStackDelay += Global.tmpDeltaMillis;
                                    Global.tmpStackException = true;
                                    break;
                                case "CM":
                                    Global.tmpStackDelay += Global.tmpDeltaMillis - Global.tmpMarkerDifference;
                                    Global.tmpStackException = true;
                                    break;
                            }
                            if (!Global.tmpStackException)
                            {
                                switch (tmpKindOfOP)
                                {
                                    case KindOfOPStates.TI:
                                    case KindOfOPStates.SP:
                                        Global.tmpStackDelay += (Global.tmpDeltaMillis - Convert.ToInt32(Global.tmpOPValueCache));
                                        break;
                                    default:
                                        Global.tmpStackDelay += Global.tmpDeltaMillis;
                                        break;
                                }
                            }
                            if (Global.tmpStackDelay < 0) Global.tmpStackDelay = 0;
                            Global.tmpStackException = false;
                            Global.tmpMarkerMillis += Global.tmpDeltaMillis;
                            Writer.Write(Convert.ToString(Global.tmpDeltaMillis));
                            Writer.Write(" ▲ms");
                            switch (Global.tmpStackStatus)
                            {
                                case 1:
                                    Writer.Write(" (" + Global.tmpStackMem + "ms)", ConsoleColor.Green); Global.tmpStackStatus = 0;
                                    break;
                                case 2:
                                    Writer.Write(" (" + Global.tmpStackMem + "ms)", ConsoleColor.Red); Global.tmpStackStatus = 0;
                                    break;
                                case 3:
                                    Writer.Write(" (" + Global.tmpMarkerMillis + "ms)"); Global.tmpStackStatus = 0;
                                    break;
                                case 4:
                                    Writer.Write(" (" + Global.tmpMarkerDifference + "ms)", ConsoleColor.Green); Global.tmpStackStatus = 0;
                                    break;
                                case 5:
                                    Writer.Write(" (0 ms)"); Global.tmpStackStatus = 0;
                                    break;
                            }

                            Writer.WriteLine(" "); // Intro endline.
                            Global.tmpLineTotal += 1;
                        } // Execution While end.
                        Global.tmpElapsedMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                        Timing.timeEndPeriod(Convert.ToUInt32(Global.timeResolution));
                        GUI.DrawSpacer();
                        Writer.Write(" Total time elapsed: ");
                        Writer.Write(Convert.ToString(Global.tmpElapsedMillis), ConsoleColor.Cyan);
                        Writer.WriteLine(" ms");
                        Global.SW.Reset();
                        Global.SW.Stop();
                        Writer.ResetConsoleColor();
                    }
                    Global.tmpFileArray.Clear();
                    Global.flagLoadLast = false;
                    Console.CursorVisible = true;
                    EndExecutionHalt();
                    Writer.Write(" Press ", ConsoleColor.DarkGray);
                    Writer.Write("[ENTER]");
                    Writer.Write(" to return to the menu.", ConsoleColor.DarkGray);
                    Writer.Write(" ", ConsoleColor.Black);
                    Console.ReadLine();
                    Global.restartArea = true;
                    GC.Collect();
                    break;
                }
                case "2":
                case "e":
                    ksoAutoScriptExecuteMode = false;
                    Global.tmpUserInput = "NULL";
                    Writer.WriteLine(" Leaving Script Execution...");
                    break;
            }
        } // Active While End
    }
}