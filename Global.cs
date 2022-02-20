using System.Diagnostics;

namespace KasTAS;

public static class Global
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
    public static bool flagExtMode = false;

    //TODO: *why* hardcode the total lines of the config file!?
    public static int cfgTotalLines = 12;
    public static int cfgKeyTotalLines = 24;
    public static int cfgValuePos = 18;
    public const string cfgFile = "kascfg.ini";
    public const string cfgKeysFile = "keydata.ini";
    public static int configHoldTime = 7000;
    public static int configReadyTime = 3000;
    public static int testReadyTime = 5000;
    public static int testIncrementalStartTime = 1000;
    public static int testIncrementalReduction = 50;
    public static int testContinousTime = 300;
    public static double testFrameTime = 33.3333;

    public static string lastFileLoaded = "NULL";
    public static string tmpUserInput = " ";
    public static bool scriptCodeValid = false;
    public static bool flagLoadLast = false;
    public static bool flagFirstRun = true;

    //TODO: never used. PV_RESW has a value assigned (9) but is never used in any way.
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

    public static string tmpOPCache = "NULL";
    public static string tmpOPValueCache = "NULL";
    public static string tmpOPAuxValueCache = "NULL";
    public static string tmpPadSpacing = " ";
    public static string tmpWritePad = " ";
    public static string tmpHeader = " ";
    //TODO: also not used
    //public static string tmpHaltKey = " ";
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
    //TODO: both not used
    //public static int tmpAuxPadMov = 0;
    //public static int tmpFileLoadPer = 0;
    public static int tmpStackDelay = 0;
    public static int tmpStackSync = 0;
    public static int tmpStackMem = 0;
    public static int tmpElapsedMillis = 0;
    public static int tmpReadErrorFound = 0;
    public static int tmpMarkerMillis = 0;
    public static int tmpMarkerDifference = 0;
    public static int timeResolution = 1;
    //TODO: not used
    //public static int tmpReadmodePause = 0;
    public static int tmpReadMaxPause = 19;
    public static int tmpLineCursor = 0;
    public static bool tmpStackException = false;
    public static bool tmpIsParsed = false;
    public static byte tmpStackStatus = 0;
    public const char charSpace = ' ';
    public const char charZero = '0';
    public const char charBar = '▄';
    public const char charUpperBar = '▀';
    public static bool restartArea = false;
    public const int WindowHeight = 21;
    public const int WindowWidth = 82;
    public const string Version = "1.0.98";
    public const string scriptsDirectory = "Scripts/";
    public static bool showTwitchTitle = true;
    //TODO: not used
    //public static string flagFirstInit = "true";

    public static KeysConverter KC = new KeysConverter();
    public static Stopwatch SW = Stopwatch.StartNew();
}