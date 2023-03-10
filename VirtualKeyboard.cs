namespace KasTAS;

// VK Functions ~
public static class VirtualKeyboard
{
    public static void VK_Down(string inKey)
    {
        Keys key = (Keys)Global.KC.ConvertFromString(inKey);
        KeyboardSend.KeyDown(key);
    }

    public static void VK_Down(ExecutionFunctions.PV_GROUPStates pvGroup)
    {
        string stringToSend = GetStringFromPV_Group(pvGroup);
        VK_Down(stringToSend);
    }

    public static void VK_Up(string inKey)
    {
        Keys key = (Keys)Global.KC.ConvertFromString(inKey);
        KeyboardSend.KeyUp(key);
    }

    public static void VK_Up(ExecutionFunctions.PV_GROUPStates pvGroup)
    {
        string stringToSend = GetStringFromPV_Group(pvGroup);
        VK_Up(stringToSend);
    }

    public static void VK_HoldUntil(string inKey, int time)
    {
        Keys key = (Keys)Global.KC.ConvertFromString(inKey);
        KeyboardSend.KeyDown(key);
        Timing.Hold(time);
        KeyboardSend.KeyUp(key);
    }
    public static void VK_HoldUntil(ExecutionFunctions.PV_GROUPStates pvGroup, int time)
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
            Timing.Hold(1);
            KeyboardSend.KeyUp(key);
            Timing.Hold(1);
        }
    }
    public static void VK_HoldTurbo(ExecutionFunctions.PV_GROUPStates pvGroup, int time)
    {
        string stringToSend = GetStringFromPV_Group(pvGroup);
        VK_HoldTurbo(stringToSend, time);
    }
    public static void VK_HoldUntilDelay(string inKey, int time)
    {
        Keys key = (Keys)Global.KC.ConvertFromString(inKey);
        KeyboardSend.KeyDown(key);
        Timing.Hold(time);
        KeyboardSend.KeyUp(key);
        Timing.Hold(25);
    }
    public static void VK_HoldUntilDelay(ExecutionFunctions.PV_GROUPStates pvGroup, int time)
    {
        string stringToSend = GetStringFromPV_Group(pvGroup);
        VK_HoldUntilDelay(stringToSend, time);
    }

    private static string GetStringFromPV_Group(ExecutionFunctions.PV_GROUPStates pvGroup)
    {
        string stringToSend = "";
        //TODO: ugly hotfix. Put all the different buttons in some sort of enumerable and call determine them that way.
        switch (pvGroup)
        {
            case ExecutionFunctions.PV_GROUPStates.None:
                break;
            case ExecutionFunctions.PV_GROUPStates.A:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.B:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.ST:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.SE:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.UP:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.DO:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.LE:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.RI:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.XX:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.YY:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.BL:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.BR:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E0:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E1:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E2:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E3:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E4:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E5:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E6:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E7:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E8:
                stringToSend = Global.VB_A;
                break;
            case ExecutionFunctions.PV_GROUPStates.E9:
                stringToSend = Global.VB_A;
                break;
        }

        return stringToSend;
    }
}