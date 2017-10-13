using System;
using System.Collections.Generic;
using System.Linq;

namespace CC3200SensorTag
{
    public class SensorReadings
    {
        public SensorReadings(DateTime readingLocalTime, Dictionary<string, string> rawReadings)
        {
            LocalTime = readingLocalTime;

            AmbientTemperature = Convert.ToDouble(rawReadings["tmp"].Split(' ').Last());

            RelativeHumidityAsPercent = Convert.ToDouble(rawReadings["hum"].Split(' ').Skip(2).Take(1).Single());

            BarometerMillibars = Convert.ToDouble(rawReadings["bar"].Split(' ').Last());

            var gyroscope = rawReadings["gyr"].Split(' ').Skip(3).ToArray();
            GyroscopeX = Convert.ToDouble(gyroscope[0]);
            GyroscopeY = Convert.ToDouble(gyroscope[1]);
            GyroscopeZ = Convert.ToDouble(gyroscope[2]);

            var accelerometer = rawReadings["acc"].Split(' ').Skip(3).ToArray();
            AccelerometerX = Convert.ToDouble(accelerometer[0]);
            AccelerometerY = Convert.ToDouble(accelerometer[1]);
            AccelerometerZ = Convert.ToDouble(accelerometer[2]);

            LightLux = Convert.ToDouble(rawReadings["opt"].Split(' ').Last());

            var magnetometer = rawReadings["mag"].Split(' ').Skip(3).ToArray();
            MagnetometerX = Convert.ToDouble(magnetometer[0]);
            MagnetometerY = Convert.ToDouble(magnetometer[1]);
            MagnetometerZ = Convert.ToDouble(magnetometer[2]);
        }

        public DateTime LocalTime { get; }

        public double LightLux { get; }

        public double MagnetometerZ { get; }

        public double MagnetometerY { get; }

        public double MagnetometerX { get; }

        public double AccelerometerZ { get; }

        public double AccelerometerY { get; }

        public double AccelerometerX { get; }

        public double GyroscopeZ { get; }

        public double GyroscopeY { get; }

        public double GyroscopeX { get; }

        public double BarometerMillibars { get; }

        public double RelativeHumidityAsPercent { get; }

        public double AmbientTemperature { get; }

        public double AmbientTemperatureMin { get; } = 0;

        public double AmbientTemperatureMax { get; } = 50;

        public override string ToString()
        {
            return $@"{LocalTime:o}
tmp {AmbientTemperature}
hum {RelativeHumidityAsPercent}
bar {BarometerMillibars}
gyr {GyroscopeX} {GyroscopeY} {GyroscopeZ}
acc {AccelerometerX} {AccelerometerY} {AccelerometerZ}
opt {LightLux}
mag {MagnetometerX} {MagnetometerY} {MagnetometerZ}";
        }
    }
}