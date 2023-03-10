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
using Windows.UI.Input;
using static KasTAS.EXF;

ComWrappers.RegisterForMarshalling(WinFormsComInterop.WinFormsComWrappers.Instance);

// ---------------------------------------------------
// Code: KasTAS.cs
// Version: 2.0.94
// Author: Karst Skarn / Owain#3593 (Discord)
// Date: 12-01-2022 (Begin) / 16-02-2022 (1.0.98) / 10-03-2023 (2.0.84)
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

        // Virtual Keyboard / Function Values
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
        public static string VB_LISTENCONTROLKEY = "L";
        public static string VB_LISTENMARKERKEY = "J";
        public static string VB_LISTENEXECUTIONKEY = "K";

        public static bool L_A = false;
        public static bool L_B = false;
        public static bool L_ST = false;
        public static bool L_SE = false;
        public static bool L_UP = false;
        public static bool L_DO = false;
        public static bool L_LE = false;
        public static bool L_RI = false;
        public static bool L_X = false;
        public static bool L_Y = false;
        public static bool L_BL = false;
        public static bool L_BR = false;
        public static bool L_E0 = false;
        public static bool L_E1 = false;
        public static bool L_E2 = false;
        public static bool L_E3 = false;
        public static bool L_E4 = false;
        public static bool L_E5 = false;
        public static bool L_E6 = false;
        public static bool L_E7 = false;
        public static bool L_E8 = false;
        public static bool L_E9 = false;
        public static bool L_MARKERKEY = false;

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
        public static PV_GROUPStates PV_GROUP = PV_GROUPStates.None;
        public static string PV_READGROUP = "NULL";

        // Configuration Values
        public const int cfgTotalLines = 14;
        public const int cfgKeyTotalLines = 27;
        public const int cfgValuePos = 18;
        public static int genCompensationValue = 2;
        public static int genDelayStackCounter = 40;
        public static string sysTarget = "DUAL";
        public static string flagExtMode = "false";
        public const string cfgFile = "kascfg.ini";
        public const string cfgKeysFile = "keydata.ini";
        public static string lastFileLoaded = "NULL";
        public static string version = "2.0.84";
        public static string scriptsDirectory = "Scripts/";
        public static string showTwitchTitle = "true";
        public static string flagFirstInit = "true";

        // Test Values
        public static int configHoldTime = 5000;
        public static int configReadyTime = 2000;
        public static int testReadyTime = 5000;
        public static int testIncrementalStartTime = 1000;
        public static int testIncrementalReduction = 50;
        public static int testContinousTime = 300;
        public static double testFrameTime = 33.3333;

        // Execution Values
        public static byte tmpStackStatus = 0;
        public static bool sysConfig = false;
        public static bool keepExecuting = true;
        public static bool configKeysMode = false;
        public static bool testKeysMode = false;
        public static bool KASScriptReadMode = false;
        public static bool KASScriptExecuteMode = false;
        public static bool KASScriptGenerateMode = false;
        public static bool scriptCodeValid = false;
        public static bool flagLoadLast = false;
        public static bool flagFirstRun = true;
        public static bool tmpStackException = false;
        public static bool tmpIsParsed = false;
        public static bool tmpFileExists = false;
        public static bool restartArea = false;
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
        public static int outVal = 0;
        public static string tmpUserInput = " ";
        public static OP_States tmpKindOfOP = OP_States.None;
        public static string tmpReadKindOfOP = "NULL";
        public static string tmpOPCache = "NULL";
        public static string tmpOPValueCache = "NULL";
        public static string tmpOPAuxValueCache = "NULL";
        public static string tmpPadSpacing = " ";
        public static string tmpWritePad = " ";
        public static string tmpHeader = " ";
        public static string tmpHaltKey = " ";
        public static string tmpFileData = " ";
        public static string tmpSubstring = " ";
        public static string tmpLoadString = " ";
        public static List<string> tmpFileArray = new List<string>();

        // Console Format Values
        public static char charSpace = ' ';
        public static char charZero = '0';
        public static char charBar = '▄';
        public static char charUpperBar = '▀';
        public static int windowH = 21;
        public static int windowW = 82;

        // Object Values
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
    public class KeyboardListen
    {
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);
        public static bool KeyCheck(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }
    }
    public class WRT
    {
        public static void WRLine(string text, string FColor, string BColor)
        {
            if (text == String.Empty || FColor == String.Empty || BColor == String.Empty) { return; }
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), FColor);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), BColor);
            System.Console.Write(text + Environment.NewLine);
            ResetConsoleColor();
        }
        public static void WR(string text, string FColor, string BColor)
        {
            if (text == String.Empty || FColor == String.Empty || BColor == String.Empty) { return; }
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), FColor);
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), BColor);
            System.Console.Write(text);
            ResetConsoleColor();
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
        public static void VK_Down(EXF.PV_GROUPStates pvGroup)
        {
            string stringToSend = GetStringFromPV_Group(pvGroup);
            VK_Down(stringToSend);
        }
        public static void VK_Up(string inKey)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyUp(key);
        }
        public static void VK_Up(EXF.PV_GROUPStates pvGroup)
        {
            string stringToSend = GetStringFromPV_Group(pvGroup);
            VK_Up(stringToSend);
        }
        public static void VK_HoldUntil(string inKey, int time)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyDown(key);
            TI.Hold(time);
            KeyboardSend.KeyUp(key);
        }
        public static void VK_HoldUntil(EXF.PV_GROUPStates pvGroup, int time)
        {
            string stringToSend = GetStringFromPV_Group(pvGroup);
            VK_HoldUntil(stringToSend, time);
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
        public static void VK_HoldTurbo(EXF.PV_GROUPStates pvGroup, int time)
        {
            string stringToSend = GetStringFromPV_Group(pvGroup);
            VK_HoldTurbo(stringToSend, time);
        }
        public static void VK_HoldUntilDelay(string inKey, int time)
        {
            Keys key = (Keys)Global.KC.ConvertFromString(inKey);
            KeyboardSend.KeyDown(key);
            TI.Hold(time);
            KeyboardSend.KeyUp(key);
            TI.Hold(25);
        }
        public static void VK_HoldUntilDelay(EXF.PV_GROUPStates pvGroup, int time)
        {
            string stringToSend = GetStringFromPV_Group(pvGroup);
            VK_HoldUntilDelay(stringToSend, time);
        }
        private static string GetStringFromPV_Group(EXF.PV_GROUPStates pvGroup)
        {
            string stringToSend = "";
            switch (pvGroup)
            {
                case EXF.PV_GROUPStates.None:
                    break;
                case EXF.PV_GROUPStates.AA:
                    stringToSend = Global.VB_A;
                    break;
                case EXF.PV_GROUPStates.BB:
                    stringToSend = Global.VB_B;
                    break;
                case EXF.PV_GROUPStates.ST:
                    stringToSend = Global.VB_ST;
                    break;
                case EXF.PV_GROUPStates.SE:
                    stringToSend = Global.VB_SE;
                    break;
                case EXF.PV_GROUPStates.UP:
                    stringToSend = Global.VB_UP;
                    break;
                case EXF.PV_GROUPStates.DO:
                    stringToSend = Global.VB_DO;
                    break;
                case EXF.PV_GROUPStates.LE:
                    stringToSend = Global.VB_LE;
                    break;
                case EXF.PV_GROUPStates.RI:
                    stringToSend = Global.VB_RI;
                    break;
                case EXF.PV_GROUPStates.XX:
                    stringToSend = Global.VB_X;
                    break;
                case EXF.PV_GROUPStates.YY:
                    stringToSend = Global.VB_Y;
                    break;
                case EXF.PV_GROUPStates.BL:
                    stringToSend = Global.VB_BL;
                    break;
                case EXF.PV_GROUPStates.BR:
                    stringToSend = Global.VB_BR;
                    break;
                case EXF.PV_GROUPStates.E0:
                    stringToSend = Global.VB_E0;
                    break;
                case EXF.PV_GROUPStates.E1:
                    stringToSend = Global.VB_E1;
                    break;
                case EXF.PV_GROUPStates.E2:
                    stringToSend = Global.VB_E2;
                    break;
                case EXF.PV_GROUPStates.E3:
                    stringToSend = Global.VB_E3;
                    break;
                case EXF.PV_GROUPStates.E4:
                    stringToSend = Global.VB_E4;
                    break;
                case EXF.PV_GROUPStates.E5:
                    stringToSend = Global.VB_E5;
                    break;
                case EXF.PV_GROUPStates.E6:
                    stringToSend = Global.VB_E6;
                    break;
                case EXF.PV_GROUPStates.E7:
                    stringToSend = Global.VB_E7;
                    break;
                case EXF.PV_GROUPStates.E8:
                    stringToSend = Global.VB_E8;
                    break;
                case EXF.PV_GROUPStates.E9:
                    stringToSend = Global.VB_E9;
                    break;
            }
            return stringToSend;
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
            WRT.WR(" 2023(C)", "DarkGray", "DarkBlue");
            WRT.WR(" ", "White", "DarkBlue");
            WRT.WR("          ", "DarkCyan", "DarkBlue");
            WRT.WR("                 █", "DarkGray", "DarkBlue");
            if (Global.showTwitchTitle == "true")
            {
                WRT.WR("  www.twitch.tv/karstskarn", "White", "DarkCyan");
            }
            else
            {
                WRT.WR("                          ", "DarkMagenta", "DarkBlue");
            }
            (int curLeft, int curTop) = Console.GetCursorPosition();
            if (Global.showTwitchTitle == "true")
            {
                WRT.WRLine(" ".PadRight(Console.BufferWidth - curLeft - 1, Global.charSpace), "White", "DarkCyan");
            }
            else
            {
                WRT.WRLine(" ".PadRight(Console.BufferWidth - curLeft - 1, Global.charSpace), "White", "DarkBlue");
            }
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
            WRT.WRLine("ead a KAS Script file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("4", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Cyan", "Black");
            WRT.WRLine("xecute a KAS Script file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("5", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("G", "Cyan", "Black");
            WRT.WRLine("enerate a KAS Script file using your keystrokes.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Current key mode is [", "White", "Black");
            if (Global.sysTarget == "DUAL")
            {
                WRT.WR("DUAL [A/B - 2 Buttons]", "Cyan", "Black");
            }
            if (Global.sysTarget == "QUAD")
            {
                WRT.WR("QUAD [A/B/X/Y - 4 Buttons]", "Cyan", "Black");
            }
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
            WRT.WR("6", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("M", "White", "Black");
            WRT.WRLine("anage output modes.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" You can write directly any", "White", "Black");
            WRT.WR(" <.kas> ", "Cyan", "Black");
            WRT.WRLine("file in the prompt", "White", "Black");
            WRT.WRLine(" and will be executed regardless of the menu you are in.", "White", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Press the execution control key ", "DarkGray", "Black");
            WRT.WR("[", "White", "Black");
            WRT.WR(Global.VB_LISTENEXECUTIONKEY, "Green", "Black");
            WRT.WR("]", "White", "Black");
            WRT.WRLine(" to stop a script execution.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawCreditHint()
        {
            Console.SetCursorPosition(0, Global.windowH - 4);
            WRT.WR(" Type ", "DarkGray", "Black");
            WRT.WR("$Credit", "Gray", "Black");
            WRT.WR(" for use info.", "DarkGray", "Black");
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
            GUI.DrawSubtitle("KAS Script Visualizer");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can read a KAS Script file and check its syntax", "DarkGray", "Black");
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
            GUI.DrawSubtitle("KAS Script Execution");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can execute a KAS Script.", "DarkGray", "Black");
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
            WRT.WRLine(" Note: All tests are performed with the DUAL buttons only.", "White", "Black");
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
            WRT.WR("D", "White", "Black");
            WRT.WRLine("ual main buttons mode (A/B - 2 Buttons).", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("Q", "White", "Black");
            WRT.WRLine("uad main buttons mode (A/B/X/Y - 4 Buttons).", "DarkGray", "Black");
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
        public static void DrawGenerateMenu()
        {
            GUI.DrawTitle();
            GUI.DrawSubtitle("KAS Script Generation");
            GUI.DrawSpacer();
            WRT.WRLine(" Here you can generate a KAS Script using your keystrokes.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" Use the following key to Start/Stop the keystrokes recording.", "DarkGray", "Black");
            WRT.WR(" Current control key is set as [", "White", "Black");
            WRT.WR(Global.VB_LISTENCONTROLKEY, "Green", "Black");
            WRT.WRLine("]", "White", "Black");
            WRT.WR(" Current insert marker key is set as [", "White", "Black");
            WRT.WR(Global.VB_LISTENMARKERKEY, "Green", "Black");
            WRT.WRLine("]", "White", "Black");
            WRT.WR(" You can change those keys in the ", "DarkGray", "Black");
            WRT.WR("<keydata.ini>", "Gray", "Black");
            WRT.WRLine(" file.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("G", "White", "Black");
            WRT.WRLine("enerate a KAS Script file.", "DarkGray", "Black");
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("2", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" NOTE:", "White", "Black");
            WRT.WR(" This function will ", "DarkGray", "Black");
            WRT.WR("only", "White", "Black");
            WRT.WRLine(" record the keystrokes that match", "DarkGray", "Black");
            WRT.WRLine(" the ones set in the keyboard keys configuration file!", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawGenFileMenu()
        {
            GUI.DrawTitle();
            GUI.DrawSubtitle("KAS Script Generation");
            GUI.DrawSpacer();
            WRT.WRLine(" Write the name of the file that will be created.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" NOTE: ", "White", "Black");
            WRT.WRLine("If the file already exists it may be overwritten!", "Red", "Black");
            WRT.WR(" Be sure to add the proper <.kas> extension when writing the name of the file!", "DarkGray", "Black");

        }
        public static void DrawGenFilePanel()
        {
            GUI.FormatConsoleExpBuffer();
            GUI.DrawTitle();
            WRT.WR(" The recording will begin when the control key ", "DarkGray", "Black");
            WRT.WR("[", "White", "Black");
            WRT.WR(Global.VB_LISTENCONTROLKEY, "Green", "Black");
            WRT.WR("]", "White", "Black");
            WRT.WRLine(" is pressed.", "DarkGray", "Black");
            WRT.WR(" Press the control key ", "DarkGray", "Black");
            WRT.WR("[", "White", "Black");
            WRT.WR(Global.VB_LISTENCONTROLKEY, "Green", "Black");
            WRT.WR("]", "White", "Black");
            WRT.WRLine(" again to stop the recording.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Current file: ", "White", "Black");
            WRT.WRLine("<" + Global.lastFileLoaded + ">", "Green", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Current active compensation delay: ", "Gray", "Black");
            WRT.WR(Global.genCompensationValue.ToString(), "Green", "Black");
            WRT.WRLine("ms ", "Gray", "Black");
            WRT.WR(" Current delay stack counter: ", "Gray", "Black");
            WRT.WR(Global.genDelayStackCounter.ToString(), "Green", "Black");
            GUI.DrawSpacer();
            WRT.WR(" Set those values to zero in the ", "DarkGray", "Black");
            WRT.WR("<kascfg.ini>", "White", "Black");
            WRT.WR(" to disable them.", "DarkGray", "Black");
            GUI.DrawSpacer();
        }
        public static void DrawGenEndFile()
        {
            GUI.FormatConsoleExpBuffer();
            GUI.DrawTitle();
            WRT.WRLine(" File generated successfully! ", "White", "Black");
            GUI.DrawSpacer();
            WRT.WR(" File name: ", "Gray", "Black");
            WRT.WRLine("<" + Global.lastFileLoaded + ">", "Green", "Black");
            FileInfo scriptFileInfo = new FileInfo(Global.scriptsDirectory + Global.lastFileLoaded);
            int tmpFileSize = File.ReadLines(Global.scriptsDirectory + Global.lastFileLoaded).Count();
            long tmpFileWeight = scriptFileInfo.Length;
            WRT.WR(" File size: ", "Gray", "Black");
            WRT.WRLine(tmpFileSize.ToString() + " Bytes", "Green", "Black");
            WRT.WR(" File lenght: ", "Gray", "Black");
            WRT.WRLine(tmpFileWeight.ToString() + " Lines", "Green", "Black");
            GUI.DrawSpacer();
            WRT.WRLine(" You can write the file name to execute it now.", "DarkGray", "Black");
            GUI.DrawSpacer();
            WRT.WR("  ▬ ", "DarkGray", "Black");
            WRT.WR("1", "White", "Black");
            WRT.WR(". ", "DarkGray", "Black");
            WRT.WR("E", "Blue", "Black");
            WRT.WRLine("xit back to the menu.", "DarkGray", "Black");
            EXF.StaticPrompt();
        }
    }
    // Execution Functions ~
    public class EXF
    {
        // Instruction Enums
        public enum PV_GROUPStates
        {
            None,
            AA,
            BB,
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
        public enum OP_States
        {
            None,
            CO,
            JT,
            SP,
            JU,
            TI,
            RE,
            EN,
            KS,
            RM
        }

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
            08 SYS TARGET   = DUAL
            09 TIME RES.    = 1
            10 READ PAUSE   = 100
            11 SHOW TW      = true
            12 GEN. COMP.D. = 2
            13 GEN. STACK   = 40
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
                        Global.sysTarget = tmpSubString;
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
                    if (ii == 12)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.genCompensationValue = Convert.ToInt32(tmpSubString);
                    }
                    if (ii == 13)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.genDelayStackCounter = Convert.ToInt32(tmpSubString);
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
            00 SYS TARGET   = DUAL
            01 EXTRA MODE   = false
            02 BUTTON A     = P
            03 BUTTON B     = O
            04 BUTTON START = X
            05 BUTTON SELEC = Z
            06 BUTTON UP    = W
            07 BUTTON DOWN  = S
            08 BUTTON LEFT  = A
            09 BUTTON RIGHT = D
            10 BUTTON X     = 0
            11 BUTTON Y     = 9
            12 BUTTON U.LE. = 1
            13 BUTTON U.RI. = 3
            14 E0           = H
            15 E1           = H
            16 E2           = H
            17 E3           = H
            18 E4           = H
            19 E5           = H
            20 E6           = H
            21 E7           = H
            22 E8           = H
            23 E9           = H
            24 LISTEN C.KEY = L
            25 LISTEN MARK. = J
            26 LISTEN EXEC. = K
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
                        Global.sysTarget = tmpSubString;
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
                    if (ii == 24)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_LISTENCONTROLKEY = tmpSubString;
                    }
                    if (ii == 25)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_LISTENMARKERKEY = tmpSubString;
                    }
                    if (ii == 26)
                    {
                        int tmpLateSubstring = tmpFileData.Length - Global.cfgValuePos;
                        string tmpSubString = tmpFileData.Substring(Global.cfgValuePos, tmpLateSubstring);
                        Global.VB_LISTENEXECUTIONKEY = tmpSubString;
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
                        Global.KASScriptExecuteMode = true;
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
                // Here it used to be a disclaimer message in older versions.
            }
        }
        public static void ResetKeyboardBooleans()
        {
            Global.L_A = false;
            Global.L_B = false;
            Global.L_ST = false;
            Global.L_SE = false;
            Global.L_UP = false;
            Global.L_DO = false;
            Global.L_LE = false;
            Global.L_RI = false;
            Global.L_X = false;
            Global.L_Y = false;
            Global.L_BL = false;
            Global.L_BR = false;
            Global.L_E0 = false;
            Global.L_E1 = false;
            Global.L_E2 = false;
            Global.L_E3 = false;
            Global.L_E4 = false;
            Global.L_E5 = false;
            Global.L_E6 = false;
            Global.L_E7 = false;
            Global.L_E8 = false;
            Global.L_E9 = false;
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
            // Used only for debug purposes.
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
        public static void RemoteExecutionHalt()
        {
            Console.SetCursorPosition(0, Global.windowH - 3);
            GUI.DrawLowerBarSpacer();
            WRT.WR(" ▬ ", "Cyan", "Black");
            WRT.WR("Press ", "DarkGray", "Black");
            WRT.WR("[ENTER]", "White", "Black");
            WRT.WR(" or the active listening key ", "DarkGray", "Black");
            WRT.WR("[", "White", "Black");
            WRT.WR(Global.VB_LISTENCONTROLKEY, "Green", "Black");
            WRT.WR("]", "White", "Black");
            WRT.WRLine(".", "DarkGray", "Black");
            Keys tmpControlKey = (Keys)char.ToUpper(Global.VB_LISTENCONTROLKEY[0]);
            bool tmpLoopHandle = true;
            TI.Hold(300);
            while (tmpLoopHandle)
            {
                if (KeyboardListen.KeyCheck(tmpControlKey) || (KeyboardListen.KeyCheck(Keys.Enter)))
                {
                    tmpLoopHandle = false;
                }
            }
         }
        public static void AltInput(string altIn)
        {
            if (altIn.ToLower() == "$credit")
            {
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" KasTAS " + Global.version + " (C) 2022 by Owain Horton / Karst Skarn [Owain#3593]", "Green", "Black");
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
                WRT.WR(" GitHub Project URL: ", "Green", "Black");
                WRT.WR("https://github.com/KarstSkarn/KasTAS", "Blue", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" Special thanks to Miepee ", "Green", "Black");
                WRT.WR("https://github.com/Miepee", "Blue", "Black");
                Console.ReadKey();
                GUI.DrawClearBuffer();
                Console.SetCursorPosition(0, Global.windowH - 3);
                GUI.DrawLowerBarSpacer();
                WRT.WR(" T", "Blue", "Black");
                WRT.WR("h", "Green", "Black");
                WRT.WR("a", "Cyan", "Black");
                WRT.WR("n", "Red", "Black");
                WRT.WR("k", "Magenta", "Black");
                WRT.WR("s", "Yellow", "Black");
                WRT.WR("!.", "White", "Black");
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
                    Global.KASScriptExecuteMode = true;
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
                    Global.KASScriptExecuteMode = true;
                    EXF.ExecuteKAS();
                }
            }
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
            if (Global.sysConfig == true)
            {
                Global.tmpUserInput = EXF.StaticPrompt();
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "d"))
                {
                    Global.tmpWritePad = "00 SYS TARGET   = DUAL";
                    EXF.WriteToKeyCfg(0, Global.tmpWritePad);
                    return;
                }
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "q"))
                {
                    Global.tmpWritePad = "00 SYS TARGET   = QUAD";
                    EXF.WriteToKeyCfg(0, Global.tmpWritePad);
                    return;
                }
                if ((Global.tmpUserInput == "3") || (Global.tmpUserInput.ToLower() == "t"))
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
                    if ((Global.tmpUserInput == "4") || (Global.tmpUserInput.ToLower() == "e"))
                {
                    Global.sysConfig = false;
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
                if ((Global.tmpUserInput == "0") || (Global.tmpUserInput.ToLower() == "u"))
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
                else if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "d"))
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
                else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "l"))
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
                else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput.ToLower() == "r"))
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
                else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput.ToLower() == "st"))
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
                else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput.ToLower() == "se"))
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
                else if ((Global.tmpUserInput == "6") || (Global.tmpUserInput.ToLower() == "a"))
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
                else if ((Global.tmpUserInput == "7") || (Global.tmpUserInput.ToLower() == "b"))
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
                else if ((Global.tmpUserInput == "8") || (Global.tmpUserInput.ToLower() == "x"))
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
                else if ((Global.tmpUserInput == "9") || (Global.tmpUserInput.ToLower() == "y"))
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
                else if ((Global.tmpUserInput == "11") || (Global.tmpUserInput.ToLower() == "bl"))
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
                else if ((Global.tmpUserInput == "12") || (Global.tmpUserInput.ToLower() == "br"))
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
                else if ((Global.tmpUserInput == "13") || (Global.tmpUserInput.ToLower() == "e0"))
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
                else if ((Global.tmpUserInput == "14") || (Global.tmpUserInput.ToLower() == "e1"))
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
                else if ((Global.tmpUserInput == "15") || (Global.tmpUserInput.ToLower() == "e2"))
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
                else if ((Global.tmpUserInput == "16") || (Global.tmpUserInput.ToLower() == "e3"))
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
                else if ((Global.tmpUserInput == "17") || (Global.tmpUserInput.ToLower() == "e4"))
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
                else if ((Global.tmpUserInput == "18") || (Global.tmpUserInput.ToLower() == "e5"))
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
                else if ((Global.tmpUserInput == "19") || (Global.tmpUserInput.ToLower() == "e6"))
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
                else if ((Global.tmpUserInput == "20") || (Global.tmpUserInput.ToLower() == "e7"))
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
                else if ((Global.tmpUserInput == "21") || (Global.tmpUserInput.ToLower() == "e8"))
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
                else if ((Global.tmpUserInput == "22") || (Global.tmpUserInput.ToLower() == "e9"))
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
                else if ((Global.tmpUserInput == "10") || (Global.tmpUserInput.ToLower() == "e"))
                {
                    GUI.DrawClearBuffer();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WRLine(" Exiting Key Configuration Mode...", "DarkGray", "Black");
                    Global.configKeysMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                }
                else if ((Global.tmpUserInput == "69") || (Global.tmpUserInput.ToLower() == "bgb"))
                {
                    // This presses the virtual keys in the same order than the BGB Emulator allows them to be configured.
                    // So it solves a little time.
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
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "i"))
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
                else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "c"))
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
                else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput.ToLower() == "f"))
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
                else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput.ToLower() == "h"))
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
                else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput.ToLower() == "e"))
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
            while (Global.KASScriptReadMode)
            {
                if (Global.restartArea == false)
                {
                    Global.tmpUserInput = EXF.StaticPrompt();
                }
                else
                {
                    EXF.ReadKAS();
                }
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "l"))
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
                                Global.tmpReadKindOfOP = "NULL";
                                Global.PV_READGROUP = "NULL";
                                if (tmpSubString == "PB") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "CO"; }
                                if (tmpSubString == "RB") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "CO"; }
                                if (tmpSubString == "JT") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "JT"; }
                                if (tmpSubString == "SP") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "SP"; }
                                if (tmpSubString == "RA") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "CO"; }
                                if (tmpSubString == "JM") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "JU"; }
                                if (tmpSubString == "HW") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "RE") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "RE"; }
                                if (tmpSubString == "EN") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "EN"; }
                                if (tmpSubString == "FP") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "SP"; }
                                if (tmpSubString == "TP") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "SP"; }
                                if (tmpSubString == "KS") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "KS"; }
                                if (tmpSubString == "SM") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "CM") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "SR") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "SW") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "DM") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "TI"; }
                                if (tmpSubString == "WR") { Global.scriptCodeValid = true; Global.tmpReadKindOfOP = "RM"; }
                                if (Global.scriptCodeValid == true)
                                {
                                    // Draw on screen the first half of the OpCode.
                                    if (Global.tmpReadKindOfOP == "KS") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpReadKindOfOP == "SP") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpReadKindOfOP == "CO") { WRT.WR(tmpSubString, "Yellow", "Black"); }
                                    if (Global.tmpReadKindOfOP == "JU") { WRT.WR(tmpSubString, "Magenta", "Black"); }
                                    if (Global.tmpReadKindOfOP == "JT") { WRT.WR(tmpSubString, "Magenta", "Black"); }
                                    if (Global.tmpReadKindOfOP == "TI") { WRT.WR(tmpSubString, "Blue", "Black"); }
                                    if (Global.tmpReadKindOfOP == "RE") { WRT.WR(tmpSubString, "DarkMagenta", "Black"); }
                                    if (Global.tmpReadKindOfOP == "EN") { WRT.WR(tmpSubString, "White", "Blue"); }
                                    if (Global.tmpReadKindOfOP == "RM") { WRT.WR(tmpSubString, "Cyan", "DarkMagenta"); }
                                }
                                else
                                {
                                    // Error: Not a valid OpCode.
                                    Global.tmpReadErrorFound++;
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "Red", "Black");
                                }
                                if (Global.tmpReadKindOfOP == "KS")
                                {
                                    // OpCode is a Key String.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkGreen", "Black");
                                }
                                if (Global.tmpReadKindOfOP == "EN")
                                {
                                    // OpCode is an end OpCode.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkMagenta", "Black");
                                }
                                if (Global.tmpReadKindOfOP == "RE")
                                {
                                    // OpCode is a Script Commentary/Rem.
                                    int tmpLateSubstring = tmpFileData.Length - 2;
                                    tmpSubString = tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(tmpSubString, "DarkMagenta", "Black");
                                }
                                // Parse next 2 XX YY of the OpCode
                                if ((tmpFileData.Length >= 5) && (Global.tmpReadKindOfOP != "RE"))
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
                                    if ((Global.tmpReadKindOfOP == "CO") || (Global.tmpReadKindOfOP == "SP"))
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
                                        if (tmpSubString == "XX") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "YY") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "BL") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "BR") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E0") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E1") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E2") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E3") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E4") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E5") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E6") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E7") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E8") { Global.scriptCodeValid = true; }
                                        if (tmpSubString == "E9") { Global.scriptCodeValid = true; }
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
                                            if (Global.tmpReadKindOfOP == "CO")
                                            {
                                                // Error: A CO type OpCode shouldn't have more characters.
                                                int tmpLateSubstring = tmpFileData.Length - 5;
                                                tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(tmpSubString, "Red", "Black");
                                            }
                                            if (Global.tmpReadKindOfOP == "SP")
                                            {
                                                // Get SoftPush OpCode time value.
                                                int tmpLateSubstring = tmpFileData.Length - 5;
                                                tmpSubString = tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(tmpSubString, "Cyan", "Black");
                                            }
                                        }
                                    }
                                    else if (Global.tmpReadKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(tmpSubString, "White", "Black");
                                    }
                                    else if (Global.tmpReadKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(tmpSubString, "Cyan", "Black");
                                    }
                                    else if (Global.tmpReadKindOfOP == "JT")
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
                                else if ((tmpFileData.Length >= 3) && (tmpFileData.Length <= 5) && (Global.tmpReadKindOfOP != "RE"))
                                {
                                    // Special case for small value OpCodes.
                                    if (Global.tmpReadKindOfOP == "JU")
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(tmpSubString, "White", "Black");
                                    }
                                    else if (Global.tmpReadKindOfOP == "TI")
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = tmpFileData.Length - 3;
                                        tmpSubString = tmpFileData.Substring(3, tmpLateSubstring);
                                        WRT.WR(" ", "White", "Black");
                                        WRT.WR(tmpSubString, "Cyan", "Black");
                                    }
                                    else if (Global.tmpReadKindOfOP == "JT")
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
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "e"))
                {
                    Global.KASScriptReadMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                    // WRT.WR(" Leaving Read Menu...", "White", "Black");
                }
            } // Active While End   
        }
        public static void ExecuteKAS()
        {
            GUI.DrawExecuteMenu();
            Global.restartArea = false;
            while (Global.KASScriptExecuteMode)
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
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "l"))
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
                    GUI.DrawSubtitle("KAS Script Execution");
                    GUI.DrawSpacer();
                    WRT.WR(" Filename: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(Global.tmpUserInput), "Cyan", "Black");
                    WRT.WR(" Total Lines: ", "White", "Black");
                    WRT.WRLine(Convert.ToString(tmpFileSize), "Green", "Black");
                    WRT.WR(" File Size: ", "White", "Black");
                    WRT.WR(Convert.ToString(tmpFileWeight), "Green", "Black");
                    WRT.WRLine(" Bytes", "DarkGray", "Black");
                    GUI.DrawSpacer();
                    EXF.RemoteExecutionHalt();
                    Console.CursorVisible = false;
                    Keys tmpExecutionKey = (Keys)char.ToUpper(Global.VB_LISTENEXECUTIONKEY[0]);
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
                            Global.scriptCodeValid = true;
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
                                Global.tmpKindOfOP = OP_States.None;
                                Global.PV_GROUP = PV_GROUPStates.None;
                                Global.tmpOPCache = "NULL";
                                switch (Global.tmpSubstring)
                                {
                                    case "PB":
                                    case "RB":
                                    case "RA":
                                        Global.tmpKindOfOP = OP_States.CO;
                                        break;
                                    case "JT":
                                        Global.tmpKindOfOP = OP_States.JT;
                                        break;
                                    case "SP":
                                    case "FP":
                                    case "TP":
                                        Global.tmpKindOfOP = OP_States.SP;
                                        break;
                                    case "JM":
                                        Global.tmpKindOfOP = OP_States.JU;
                                        break;
                                    case "HW":
                                    case "SW":
                                    case "SR":
                                    case "SM":
                                    case "CM":
                                    case "DM":
                                        Global.tmpKindOfOP = OP_States.TI;
                                        break;
                                    case "RE":
                                        Global.tmpKindOfOP = OP_States.RE;
                                        break;
                                    case "EN":
                                        Global.tmpKindOfOP = OP_States.EN;
                                        break;
                                    case "KS":
                                        Global.tmpKindOfOP = OP_States.KS;
                                        break;
                                    case "WR":
                                        Global.tmpKindOfOP = OP_States.RM;
                                        break;

                                    default:
                                        Global.scriptCodeValid = false;
                                        break;
                                }

                                if (Global.scriptCodeValid)
                                {
                                    // Draw on screen the first half of the OpCode.
                                    switch (Global.tmpKindOfOP)
                                    {
                                        case OP_States.SP:
                                        case OP_States.CO:
                                        case OP_States.KS:
                                            WRT.WR(Global.tmpSubstring, "Yellow", "Black");
                                            break;
                                        case OP_States.JU:
                                        case OP_States.JT:
                                            WRT.WR(Global.tmpSubstring, "Magenta", "Black");
                                            break;
                                        case OP_States.TI:
                                            WRT.WR(Global.tmpSubstring, "Blue", "Black");
                                            break;
                                        case OP_States.RE:
                                            WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black");
                                            break;
                                        case OP_States.EN:
                                            WRT.WR(Global.tmpSubstring, "White", "Blue");
                                            break;
                                        case OP_States.RM:
                                            WRT.WR(Global.tmpSubstring, "Cyan", "DarkMagenta");
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
                                    WRT.WR(Global.tmpSubstring, "Red", "Black");
                                }
                                // Small OPCodes Special Identification Area
                                switch (Global.tmpKindOfOP)
                                {
                                    // OpCode is a Special Key String.
                                    case OP_States.KS:
                                        {
                                            int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                            Global.tmpOPValueCache = Global.tmpSubstring;
                                            WRT.WR(Global.tmpSubstring, "DarkGreen", "Black");
                                            break;
                                        }
                                    // OpCode is a Script Commentary/Rem.
                                    case OP_States.RE:
                                        {
                                            // OpCode is a Script Commentary/Rem.
                                            int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                            WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black");
                                            break;
                                        }
                                    // OpCode is an end OpCode.
                                    case OP_States.EN:
                                        {
                                            // OpCode is an end OpCode.
                                            int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                            Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                            WRT.WR(Global.tmpSubstring, "DarkMagenta", "Black");
                                            break;
                                        }
                                }
                                // Small OpCodes Special Drawing
                                if (Global.tmpFileData.Length <= 3)
                                {
                                    int tmpLateSubstring = Global.tmpFileData.Length - 2;
                                    Global.tmpSubstring = Global.tmpFileData.Substring(2, tmpLateSubstring);
                                    WRT.WR(Global.tmpSubstring, "Red", "Black");
                                }
                                // Parse next 2 XX YY of the OpCode
                                if ((Global.tmpFileData.Length >= 5) && (Global.tmpKindOfOP != OP_States.RE) && (Global.tmpKindOfOP != OP_States.KS))
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
                                    if ((Global.tmpKindOfOP == OP_States.CO) || (Global.tmpKindOfOP == OP_States.SP))
                                    {
                                        // OpCode is a button control OpCode.
                                        Global.scriptCodeValid = false;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, 2);
                                        switch (Global.tmpSubstring)
                                        {
                                            case "AA":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.AA;
                                                break;
                                            case "BB":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.BB;
                                                break;
                                            case "ST":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.ST;
                                                break;
                                            case "SE":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.SE;
                                                break;
                                            case "UP":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.UP;
                                                break;
                                            case "DO":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.DO;
                                                break;
                                            case "LE":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.LE;
                                                break;
                                            case "RI":
                                                Global.scriptCodeValid = true;
                                                Global.PV_GROUP = PV_GROUPStates.RI;
                                                break;
                                        }
                                        if (Global.sysTarget == "QUAD")
                                        {
                                            switch (Global.tmpSubstring)
                                            {
                                                case "XX":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.XX;
                                                    break;
                                                case "YY":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.YY;
                                                    break;
                                                case "BL":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.BL;
                                                    break;
                                                case "BR":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.BR;
                                                    break;
                                            }
                                        }
                                        if (Global.flagExtMode == "true")
                                        {
                                            switch (Global.tmpSubstring)
                                            {
                                                case "E0":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E0;
                                                    break;
                                                case "E1":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E1;
                                                    break;
                                                case "E2":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E2;
                                                    break;
                                                case "E3":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E3;
                                                    break;
                                                case "E4":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E4;
                                                    break;
                                                case "E5":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E5;
                                                    break;
                                                case "E6":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E6;
                                                    break;
                                                case "E7":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E7;
                                                    break;
                                                case "E8":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E8;
                                                    break;
                                                case "E9":
                                                    Global.scriptCodeValid = true;
                                                    Global.PV_GROUP = PV_GROUPStates.E9;
                                                    break;
                                            }
                                        }
                                        if (Global.scriptCodeValid)
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
                                            if (Global.tmpKindOfOP == OP_States.CO)
                                            {
                                                // Error: A CO type OpCode shouldn't have more characters.
                                                int tmpLateSubstring = Global.tmpFileData.Length - 5;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(5, tmpLateSubstring);
                                                WRT.WR(Global.tmpSubstring, "Red", "Black");
                                            }
                                            if (Global.tmpKindOfOP == OP_States.SP)
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
                                    else if (Global.tmpKindOfOP == OP_States.JU)
                                    {
                                        // OpCode is a Jump OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(Global.tmpSubstring, "White", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == OP_States.TI)
                                    {
                                        // OpCode is a Time/Wait OpCode.
                                        int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                        Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                        Global.tmpOPValueCache = Global.tmpSubstring;
                                        WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                    }
                                    else if (Global.tmpKindOfOP == OP_States.JT)
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
                                else if (((Global.tmpFileData.Length > 3) && (Global.tmpFileData.Length < 5)) && (Global.tmpKindOfOP != OP_States.RE))
                                {
                                    // Special case for small value OpCodes
                                    switch (Global.tmpKindOfOP)
                                    {
                                        case OP_States.JU:
                                            {
                                                // OpCode is a Jump OpCode.
                                                int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                                Global.tmpOPValueCache = Global.tmpSubstring;
                                                WRT.WR(" ", "White", "Black");
                                                WRT.WR(Global.tmpSubstring, "White", "Black");
                                                break;
                                            }
                                        case OP_States.TI:
                                            {
                                                // OpCode is a Time/Wait OpCode.
                                                int tmpLateSubstring = Global.tmpFileData.Length - 3;
                                                Global.tmpSubstring = Global.tmpFileData.Substring(3, tmpLateSubstring);
                                                Global.tmpOPValueCache = Global.tmpSubstring;
                                                WRT.WR(" ", "White", "Black");
                                                WRT.WR(Global.tmpSubstring, "Cyan", "Black");
                                                break;
                                            }
                                        case OP_States.JT:
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
                                                break;
                                            }
                                    }
                                }
                            }
                            // Draw close bar.
                            Global.tmpWritePad = "| ";
                            Global.tmpWritePad = (Global.tmpWritePad).PadLeft((31 - Global.tmpFileData.Length), Global.charSpace);
                            WRT.WR(Global.tmpWritePad, "DarkMagenta", "Black");
                            // Execute readed OpCodes.
                            switch (Global.tmpOPCache)
                            {
                                // PB - Push Button.
                                case "PB":
                                    {
                                        VK.VK_Down(Global.PV_GROUP);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // RB - Release Button.
                                case "RB":
                                    {
                                        VK.VK_Up(Global.PV_GROUP);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // RA - Release all buttons.
                                case "RA":
                                    {
                                        VK.VK_Up(Global.VB_A);
                                        VK.VK_Up(Global.VB_B);
                                        VK.VK_Up(Global.VB_ST);
                                        VK.VK_Up(Global.VB_SE);
                                        VK.VK_Up(Global.VB_UP);
                                        VK.VK_Up(Global.VB_DO);
                                        VK.VK_Up(Global.VB_LE);
                                        VK.VK_Up(Global.VB_RI);
                                        if (Global.sysTarget == "QUAD")
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
                                        break;
                                    }
                                // HW - Hold/Wait a given time.
                                case "HW":
                                    {
                                        TI.Hold(Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // JM - Absolute Jump / Infinite Loop to a given line.
                                case "JM":
                                    {
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpOPValueCache);
                                        break;
                                    }
                                // JT - Jump X Times to a given line.
                                case "JT":
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
                                        break;
                                    }
                                // RE - Comment / REM
                                case "RE":
                                    {
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // SP - Soft Push a button a given time
                                case "SP":
                                    {
                                        VK.VK_HoldUntilDelay(Global.PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // FP - Fast Push a button a given time.
                                case "FP":
                                    {
                                        VK.VK_HoldUntil(Global.PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // TP - Turbo-Pushes a button a given time.
                                case "TP":
                                    {
                                        VK.VK_HoldTurbo(Global.PV_GROUP, Convert.ToInt32(Global.tmpOPValueCache));
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // KS - Attempts to push a Key String.
                                case "KS":
                                    {
                                        VK.VK_HoldUntilDelay(Global.tmpOPValueCache, 50);
                                        Global.tmpLineCursor = Convert.ToInt32(Global.tmpLineCursor);
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // SW - Sync Waits until there's no delay.
                                case "SW":
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
                                        break;
                                    }
                                // SR - Resets the Sync Delay Stack.
                                case "SR":
                                    {
                                        Global.tmpStackDelay = 0;
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // DM - Displays marker time.
                                case "DM":
                                    {
                                        Global.tmpStackStatus = 3;
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // SM - Sets a new marker and resets the marker counter.
                                case "SM":
                                    {
                                        Global.tmpMarkerMillis = 0;
                                        Global.tmpLineCursor++;
                                        break;
                                    }
                                // CM - Waits if the time marker is lower than the value.
                                case "CM":
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
                                        break;
                                    }
                                // EN - Ends the script execution.
                                case "EN":
                                    {
                                        Global.tmpLineCursor = tmpFileSize + 1;
                                        break;
                                    }
                                // WR - Wait for Remote execution.
                                case "WR":
                                    {
                                        Keys tmpControlKey = (Keys)char.ToUpper(Global.VB_LISTENCONTROLKEY[0]);
                                        bool tmpRemoteFlag = false;
                                        while (!tmpRemoteFlag)
                                        {
                                            if (KeyboardListen.KeyCheck(tmpExecutionKey))
                                            {
                                                Global.tmpLineCursor = tmpFileSize + 1;
                                                tmpRemoteFlag = true;
                                            }
                                            if (KeyboardListen.KeyCheck(tmpControlKey))
                                            {
                                                tmpRemoteFlag = true;
                                            }
                                        }
                                        Global.tmpLineCursor++;
                                        break;
                                    }
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
                            if (!Global.tmpIsParsed) { Global.tmpOPValueCache = "0"; }
                            // Sync count all delay.
                            switch (Global.tmpOPCache)
                            {
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
                                switch (Global.tmpKindOfOP)
                                {
                                    case OP_States.TI:
                                    case OP_States.SP:
                                        Global.tmpStackDelay += (Global.tmpDeltaMillis - Convert.ToInt32(Global.tmpOPValueCache));
                                        break;
                                    default:
                                        Global.tmpStackDelay += Global.tmpDeltaMillis;
                                        break;
                                }
                            }
                            if (Global.tmpStackDelay < 0) { Global.tmpStackDelay = 0; }
                            Global.tmpStackException = false;
                            Global.tmpMarkerMillis += Global.tmpDeltaMillis;
                            WRT.WR(Convert.ToString(Global.tmpDeltaMillis), "White", "Black");
                            WRT.WR(" ▲ms", "White", "Black");
                            switch (Global.tmpStackStatus)
                            {
                                case 1:
                                    WRT.WR(" (" + Global.tmpStackMem + "ms)", "Green", "Black"); 
                                    Global.tmpStackStatus = 0;
                                    break;
                                case 2:
                                    WRT.WR(" (" + Global.tmpStackMem + "ms)", "Red", "Black"); 
                                    Global.tmpStackStatus = 0;
                                    break;
                                case 3:
                                    WRT.WR(" (" + Global.tmpMarkerMillis + "ms)", "White", "Black"); 
                                    Global.tmpStackStatus = 0;
                                    break;
                                case 4:
                                    WRT.WR(" (" + Global.tmpMarkerDifference + "ms)", "Green", "Black"); 
                                    Global.tmpStackStatus = 0;
                                    break;
                                case 5:
                                    WRT.WR(" (0 ms)", "White", "Black"); 
                                    Global.tmpStackStatus = 0;
                                    break;
                            }
                            WRT.WRLine(" ", "White", "Black"); // Intro endline.
                            if (KeyboardListen.KeyCheck(tmpExecutionKey))
                            {
                                Global.tmpLineCursor = tmpFileSize + 1; // End execution if execution key is pressed
                            }
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
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "e"))
                {
                    Global.KASScriptExecuteMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                    // WRT.WRLine(" Leaving Script Execution...", "White", "Black");
                }
            } // Active While End
        }
        public static void GenerateKAS()
        {
            GUI.DrawGenerateMenu();
            Global.restartArea = false;
            while(Global.KASScriptGenerateMode == true)
            {
                if (Global.restartArea == false)
                {
                    Global.tmpUserInput = EXF.StaticPrompt();
                }
                else
                {
                    EXF.GenerateKAS();
                }
                if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "g"))
                {
                    GUI.DrawGenFileMenu();
                    Global.tmpUserInput = EXF.FileStaticPrompt();
                    Global.lastFileLoaded = Global.tmpUserInput;
                    Global.tmpWritePad = "00 LAST FILE    = " + Global.lastFileLoaded;
                    EXF.WriteToCfg(0, Global.tmpWritePad);
                    // Header Line
                    string tmpOutString = "RE - KAS Script File -";
                    File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                    WRT.WR(tmpOutString, "DarkMagenta", "Black");
                    System.DateTime tmpDateTime = System.DateTime.Now;
                    string tmpTimeString = tmpDateTime.Day + "/" + tmpDateTime.Month + "/" + tmpDateTime.Year + " " + tmpDateTime.Hour + ":" + tmpDateTime.Minute + ":" + tmpDateTime.Second;
                    tmpOutString = "RE " + tmpTimeString;
                    // Creation Time Line
                    File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                    GC.Collect();
                    // Initialize temporary key values
                    bool tmpStartFlag = false;
                    bool tmpChangesFlag = false;
                    int tmpTotalKeystrokes = 0;
                    int tmpOldMillis = 0;
                    int tmpCalc = 0;
                    int tmpMarkerCounter = 0;
                    Keys[] tmpKeyArray = new Keys[22];
                    bool[] tmpKeyBoolArray = new bool[22];
                    string[] tmpKeyStringArray = new string[22];
                    int tmpKeyArrayLimit = 7;
                    EXF.ResetKeyboardBooleans();
                    Keys tmpControlKey = (Keys)char.ToUpper(Global.VB_LISTENCONTROLKEY[0]);
                    Keys tmpMarkerKey = (Keys)char.ToUpper(Global.VB_LISTENMARKERKEY[0]);
                    Keys tmpAKey = (Keys)char.ToUpper(Global.VB_A[0]);
                    Keys tmpBKey = (Keys)char.ToUpper(Global.VB_B[0]);
                    Keys tmpSTKey = (Keys)char.ToUpper(Global.VB_ST[0]);
                    Keys tmpSEKey = (Keys)char.ToUpper(Global.VB_SE[0]);
                    Keys tmpUPKey = (Keys)char.ToUpper(Global.VB_UP[0]);
                    Keys tmpDOKey = (Keys)char.ToUpper(Global.VB_DO[0]);
                    Keys tmpLEKey = (Keys)char.ToUpper(Global.VB_LE[0]);
                    Keys tmpRIKey = (Keys)char.ToUpper(Global.VB_RI[0]);
                    tmpKeyArray[0] = tmpAKey;
                    tmpKeyArray[1] = tmpBKey;
                    tmpKeyArray[2] = tmpSTKey;
                    tmpKeyArray[3] = tmpSEKey;
                    tmpKeyArray[4] = tmpUPKey;
                    tmpKeyArray[5] = tmpDOKey;
                    tmpKeyArray[6] = tmpLEKey;
                    tmpKeyArray[7] = tmpRIKey;
                    tmpKeyBoolArray[0] = Global.L_A;
                    tmpKeyBoolArray[1] = Global.L_B;
                    tmpKeyBoolArray[2] = Global.L_ST;
                    tmpKeyBoolArray[3] = Global.L_SE;
                    tmpKeyBoolArray[4] = Global.L_UP;
                    tmpKeyBoolArray[5] = Global.L_DO;
                    tmpKeyBoolArray[6] = Global.L_LE;
                    tmpKeyBoolArray[7] = Global.L_RI;
                    tmpKeyStringArray[0] = "AA";
                    tmpKeyStringArray[1] = "BB";
                    tmpKeyStringArray[2] = "ST";
                    tmpKeyStringArray[3] = "SE";
                    tmpKeyStringArray[4] = "UP";
                    tmpKeyStringArray[5] = "DO";
                    tmpKeyStringArray[6] = "LE";
                    tmpKeyStringArray[7] = "RI";
                    if (Global.sysTarget == "QUAD")
                    {
                        Keys tmpXKey = (Keys)char.ToUpper(Global.VB_X[0]);
                        Keys tmpYKey = (Keys)char.ToUpper(Global.VB_Y[0]);
                        Keys tmpBLKey = (Keys)char.ToUpper(Global.VB_BL[0]);
                        Keys tmpBRKey = (Keys)char.ToUpper(Global.VB_BR[0]);
                        tmpKeyArray[8] = tmpXKey;
                        tmpKeyArray[9] = tmpYKey;
                        tmpKeyArray[10] = tmpBLKey;
                        tmpKeyArray[11] = tmpBRKey;
                        tmpKeyBoolArray[8] = Global.L_X;
                        tmpKeyBoolArray[9] = Global.L_Y;
                        tmpKeyBoolArray[10] = Global.L_BL;
                        tmpKeyBoolArray[11] = Global.L_BR;
                        tmpKeyStringArray[8] = "XX";
                        tmpKeyStringArray[9] = "YY";
                        tmpKeyStringArray[10] = "BL";
                        tmpKeyStringArray[11] = "BR";
                        tmpKeyArrayLimit = 11;
                    }
                    if (Global.flagExtMode == "true")
                    {
                        Keys tmpE0Key = (Keys)char.ToUpper(Global.VB_E0[0]);
                        Keys tmpE1Key = (Keys)char.ToUpper(Global.VB_E1[0]);
                        Keys tmpE2Key = (Keys)char.ToUpper(Global.VB_E2[0]);
                        Keys tmpE3Key = (Keys)char.ToUpper(Global.VB_E3[0]);
                        Keys tmpE4Key = (Keys)char.ToUpper(Global.VB_E4[0]);
                        Keys tmpE5Key = (Keys)char.ToUpper(Global.VB_E5[0]);
                        Keys tmpE6Key = (Keys)char.ToUpper(Global.VB_E6[0]);
                        Keys tmpE7Key = (Keys)char.ToUpper(Global.VB_E7[0]);
                        Keys tmpE8Key = (Keys)char.ToUpper(Global.VB_E8[0]);
                        Keys tmpE9Key = (Keys)char.ToUpper(Global.VB_E9[0]);
                        tmpKeyArray[12] = tmpE0Key;
                        tmpKeyArray[13] = tmpE1Key;
                        tmpKeyArray[14] = tmpE2Key;
                        tmpKeyArray[15] = tmpE3Key;
                        tmpKeyArray[16] = tmpE4Key;
                        tmpKeyArray[17] = tmpE5Key;
                        tmpKeyArray[18] = tmpE6Key;
                        tmpKeyArray[19] = tmpE7Key;
                        tmpKeyArray[20] = tmpE8Key;
                        tmpKeyArray[21] = tmpE9Key;
                        tmpKeyBoolArray[12] = Global.L_E0;
                        tmpKeyBoolArray[13] = Global.L_E1;
                        tmpKeyBoolArray[14] = Global.L_E2;
                        tmpKeyBoolArray[15] = Global.L_E3;
                        tmpKeyBoolArray[16] = Global.L_E4;
                        tmpKeyBoolArray[17] = Global.L_E5;
                        tmpKeyBoolArray[18] = Global.L_E6;
                        tmpKeyBoolArray[19] = Global.L_E7;
                        tmpKeyBoolArray[20] = Global.L_E8;
                        tmpKeyBoolArray[21] = Global.L_E9;
                        tmpKeyStringArray[12] = "E0";
                        tmpKeyStringArray[13] = "E1";
                        tmpKeyStringArray[14] = "E2";
                        tmpKeyStringArray[15] = "E3";
                        tmpKeyStringArray[16] = "E4";
                        tmpKeyStringArray[17] = "E5";
                        tmpKeyStringArray[18] = "E6";
                        tmpKeyStringArray[19] = "E7";
                        tmpKeyStringArray[20] = "E8";
                        tmpKeyStringArray[21] = "E9";
                        tmpKeyArrayLimit = 21;
                    }
                    GUI.DrawGenFilePanel();
                    Console.SetCursorPosition(0, Global.windowH - 3);
                    GUI.DrawLowerBarSpacer();
                    WRT.WR(" ▬ ", "Cyan", "Black");
                    WRT.WR("Recording status is [", "White", "Black");
                    WRT.WR("OFF", "DarkRed", "Black");
                    WRT.WR("]", "White", "Black");
                    WRT.WR(" | Total events registered: [", "White", "Black");
                    WRT.WR(tmpTotalKeystrokes.ToString(), "Cyan", "Black");
                    WRT.WRLine("]", "White", "Black");
                    while (!tmpStartFlag)
                    {
                        if(KeyboardListen.KeyCheck(tmpControlKey))
                        {
                            // Start Milliseconds Control Counter.
                            Global.tmpMarkerMillis = 0;
                            Global.SW.Start();
                            // Stop in case it was already running from another execution.
                            Global.SW.Stop();
                            Global.SW.Start();
                            Global.SW.Restart();
                            Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                            // Recording Start Line
                            tmpOutString = "RE Recording Started";
                            File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                            // Recording Start Time Line
                            tmpDateTime = System.DateTime.Now;
                            tmpTimeString = tmpDateTime.Day + "/" + tmpDateTime.Month + "/" + tmpDateTime.Year + " " + tmpDateTime.Hour + ":" + tmpDateTime.Minute + ":" + tmpDateTime.Second;
                            tmpOutString = "RE " + tmpTimeString;
                            File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                            Console.SetCursorPosition(0, Global.windowH - 3);
                            GUI.DrawLowerBarSpacer();
                            WRT.WR(" ▬ ", "Cyan", "Black");
                            WRT.WR("Recording status is [", "White", "Black");
                            WRT.WR("ON", "Green", "Black");
                            WRT.WR("]", "White", "Black");
                            WRT.WR(" | Total events registered: [", "White", "Black");
                            WRT.WR(tmpTotalKeystrokes.ToString(), "Cyan", "Black");
                            WRT.WRLine("]   ", "White", "Black");
                            tmpStartFlag = true;
                        }
                    }
                    while (tmpStartFlag)
                    {
                        tmpChangesFlag = false;
                        // Stop Control Key
                        if (KeyboardListen.KeyCheck(tmpControlKey))
                        {
                            Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                            if (Global.tmpLastMillis >= 500)
                            {
                                tmpStartFlag = false; // End the recording sequence
                            }
                        }
                        // Insert Marker Key
                        if (KeyboardListen.KeyCheck(tmpMarkerKey))
                        {
                            if (!Global.L_MARKERKEY)
                            {
                                tmpTotalKeystrokes++;
                                tmpOutString = "RE - MARKER #" + tmpMarkerCounter + " -";
                                File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                                tmpMarkerCounter++;
                                Global.L_MARKERKEY = true;
                            }
                        }
                        else
                        {
                            Global.L_MARKERKEY = false;
                        }
                        // ?
                        for (int i = 0; i <= tmpKeyArrayLimit; i++)
                        {
                            if (KeyboardListen.KeyCheck(tmpKeyArray[i]))
                            {
                                if (!tmpKeyBoolArray[i])
                                {
                                    tmpChangesFlag = true;
                                    tmpTotalKeystrokes++;
                                    Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                                    if (Global.tmpLastMillis > tmpOldMillis)
                                    {
                                        tmpCalc = Global.tmpLastMillis - tmpOldMillis;
                                        if (tmpCalc > (Global.genCompensationValue + 1)) { tmpCalc -= Global.genCompensationValue; }
                                        tmpOldMillis = Global.tmpLastMillis;
                                        File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, "SW " + tmpCalc + Environment.NewLine);
                                    }
                                    tmpKeyBoolArray[i] = true;
                                    File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, "PB " + tmpKeyStringArray[i] + Environment.NewLine);
                                }
                            }
                            else
                            {
                                if (tmpKeyBoolArray[i])
                                {
                                    tmpChangesFlag = true;
                                    tmpTotalKeystrokes++;
                                    Global.tmpLastMillis = Convert.ToInt32(Global.SW.ElapsedMilliseconds);
                                    if (Global.tmpLastMillis > tmpOldMillis)
                                    {
                                        tmpCalc = Global.tmpLastMillis - tmpOldMillis;
                                        if (tmpCalc > (Global.genCompensationValue + 1)) { tmpCalc -= Global.genCompensationValue; }
                                        tmpOldMillis = Global.tmpLastMillis;
                                        File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, "SW " + tmpCalc + Environment.NewLine);
                                    }
                                    tmpKeyBoolArray[i] = false;
                                    File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, "RB " + tmpKeyStringArray[i] + Environment.NewLine);
                                }
                            }
                        }
                        if (tmpChangesFlag)
                        {
                            Console.SetCursorPosition(0, Global.windowH - 3);
                            GUI.DrawLowerBarSpacer();
                            WRT.WR(" ▬ ", "Cyan", "Black");
                            WRT.WR("Recording status is [", "White", "Black");
                            WRT.WR("ON", "Green", "Black");
                            WRT.WR("]", "White", "Black");
                            WRT.WR(" | Total events registered: [", "White", "Black");
                            WRT.WR(tmpTotalKeystrokes.ToString(), "Cyan", "Black");
                            WRT.WRLine("]", "White", "Black");
                            if (Global.genDelayStackCounter > 0)
                            {
                                if ((tmpTotalKeystrokes % Global.genDelayStackCounter) == 0) // Insert Sync Resets
                                {
                                    tmpOutString = "SR";
                                    File.AppendAllText(Global.scriptsDirectory + Global.tmpUserInput, tmpOutString + Environment.NewLine);
                                }
                            }
                        }
                    } // Active Generation While End
                    GUI.DrawGenEndFile();
                }
                if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "e"))
                {
                    Global.KASScriptGenerateMode = false;
                    Global.tmpUserInput = "NULL";
                    return;
                } // Menu end
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
            if ((Global.tmpUserInput == "1") || (Global.tmpUserInput.ToLower() == "c"))
            {
                Global.configKeysMode = true;
                EXF.ConfigKeys();
            }
            else if ((Global.tmpUserInput == "2") || (Global.tmpUserInput.ToLower() == "t"))
            {
                Global.testKeysMode = true;
                EXF.TestKeys();
            }
            else if ((Global.tmpUserInput == "3") || (Global.tmpUserInput.ToLower() == "r"))
            {
                Global.KASScriptReadMode = true;
                EXF.ReadKAS();
            }
            else if ((Global.tmpUserInput == "4") || (Global.tmpUserInput.ToLower() == "e"))
            {
                Global.KASScriptExecuteMode = true;
                EXF.ExecuteKAS();
            }
            else if ((Global.tmpUserInput == "5") || (Global.tmpUserInput.ToLower() == "g"))
            {
                Global.KASScriptGenerateMode = true;
                EXF.GenerateKAS();
            }
            else if ((Global.tmpUserInput == "6") || (Global.tmpUserInput.ToLower() == "m"))
            {
                Global.sysConfig = true;
                EXF.ChangeTarget();
            }
            EXF.AltInput(Global.tmpUserInput);
        }
    }
}



