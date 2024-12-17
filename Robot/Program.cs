using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Gpio;
using System.Device.Wifi;
using System.Device.Pwm;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Networking;
using System.Reflection;
namespace Robot
{
    public class Program
    {
        private static GpioController s_GpioController;

        public static void Main()
        {
            
            WifiAdapter[] wifiAdapters = WifiAdapter.FindAllAdapters();
            var adapter = wifiAdapters[0];
           
            var report = adapter.NetworkReport;
            foreach (var network in report.AvailableNetworks)
            {
                Debug.WriteLine(network.Ssid);
            }


            adapter.AvailableNetworksChanged += Adapter_AvailableNetworksChanged;
            Thread.Sleep(10_000);
            while (true)
            {
                try
                {
                    Debug.WriteLine("starting Wi-Fi scan");
                    adapter.ScanAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failure starting a scan operation: {ex}");
                }

                Thread.Sleep(10000);
            }
            //bool goingUp = true;
            //float dutyCycleGreen = 0.10f;
            //float dutyCycleRed = .95f;
            //Debug.WriteLine("Hello from nanoFramework!");

            //s_GpioController = new GpioController();
            ////GpioPin ledRed = s_GpioController.OpenPin(13, PinMode.Output);
            ////GpioPin ledYellow = s_GpioController.OpenPin(12, PinMode.Output);
            //Configuration.SetPinFunction(13, DeviceFunction.PWM1);
            //PwmChannel pwmPinGreen = PwmChannel.CreateFromPin(13, frequency: 4000, dutyCyclePercentage: 0.1);

            //Configuration.SetPinFunction(12, DeviceFunction.PWM1);
            //PwmChannel pwmPinRed = PwmChannel.CreateFromPin(12, frequency: 4000, dutyCyclePercentage: 0.1);



            //pwmPinGreen.Start();
            //pwmPinRed.Start();
            //while (true)
            //{
            //    if (goingUp)
            //    {
            //        // slowly increase light intensity
            //        dutyCycleGreen += 0.05f;
            //        dutyCycleRed -= 0.05f;
            //        // change direction if reaching maximum duty cycle (100%)
            //        if (dutyCycleGreen > .95)
            //            goingUp = !goingUp;
            //    }
            //    else
            //    {
            //        // slowly decrease light intensity
            //        dutyCycleGreen -= 0.05f;
            //        dutyCycleRed += 0.05f;
            //        // change direction if reaching minimum duty cycle (0%)
            //        if (dutyCycleGreen < 0.10)
            //            goingUp = !goingUp;
            //    }

            //    // update duty cycle
            //    pwmPinGreen.DutyCycle = dutyCycleGreen;
            //    pwmPinRed.DutyCycle = dutyCycleRed;
            //    Thread.Sleep(500);
            //}


            //ledRed.Write(PinValue.Low);
            //ledYellow.Write(PinValue.High);

            //while (true)
            //{
            //    ledRed.Toggle();
            //    ledYellow.Toggle();
            //    Thread.Sleep(100);
            //    ledRed.Toggle();
            //    ledYellow.Toggle();
            //    Thread.Sleep(100);
            //    ledRed.Toggle();
            //    ledYellow.Toggle();
            //    Thread.Sleep(100);
            //    ledRed.Toggle();
            //    ledYellow.Toggle();
            //    Thread.Sleep(500);
            //}

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        private static void Adapter_AvailableNetworksChanged(WifiAdapter sender, object e)
        {
            var report = sender.NetworkReport;
            foreach (var network in report.AvailableNetworks)
            {
                Debug.WriteLine(network.Ssid);
                if(network.Ssid == "realme C21") sender.Connect(network.Ssid, WifiReconnectionKind.Automatic, "")
            }
        }
    }
}
