﻿using Glovebox.Adafruit.Mini8x8Matrix;
using Glovebox.RaspberryPi.IO.Actuators;
using Glovebox.RaspberryPi.IO.Sensors;
using System.Threading;

namespace MakerDenMono {
    class MainClass : MakerBaseIoT {
        public static void Main(string[] args) {

            InitializeDrivers();

            StartNetworkServices("Mono", true);

            using (Sys sys = new Sys("dgrpi2"))
            using (SensorCPUTemp cpuTemp = new SensorCPUTemp(10000, "cpu01"))
            using (SensorMemory mem = new SensorMemory(2000, "mem01"))
            using (led = new LedDigital(gpioDriver, "led01"))
            using (AdaFruitMatrixRun matrix = new AdaFruitMatrixRun(i2cDriver.Connect(0x70)))
            using (SensorLight light = new SensorLight(spiConnection, 1000, "light01"))
            using (SensorMcp9701a tempMcp9701a = new SensorMcp9701a(spiConnection, 15000, "temp02")) {

                cpuTemp.OnBeforeMeasurement += OnBeforeMeasure;
                cpuTemp.OnAfterMeasurement += OnMeasureCompleted;

                mem.OnBeforeMeasurement += OnBeforeMeasure;
                mem.OnAfterMeasurement += OnMeasureCompleted;

                light.OnBeforeMeasurement += OnBeforeMeasure;
                light.OnAfterMeasurement += OnMeasureCompleted;

                tempMcp9701a.OnBeforeMeasurement += OnBeforeMeasure;
                tempMcp9701a.OnAfterMeasurement += OnMeasureCompleted;

                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}
