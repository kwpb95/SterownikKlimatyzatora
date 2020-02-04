using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sterownik.classes
{
    public class InputData
    {
        private int temperature;
        private int humidity;
        private int temperatureOut;
        private int humidityOut;
        private int desiredTemperature;
        public InputData(int temperature,int humidity, int desiredTemperature, int temperatureOut, int humidityOut)
        {
            DesiredTemperature = desiredTemperature;
            Temperature = temperature;
            Humidity = humidity;
            HumidityOut = humidityOut;
            TemperatureOut = temperatureOut;
         
        }
        public int Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                temperature = value;
            }
        }

        public int Humidity
        {
            get
            {
                return humidity;
            }

            set
            {
                humidity = value;
            }
        }

        public int DesiredTemperature
        {
            get
            {
                return desiredTemperature;
            }

            set
            {
                desiredTemperature = value;
            }
        }

        public int HumidityOut
        {
            get
            {
                return humidityOut;
            }

            set
            {
                humidityOut = value;
            }
        }

        public int TemperatureOut
        {
            get
            {
                return temperatureOut;
            }

            set
            {
                temperatureOut = value;
            }
        }
    }
}
