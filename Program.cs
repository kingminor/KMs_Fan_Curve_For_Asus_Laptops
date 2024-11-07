using System;
using King_Minors_Fan_Curve;
using AsusFanControl;

class Program {

    public static int UlongToInt(ulong input) { //Default output is 100C
        int output;
        try {
            output = Convert.ToInt32(input);
            return output;
        }
        catch (OverflowException) {
            Console.WriteLine("Value is too large to fit in an int.");
            return 100;
        }
    }

    public static void Main (string[] args) {
        FanCurve fanCurve = new FanCurve();
        fanCurve.InitializeFanCurve();

        var asusControl = new AsusControl();

        Console.WriteLine("[STARTING FAN CONTROL BASED ON INPUTED CURVE]");
        Console.WriteLine("[FAN SPEED WILL UPDATE EVERY 10 Seconds]");

        while (1 > 0) {
            var ulongCpuTemp = asusControl.Thermal_Read_Cpu_Temperature();
            int cpuTemp = UlongToInt(ulongCpuTemp);
            int fanSpeed = fanCurve.CalculateFanSpeed(cpuTemp);
            asusControl.SetFanSpeeds(fanSpeed);

            var fanRPM = asusControl.GetFanSpeeds();
            Console.WriteLine($"Temp={cpuTemp}, FanSpeed={fanSpeed}. {string.Join(" ", fanRPM)} RPM");
            Thread.Sleep(10000);
        }

    }
}