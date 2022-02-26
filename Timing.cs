using System.Runtime.InteropServices;

namespace KasTAS;

// Timing Functions ~
public static class Timing
{
    public static void Hold(int time)
    {
        Thread.Sleep(time);
    }

    //TODO: wont work cross-platform
    [DllImport("winmm.dll", SetLastError = true)]
    public static extern uint timeBeginPeriod(uint uPeriod);

    [DllImport("winmm.dll", SetLastError = true)]
    public static extern uint timeEndPeriod(uint uPeriod);
}