using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ---------------------------------------------------
// Code: KasTAS.cs
// Version: 0.0.94
// Author: Karst Skarn / Owain#3593 (Discord)
// Date: 12-01-2022 (Begin) / 21-01-2022 (0.0.94)
// Description: C# executable able to read from a custom TAS-Script file and execute keyboard patterns.
// Comments: It should technically work with any kind of emulator/game since it always applies those keys to the active window.
//           Based on WinKeys by Stefan Stranger.
// ---------------------------------------------------

bool keepExecuting = true;
while (keepExecuting == true)
{
    KasTAS.MN.Main();
}


namespace KasTAS
{
    public class Global
    {
        // ---------------------------------------------------
        // Default Keyboard Keys
        //    UP                 A  |    W                P
        // LE DO RI    SE ST   B    | A  S  D    Z  X   O
        // ---------------------------------------------------
        public static string VB_A = "P";
        public static string VB_B = "O";
        public static string VB_ST = "X";
        public static string VB_SE = "Z";
        public static string VB_UP = "W";
        public static string VB_DO = "S";
        public static string VB_LE = "A";
        public static string VB_RI = "D";
        public static string VB_X = "0";
        public static string VB_Y = "9";
        public static string VB_BL = "1";
        public static string VB_BR = "3";
        public static string VB_E0 = "H";
        public static string VB_E1 = "H";
        public static string VB_E2 = "H";
        public static string VB_E3 = "H";
        public static string VB_E4 = "H";
        public static string VB_E5 = "H";
        public static string VB_E6 = "H";
        public static string VB_E7 = "H";
        public static string VB_E8 = "H";
        public static string VB_E9 = "H";

        // Target [GB/SNES]
        public static string SysTarget = "GB";
        public static bool SysConfig = false;
        public static string flagExtMode = "false";

        public static int cfgTotalLines = 12;
        public static int cfgKeyTotalLines = 24;
        public static string lineSixtyNine = "GOGINGA GOGINGA GOGOGIGOAGOGAGUGGEGA GUNGA GINGA";
        public static int cfgValuePos = 18;
        public static string cfgFile = "kascfg.ini";
        public static string cfgKeysFile = "keydata.ini";
        public static int configHoldTime = 7000;
        public static int configReadyTime = 3000;
        public static int testReadyTime = 5000;
        public static int testIncrementalStartTime = 1000;
        public static int testIncrementalReduction = 50;
        public static int testContinousTime = 300;
        public static double testFrameTime = 33.3333;

        public static string lastFileLoaded = "NULL";
        public static string tmpUserInput = " ";
        public static bool keepExecuting = true;
        public static bool configKeysMode = false;
        public static bool testKeysMode = false;
        public static bool KSOAutoScriptReadMode = false;
        public static bool KSOAutoScriptExecuteMode = false;
        public static bool scriptCodeValid = false;
        public static bool flagLoadLast = false;
        public static bool flagFirstRun = true;

        public static bool PV_A = false;
        public static bool PV_B = false;
        public static bool PV_ST = false;
        public static bool PV_SE = false;
        public static bool PV_UP = false;
        public static bool PV_DO = false;
        public static bool PV_LE = false;
        public static bool PV_RI = false;
        public static bool PV_X = false;
        public static bool PV_Y = false;
        public static bool PV_BL = false;
        public static bool PV_BR = false;
        public static byte PV_RESW = 0;

        public static string PV_GROUP = " ";
        public static string tmpKindOfOP = "NULL";
        public static string tmpOPCache = "NULL";
        public static string tmpOPValueCache = "NULL";
        public static string tmpOPAuxValueCache = "NULL";
        public static string tmpPadSpacing = " ";
        public static string tmpWritePad = " ";
        public static string tmpHeader = " ";
        public static string tmpHaltKey = " ";
        public static string tmpFileData = " ";
        public static string tmpSubstring = " ";
        public static List<string> tmpFileArray = new List<string>();
        public static string tmpLoadString = " ";
        public static int tmpLastMillis = 0;
        public static int tmpJumpTimes = 0;
        public static int tmpJumpCurrent = 0;
        public static int tmpDeltaMillis = 0;
        public static int tmpLineTotal = 0;
        public static int tmpVKMillis = 0;
        public static int tmpAuxPadMov = 0;
        public static int tmpFileLoadPer = 0;
        public static int tmpStackDelay = 0;
        public static int tmpStackSync = 0;
        public static int tmpStackMem = 0;
        public static int tmpElapsedMillis = 0;
        public static int tmpReadErrorFound = 0;
        public static int tmpMarkerMillis = 0;
        public static int tmpMarkerDifference = 0;
        public static int timeResolution = 1;
        public static int tmpReadmodePause = 0;
        public static int tmpReadMaxPause = 19;
        public static int tmpLineCursor = 0;
        public static bool tmpStackException = false;
        public static bool tmpIsParsed = false;
        public static bool tmpFileExists = false;
        public static int outVal = 0;
        public static byte tmpStackStatus = 0;
        public static char charSpace = ' ';
        public static char charZero = '0';
        public static char charBar = '▄';
        public static char charUpperBar = '▀';
        public static bool restartArea = false;
        public static int windowH = 21;
        public static int windowW = 82;
        public static string version = "1.0.98";
        public static string scriptsDirectory = "Scripts/";
        public static string showTwitchTitle = "true";
        public static string flagFirstInit = "true";

        public static KeysConverter KC = new KeysConverter();
        public static Stopwatch SW = System.Diagnostics.Stopwatch.StartNew();
    }
    public class KeyboardSend
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        private const int KEYEVENTF_EXTENDEDKEY = 1;
        private const int KEYEVENTF_KEYUP = 2;
        public static void KeyDown(Keys vKey)
        {
            keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }
        public static void KeyUp(Keys vKey)
        {
            keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }
    }
    public class WRT
    {
        public static void WRLine(string Text, string FColor, string BColor)
        {
            if (Text != string.Empty)
            {
                if (FColor != string.Empty)
                {
                    if (FColor == "White") { System.Console.ForegroundColor = ConsoleColor.White; }
                    if (FColor == "Gray") { System.Console.ForegroundColor = ConsoleColor.Gray; }
                    if (FColor == "Black") { System.Console.ForegroundColor = ConsoleColor.Black; }
                    if (FColor == "Blue") { System.Console.ForegroundColor = ConsoleColor.Blue; }
                    if (FColor == "Cyan") { System.Console.ForegroundColor = ConsoleColor.Cyan; }
                    if (FColor == "Green") { System.Console.ForegroundColor = ConsoleColor.Green; }
                    if (FColor == "Red") { System.Console.ForegroundColor = ConsoleColor.Red; }
                    if (FColor == "Yellow") { System.Console.ForegroundColor = ConsoleColor.Yellow; }
                    if (FColor == "Magenta") { System.Console.ForegroundColor = ConsoleColor.Magenta; }
                    if (FColor == "DarkGray") { System.Console.ForegroundColor = ConsoleColor.DarkGray; }
                    if (FColor == "DarkBlue") { System.Console.ForegroundColor = ConsoleColor.DarkBlue; }
                    if (FColor == "DarkCyan") { System.Console.ForegroundColor = ConsoleColor.DarkCyan; }
                    if (FColor == "DarkGreen") { System.Console.ForegroundColor = ConsoleColor.DarkGreen; }
                    if (FColor == "DarkRed") { System.Console.ForegroundColor = ConsoleColor.DarkRed; }
                    if (FColor == "DarkMagenta") { System.Console.ForegroundColor = ConsoleColor.DarkMagenta; }
                    if (FColor == "DarkYellow") { System.Console.ForegroundColor = ConsoleColor.DarkYellow; }
                }
                if (BColor != string.Empty)
                {
                    if (BColor == "White") { System.Console.BackgroundColor = ConsoleColor.White; }
                    if (BColor == "Gray") { System.Console.BackgroundColor = ConsoleColor.Gray; }
                    if (BColor == "Black") { System.Console.BackgroundColor = ConsoleColor.Black; }
                    if (BColor == "Blue") { System.Console.BackgroundColor = ConsoleColor.Blue; }
                    if (BColor == "Cyan") { System.Console.BackgroundColor = ConsoleColor.Cyan; }
                    if (BColor == "Green") { System.Console.BackgroundColor = ConsoleColor.Green; }
                    if (BColor == "Red") { System.Console.BackgroundColor = ConsoleColor.Red; }
                    if (BColor == "Yellow") { System.Console.BackgroundColor = ConsoleColor.Yellow; }
                    if (BColor == "Magenta") { System.Console.BackgroundColor = ConsoleColor.Magenta; }
                    if (BColor == "DarkGray") { System.Console.BackgroundColor = ConsoleColor.DarkGray; }
                    if (BColor == "DarkBlue") { System.Console.BackgroundColor = ConsoleColor.DarkBlue; }
                    if (BColor == "DarkCyan") { System.Console.BackgroundColor = ConsoleColor.DarkCyan; }
                    if (BColor == "DarkGreen") { System.Console.BackgroundColor = ConsoleColor.DarkGreen; }
                    if (BColor == "DarkRed") { System.Console.BackgroundColor = ConsoleColor.DarkRed; }
                    if (BColor == "DarkMagenta") { System.Console.BackgroundColor = ConsoleColor.DarkMagenta; }
                    if (BColor == "DarkYellow") { System.Console.BackgroundColor = ConsoleColor.DarkYellow; }
                }
                System.Console.WriteLine(Text);
            }
        }
        public static void WR(string Text, string FColor, string BColor)
        {
            if (Text != string.Empty)
            {
                if (FColor != string.Empty)
                {
                    if (FColor == "White") { System.Console.ForegroundColor = ConsoleColor.White; }
                    if (FColor == "Gray") { System.Console.ForegroundColor = ConsoleColor.Gray; }
                    if (FColor == "Black") { System.Console.ForegroundColor = ConsoleColor.Black; }
                    if (FColor == "Blue") { System.Console.ForegroundColor = ConsoleColor.Blue; }
                    if (FColor == "Cyan") { System.Console.ForegroundColor = ConsoleColor.Cyan; }
                    if (FColor == "Green") { System.Console.ForegroundColor = ConsoleColor.Green; }
                    if (FColor == "Red") { System.Console.ForegroundColor = ConsoleColor.Red; }
                    if (FColor == "Yellow") { System.Console.ForegroundColor = ConsoleColor.Yellow; }
                    if (FColor == "Magenta") { System.Console.ForegroundColor = ConsoleColor.Magenta; }
                    if (FColor == "DarkGray") { System.Console.ForegroundColor = ConsoleColor.DarkGray; }
                    if (FColor == "DarkBlue") { System.Console.ForegroundColor = ConsoleColor.DarkBlue; }
                    if (FColor == "DarkCyan") { System.Console.ForegroundColor = ConsoleColor.DarkCyan; }
                    if (FColor == "DarkGreen") { System.Console.ForegroundColor = ConsoleColor.DarkGreen; }
                    if (FColor == "DarkRed") { System.Console.ForegroundColor = ConsoleColor.DarkRed; }
                    if (FColor == "DarkMagenta") { System.Console.ForegroundColor = ConsoleColor.DarkMagenta; }
                    if (FColor == "DarkYellow") { System.Console.ForegroundColor = ConsoleColor.DarkYellow; }
                }
                if (BColor != string.Empty)
                {
                    if (BColor == "White") { System.Console.BackgroundColor = ConsoleColor.White; }
                    if (BColor == "Gray") { System.Console.BackgroundColor = ConsoleColor.Gray; }
                    if (BColor == "Black") { System.Console.BackgroundColor = ConsoleColor.Black; }
                    if (BColor == "Blue") { System.Console.BackgroundColor = ConsoleColor.Blue; }
                    if (BColor == "Cyan") { System.Console.BackgroundColor = ConsoleColor.Cyan; }
                    if (BColor == "Green") { System.Console.BackgroundColor = ConsoleColor.Green; }
                    if (BColor == "Red") { System.Console.BackgroundColor = ConsoleColor.Red; }
                    if (BColor == "Yellow") { System.Console.BackgroundColor = ConsoleColor.Yellow; }
                    if (BColor == "Magenta") { System.Console.BackgroundColor = ConsoleColor.Magenta; }
                    if (BColor == "DarkGray") { System.Console.BackgroundColor = ConsoleColor.DarkGray; }
                    if (BColor == "DarkBlue") { System.Console.BackgroundColor = ConsoleColor.DarkBlue; }
                    if (BColor == "DarkCyan") { System.Console.BackgroundColor = ConsoleColor.DarkCyan; }
                    if (BColor == "DarkGreen") { System.Console.BackgroundColor = ConsoleColor.DarkGreen; }
                    if (BColor == "DarkRed") { System.Console.BackgroundColor = ConsoleColor.DarkRed; }
                    if (BColor == "DarkMagenta") { System.Console.BackgroundColor = ConsoleColor.DarkMagenta; }
                    if (BColor == "DarkYellow") { System.Console.BackgroundColor = ConsoleColor.DarkYellow; }
                }
                System.Console.Write(Text);
            }
        }
        public static void ResetConsoleColor()
        {
            System.Console.ResetColor();
        }
    }
    public class TBP
    {
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint timeBeginPeriod(uint uPeriod);
    }
    public class TEP
    {
        [DllImport("winmm.dll", SetLastError = true)]
        public static extern uint timeEndPeriod(uint uPeriod);
    }
    // Timing Functions ~
    public class TI
    {
        public static void Hold(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
    }
    // VK Functions ~
    public class VK
    {
        public static void VK_Down(string inKey)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyDown(key);
        }
        public static void VK_Up(string inKey)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyUp(key);
        }
        public static void VK_HoldUntil(string inKey, int time)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyDown(key);
            TI.Hold(time);
            KeyboardSend.KeyUp(key);
        }
        public static void VK_HoldTurbo(string inKey, int time)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            Global.tmpVKMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
            while ((Global.SW.ElapsedMilliseconds - Global.tmpVKMillis) < time)
            {
                KeyboardSend.KeyDown(key);
                TI.Hold(1);
                KeyboardSend.KeyUp(key);
                TI.Hold(1);
            }
        }
        public static void VK_HoldUntilDelay(string inKey, int time)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyDown(key);
            TI.Hold(time);
            KeyboardSend.KeyUp(key);
            TI.Hold(25);
        }
    }
    // GUI Functions ~
    public class GUI
    {
        public static void FormatConsole()
        {
            System.Console.Title = "KasTAS";
            System.Console.SetWindowSize(Global.windowW, Global.windowH);
            System.Console.Clear();
            System.Console.SetBufferSize(Global.windowW, Global.windowH);
        }
        public static void FormatConsoleExpBuffer()
        {
            System.Console.SetWindowSize(Global.windowW, Global.windowH);
            System.Console.SetBufferSize(Global.windowW, 2500);
        }
        public static void DrawSpacer()
        {
            WRT.WRLine(" ", "White", "Black");
        }
        public static void DrawUpperBarSpacer()
        {
            WRT.WRLine("▀".PadRight(Console.BufferWidth - 1, Global.charUpperBar), "DarkGray", "Black");
        }
        public static void DrawLowerBarSpacer()
        {
            WRT.WRLine("▄".PadRight(Console.BufferWidth - 1, Global.charBar), "DarkGray", "Black");
        }
        public static void DrawClearBuffer()
        {
            Console.SetCursorPosition(0, Global.windowH - 2);
            WRT.WRLine(" ".PadRight(Console.BufferWidth - 1, Global.charSpace), "White", "Black");
        }
        public static void DrawTitle()
        {
            System.Console.Clear();
            WRT.WR(" ♦ Kas", "White", "DarkBlue");
            WRT.WR("TAS", "Cyan", "DarkBlue");
            WRT.WR(" ", "White", "DarkBlue");
            WRT.WR(Global.version, "Gray", "DarkBlue");
            WRT.WR(" 2022(C)", "DarkGray", "DarkBlue");
            WRT.WR(" ", "White", "DarkBlue");
            WRT.WR("          ", "DarkCyan", "DarkBlue");
            WRT.WR("                  ", "White", "DarkBlue");
            if (Global.showTwitchTitle == "true")
            {
                WRT.WR("www.twitch.tv/karstskarn", "DarkMagenta", "DarkBlue");
            }
            else
            {
                WRT.WR("                        ", "DarkMagenta", "DarkBlue");
            }
            (int curLeft, int curTop) = Console.GetCursorPosition();
            WRT.WRLine(" ".PadRight(Console.BufferWidth - curLeft - 1, Global.charSpace), "White", "DarkBlue");
            GUI.DrawUpperBarSpacer();
        }
        public static void DrawMainMenu()
        {
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("C", "White", "Black");
            WRT.WRLine("onfigure the keys.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("T", "White", "Black");
            WRT.WRLine("est the keys.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("3", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("R", "White", "Black");
            WRT.WRLine("ead a KSOAutoScript file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("4", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "White", "Black");
            WRT.WRLine("xecute a KSOAutoScript file.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Current key mode is [", "White", "Black");
            WRT.WR(Global.SysTarget, "Cyan", "Black");
            WRT.WRLine("]", "White", "Black");
            WRT.WR(" Extended key mode is [", "White", "Black");
            if (Global.flagExtMode == "true")
            {
                WRT.WR("ENABLED", "Green", "Black");
            }
            else
            {
                WRT.WR("DISABLED", "Cyan", "Black");
            }
            WRT.WRLine("]", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("5", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("M", "White", "Black");
            WRT.WRLine("anage output modes.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" You can write directly any", "White", "Black");
            WRT.WR(" <.kas> ", "Cyan", "Black");
            WRT.WRLine("file in the prompt", "White", "Black");
            WRT.WRLine(" and will be executed regardless of the menu you are in.", "White", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Press ", "DarkGray", "Black");
            WRT.WR("CTRL + C", "White", "Black");
            WRT.WR(" or ", "DarkGray", "Black");
            WRT.WR("CTRL + BREAK", "White", "Black");
            WRT.WRLine(" to force the executable shutdown.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawCreditHint()
        {
            Console.SetCursorPosition(0, Global.windowH - 4);
            WRT.WR(" Type $Credit for use info.", "DarkGray", "Black");
        }
        public static void DrawSubtitle(string title)
        {
            WRT.WR(" ", "White", "Black");
            WRT.WR("♦", "Black", "White");
            WRT.WRLine(" " + title, "White", "Black");
        }
        public static void DrawReadMenu()
        {
            GUI.DrawTitle();
            GUI.DrawSubtitle("KSOAutoScript Visualizer");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can read a KSOAutoScript file and check its syntax", "DarkGray", "Black");
            WRT.WRLine(" integrity.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("L", "White", "Black");
            WRT.WRLine("oad a file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawExecuteMenu()
        {
            GUI.DrawTitle();
            GUI.DrawSubtitle("KSOAutoScript Execution");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can execute a KSOAutoScript.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("L", "White", "Black");
            WRT.WRLine("oad a file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawTestMenu()
        {
            GUI.FormatConsole();
            GUI.DrawTitle();
            GUI.DrawSubtitle("Virtual Keyboard Keys Test");
            GUI.DrawSpacer();
            WRT.WRLine(" This function will test the VKeys.", "DarkGray", "Black");
            WRT.WRLine(" Make sure you got your Software/Game window active.", "DarkGray", "Black");
            WRT.WR(" You will have a delay of ", "DarkGray", "Black"); ;
            WRT.WR(Convert.ToString(Global.testReadyTime), "White", "Black"); ;
            WRT.WRLine(" ms before the test actually starts.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" Select the test type: ", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black"); ;
            WRT.WR("1", "White", "Black"); ;
            WRT.WR(". ", "DarkGray", "Black"); ;
            WRT.WR("I", "White", "Black"); ;
            WRT.WRLine("ncremental.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black"); ;
            WRT.WR("2", "White", "Black"); ;
            WRT.WR(". ", "DarkGray", "Black"); ;
            WRT.WR("C", "White", "Black"); ;
            WRT.WRLine("ontinous.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black"); ;
            WRT.WR("3", "White", "Black"); ;
            WRT.WR(". ", "DarkGray", "Black"); ;
            WRT.WR("F", "White", "Black"); ;
            WRT.WRLine("rame.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black"); ;
            WRT.WR("4", "White", "Black"); ;
            WRT.WR(". ", "DarkGray", "Black"); ;
            WRT.WR("H", "White", "Black"); ;
            WRT.WRLine("old push.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black"); ;
            WRT.WR("5", "White", "Black"); ;
            WRT.WR(". ", "DarkGray", "Black"); ;
            WRT.WR("E", "Blue", "Black"); ;
            WRT.WRLine("xit test mode.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" Note: All tests are performed with the GB buttons only.", "White", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawConfigMenu()
        {
            GUI.FormatConsole();
            GUI.DrawTitle();
            GUI.DrawSubtitle("Keyboard Keys Configuration");
            GUI.DrawSpacer();
            WRT.WR(" Each Key will be held for ", "DarkGray", "Black");
            WRT.WR(Global.configHoldTime.ToString(), "White", "Black");
            WRT.WR(" ms.", "White", "Black");
            WRT.WR(" You will have ", "DarkGray", "Black");
            WRT.WR(Global.configReadyTime.ToString(), "White", "Black");
            WRT.WR(" ms", "White", "Black");
            WRT.WR(" before every keypress. ", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Select the keys to be configured one by one: ", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("0", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("U", "White", "Black");
            WRT.WR("P.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_UP + "}", "Green", "Black");
            WRT.WR("                   ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("6", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("A", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_A + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("D", "White", "Black");
            WRT.WR("OWN.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_DO + "}", "Green", "Black");
            WRT.WR("                 ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("7", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("B", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_B + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("L", "White", "Black");
            WRT.WR("EFT.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_LE + "}", "Green", "Black");
            WRT.WR("                 ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("8", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("X", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_X + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("3", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("R", "White", "Black");
            WRT.WR("IGHT.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_RI + "}", "Green", "Black");
            WRT.WR("                ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("9", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("Y", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_Y + "}", "Green", "Black");
            //GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("4", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("ST", "White", "Black");
            WRT.WR("ART.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_ST + "}", "Green", "Black");
            WRT.WR("                ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("11", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("BL", "White", "Black");
            WRT.WR(" - Button Left.", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_BL + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("5", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("SE", "White", "Black");
            WRT.WR("LECT.", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_SE + "}", "Green", "Black");
            WRT.WR("               ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("12", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("BR", "White", "Black");
            WRT.WR(" - Button Right.", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_BR + "}", "Green", "Black");
            // Extra Keys
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("13", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E0", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_E0 + "}", "Green", "Black");
            WRT.WR("                  ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("18", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E5", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_E5 + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("14", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E1", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_E1 + "}", "Green", "Black");
            WRT.WR("                  ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("19", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E6", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_E6 + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("15", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E2", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_E2 + "}", "Green", "Black");
            WRT.WR("                  ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("20", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E7", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_E7 + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("16", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E3", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_E3 + "}", "Green", "Black");
            WRT.WR("                  ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("21", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E8", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_E8 + "}", "Green", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("17", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E4", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WR(" {" + Global.VB_E4 + "}", "Green", "Black");
            WRT.WR("                  ", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("22", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E9", "White", "Black");
            WRT.WR(".", "DarkGray", "Black");
            WRT.WRLine(" {" + Global.VB_E9 + "}", "Green", "Black");
            //GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("10", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit Configuration Mode.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawTargetMenu()
        {
            GUI.DrawTitle();
            GUI.DrawSubtitle("Output modes configuration");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can switch between the keyboard output modes.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" Basic buttons output mode.", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("G", "White", "Black");
            WRT.WRLine("B.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("S", "White", "Black");
            WRT.WRLine("NES.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" Extended keys mode.", "White", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("3", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("T", "White", "Black");
            WRT.WRLine("oggle extended mode.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("4", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
    }
    // Execution Functions ~
    public class EXF
    {
        public static void LineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
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
            if (Directory.Exists("Scripts") == false)
            {
                Directory.CreateDirectory("Scripts");
            }
            Global.tmpFileExists = File.Exists(Global.cfgFile);
            if (Global.tmpFileExists == false) 
            { 
                Global.tmpUserInput = "NULL";
                System.Console.Clear();
                WRT.WRLine(" Error: The file <kascfg.ini> is missing or in wrong format.", "Red", "Black");
                WRT.WRLine(" If you continue beyond this point the preset values will be loaded.", "Red", "Black");
                WRT.WRLine(" Press [ENTER] to continue.", "Red", "Black");
                System.Console.ReadLine();
                return; 
            }
            int tmpFileSize = File.ReadLines(Global.cfgFile).Count();
            if (tmpFileSize == Global.cfgTotalLines)
            {
                for (int ii = 0; ii <= (Global.cfgTotalLines - 1); ii++)
                {
                    int skip = 0;
                    if (ii == 0) { skip = 0; } else { skip = (ii); }
                    int take = 1;
                    string tmpFileData = File.ReadLines(Global.cfgFile).Skip(skip).Take(take).First();
                    if (ii == 0)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.lastFileLoaded = tmpSubString;
                    }
                    if (ii == 1)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.configHoldTime = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 2)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.configReadyTime = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 3)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.testReadyTime = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 4)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.testIncrementalStartTime = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 5)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.testIncrementalReduction = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 6)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.testContinousTime = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 7)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.testFrameTime = Convert.ToDouble(tmpSubString);
                    }
                    if (ii == 8)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.SysTarget = tmpSubString;
                    }
                    if (ii == 9)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.timeResolution = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 10)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.tmpReadMaxPause = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 11)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.showTwitchTitle = tmpSubString;
                    }
                }
            }
            else
            {
                System.Console.Clear();
                WRT.WRLine(" Error: The file <kascfg.ini> is missing or in wrong format.", "Red", "Black");
                WRT.WRLine(" If you continue beyond this point the preset values will be loaded.", "Red", "Black");
                WRT.WRLine(" Press [ENTER] to continue.", "Red", "Black");
                System.Console.ReadLine();
                return;
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
            04 BUTTON SELEC = Z
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
            Global.tmpFileExists = File.Exists(Global.cfgKeysFile);
            if (Global.tmpFileExists == false)
            {
                Global.tmpUserInput = "NULL";
                System.Console.Clear();
                WRT.WRLine(" Error: The file <keydata.ini> is missing or in wrong format.", "Red", "Black");
                WRT.WRLine(" If you continue beyond this point the preset values will be loaded.", "Red", "Black");
                WRT.WRLine(" Press [ENTER] to continue.", "Red", "Black");
                System.Console.ReadLine();
                return;
            }
            int tmpFileSize = File.ReadLines(Global.cfgKeysFile).Count();
            if (tmpFileSize == Global.cfgKeyTotalLines)
            {
                for (int ii = 0; ii <= (Global.cfgKeyTotalLines - 1); ii++)
                {
                    int skip = 0;
                    if (ii == 0) { skip = 0; } else { skip = (ii); }
                    int take = 1;
                    string tmpFileData = File.ReadLines(Global.cfgKeysFile).Skip(skip).Take(take).First();
                    if (ii == 0)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.SysTarget = tmpSubString;
                    }
                    if (ii == 1)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.flagExtMode = tmpSubString;
                    }
                    if (ii == 2)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_A = tmpSubString;
                    }
                    if (ii == 3)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_B = tmpSubString;
                    }
                    if (ii == 4)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_ST = tmpSubString;
                    }
                    if (ii == 5)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_SE = tmpSubString;
                    }
                    if (ii == 6)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_UP = tmpSubString;
                    }
                    if (ii == 7)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_DO = tmpSubString;
                    }
                    if (ii == 8)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_LE = tmpSubString;
                    }
                    if (ii == 9)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_RI = tmpSubString;
                    }
                    if (ii == 10)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_X = tmpSubString;
                    }
                    if (ii == 11)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_Y = tmpSubString;
                    }
                    if (ii == 12)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_BL = tmpSubString;
                    }
                    if (ii == 13)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_BR = tmpSubString;
                    }
                    if (ii == 14)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E0 = tmpSubString;
                    }
                    if (ii == 15)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E1 = tmpSubString;
                    }
                    if (ii == 16)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E2 = tmpSubString;
                    }
                    if (ii == 17)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E3 = tmpSubString;
                    }
                    if (ii == 18)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E4 = tmpSubString;
                    }
                    if (ii == 19)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E5 = tmpSubString;
                    }
                    if (ii == 20)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E6 = tmpSubString;
                    }
                    if (ii == 21)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E7 = tmpSubString;
                    }
                    if (ii == 22)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E8 = tmpSubString;
                    }
                    if (ii == 23)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_E9 = tmpSubString;
                    }
                }
            }
            else
            {
                System.Console.Clear();
                WRT.WRLine(" Error: The file <keydata.ini> is missing or in wrong format.", "Red", "Black");
                WRT.WRLine(" If you continue beyond this point the preset values will be loaded.", "Red", "Black");
                WRT.WRLine(" Press [ENTER] to continue.", "Red", "Black");
                System.Console.ReadLine();
                return;
            }
        }
        public static void LoadLastHalt()
        {
            if (Global.lastFileLoaded != "NULL")
            {
                if (Global.flagFirstRun == true)
                {
                    GUI.DrawTitle();
                    GUI.DrawSpacer();
                    WRT.WR(" Last executed file was <", "White", "Black");
                    WRT.WR(Global.lastFileLoaded, "Cyan", "Black");
                    WRT.WRLine(">", "White", "Black");
                    Global.tmpUserInput = "NULL";
                    WRT.WRLine(" Do you want to execute it again?", "White", "Black");
                    WRT.WR(" Write ", "DarkGray", "Black");
                    WRT.WR("[N/n]", "White", "Black");
                    WRT.WR(" and then press ", "DarkGray", "Black");
                    WRT.WR("[ENTER]", "White", "Black");
                    WRT.WR(" or ", "DarkGray", "Black");
                    WRT.WR("leave empty", "White", "Black");
                    WRT.WRLine(" to load it again. ", "DarkGray", "Black");
                    GUI.DrawSpacer();
                    Global.tmpUserInput = EXF.StaticPrompt();
                    if ((Global.tmpUserInput != "N") && (Global.tmpUserInput != "n"))
                    {
                        Global.flagLoadLast = true;
                        Global.KSOAutoScriptExecuteMode = true;
                        Global.flagFirstRun = false;
                        EXF.ExecuteKAS();
                    }
                    else 
                    {
                        Global.tmpWritePad = "00 LAST FILE    = NULL";
                        EXF.WriteToCfg(0, Global.tmpWritePad);
                        Global.flagFirstRun = false;
                        Global.flagLoadLast = false; 
                    }
                }
            }
        }
        public static void FirstInitHalt()
        {
            if (Global.flagFirstInit == "true")
            {
                GUI.DrawTitle();
                WRT.WRLine(" ♦ Disclaimer".PadRight(Global.windowW - 1, Global.charSpace), "Black", "DarkGray");
                WRT.WRLine(" It seems this is the first time you run this software.", "White", "Black");
                WRT.WRLine(" Due an incompatibility/malfunction issue with Windows Forms most of", "DarkGray", "Black");
                WRT.WR(" times when you press ", "DarkGray", "Black");
                WRT.WR("[ENTER]", "White", "Black");
                WRT.WR(" this window", "DarkGray", "Black");
                WRT.WRLine(" will make a Windows 'Ding' sound.", "White", "Black");
                WRT.WRLine(" This won't affect the performance of the software in any way.", "White", "Black");
                WRT.WRLine(" And there's no easy way to fix it without messing too much with the user system", "DarkGray", "Black");
                WRT.WRLine(" or without being too invasive in an attempt to fix it.", "DarkGray", "Black");
                WRT.WRLine(" And despite being a Console Executable it needs Windows Forms for some", "DarkGray", "Black");
                WRT.WRLine(" essential specific functions.", "DarkGray", "Black");
                GUI.DrawSpacer();
                WRT.WRLine(" If this is a problem for you the easiest way to get rid of the sound", "DarkGray", "Black");
                WRT.WR(" is it to", "DarkGray", "Black");
                WRT.WR(" mute the application ", "White", "Black");
                WRT.WRLine("'Console Window Host' (conhost.exe) ", "Cyan", "Black");
                WRT.WRLine(" in the Windows Sound Mixer.", "White", "Black");
                WRT.WR(" Usually located at the", "DarkGray", "Black");
                WRT.WRLine(" right bottom corner of your desktop.", "White", "Black");
                GUI.DrawSpacer();
                WRT.WRLine(" Sorry for the inconvenience, and thank you for using this software!", "Green", "Black");
                Global.tmpUserInput = EXF.NoReturnStaticPrompt();
                GUI.DrawTitle();
                WRT.WRLine("                    ░░  ░░          ░░   ", "DarkGray", "Black");
                WRT.WR("                    ░░    ░░        ░░    ", "DarkGray", "Black");
                WRT.WR("100% ", "Cyan", "Black");
                WRT.WR("IT", "Green", "Black");
                WRT.WR("ALI", "White", "Black");
                WRT.WR("AN", "Red", "Black");
                WRT.WR(" SPAGUETTI ", "Yellow", "Black");
                WRT.WRLine("C0DE", "White", "Black");
                WRT.WR("                  ░░        ░░    ░░      ", "DarkGray", "Black");
                WRT.WRLine("FRESH-MADE!", "White", "Black");
                WRT.WR("                            ░░  ░░        ", "DarkGray", "Black");
                WRT.WR("AL DENTE! ", "White", "Black");
                WRT.WRLine("OwO", "Magenta", "Black");
                WRT.WR("                          ░░              ", "DarkGray", "Black");
                WRT.WR("+999.999.999.999 ", "Green", "Black");
                WRT.WR("Social Credits! ", "White", "Black");
                WRT.WR("                                                 ", "DarkGray", "Black");
                WRT.WRLine("{•} < Windows Ding!", "DarkGray", "Black");
                WRT.WRLine("        ▄█▀▀█▄        ▄█▀▀▀▀▀▀▀▀█▄          ", "Yellow", "Black");
                WRT.WRLine("      ▄█▀    ▀█▄    ▄█▀          ▀█▄                        ", "Yellow", "Black");
                WRT.WRLine("    ▄█▀   ▄██▄ ▀█▄▄█▀   ▄█▀▀▀▀█▄   ▀█▄              ▄██▄    ", "Yellow", "Black");
                WRT.WRLine("  ▄█▀   ▄█▀  ▄████▀   ▄█▀      ▀█▄   ▀██▀▀█▄      ▄█▀  ▀█▄  ", "Yellow", "Black");
                WRT.WRLine("  ██  ▄█▀▄██▄███▀   ▄█▀   ▄██▄   ▀█▄   ▀█▄ ▀█▄  ▄█▀      ▀█▄", "Yellow", "Black");
                WRT.WRLine("▄█▀ ▄█▀ █▀ ▄██▀   ▄█▀   ▄█▀  ▀█▄   ▀█▄   ▀█▄ ▀██▀         █▀", "Yellow", "Black");
                WRT.WR("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", "Gray", "White");
                WRT.WR("    ", "White", "Black");
                WRT.WRLine("▄█▀", "Yellow", "Black");
                WRT.WR(" ▀██████████████████████████████████████    ▒▓████▀   ", "White", "Black");
                WRT.WRLine("▄█▀ ", "Yellow", "Black");
                WRT.WR("  ██████████████████████████████████████    ▒▓████  ", "White", "Black");
                WRT.WRLine("▄█▀ ", "Yellow", "Black");
                WRT.WR("   ▀██████████████████████████████████     ▓████▀   ", "White", "Black");
                WRT.WRLine("▀█▄ ", "Yellow", "Black");
                WRT.WR("     ▀██████████████████████████████    ██████▀       ", "White", "Black");
                WRT.WRLine("▀█▄ ", "Yellow", "Black");
                WRT.WRLine("       ▀██████████████████████████  ████████▀      ", "White", "Black");
                WRT.WR("            ", "White", "Black");
                WRT.WR("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", "Gray", "White");
                WRT.WR(" Press any key to continue.", "White", "Black");
                int graphSeq = 0;
                int musSeq = 5;
                while (true)
                {
                    DateTime beginWait = DateTime.Now;
                    if (!Console.KeyAvailable)
                    {
                        Console.SetCursorPosition(0, 2);
                        Console.CursorVisible = false;
                        if (graphSeq == 0)
                        {
                            WRT.WRLine("                    ░░  ░░          ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░    ░░        ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                  ░░        ░░    ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                            ░░  ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                          ░░         ", "DarkGray", "Black");
                        }
                        if (graphSeq == 1)
                        {
                            WRT.WRLine("                   ░░    ░░         ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░    ░░       ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                  ░░       ░░      ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                           ░░    ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░             ", "DarkGray", "Black");
                        }
                        if (graphSeq == 2)
                        {
                            WRT.WRLine("                   ░░     ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                     ░░    ░░       ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                   ░░     ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                          ░░              ", "DarkGray", "Black");
                        }
                        if (graphSeq == 3)
                        {
                            WRT.WRLine("                   ░░      ░░       ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                     ░░   ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░   ░░         ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                        ░░         ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░               ", "DarkGray", "Black");
                        }
                        if (graphSeq == 4)
                        {
                            WRT.WRLine("                    ░░      ░░       ░░  ", "DarkGray", "Black");
                            WRT.WRLine("                     ░░    ░░       ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░    ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                        ░░                ", "DarkGray", "Black");
                        }
                        if (graphSeq == 5)
                        {
                            WRT.WRLine("                   ░░      ░░       ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                     ░░   ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░   ░░         ░░   ", "DarkGray", "Black");
                            WRT.WRLine("                        ░░         ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░               ", "DarkGray", "Black");
                        }
                        if (graphSeq == 6)
                        {
                            WRT.WRLine("                   ░░     ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                     ░░    ░░       ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                   ░░     ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                          ░░              ", "DarkGray", "Black");
                        }
                        if (graphSeq == 7)
                        {
                            WRT.WRLine("                   ░░    ░░         ░░    ", "DarkGray", "Black");
                            WRT.WRLine("                    ░░    ░░       ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                  ░░       ░░      ░░     ", "DarkGray", "Black");
                            WRT.WRLine("                           ░░    ░░      ", "DarkGray", "Black");
                            WRT.WRLine("                         ░░               ", "DarkGray", "Black");
                        }
                        graphSeq++;
                        if (graphSeq == 8)
                        { graphSeq = 0; }
                        // M1
                        if (musSeq == 5) { Console.Beep(587, 220); }
                        if (musSeq == 6) { Console.Beep(587, 220); }
                        if (musSeq == 7) { Console.Beep(659, 220); }
                        if (musSeq == 8) { Console.Beep(587, 220); }
                        if (musSeq == 9) { Console.Beep(523, 220); }
                        if (musSeq == 10) { Console.Beep(587, 220); }
                        // M2
                        if (musSeq == 11) { Console.Beep(392, 220); }
                        if (musSeq == 12) { TI.Hold(220); }
                        if (musSeq == 13) { Console.Beep(523, 220); }
                        if (musSeq == 14) { Console.Beep(466, 220); }
                        if (musSeq == 15) { Console.Beep(392, 220); }
                        if (musSeq == 16) { TI.Hold(220); }
                        if (musSeq == 17) { Console.Beep(466, 220); }
                        if (musSeq == 18) { TI.Hold(220); }
                        // M3
                        if (musSeq == 19) { Console.Beep(523, 220); }
                        if (musSeq == 20) { TI.Hold(220); }
                        if (musSeq == 21) { Console.Beep(523, 220); }
                        if (musSeq == 22) { Console.Beep(523, 220); }
                        if (musSeq == 23) { Console.Beep(587, 220); }
                        if (musSeq == 24) { Console.Beep(523, 220); }
                        if (musSeq == 25) { Console.Beep(466, 220); }
                        if (musSeq == 26) { Console.Beep(523, 220); }
                        // M4
                        if (musSeq == 27) { Console.Beep(587, 220); }
                        if (musSeq == 28) { TI.Hold(220); }
                        if (musSeq == 29) { Console.Beep(466, 220); }
                        if (musSeq == 30) { Console.Beep(392, 220); }
                        if (musSeq == 31) { Console.Beep(392, 220); }
                        if (musSeq == 32) { Console.Beep(466, 220); }
                        if (musSeq == 33) { Console.Beep(392, 220); }
                        if (musSeq == 34) { Console.Beep(466, 220); }
                        // M5
                        if (musSeq == 35) { Console.Beep(392, 220); }
                        if (musSeq == 36) { TI.Hold(220); }
                        if (musSeq == 37) { Console.Beep(587, 220); }
                        if (musSeq == 38) { Console.Beep(587, 220); }
                        if (musSeq == 39) { Console.Beep(622, 220); }
                        if (musSeq == 40) { Console.Beep(587, 220); }
                        if (musSeq == 41) { Console.Beep(523, 220); }
                        if (musSeq == 42) { Console.Beep(466, 220); }
                        // M6
                        if (musSeq == 43) { Console.Beep(587, 220); }
                        if (musSeq == 44) { Console.Beep(523, 220); }
                        if (musSeq == 45) { Console.Beep(523, 220); }
                        if (musSeq == 46) { Console.Beep(587, 220); }
                        if (musSeq == 47) { Console.Beep(392, 220); }
                        if (musSeq == 48) { TI.Hold(220); }
                        if (musSeq == 49) { Console.Beep(392, 220); }
                        if (musSeq == 50) { Console.Beep(466, 220); }
                        // M7
                        if (musSeq == 51) { Console.Beep(440, 220); }
                        if (musSeq == 52) { TI.Hold(220); }
                        if (musSeq == 53) { Console.Beep(440, 220); }
                        if (musSeq == 54) { Console.Beep(440, 220); }
                        if (musSeq == 55) { Console.Beep(587, 220); }
                        if (musSeq == 56) { TI.Hold(220); }
                        if (musSeq == 57) { Console.Beep(587, 220); }
                        if (musSeq == 58) { TI.Hold(220); }
                        // M8
                        if (musSeq == 59) { Console.Beep(466, 220); }
                        if (musSeq == 60) { TI.Hold(220); }
                        if (musSeq == 61) { TI.Hold(220); }
                        if (musSeq == 62) { TI.Hold(220); }
                        musSeq++;
                        if (musSeq == 63)
                        { musSeq = 5; }
                    }
                    else
                    {
                        Console.CursorVisible = true;
                        return;
                    }
                }
            }
        }
        public static void WriteToCfg(int line, string value)
        {
            char padChar = '0';
            value = value.PadLeft(2, padChar);
            line += 1;
            if (line > Global.cfgTotalLines) { line = Global.cfgTotalLines; }
            if (File.Exists(Global.cfgFile) == true)
            {
                EXF.LineChanger(value, Global.cfgFile, line);
            }
            else
            {
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" Fatal Error: " + Global.cfgFile + " doesn't exists in the executable directory.", "Red", "Black");
                Console.ReadKey();
            }
        }
        public static void WriteToKeyCfg(int line, string value)
        {
            char padChar = '0';
            value = value.PadLeft(2, padChar);
            line += 1;
            if (line > Global.cfgKeyTotalLines) { line = Global.cfgKeyTotalLines; }
            if (File.Exists(Global.cfgKeysFile) == true)
            {
                EXF.LineChanger(value, Global.cfgKeysFile, line);
            }
            else
            {
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" Fatal Error: " + Global.cfgKeysFile + " doesn't exists in the executable directory.", "Red", "Black");
                Console.ReadKey();
            }
        }
        public static void DebugHalt()
        {
            WRT.WRLine(" ", "White", "Black");
            WRT.WRLine(" [Debug] Execution Halted.", "Yellow", "Black");
            System.Console.ReadLine();
        }
        public static void ReadHalt()
        {
            WRT.WR(" ▬ ", "Cyan", "Black");
            WRT.WRLine("End of file.", "White", "Black");
        }
        public static void EndExecutionHalt()
        {
            WRT.WR(" ▬ ", "Cyan", "Black");
            WRT.WRLine(" End of script execution.", "White", "Black");
        }
        public static void ExecutionHalt()
        {
            Console.SetCursorPosition(0, Global.windowH - 3);
            GUI.DrawLowerBarSpacer();
            WRT.WR(" ▬ ", "Cyan", "Black");
            WRT.WR("Press ", "DarkGray", "Black");
            WRT.WR("[ENTER]", "White", "Black");
            WRT.WR(" to execute the script.", "DarkGray", "Black");
            System.Console.ReadLine();
        }
        public static void AltInput(string altIn)
        {
            if ((altIn == "$Credit") || (altIn == "$credit"))
            {
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" KasTAS 1.0.98 (C) 2022 by Owain Horton / Karst Skarn [Owain#3593]", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" is licensed under CC BY-NC-SA International 4.0", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" (KasTAS.exe - CC Attribution-NonCommercial-ShareAlike 4.0 International)", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" To view a copy of this license.", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" Visit http://creativecommons.org/licenses/by-nc-sa/4.0/", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" In case of public use (Stream, video recording...)", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" The attribution and mention is strongly encouraged.", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" https://www.twitch.tv/karstskarn", "Green", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" Thanks!.", "Green", "Black");
                Console.ReadKey();
                Global.tmpUserInput = "NULL";
                return;
            }
        }
        public static string Prompt()
        {
            WRT.WR(" ► ", "Cyan", "Black");
            WRT.WR("Choose an option [", "DarkGray", "Black");
            WRT.WR("N", "White", "Black");
            WRT.WR("|", "DarkGray", "Black");
            WRT.WR("C", "White", "Black");
            WRT.WR("]:", "DarkGray", "Black");
            WRT.WR(" ", "Cyan", "Black");
            string userInput = Console.ReadLine();
            if (userInput.Length >= 5)
            {
                Global.tmpUserInput = userInput.Substring((userInput.Length - 4), 4);
                if (Global.tmpUserInput == ".kas")
                {
                    Global.flagLoadLast = true;
                    Global.lastFileLoaded = userInput;
                    Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                    EXF.WriteToCfg(0, Global.tmpWritePad);
                    Global.KSOAutoScriptExecuteMode = true;
                    EXF.ExecuteKAS();
                }
            }
            return userInput;
        }
        public static string StaticPrompt()
        {
            GUI.DrawClearBuffer();
            Console.SetCursorPosition(0, Global.windowH - 3);
            GUI.DrawLowerBarSpacer();
            WRT.WR(" ► ", "Cyan", "Black");
            WRT.WR("Choose an option [", "DarkGray", "Black");
            WRT.WR("N", "White", "Black");
            WRT.WR("|", "DarkGray", "Black");
            WRT.WR("C", "White", "Black");
            WRT.WR("]:", "DarkGray", "Black");
            WRT.WR(" ", "Cyan", "Black");
            string userInput = Console.ReadLine();
            if (userInput.Length >= 5)
            {
                Global.tmpUserInput = userInput.Substring((userInput.Length - 4), 4);
                if (Global.tmpUserInput == ".kas")
                {
                    Global.flagLoadLast = true;
                    Global.lastFileLoaded = userInput;
                    Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                    EXF.WriteToCfg(0, Global.tmpWritePad);
                    Global.KSOAutoScriptExecuteMode = true;
                    EXF.ExecuteKAS();
                }
            }
            return userInput;
        }
        public static string NoReturnStaticPrompt()
        {
            GUI.DrawClearBuffer();
            Console.SetCursorPosition(0, Global.windowH - 3);
            GUI.DrawLowerBarSpacer();
            WRT.WR(" ► ", "Cyan", "Black");
            WRT.WR("Press ", "DarkGray", "Black");
            WRT.WR("[ENTER]", "White", "Black");
            WRT.WR(" to continue:", "DarkGray", "Black");
            WRT.WR(" ", "Cyan", "Black");
            string userInput = "NULL";
            System.Console.ReadLine();
            return userInput;
        }
        public static string FileStaticPrompt()
        {
            GUI.DrawClearBuffer();
            Console.SetCursorPosition(0, Global.windowH - 3);
            GUI.DrawLowerBarSpacer();
            WRT.WR(" ► ", "Cyan", "Black");
            WRT.WR("Input ", "DarkGray", "Black");
            WRT.WR("[FILENAME]", "White", "Black");
            WRT.WR(" ", "DarkGray", "Black");
            WRT.WR("(Scripts/", "White", "Black");
            WRT.WR("...", "Cyan", "Black");
            WRT.WR(")", "White", "Black");
            WRT.WR(":", "DarkGray", "Black");
            WRT.WR(" ", "Cyan", "Black");
            string userInput = Console.ReadLine();
            return userInput;
        }
        public static void ChangeTarget()
        {
            GUI.DrawTargetMenu();
            if (Global.SysConfig == true)
            {
                Global.tmpUserInput = EXF.StaticPrompt();
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "G") || (Global.tmpUserInput == "g"))
                {
                    Global.tmpWritePad = "00 SYS TARGET   = GB";
                    EXF.WriteToKeyCfg(0, Global.tmpWritePad);
                    return;
                }
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "S") || (Global.tmpUserInput == "s"))
                {
                    Global.tmpWritePad = "00 SYS TARGET   = SNES";
                    EXF.WriteToKeyCfg(0, Global.tmpWritePad);
                    return;
                }
                if ((Global.tmpUserInput == "3") || (Global.tmpUserInput == "T") || (Global.tmpUserInput == "t"))
                {
                    if (Global.flagExtMode == "true")
                    {
                        Global.tmpWritePad = "01 EXTRA MODE   = false";
                        EXF.WriteToKeyCfg(1, Global.tmpWritePad);
                        return;
                    }
                    if (Global.flagExtMode == "false")
                    {
                        Global.tmpWritePad = "01 EXTRA MODE   = true";
                        EXF.WriteToKeyCfg(1, Global.tmpWritePad);
                        return;
                    }
                }
                    if ((Global.tmpUserInput == "4") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
                {
                    Global.SysConfig = false;
                    Global.tmpUserInput = "NULL";
                    return;
                }
            }
        }
        public static void ConfigKeys()
        {
            GUI.DrawConfigMenu();
            while (Global.configKeysMode)
            {
                Global.tmpUserInput = EXF.StaticPrompt();
                if ((Global.tmpUserInput == "0") || (Global.tmpUserInput == "U") || (Global.tmpUserInput == "u"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [UP] (" + Convert.ToString(Global.VB_UP) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_UP);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_UP);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "D") || (Global.tmpUserInput == "d"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [DOWN] (" + Convert.ToString(Global.VB_DO) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_DO);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_DO);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "L") || (Global.tmpUserInput == "l"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [LEFT] (" + Convert.ToString(Global.VB_LE) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_LE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_LE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput == "R") || (Global.tmpUserInput == "r"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [RIGHT] (" + Convert.ToString(Global.VB_RI) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_RI);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_RI);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput == "ST") || (Global.tmpUserInput == "st") || (Global.tmpUserInput == "sT") || (Global.tmpUserInput == "St"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [START] (" + Convert.ToString(Global.VB_ST) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_ST);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_ST);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput == "SE") || (Global.tmpUserInput == "se") || (Global.tmpUserInput == "Se") || (Global.tmpUserInput == "sE"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [SELECT] (" + Convert.ToString(Global.VB_SE) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_SE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_SE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "6") || (Global.tmpUserInput == "A") || (Global.tmpUserInput == "a"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [A] (" + Convert.ToString(Global.VB_A) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_A);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_A);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "7") || (Global.tmpUserInput == "B") || (Global.tmpUserInput == "b"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [B] (" + Convert.ToString(Global.VB_B) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_B);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_B);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "8") || (Global.tmpUserInput == "X") || (Global.tmpUserInput == "x"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [X] (" + Convert.ToString(Global.VB_X) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_X);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_X);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "9") || (Global.tmpUserInput == "Y") || (Global.tmpUserInput == "y"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [Y] (" + Convert.ToString(Global.VB_Y) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_Y);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_Y);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "11") || (Global.tmpUserInput == "BL") || (Global.tmpUserInput == "bl") || (Global.tmpUserInput == "Bl") || (Global.tmpUserInput == "bL"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [BL] (" + Convert.ToString(Global.VB_BL) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_BL);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_BL);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "12") || (Global.tmpUserInput == "BR") || (Global.tmpUserInput == "br") || (Global.tmpUserInput == "Br") || (Global.tmpUserInput == "bR"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [BR] (" + Convert.ToString(Global.VB_BR) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_BR);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_BR);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "13") || (Global.tmpUserInput == "E0") || (Global.tmpUserInput == "e0"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E0] (" + Convert.ToString(Global.VB_E0) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E0);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E0);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "14") || (Global.tmpUserInput == "E1") || (Global.tmpUserInput == "e1"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E1] (" + Convert.ToString(Global.VB_E1) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E1);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E1);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "15") || (Global.tmpUserInput == "E2") || (Global.tmpUserInput == "e2"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E2] (" + Convert.ToString(Global.VB_E2) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E2);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E2);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "16") || (Global.tmpUserInput == "E3") || (Global.tmpUserInput == "e3"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E3] (" + Convert.ToString(Global.VB_E3) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E3);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E3);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "17") || (Global.tmpUserInput == "E4") || (Global.tmpUserInput == "e4"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E4] (" + Convert.ToString(Global.VB_E4) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E4);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E4);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "18") || (Global.tmpUserInput == "E5") || (Global.tmpUserInput == "e5"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E5] (" + Convert.ToString(Global.VB_E5) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E5);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E5);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "19") || (Global.tmpUserInput == "E6") || (Global.tmpUserInput == "e6"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E6] (" + Convert.ToString(Global.VB_E6) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E6);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E6);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "20") || (Global.tmpUserInput == "E7") || (Global.tmpUserInput == "e7"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E7] (" + Convert.ToString(Global.VB_E7) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E7);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E7);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "21") || (Global.tmpUserInput == "E8") || (Global.tmpUserInput == "e8"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E8] (" + Convert.ToString(Global.VB_E8) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E8);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E8);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "22") || (Global.tmpUserInput == "E9") || (Global.tmpUserInput == "e9"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [E9] (" + Convert.ToString(Global.VB_E9) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_E9);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_E9);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "10") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Exiting Key Configuration Mode...", "DarkGray", "Black");
                    Global.configKeysMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                }
                else if ((Global.tmpUserInput == "69") || (Global.tmpUserInput == "BGB") || (Global.tmpUserInput == "bgb") || (Global.tmpUserInput == "Bgb") || (Global.tmpUserInput == "bGb"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [RIGHT] (" + Convert.ToString(Global.VB_RI) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_RI);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_RI);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [LEFT] (" + Convert.ToString(Global.VB_LE) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_LE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_LE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [UP] (" + Convert.ToString(Global.VB_UP) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_UP);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_UP);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [DOWN] (" + Convert.ToString(Global.VB_DO) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_DO);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_DO);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [A] (" + Convert.ToString(Global.VB_A) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_A);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_A);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [B] (" + Convert.ToString(Global.VB_B) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_B);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_B);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [SELECT] (" + Convert.ToString(Global.VB_SE) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_SE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_SE);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Pressing [START] (" + Convert.ToString(Global.VB_ST) + ") in " + Convert.ToString(Global.configReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.configReadyTime);
                    VK.VK_Down(Global.VB_ST);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Pressed!", "Green", "Black");
                    TI.Hold(Global.configHoldTime);
                    VK.VK_Up(Global.VB_ST);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Virtual Key Released!", "Green", "Black");
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" All keys for BGB Emulator should be assigned now!", "White", "Black");
                }
                break;
            }
            EXF.ConfigKeys();
        }
        public static void TestKeys()
        {
            GUI.DrawTestMenu();
            while (Global.testKeysMode)
            {
                Global.tmpUserInput = EXF.StaticPrompt();
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "I"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Performing Incremental test in " + Convert.ToString(Global.testReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.testReadyTime);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Starting Incremental test!", "Green", "Black");
                    int testWorkingTime = Convert.ToInt32(Global.testIncrementalStartTime);
                    for (int ii = 0; ii <= 40; ii++)
                    {
                        VK.VK_Down(Global.VB_A);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_A);
                        VK.VK_Down(Global.VB_B);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_B);
                        VK.VK_Down(Global.VB_ST);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_ST);
                        VK.VK_Down(Global.VB_SE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_SE);
                        VK.VK_Down(Global.VB_RI);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_RI);
                        VK.VK_Down(Global.VB_DO);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_DO);
                        VK.VK_Down(Global.VB_LE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_LE);
                        VK.VK_Down(Global.VB_UP);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_UP);
                        testWorkingTime = testWorkingTime - Global.testIncrementalReduction;
                        if (testWorkingTime <= 30)
                        {
                            testWorkingTime = 15;
                        }
                    }
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Incremental Test did end!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "C"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Performing Continous test in " + Convert.ToString(Global.testReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.testReadyTime);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Starting Continous test!", "Green", "Black");
                    int testWorkingTime = Global.testContinousTime;
                    for (int ii = 0; ii <= 10; ii++)
                    {
                        VK.VK_Down(Global.VB_A);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_A);
                        VK.VK_Down(Global.VB_B);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_B);
                        VK.VK_Down(Global.VB_ST);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_ST);
                        VK.VK_Down(Global.VB_SE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_SE);
                        VK.VK_Down(Global.VB_RI);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_RI);
                        VK.VK_Down(Global.VB_DO);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_DO);
                        VK.VK_Down(Global.VB_LE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_LE);
                        VK.VK_Down(Global.VB_UP);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_UP);
                    }
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Continous Test did end!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput == "F"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Performing Frame test in " + Convert.ToString(Global.testReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.testReadyTime);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Starting Frame test!", "Green", "Black");
                    int testWorkingTime = Convert.ToInt32(Global.testFrameTime);
                    for (int ii = 0; ii <= 50; ii++)
                    {
                        VK.VK_Down(Global.VB_A);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_A);
                        VK.VK_Down(Global.VB_B);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_B);
                        VK.VK_Down(Global.VB_ST);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_ST);
                        VK.VK_Down(Global.VB_SE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_SE);
                        VK.VK_Down(Global.VB_RI);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_RI);
                        VK.VK_Down(Global.VB_DO);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_DO);
                        VK.VK_Down(Global.VB_LE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_LE);
                        VK.VK_Down(Global.VB_UP);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_UP);
                    }
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Frame Test did end!", "White", "Black");
                }
                else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput == "H"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Performing TI.Hold(test in " + Convert.ToString(Global.testReadyTime) + " ms...", "DarkGray", "Black");
                    TI.Hold(Global.testReadyTime);
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Starting TI.Hold(test!", "White", "Black");
                    int testWorkingTime = Global.testContinousTime;
                    for (int ii = 0; ii <= 10; ii++)
                    {
                        VK.VK_Down(Global.VB_A);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_B);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_ST);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_SE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_RI);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_DO);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_LE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Down(Global.VB_UP);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_A);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_B);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_ST);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_SE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_RI);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_DO);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_LE);
                        TI.Hold(testWorkingTime);
                        VK.VK_Up(Global.VB_UP);
                        TI.Hold(testWorkingTime);
                    }
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" TI.Hold(Test did end!", "Green", "Black");
                }
                else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
                {
                    WRT.WRLine(" Exiting Test Mode", "DarkGray", "Black");
                    Global.testKeysMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                }
                else
                {
                    EXF.TestKeys();
                }
            }
        }
        public static void ReadKAS()
        {
            GUI.DrawReadMenu();
            Global.restartArea = false;
            while (Global.KSOAutoScriptReadMode)
            {
                if (Global.restartArea == false)
                {
                    Global.tmpUserInput = EXF.StaticPrompt();
                }
                else
                {
                    EXF.ReadKAS();
                }
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "L") || (Global.tmpUserInput == "l"))
                {
                    Global.tmpUserInput = EXF.FileStaticPrompt();
                    Global.tmpFileExists = File.Exists(Global.scriptsDirectory + Global.tmpUserInput);
                    if (Global.tmpFileExists == false) { Global.flagLoadLast = false; Global.tmpUserInput = "NULL"; return; }
                    FileInfo scriptFileInfo = new FileInfo(Global.scriptsDirectory + Global.tmpUserInput);
                    int tmpFileSize = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Count();
                    long tmpFileWeight = scriptFileInfo.Length;
                    GUI.DrawTitle();
                    GUI.DrawSpacer();
                    WRT.WR(" Filename: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(Global.tmpUserInput), "Cyan", "Black");
                    WRT.WR(" Total Lines: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(tmpFileSize), "Green", "Black");
                    WRT.WR(" File Size: ", "White", "Black");
                    WRT.WR(Convert.ToString(tmpFileWeight), "Green", "Black");
                    WRT.WRLine(" Bytes", "DarkGray", "Black");
                    GUI.DrawSpacer();
                    GUI.FormatConsoleExpBuffer();
                    //                   D        H        | O  V                         | A T     "
                    Global.tmpHeader = " LINE #            | OP VALUE                               ";
                    Global.tmpHeader = Global.tmpHeader.PadRight(Global.windowW - 1, Global.charSpace);
                    WRT.WRLine(Global.tmpHeader, "Black", "White");
                    WRT.ResetConsoleColor();
                    Global.tmpReadErrorFound = 0;
                    int tmpReadModePause = 0;
                    if (tmpFileSize >= 0)
                    {
                        for (int ii = 0; ii <= (tmpFileSize-1); ii++)
                        {
                            Global.scriptCodeValid = false;
                            string tmpLineNumber = Convert.ToString(ii);
                            tmpLineNumber = (tmpLineNumber).PadLeft(8, Global.charZero);
                            WRT.WR(" ", "White", "Black");
                            WRT.WR(tmpLineNumber, "White", "Black");
                            WRT.WR(" ", "White", "Black");
                            string tmpHexLineNumber = " ";
                            tmpHexLineNumber = tmpHexLineNumber.PadLeft(8, Global.charSpace);
                            WRT.WR(tmpHexLineNumber, "DarkGray", "Black");
                            WRT.WR(" | ", "DarkMagenta", "Black");
                            int skip = 0;
                            if (ii == 0) { skip = 0; } else { skip = ii; }
                            int take = 1;
                            string tmpFileData = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Skip(skip).Take(take).First();
                            if (tmpFileData.Length >= 0)
                            {
                                // Parse first XX of OpCode.
                                string tmpSubString = tmpFileData.Substring(0, 2);
                                Global.tmpKindOfOP = "NULL";
                                Global.PV_RESW = 9;
                                Global.PV_GROUP = "NULL";
                                if (tmpSubString == "PB") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (tmpSubString == "RB") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (tmpSubString == "JT") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "JT"; }
                                if (tmpSubString == "SP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (tmpSubString == "RA") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (tmpSubString == "JM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "JU"; }
                                if (tmpSubString == "HW") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (tmpSubString == "RE") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "RE"; }
                                if (tmpSubString == "EN") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "EN"; }
                                if (tmpSubString == "FP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (tmpSubString == "TP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (tmpSubString == "KS") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "KS"; }
                                if (tmpSubString == "SM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (tmpSubString == "CM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (tmpSubString == "SR") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (tmpSubString == "SW") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (tmpSubString == "DM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.scriptCodeValid == true)
                                {
                                    // Draw on screen the first half of the OpCode.
                                    if (Global.tmpKindOfOP == "KS") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "SP") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "CO") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "JU") { WRT.WR(tmpSubString, "Magenta", "Black"); }
                                    if (Global.tmpKindOfOP == "JT") { WRT.WR(tmpSubString, "Magenta", "Black"); }
                                    if (Global.tmpKindOfOP == "TI") { WRT.WR(tmpSubString, "Blue", "Black"); }
                                    if (Global.tmpKindOfOP == "RE") { WRT.WR(tmpSubString, "DarkMagenta", "Black"); }
                                    if (Global.tmpKindOfOP == "EN") { WRT.WR(tmpSubString, "White", "Blue"); }
                                }
                                else
                                {
                                    // Error: Not a valid OpCode.
                                    Global.tmpReadErrorFound++;
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "Red", "Black");
                                }
                                if (Global.tmpKindOfOP == "KS")
                                {
                                    // OpCode is a Key String.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkGreen", "Black");
                                }
                                if (Global.tmpKindOfOP == "EN")
                                {
                                    // OpCode is an end OpCode.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkMagenta", "Black");
                                }
                                if (Global.tmpKindOfOP == "RE")
                                {
                                    // OpCode is a Script Commentary/Rem.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkMagenta", "Black");
                                }
                                // Parse next 2 XX YY of the OpCode
                                if ((tmpFileData.Length >= 5) && (Global.tmpKindOfOP != "RE"))
                                {
                                    tmpSubString = tmpFileData.Substring(2, 1);
                                    if (tmpSubString == " ")
                                    {
                                        WRT.WR(tmpSubString, "Yellow", "Black");
                                    }
                                    else
                                    {
                                        Global.tmpReadErrorFound++;
                                        WRT.WR(tmpSubString, "White", "Red");
                                    }
                                    if ((Global.tmpKindOfOP == "CO") || (Global.tmpKindOfOP == "SP"))
                                    {
                                        // OpCode is a button control OpCode.
                                        Global.scriptCodeValid = false;
                                        tmpSubString = tmpFileData.Substring(3, 2);
                                        if (tmpSubString == "AA") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "BB") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "ST") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "SE") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "UP") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "DO") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "LE") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "RI") { Global.scriptCodeValid = true; }
                                        if (Global.SysTarget == "SNES")
                                        {
                                            if (tmpSubString == "XX") { Global.scriptCodeValid = true; }
                                            if (tmpSubString == "YY") { Global.scriptCodeValid = true; }
                                            if (tmpSubString == "BL") { Global.scriptCodeValid = true; }
                                            if (tmpSubString == "BR") { Global.scriptCodeValid = true; }
                                        }
                                        if (Global.flagExtMode == "true")
                                        {
                                            if (Global.tmpSubstring == "E0") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E1") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E2") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E3") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E4") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E5") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E6") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E7") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E8") { Global.scriptCodeValid = true; }
                                            if (Global.tmpSubstring == "E9") { Global.scriptCodeValid = true; }
                                        }
                                        if (Global.scriptCodeValid == true)
                                        {
                                            WRT.WR(tmpSubString, "Green", "Black");
                                        }
                                        else
                                        {
                                            Global.tmpReadErrorFound++;
                                            WRT.WR(tmpSubString, "Red", "Black");
                                        }
                                        if (tmpFileData.Length >= 5)
                                        {
                                            if (Global.tmpKindOfOP == "CO")
                                            {
                                                // Error: A CO type OpCode shouldn't have more characters.
                                                int tmpLateSubstring = tmpFileData.Length - 5;
                                                tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(tmpSubString, "Red", "Black");
                                            }
                                            if (Global.tmpKindOfOP == "SP")
                                            {
                                                // Get SoftPush OpCode time value.
                                                int tmpLateSubstring = tmpFileData.Length - 5;
                                                tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(tmpSubString, "Cyan", "Black");
                                            }
                                        }
                                    }
                                    else if (Global.tmpKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(tmpSubString, "White", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(tmpSubString, "Cyan", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "JT")
                                    {
                                        // OpCode is a Jump X Times OpCode.
                                        // Get how many times it will repeat (Max = 99).
                                        tmpSubString = tmpFileData.Substring(3, 2);
                                        WRT.WR(tmpSubString, "DarkCyan", "Black");
                                        tmpSubString = tmpFileData.Substring(5, 1);
                                        if (tmpSubString == " ")
                                        {
                                            // Is a valid JT OpCode. Draw line on screen.
                                            int tmpLateSubstring = tmpFileData.Length - 5;
                                            tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                            WRT.WR(tmpSubString, "Cyan", "Black");
                                        }
                                        else
                                        {
                                            // Error: Format error on JT OpCode.
                                            Global.tmpReadErrorFound++;
                                            int tmpLateSubstring = tmpFileData.Length - 5;
                                            tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                            WRT.WR(tmpSubString, "Red", "Black");
                                        }
                                    }
                                }
                                else if ((tmpFileData.Length >= 3) && (tmpFileData.Length <= 5) && (Global.tmpKindOfOP != "RE"))
                                {
                                    // Special case for small value OpCodes.
                                    if (Global.tmpKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(tmpSubString, "White", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(tmpSubString, "Cyan", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "JT")
                                    {
                                        // OpCode is a Jump X Times OpCode.
                                        // Get how many times it will repeat (Max = 99).
                                        tmpSubString = tmpFileData.Substring(3, 2);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(tmpSubString, "DarkCyan", "Black");
                                        tmpSubString = tmpFileData.Substring(5, 1);
                                        if (tmpSubString == " ")
                                        {
                                            // Is a valid JT OpCode. Draw line on screen.
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = tmpFileData.Length - 6;
                                            tmpSubString = tmpFileData.Substring(6, tmpLateSubstring);
                                            WRT.WR(tmpSubString, "White", "Black");
                                        }
                                        else
                                        {
                                            // Error: Format error on JT OpCode.
                                            Global.tmpReadErrorFound++;
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = tmpFileData.Length - 6;
                                            tmpSubString = tmpFileData.Substring(6, tmpLateSubstring);
                                            WRT.WR(tmpSubString, "Red", "Black");
                                        }
                                    }
                                }
                            }
                            WRT.ResetConsoleColor();
                            Global.tmpPadSpacing = " ".PadRight(80 - Global.tmpFileData.Length - 49);
                            WRT.WRLine(Global.tmpPadSpacing, "White", "Black"); // Intro endline
                            tmpReadModePause++;
                            if (tmpReadModePause == Global.tmpReadMaxPause)
                            {
                                WRT.WR(" ▬ ", "Cyan", "Black");
                                WRT.WR("                ", "White", "Black");
                                WRT.WR("|", "DarkMagenta", "Black");
                                WRT.WR(" Errors found: ", "White", "Black");
                                if (Global.tmpReadErrorFound == 0)
                                {
                                    WRT.WR(Convert.ToString(Global.tmpReadErrorFound), "Green", "Black");
                                }
                                else { WRT.WR(Convert.ToString(Global.tmpReadErrorFound), "Red", "Black"); }
                                WRT.WR(" Press ", "DarkGray", "Black");
                                WRT.WR("[ENTER]", "White", "Black");
                                WRT.WR(" to continue reading. ", "DarkGray", "Black");
                                Console.ReadLine();
                                tmpReadModePause = 0;
                            }
                        }
                    }
                    WRT.WR(" Total errors found: ", "White", "Black");
                    if (Global.tmpReadErrorFound == 0)
                    {
                        WRT.WRLine(Convert.ToString(Global.tmpReadErrorFound), "Green", "Black");
                    }
                    else
                    {
                        WRT.WRLine(Convert.ToString(Global.tmpReadErrorFound), "Red", "Black");
                    }
                    EXF.ReadHalt();
                    WRT.WR(" Press ", "DarkGray", "Black");
                    WRT.WR("[ENTER]", "White", "Black");
                    WRT.WR(" to return to the menu.", "DarkGray", "Black");
                    WRT.WR(" ", "Black", "Black");
                    Console.ReadLine();
                    Global.restartArea = true;
                    GC.Collect();
                }
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
                {
                    Global.KSOAutoScriptReadMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                    WRT.WR(" Leaving Read Menu...", "White", "Black");
                }
            } // Active While End   
        }
        public static void ExecuteKAS()
        {
            GUI.DrawExecuteMenu();
            Global.restartArea = false;
            while (Global.KSOAutoScriptExecuteMode)
            {
                if (Global.restartArea == false)
                {
                    if (Global.flagLoadLast == false)
                    {
                        Global.tmpUserInput = EXF.StaticPrompt();
                    }
                    else { Global.tmpUserInput = "1"; }
                }
                else
                {
                    EXF.ExecuteKAS();
                }
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "L") || (Global.tmpUserInput == "l"))
                {
                    if (Global.flagLoadLast == false)
                    {
                        Global.tmpUserInput = EXF.FileStaticPrompt();
                        Global.lastFileLoaded = Global.tmpUserInput;
                        Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                        EXF.WriteToCfg(0, Global.tmpWritePad);
                    }
                    else { Global.tmpUserInput = Global.lastFileLoaded; }
                    Global.tmpFileExists = File.Exists(Global.scriptsDirectory + Global.tmpUserInput);
                    if (Global.tmpFileExists == false) { Global.flagLoadLast = false; Global.tmpUserInput = "NULL"; return; }
                    FileInfo scriptFileInfo = new FileInfo(Global.scriptsDirectory + Global.tmpUserInput);
                    int tmpFileSize = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Count();
                    long tmpFileWeight = scriptFileInfo.Length;
                    GUI.DrawTitle();
                    GUI.DrawSubtitle("KSOAutoScript Execution");
                    GUI.DrawSpacer();
                    WRT.WR(" Filename: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(Global.tmpUserInput), "Cyan", "Black");
                    WRT.WR(" Total Lines: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(tmpFileSize), "Green", "Black");
                    WRT.WR(" File Size: ", "White", "Black");
                    WRT.WR(Convert.ToString(tmpFileWeight), "Green", "Black");
                    WRT.WRLine(" Bytes", "DarkGray", "Black");
                    GUI.DrawSpacer();
                    EXF.ExecutionHalt();
                    Console.CursorVisible = false;
                    GC.Collect();
                    GUI.FormatConsoleExpBuffer();
                    GUI.DrawTitle();
                    //                   D        H        | O  V                         | A T     "
                    Global.tmpHeader = " LINE #   LINE @   | OP VALUE                     | ▲ TIME  ";
                    Global.tmpHeader = Global.tmpHeader.PadRight(Global.windowW - 1, Global.charSpace);
                    WRT.WRLine(Global.tmpHeader, "Black", "White");
                    WRT.ResetConsoleColor();
                    if (tmpFileSize > 0)
                    {
                        TBP.timeBeginPeriod(Convert.ToUInt32(Global.timeResolution));
                        // Load the whole file into an array.
                        for (int jj = 0; jj <= (tmpFileSize - 1); jj++)
                        {
                            int skip = 0;
                            if (jj == 0) { skip = 0; } else { skip = jj; }
                            int take = 1;
                            Global.tmpLoadString = File.ReadLines(Global.scriptsDirectory + Global.tmpUserInput).Skip(skip).Take(take).First();
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
                            Global.scriptCodeValid = false;
                            string tmpLineNumber = Convert.ToString(Global.tmpLineCursor);
                            tmpLineNumber = tmpLineNumber.PadLeft(8, Global.charZero);
                            WRT.WR(" " + tmpLineNumber, "White", "Black");
                            WRT.WR(" ", "White", "Black");
                            string padLineTotal = Convert.ToString(Global.tmpLineTotal);
                            padLineTotal = padLineTotal.PadLeft(8, Global.charZero);
                            WRT.WR(padLineTotal, "DarkGray", "Black");
                            WRT.WR(" | ", "DarkMagenta", "Black");
                            if (Global.tmpLineCursor >= tmpFileSize)
                            {
                                break;
                            }
                            else
                            {
                                Global.tmpFileData = Global.tmpFileArray[Global.tmpLineCursor];
                            }
                            if (Global.tmpFileData.Length > 0)
                            {
                                // Limit max. length of a line.
                                if (Global.tmpFileData.Length >= 30)
                                {
                                    Global.tmpFileData = Global.tmpFileData.Substring(0, 30);
                                }
                                // Parse first XX of OpCode.
                                Global.tmpSubstring = Global.tmpFileData.Substring(0, 2);
                                Global.tmpKindOfOP = "NULL";
                                Global.PV_RESW = 9;
                                Global.PV_GROUP = "NULL";
                                Global.tmpOPCache = "NULL";
                                if (Global.tmpSubstring == "PB") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (Global.tmpSubstring == "RB") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (Global.tmpSubstring == "JT") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "JT"; }
                                if (Global.tmpSubstring == "SP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (Global.tmpSubstring == "RA") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "CO"; }
                                if (Global.tmpSubstring == "JM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "JU"; }
                                if (Global.tmpSubstring == "HW") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.tmpSubstring == "RE") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "RE"; }
                                if (Global.tmpSubstring == "EN") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "EN"; }
                                if (Global.tmpSubstring == "FP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (Global.tmpSubstring == "TP") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "SP"; }
                                if (Global.tmpSubstring == "KS") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "KS"; }
                                if (Global.tmpSubstring == "SW") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.tmpSubstring == "SR") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.tmpSubstring == "SM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.tmpSubstring == "CM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.tmpSubstring == "DM") { Global.scriptCodeValid = true; Global.tmpKindOfOP = "TI"; }
                                if (Global.scriptCodeValid == true)
                                {
                                    // Draw on screen the first half of the OpCode.
                                    if (Global.tmpKindOfOP == "SP") { WRT.WR(Global.tmpSubstring, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "CO") { WRT.WR(Global.tmpSubstring, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "KS") { WRT.WR(Global.tmpSubstring, "Yellow", "Black"); }
                                    if (Global.tmpKindOfOP == "JU") { WRT.WR(Global.tmpSubstring, "Magenta", "Black"); }
                                    if (Global.tmpKindOfOP == "JT") { WRT.WR(Global.tmpSubstring, "Magenta", "Black"); }
                                    if (Global.tmpKindOfOP == "TI") { WRT.WR(Global.tmpSubstring, "Blue", "Black"); }
                                    if (Global.tmpKindOfOP == "RE") { WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black"); }
                                    if (Global.tmpKindOfOP == "EN") { WRT.WR(Global.tmpSubstring, "White", "Blue"); }
                                    // Save the OP type for further use
                                    Global.tmpOPCache = Global.tmpSubstring;
                                }
                                else
                                {
                                    // Error: Not a valid OpCode.
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(Global.tmpSubstring, "Red", "Black");
                                }
                                // Small OPCodes Special Identification Area
                                if (Global.tmpKindOfOP == "KS")
                                {
                                    // OpCode is a Special Key String.
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    Global.tmpOPValueCache = Global.tmpSubstring;
                                    WRT.WR(Global.tmpSubstring, "DarkGreen", "Black");
                                }
                                if (Global.tmpKindOfOP == "RE")
                                {
                                    // OpCode is a Script Commentary/Rem.
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black");
                                }
                                if (Global.tmpKindOfOP == "EN")
                                {
                                    // OpCode is an end OpCode.
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black");
                                }
                                // Small OpCodes Special Drawing
                                if (Global.tmpFileData.Length <= 3)
                                {
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(Global.tmpSubstring, "Red", "Black");
                                }
                                // Parse next 2 XX YY of the OpCode
                                if ((Global.tmpFileData.Length >= 5) && (Global.tmpKindOfOP != "RE") && (Global.tmpKindOfOP != "KS"))
                                {
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, 1);
                                    if (Global.tmpSubstring == " ")
                                    {
                                        WRT.WR(Global.tmpSubstring, "Yellow", "Black");
                                    }
                                    else
                                    {
                                        // Error: OpCode has a format error.
                                        WRT.WR(Global.tmpSubstring, "White", "Red");
                                    }
                                    // Read the buttons and draw them in Control OpCodes.
                                    if ((Global.tmpKindOfOP == "CO") || (Global.tmpKindOfOP == "SP"))
                                    {
                                        // OpCode is a button control OpCode.
                                        Global.scriptCodeValid = false;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        if (Global.tmpSubstring == "AA") { Global.scriptCodeValid = true; Global.PV_GROUP = "A"; }
                                        if (Global.tmpSubstring == "BB") { Global.scriptCodeValid = true; Global.PV_GROUP = "B"; }
                                        if (Global.tmpSubstring == "ST") { Global.scriptCodeValid = true; Global.PV_GROUP = "ST"; }
                                        if (Global.tmpSubstring == "SE") { Global.scriptCodeValid = true; Global.PV_GROUP = "SE"; }
                                        if (Global.tmpSubstring == "UP") { Global.scriptCodeValid = true; Global.PV_GROUP = "UP"; }
                                        if (Global.tmpSubstring == "DO") { Global.scriptCodeValid = true; Global.PV_GROUP = "DO"; }
                                        if (Global.tmpSubstring == "LE") { Global.scriptCodeValid = true; Global.PV_GROUP = "LE"; }
                                        if (Global.tmpSubstring == "RI") { Global.scriptCodeValid = true; Global.PV_GROUP = "RI"; }
                                        if (Global.SysTarget == "SNES")
                                        {
                                            if (Global.tmpSubstring == "XX") { Global.scriptCodeValid = true; Global.PV_GROUP = "XX"; }
                                            if (Global.tmpSubstring == "YY") { Global.scriptCodeValid = true; Global.PV_GROUP = "YY"; }
                                            if (Global.tmpSubstring == "BL") { Global.scriptCodeValid = true; Global.PV_GROUP = "BL"; }
                                            if (Global.tmpSubstring == "BR") { Global.scriptCodeValid = true; Global.PV_GROUP = "BR"; }
                                        }
                                        if (Global.flagExtMode == "true")
                                        {
                                            if (Global.tmpSubstring == "E0") { Global.scriptCodeValid = true; Global.PV_GROUP = "E0"; }
                                            if (Global.tmpSubstring == "E1") { Global.scriptCodeValid = true; Global.PV_GROUP = "E1"; }
                                            if (Global.tmpSubstring == "E2") { Global.scriptCodeValid = true; Global.PV_GROUP = "E2"; }
                                            if (Global.tmpSubstring == "E3") { Global.scriptCodeValid = true; Global.PV_GROUP = "E3"; }
                                            if (Global.tmpSubstring == "E4") { Global.scriptCodeValid = true; Global.PV_GROUP = "E4"; }
                                            if (Global.tmpSubstring == "E5") { Global.scriptCodeValid = true; Global.PV_GROUP = "E5"; }
                                            if (Global.tmpSubstring == "E6") { Global.scriptCodeValid = true; Global.PV_GROUP = "E6"; }
                                            if (Global.tmpSubstring == "E7") { Global.scriptCodeValid = true; Global.PV_GROUP = "E7"; }
                                            if (Global.tmpSubstring == "E8") { Global.scriptCodeValid = true; Global.PV_GROUP = "E8"; }
                                            if (Global.tmpSubstring == "E9") { Global.scriptCodeValid = true; Global.PV_GROUP = "E9"; }
                                        }
                                        if (Global.scriptCodeValid == true)
                                        {
                                            WRT.WR(Global.tmpSubstring, "Green", "Black");
                                        }
                                        else
                                        {
                                            // Error: Button Value isn't a valid button.
                                            int tmpLateSubstring = Global.tmpFileData.Length - 5;
                                            WRT.WR(Global.tmpSubstring, "Red", "Black");
                                        }
                                        // Draw the rest of an OpCode if is bigger than 5 spaces.
                                        if (Global.tmpFileData.Length >= 5)
                                        {
                                            if (Global.tmpKindOfOP == "CO")
                                            {
                                                // Error: A CO type OpCode shouldn't have more characters.
                                                int tmpLateSubstring = Global.tmpFileData.Length - 5;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(Global.tmpSubstring, "Red", "Black");
                                            }
                                            if (Global.tmpKindOfOP == "SP")
                                            {
                                                // Get SoftPush OpCode time value.
                                                WRT.WR(" ", "White", "Black");
                                                int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                                // Save the value for further use.
                                                Global.tmpOPValueCache = Global.tmpSubstring;
                                                WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                            }
                                        }
                                    }
                                    else if (Global.tmpKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(Global.tmpSubstring, "White", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "JT")
                                    {
                                        // OpCode is a Jump X Times OpCode.
                                        // Get how many times it will repeat (Max = 99).
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        WRT.WR(Global.tmpSubstring, "DarkCyan", "Black");
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(5, 1);
                                        if (Global.tmpSubstring == " ")
                                        {
                                            // Is a valid JT OpCode. Draw line on screen.
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                        }
                                        else
                                        {
                                            // Error: Format error on JT OpCode.
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            WRT.WR(Global.tmpSubstring, "Red", "Black");
                                        }
                                    }
                                }
                                else if (((Global.tmpFileData.Length > 3) && (Global.tmpFileData.Length < 5)) && (Global.tmpKindOfOP != "RE"))
                                {
                                    // Special case for small value OpCodes.
                                    if (Global.tmpKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(Global.tmpSubstring, "White", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == "JT")
                                    {
                                        // OpCode is a Jump X Times OpCode.
                                        // Get how many times it will repeat (Max = 99).
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(Global.tmpSubstring, "DarkCyan", "Black");
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(5, 1);
                                        if (Global.tmpSubstring == " ")
                                        {
                                            // Is a valid JT OpCode. Draw line on screen.
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            WRT.WR(Global.tmpSubstring, "White", "Black");
                                        }
                                        else
                                        {
                                            // Error: Format error on JT OpCode.
                                            WRT.WR(" ", "White", "Black");
                                            int tmpLateSubstring = Global.tmpFileData.Length - 6;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(6, tmpLateSubstring);
                                            Global.tmpOPAuxValueCache = Global.tmpSubstring;
                                            WRT.WR(Global.tmpSubstring, "Red", "Black");
                                        }
                                    }
                                }
                            }
                            // Draw close bar.
                            Global.tmpWritePad = "| ";
                            Global.tmpWritePad = (Global.tmpWritePad).PadLeft((31 - Global.tmpFileData.Length), Global.charSpace);
                            WRT.WR(Global.tmpWritePad, "DarkMagenta", "Black");
                            // Execute readed OpCodes.
                            // PB - Push Button.
                            if (Global.tmpOPCache == "PB")
                            {
                                if (Global.PV_GROUP == "A")
                                {
                                    VK.VK_Down(Global.VB_A);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "B")
                                {
                                    VK.VK_Down(Global.VB_B);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "ST")
                                {
                                    VK.VK_Down(Global.VB_ST);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "SE")
                                {
                                    VK.VK_Down(Global.VB_SE);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "UP")
                                {
                                    VK.VK_Down(Global.VB_UP);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "DO")
                                {
                                    VK.VK_Down(Global.VB_DO);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "LE")
                                {
                                    VK.VK_Down(Global.VB_LE);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "RI")
                                {
                                    VK.VK_Down(Global.VB_RI);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.SysTarget == "SNES")
                                {
                                    if (Global.PV_GROUP == "XX")
                                    {
                                        VK.VK_Down(Global.VB_X);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "YY")
                                    {
                                        VK.VK_Down(Global.VB_Y);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BL")
                                    {
                                        VK.VK_Down(Global.VB_BL);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BR")
                                    {
                                        VK.VK_Down(Global.VB_BR);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    if (Global.PV_GROUP == "E0")
                                    {
                                        VK.VK_Down(Global.VB_E0);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E1")
                                    {
                                        VK.VK_Down(Global.VB_E1);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E2")
                                    {
                                        VK.VK_Down(Global.VB_E2);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E3")
                                    {
                                        VK.VK_Down(Global.VB_E3);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E4")
                                    {
                                        VK.VK_Down(Global.VB_E4);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E5")
                                    {
                                        VK.VK_Down(Global.VB_E5);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E6")
                                    {
                                        VK.VK_Down(Global.VB_E6);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E7")
                                    {
                                        VK.VK_Down(Global.VB_E7);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E8")
                                    {
                                        VK.VK_Down(Global.VB_E8);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E9")
                                    {
                                        VK.VK_Down(Global.VB_E9);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                            }
                            // RB - Release Button.
                            if (Global.tmpOPCache == "RB")
                            {
                                if (Global.PV_GROUP == "A")
                                {
                                    VK.VK_Up(Global.VB_A);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "B")
                                {
                                    VK.VK_Up(Global.VB_B);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "ST")
                                {
                                    VK.VK_Up(Global.VB_ST);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "SE")
                                {
                                    VK.VK_Up(Global.VB_SE);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "UP")
                                {
                                    VK.VK_Up(Global.VB_UP);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "DO")
                                {
                                    VK.VK_Up(Global.VB_DO);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "LE")
                                {
                                    VK.VK_Up(Global.VB_LE);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "RI")
                                {
                                    VK.VK_Up(Global.VB_RI);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.SysTarget == "SNES")
                                {
                                    if (Global.PV_GROUP == "XX")
                                    {
                                        VK.VK_Up(Global.VB_X);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "YY")
                                    {
                                        VK.VK_Up(Global.VB_Y);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BL")
                                    {
                                        VK.VK_Up(Global.VB_BL);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BR")
                                    {
                                        VK.VK_Up(Global.VB_BR);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    if (Global.PV_GROUP == "E0")
                                    {
                                        VK.VK_Up(Global.VB_E0);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E1")
                                    {
                                        VK.VK_Up(Global.VB_E1);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E2")
                                    {
                                        VK.VK_Up(Global.VB_E2);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E3")
                                    {
                                        VK.VK_Up(Global.VB_E3);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E4")
                                    {
                                        VK.VK_Up(Global.VB_E4);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E5")
                                    {
                                        VK.VK_Up(Global.VB_E5);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E6")
                                    {
                                        VK.VK_Up(Global.VB_E6);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E7")
                                    {
                                        VK.VK_Up(Global.VB_E7);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E8")
                                    {
                                        VK.VK_Up(Global.VB_E8);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E9")
                                    {
                                        VK.VK_Up(Global.VB_E9);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                            }
                            // RA - Release all buttons.
                            if (Global.tmpOPCache == "RA")
                            {
                                VK.VK_Up(Global.VB_A);
                                VK.VK_Up(Global.VB_B);
                                VK.VK_Up(Global.VB_ST);
                                VK.VK_Up(Global.VB_SE);
                                VK.VK_Up(Global.VB_UP);
                                VK.VK_Up(Global.VB_DO);
                                VK.VK_Up(Global.VB_LE);
                                VK.VK_Up(Global.VB_RI);
                                if (Global.SysTarget == "SNES")
                                {
                                    VK.VK_Up(Global.VB_X);
                                    VK.VK_Up(Global.VB_Y);
                                    VK.VK_Up(Global.VB_BL);
                                    VK.VK_Up(Global.VB_BR);
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    VK.VK_Up(Global.VB_E0);
                                    VK.VK_Up(Global.VB_E1);
                                    VK.VK_Up(Global.VB_E2);
                                    VK.VK_Up(Global.VB_E3);
                                    VK.VK_Up(Global.VB_E4);
                                    VK.VK_Up(Global.VB_E5);
                                    VK.VK_Up(Global.VB_E6);
                                    VK.VK_Up(Global.VB_E7);
                                    VK.VK_Up(Global.VB_E8);
                                    VK.VK_Up(Global.VB_E9);
                                }
                                Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                Global.tmpLineCursor++;
                            }
                            // HW - Hold/Wait a given time.
                            if (Global.tmpOPCache == "HW")
                            {
                                TI.Hold(Convert.ToInt32(Global.tmpOPValueCache));
                                Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                Global.tmpLineCursor++;
                            }
                            // JM - Absolute Jump / Infinite Loop to a given line.
                            if (Global.tmpOPCache == "JM")
                            {
                                Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPValueCache);
                            }
                            // JT - Jump X Times to a given line.
                            if (Global.tmpOPCache == "JT")
                            {
                                if (Global.tmpJumpCurrent != Global.tmpLineCursor)
                                {
                                    Global.tmpJumpTimes = Convert.ToInt32(Global.tmpOPValueCache);
                                    Global.tmpJumpTimes = Global.tmpJumpTimes - 1;
                                    Global.tmpJumpCurrent = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPAuxValueCache);
                                }
                                if (Global.tmpJumpCurrent == Global.tmpLineCursor)
                                {
                                    if (Global.tmpJumpTimes >= 1)
                                    {
                                        Global.tmpJumpTimes = Global.tmpJumpTimes - 1;
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPAuxValueCache);
                                    }
                                    else if (Global.tmpJumpTimes <= 0)
                                    {
                                        Global.tmpLineCursor++;
                                        Global.tmpJumpCurrent = 0;
                                    }
                                }
                            }
                            // RE - Comment / REM
                            if (Global.tmpOPCache == "RE")
                            {
                                Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                Global.tmpLineCursor++;
                            }
                            // SP - Soft Push a button a given time
                            if (Global.tmpOPCache == "SP")
                            {
                                if (Global.PV_GROUP == "A")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_A, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "B")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_B, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "ST")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_ST, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "SE")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_SE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "UP")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_UP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "DO")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_DO, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "LE")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_LE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "RI")
                                {
                                    VK.VK_HoldUntilDelay(Global.VB_RI, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.SysTarget == "SNES")
                                {
                                    if (Global.PV_GROUP == "XX")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_X, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "YY")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_Y, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BL")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_BL, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BR")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_BR, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    if (Global.PV_GROUP == "E0")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E0, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E1")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E1, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E2")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E2, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E3")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E3, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E4")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E4, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E5")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E5, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E6")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E6, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E7")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E7, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E8")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E8, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E9")
                                    {
                                        VK.VK_HoldUntilDelay(Global.VB_E9, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                            }
                            // FP - Fast Push a button a given time.
                            if (Global.tmpOPCache == "FP")
                            {
                                if (Global.PV_GROUP == "A")
                                {
                                    VK.VK_HoldUntil(Global.VB_A, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "B")
                                {
                                    VK.VK_HoldUntil(Global.VB_B, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "ST")
                                {
                                    VK.VK_HoldUntil(Global.VB_ST, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "SE")
                                {
                                    VK.VK_HoldUntil(Global.VB_SE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "UP")
                                {
                                    VK.VK_HoldUntil(Global.VB_UP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "DO")
                                {
                                    VK.VK_HoldUntil(Global.VB_DO, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "LE")
                                {
                                    VK.VK_HoldUntil(Global.VB_LE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "RI")
                                {
                                    VK.VK_HoldUntil(Global.VB_RI, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.SysTarget == "SNES")
                                {
                                    if (Global.PV_GROUP == "XX")
                                    {
                                        VK.VK_HoldUntil(Global.VB_X, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "YY")
                                    {
                                        VK.VK_HoldUntil(Global.VB_Y, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BL")
                                    {
                                        VK.VK_HoldUntil(Global.VB_BL, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BR")
                                    {
                                        VK.VK_HoldUntil(Global.VB_BR, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    if (Global.PV_GROUP == "E0")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E0, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E1")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E1, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E2")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E2, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E3")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E3, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E4")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E4, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E5")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E5, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E6")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E6, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E7")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E7, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E8")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E8, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E9")
                                    {
                                        VK.VK_HoldUntil(Global.VB_E9, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                            }
                            // TP - Turbo-Pushes a button a given time.
                            if (Global.tmpOPCache == "TP")
                            {
                                if (Global.PV_GROUP == "A")
                                {
                                    VK.VK_HoldTurbo(Global.VB_A, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "B")
                                {
                                    VK.VK_HoldTurbo(Global.VB_B, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "ST")
                                {
                                    VK.VK_HoldTurbo(Global.VB_ST, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "SE")
                                {
                                    VK.VK_HoldTurbo(Global.VB_SE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "UP")
                                {
                                    VK.VK_HoldTurbo(Global.VB_UP, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "DO")
                                {
                                    VK.VK_HoldTurbo(Global.VB_DO, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "LE")
                                {
                                    VK.VK_HoldTurbo(Global.VB_LE, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.PV_GROUP == "RI")
                                {
                                    VK.VK_HoldTurbo(Global.VB_RI, Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                    Global.tmpLineCursor++;
                                }
                                if (Global.SysTarget == "SNES")
                                {
                                    if (Global.PV_GROUP == "XX")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_X, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "YY")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_Y, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BL")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_BL, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "BR")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_BR, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                                if (Global.flagExtMode == "true")
                                {
                                    if (Global.PV_GROUP == "E0")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E0, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E1")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E1, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E2")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E2, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E3")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E3, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E4")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E4, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E5")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E5, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E6")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E6, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E7")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E7, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E8")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E8, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                    if (Global.PV_GROUP == "E9")
                                    {
                                        VK.VK_HoldTurbo(Global.VB_E9, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                    }
                                }
                            }
                            // KS - Attempts to push a Key String.
                            if (Global.tmpOPCache == "KS")
                            {
                                VK.VK_HoldUntilDelay(Global.tmpOPValueCache, 50);
                                Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                Global.tmpLineCursor++;
                            }
                            // SW - Sync Waits until there's no delay.
                            if (Global.tmpOPCache == "SW")
                            {
                                if (Global.tmpStackDelay <= Convert.ToInt32(Global.tmpOPValueCache))
                                {
                                    Global.tmpStackSync = Convert.ToInt32(Global.tmpOPValueCache) - Global.tmpStackDelay;
                                    Global.tmpStackMem = Global.tmpStackDelay;
                                    TI.Hold(Global.tmpStackSync);
                                    Global.tmpStackDelay = Global.tmpStackDelay - Global.tmpStackSync;
                                    if (Global.tmpStackDelay < 0) { Global.tmpStackDelay = 0; }
                                    Global.tmpStackStatus = 1;
                                }
                                else
                                {
                                    TI.Hold(Convert.ToInt32(Global.tmpOPValueCache));
                                    Global.tmpStackDelay -= Convert.ToInt32(Global.tmpOPValueCache);
                                    Global.tmpStackMem = Global.tmpStackDelay;
                                    Global.tmpStackStatus = 2;
                                }
                                Global.tmpLineCursor++;
                            }
                            // SR - Resets the Sync Delay Stack.
                            if (Global.tmpOPCache == "SR")
                            {
                                Global.tmpStackDelay = 0;
                                Global.tmpLineCursor++;
                            }
                            // DM - Displays marker time.
                            if (Global.tmpOPCache == "DM")
                            {
                                Global.tmpStackStatus = 3;
                                Global.tmpLineCursor++;
                            }
                            // SM - Sets a new marker and resets the marker counter.
                            if (Global.tmpOPCache == "SM")
                            {
                                Global.tmpMarkerMillis = 0;
                                Global.tmpLineCursor++;
                            }
                            // CM - Waits if the time marker is lower than the value.
                            if (Global.tmpOPCache == "CM")
                            {
                                if (Global.tmpMarkerMillis < Convert.ToInt32(Global.tmpOPValueCache))
                                {
                                    Global.tmpMarkerDifference = Convert.ToInt32(Global.tmpOPValueCache) - Global.tmpMarkerMillis;
                                    TI.Hold(Global.tmpMarkerDifference);
                                    Global.tmpStackStatus = 4;
                                }
                                else
                                {
                                    Global.tmpMarkerDifference = 0;
                                    Global.tmpStackStatus = 5;
                                }
                                Global.tmpLineCursor++;
                            }
                            // EN - Ends the script execution.
                            if (Global.tmpOPCache == "EN")
                            {
                                Global.tmpLineCursor = tmpFileSize + 1;
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
                            Global.tmpIsParsed = int.TryParse((Global.tmpOPValueCache), out Global.outVal);
                            if (Global.tmpIsParsed == false) { Global.tmpOPValueCache = "0"; } 
                            // Sync count all delay.
                            if (Global.tmpOPCache == "SP")
                            {
                                Global.tmpStackDelay += (Global.tmpDeltaMillis - (Convert.ToInt32(Global.tmpOPValueCache) + 25));
                                Global.tmpStackException = true;
                            }
                            else if (Global.tmpOPCache == "KS")
                            {
                                Global.tmpStackDelay += (Global.tmpDeltaMillis - (Convert.ToInt32(Global.tmpOPValueCache) + 50));
                                Global.tmpStackException = true;
                            }
                            else if (Global.tmpOPCache == "SM")
                            {
                                Global.tmpStackDelay += Global.tmpDeltaMillis;
                                Global.tmpStackException = true;
                            }
                            else if (Global.tmpOPCache == "DM")
                            {
                                Global.tmpStackDelay += Global.tmpDeltaMillis;
                                Global.tmpStackException = true;
                            }
                            else if (Global.tmpOPCache == "SR")
                            {
                                Global.tmpStackDelay += Global.tmpDeltaMillis;
                                Global.tmpStackException = true;
                            }
                            else if (Global.tmpOPCache == "CM")
                            {
                                Global.tmpStackDelay += Global.tmpDeltaMillis - Global.tmpMarkerDifference;
                                Global.tmpStackException = true;
                            }
                            if (Global.tmpStackException == false)
                            {
                                if (Global.tmpKindOfOP == "TI")
                                {
                                    Global.tmpStackDelay += (Global.tmpDeltaMillis - Convert.ToInt32(Global.tmpOPValueCache));
                                }
                                else if (Global.tmpKindOfOP == "SP")
                                {
                                    Global.tmpStackDelay += (Global.tmpDeltaMillis - Convert.ToInt32(Global.tmpOPValueCache));
                                }
                                else
                                {
                                    Global.tmpStackDelay += Global.tmpDeltaMillis;
                                }
                            }
                            if (Global.tmpStackDelay < 0) { Global.tmpStackDelay = 0; }
                            Global.tmpStackException = false;
                            Global.tmpMarkerMillis += Global.tmpDeltaMillis;
                            WRT.WR(Convert.ToString(Global.tmpDeltaMillis), "White", "Black");
                            WRT.WR(" ▲ms", "White", "Black");
                            if (Global.tmpStackStatus == 1) { WRT.WR(" (" + Global.tmpStackMem + "ms)", "Green", "Black"); Global.tmpStackStatus = 0; }
                            if (Global.tmpStackStatus == 2) { WRT.WR(" (" + Global.tmpStackMem + "ms)", "Red", "Black"); Global.tmpStackStatus = 0; }
                            if (Global.tmpStackStatus == 3) { WRT.WR(" (" + Global.tmpMarkerMillis + "ms)", "White", "Black"); Global.tmpStackStatus = 0; }
                            if (Global.tmpStackStatus == 4) { WRT.WR(" (" + Global.tmpMarkerDifference + "ms)", "Green", "Black"); Global.tmpStackStatus = 0; }
                            if (Global.tmpStackStatus == 5) { WRT.WR(" (0 ms)", "White", "Black"); Global.tmpStackStatus = 0; }
                            WRT.WRLine(" ", "White", "Black"); // Intro endline.
                            Global.tmpLineTotal += 1;
                        } // Execution While end.
                        Global.tmpElapsedMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                        TEP.timeEndPeriod(Convert.ToUInt32(Global.timeResolution));
                        GUI.DrawSpacer();
                        WRT.WR(" Total time elapsed: ", "White", "Black");
                        WRT.WR(Convert.ToString(Global.tmpElapsedMillis), "Cyan", "Black");
                        WRT.WRLine(" ms", "White", "Black");
                        Global.SW.Reset();
                        Global.SW.Stop();
                        WRT.ResetConsoleColor();
                    }
                    Global.tmpFileArray.Clear();
                    Global.flagLoadLast = false;
                    Console.CursorVisible = true;
                    EXF.EndExecutionHalt();
                    WRT.WR(" Press ", "DarkGray", "Black");
                    WRT.WR("[ENTER]", "White", "Black");
                    WRT.WR(" to return to the menu.", "DarkGray", "Black");
                    WRT.WR(" ", "Black", "Black");
                    Console.ReadLine();
                    Global.restartArea = true;
                    GC.Collect();
                }
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
                {
                    Global.KSOAutoScriptExecuteMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                    WRT.WRLine(" Leaving Script Execution...", "White", "Black");
                }
            } // Active While End
        }
    }
    public class MN
    {
        public static void Main()
        {
            GUI.FormatConsole();
            EXF.LoadConfig();
            EXF.LoadKeyConfig();
            EXF.LoadLastHalt();
            GC.Collect();
            GUI.DrawTitle();
            GUI.DrawMainMenu();
            GUI.DrawCreditHint();
            Global.flagFirstRun = false;
            Global.tmpUserInput = EXF.StaticPrompt();
            if ((Global.tmpUserInput == "1") || (Global.tmpUserInput == "C") || (Global.tmpUserInput == "c"))
            {
                Global.configKeysMode = true;
                EXF.ConfigKeys();
            }
            else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput == "T") || (Global.tmpUserInput == "t"))
            {
                Global.testKeysMode = true;
                EXF.TestKeys();
            }
            else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput == "R") || (Global.tmpUserInput == "r"))
            {
                Global.KSOAutoScriptReadMode = true;
                EXF.ReadKAS();
            }
            else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput == "E") || (Global.tmpUserInput == "e"))
            {
                Global.KSOAutoScriptExecuteMode = true;
                EXF.ExecuteKAS();
            }
            else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput == "M") || (Global.tmpUserInput == "m"))
            {
                Global.SysConfig = true;
                EXF.ChangeTarget();
            }
            EXF.AltInput(Global.tmpUserInput);
        }
    }
}



