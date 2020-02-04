using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Fuzzy;

namespace sterownik.classes
{
    /// <summary>
    /// Klasa służąca do tworzenia odpowiednich zbiorów do danych wejsciowych oraz do zarządzania logiką, dziedziczy z AForge Fuzy
    /// </summary>
    public class FuzzyControl
    {
        //przedziały temperatury zewnetrznej
        private FuzzySet tmpColdOut;
        private FuzzySet tmpHotOut;
        //przedziały temperatury
        private FuzzySet tmpVeryCold;      
        private FuzzySet tmpCold;
        private FuzzySet tmpMedium;
        private FuzzySet tmpHot;
        private FuzzySet tmpVeryHot;
        //przedziały wilgotności
        private FuzzySet smallHumidity;
        private FuzzySet goodHumidity;
        private FuzzySet bigHumidity;
        //przedział siły wentylatora (od zera do 100)
        private FuzzySet veryLowStrenght;
        private FuzzySet lowStrenght;
        private FuzzySet mediumStrenght;
        private FuzzySet hightStrenght;
        private FuzzySet veryHightStrenght;
        //zmienne lingwistyczne
        LinguisticVariable airOutTemperature;
        LinguisticVariable airTemperature;
        LinguisticVariable Humidity;
        LinguisticVariable HumidityOut;
        LinguisticVariable FunTemperature;
        LinguisticVariable FunStrenght;
        LinguisticVariable desiredTemperature;
        //baza danych zmienncyh lingwistycznych
        Database db;
        //system wnioskowania
        InferenceSystem IS;




        public FuzzyControl()
        {
            SetFuzySet();
            SetLinguisticVariable();
            SetDatabase();
            CreateInferenceSystem();


        }
        private void CreateInferenceSystem() //tworzy system wnioskowania, wraz z gotową bazą reguł
        {
            IS = new InferenceSystem(db, new CentroidDefuzzifier(1000));
            IS.NewRule("Rule 1", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunTemperature IS Hot");
            IS.NewRule("Rule 2", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunTemperature IS Hot");
            IS.NewRule("Rule 3", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 4", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunTemperature IS Hot");
            IS.NewRule("Rule 5", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 6", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 7", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 8", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 9", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 10", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunTemperature IS Medium");
            IS.NewRule("Rule 11", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunTemperature IS Cold");
            IS.NewRule("Rule 12", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunTemperature IS Cold");
            IS.NewRule("Rule 13", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunTemperature IS VeryCold");
            IS.NewRule("Rule 14", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunTemperature IS VeryCold");
            IS.NewRule("Rule 15", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunTemperature IS VeryCold");

            IS.NewRule("Rule 16", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 17", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 18", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunTemperature IS Hot");
            IS.NewRule("Rule 19", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunTemperature IS Hot");
            IS.NewRule("Rule 20", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunTemperature IS Hot");
            IS.NewRule("Rule 21", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunTemperature IS Medium");
            IS.NewRule("Rule 22", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunTemperature IS Hot");
            IS.NewRule("Rule 23", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunTemperature IS Medium");
            IS.NewRule("Rule 24", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunTemperature IS Medium");
            IS.NewRule("Rule 25", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunTemperature IS Medium");
            IS.NewRule("Rule 26", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunTemperature IS Medium");
            IS.NewRule("Rule 27", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunTemperature IS Cold");
            IS.NewRule("Rule 28", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunTemperature IS Cold");
            IS.NewRule("Rule 29", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunTemperature IS Cold");
            IS.NewRule("Rule 30", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunTemperature IS VeryCold");

            IS.NewRule("Rule 31", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 32", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 33", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 34", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 35", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 36", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule 37", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunTemperature IS Hot");
            IS.NewRule("Rule 38", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunTemperature IS Hot");
            IS.NewRule("Rule 39", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunTemperature IS Medium");
            IS.NewRule("Rule 40", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunTemperature IS Medium");
            IS.NewRule("Rule 41", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunTemperature IS Medium");
            IS.NewRule("Rule 42", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunTemperature IS Cold");
            IS.NewRule("Rule 43", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunTemperature IS Cold");
            IS.NewRule("Rule 44", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunTemperature IS Cold");
            IS.NewRule("Rule 45", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunTemperature IS Cold");

            IS.NewRule("Rule 46", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 47", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 48", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 49", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 50", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 51", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 52", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 53", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 54", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 55", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunStrenght IS Hight");
            IS.NewRule("Rule 56", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunStrenght IS Medium");
            IS.NewRule("Rule 57", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunStrenght IS Medium");
            IS.NewRule("Rule 58", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Cold THEN FunStrenght IS Low");
            IS.NewRule("Rule 59", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Cold THEN FunStrenght IS Medium");
            IS.NewRule("Rule 60", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Cold THEN FunStrenght IS Medium");

            IS.NewRule("Rule 61", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 62", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 63", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 64", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunStrenght IS Hight");
            IS.NewRule("Rule 65", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunStrenght IS Hight");
            IS.NewRule("Rule 66", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 67", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 68", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 69", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 70", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 71", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 72", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunStrenght IS Low");
            IS.NewRule("Rule 73", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Medium THEN FunStrenght IS Medium");
            IS.NewRule("Rule 74", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Medium THEN FunStrenght IS VeryLow");
            IS.NewRule("Rule 75", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Medium THEN FunStrenght IS Low");

            IS.NewRule("Rule 76", "IF AirTemperature IS VeryCold AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 77", "IF AirTemperature IS VeryCold AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 78", "IF AirTemperature IS VeryCold AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 79", "IF AirTemperature IS Cold AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 80", "IF AirTemperature IS Cold AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 81", "IF AirTemperature IS Cold AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule 82", "IF AirTemperature IS Medium AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 83", "IF AirTemperature IS Medium AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 84", "IF AirTemperature IS Medium AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 85", "IF AirTemperature IS Hot AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 86", "IF AirTemperature IS Hot AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 87", "IF AirTemperature IS Hot AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunStrenght IS Low");
            IS.NewRule("Rule 88", "IF AirTemperature IS VeryHot AND Humidity IS Small AND DesiredTemperature IS Hot THEN FunStrenght IS Medium");
            IS.NewRule("Rule 89", "IF AirTemperature IS VeryHot AND Humidity IS Good AND DesiredTemperature IS Hot THEN FunStrenght IS VeryLow");
            IS.NewRule("Rule 90", "IF AirTemperature IS VeryHot AND Humidity IS Big AND DesiredTemperature IS Hot THEN FunStrenght IS Low");

            //zasady dla temp i wilgotnosci zewnetrznej
            IS.NewRule("Rule91", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Small THEN FunTemperature IS VeryHot");
            IS.NewRule("Rule92", "IF AirOutTemperature IS HotOut AND HumidityOut IS Small THEN FunTemperature IS Cold");

            IS.NewRule("Rule93", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Good THEN FunTemperature IS Hot");
            IS.NewRule("Rule94", "IF AirOutTemperature IS HotOut AND HumidityOut IS Good THEN FunTemperature IS Cold");

            IS.NewRule("Rule95", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Big THEN FunTemperature IS Hot");
            IS.NewRule("Rule96", "IF AirOutTemperature IS HotOut AND HumidityOut IS Big THEN FunTemperature IS VeryCold");

            IS.NewRule("Rule97", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Small THEN FunStrenght IS VeryHight");
            IS.NewRule("Rule98", "IF AirOutTemperature IS HotOut AND HumidityOut IS Small THEN FunStrenght IS Low");

            IS.NewRule("Rule99", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Good THEN FunStrenght IS Hight");
            IS.NewRule("Rule100", "IF AirOutTemperature IS HotOut AND HumidityOut IS Good THEN FunStrenght IS Low");

            IS.NewRule("Rule101", "IF AirOutTemperature IS ColdOut AND HumidityOut IS Big THEN FunStrenght IS Hight");
            IS.NewRule("Rule102", "IF AirOutTemperature IS HotOut AND HumidityOut IS Big THEN FunStrenght IS VeryLow");



        }
        private void SetFuzySet() //ustawia wartość przedziałów
        {
            //ustawia wartości przedziałów dla temperatury zewnętrzej
            TrapezoidalFunction function = new TrapezoidalFunction(-10,30 , TrapezoidalFunction.EdgeType.Right);
            tmpColdOut = new FuzzySet("ColdOut", function);
            function = new TrapezoidalFunction(-10, 30, TrapezoidalFunction.EdgeType.Left);
            tmpHotOut = new FuzzySet("HotOut", function);
            
            //ustawia wartości przedziałów dla temperatury
            function = new TrapezoidalFunction(1, 10, TrapezoidalFunction.EdgeType.Right);
            tmpVeryCold = new FuzzySet("VeryCold", function);
            function = new TrapezoidalFunction(5, 10, 13, 16);
            tmpCold = new FuzzySet("Cold", function);
            function = new TrapezoidalFunction(13, 16, 21, 26);
            tmpMedium = new FuzzySet("Medium", function);
            function = new TrapezoidalFunction(21, 25, 30, 35);
            tmpHot = new FuzzySet("Hot", function);
            function = new TrapezoidalFunction(30, 35, TrapezoidalFunction.EdgeType.Left);
            tmpVeryHot = new FuzzySet("VeryHot", function);
            //ustawia wartości przedziałów dla wilgotności
            function = new TrapezoidalFunction(0, 40, TrapezoidalFunction.EdgeType.Right);
            smallHumidity = new FuzzySet("Small", function);
            function = new TrapezoidalFunction(20, 40, 60, 80);
            goodHumidity = new FuzzySet("Good", function);
            function = new TrapezoidalFunction(60, 100, TrapezoidalFunction.EdgeType.Left);
            bigHumidity = new FuzzySet("Big", function);
            //ustawia wartość przedziału przedział siły wentylatora (od zera do 100)
            function = new TrapezoidalFunction(20, 30, TrapezoidalFunction.EdgeType.Right);
            veryLowStrenght = new FuzzySet("VeryLow", function);
            function = new TrapezoidalFunction(20, 30, 40, 50);
            lowStrenght = new FuzzySet("Low", function);
            function = new TrapezoidalFunction(40, 50, 60, 70);
            mediumStrenght = new FuzzySet("Medium", function);
            function = new TrapezoidalFunction(60, 70, 80, 90);
            hightStrenght = new FuzzySet("Hight", function);
            function = new TrapezoidalFunction(80, 90, TrapezoidalFunction.EdgeType.Left);
            veryHightStrenght = new FuzzySet("VeryHight", function);
        }
        private void SetLinguisticVariable() //ustawia zmienne lingwistyczne
        {
            airOutTemperature = new LinguisticVariable("AirOutTemperature", -20, 40);
            airOutTemperature.AddLabel(tmpColdOut);
            airOutTemperature.AddLabel(tmpHotOut);
            airTemperature = new LinguisticVariable("AirTemperature", 0, 40);
            airTemperature.AddLabel(tmpVeryCold);
            airTemperature.AddLabel(tmpCold);
            airTemperature.AddLabel(tmpMedium);
            airTemperature.AddLabel(tmpHot);
            airTemperature.AddLabel(tmpVeryHot);
            Humidity = new LinguisticVariable("Humidity", 0, 100);
            Humidity.AddLabel(smallHumidity);
            Humidity.AddLabel(goodHumidity);
            Humidity.AddLabel(bigHumidity);
            HumidityOut= new LinguisticVariable("HumidityOut", 0, 100);
            HumidityOut.AddLabel(smallHumidity);
            HumidityOut.AddLabel(goodHumidity);
            HumidityOut.AddLabel(bigHumidity);
            desiredTemperature = new LinguisticVariable("DesiredTemperature", 0, 40);
            desiredTemperature.AddLabel(tmpVeryCold);
            desiredTemperature.AddLabel(tmpCold);
            desiredTemperature.AddLabel(tmpMedium);
            desiredTemperature.AddLabel(tmpHot);
            desiredTemperature.AddLabel(tmpVeryHot);
            FunTemperature = new LinguisticVariable("FunTemperature", 0, 40);
            FunTemperature.AddLabel(tmpVeryCold);
            FunTemperature.AddLabel(tmpCold);
            FunTemperature.AddLabel(tmpMedium);
            FunTemperature.AddLabel(tmpHot);
            FunTemperature.AddLabel(tmpVeryHot);
            FunStrenght = new LinguisticVariable("FunStrenght", 0, 100);
            FunStrenght.AddLabel(veryLowStrenght);
            FunStrenght.AddLabel(lowStrenght);
            FunStrenght.AddLabel(mediumStrenght);
            FunStrenght.AddLabel(hightStrenght);
            FunStrenght.AddLabel(veryHightStrenght);
        }
        private void SetDatabase() //tworzy baze danych wyrazeń lingwistycznych
        {
            db = new Database();
            db.AddVariable(airTemperature);
            db.AddVariable(Humidity);
            db.AddVariable(FunTemperature);
            db.AddVariable(FunStrenght);
            db.AddVariable(desiredTemperature);
            db.AddVariable(airOutTemperature);
            db.AddVariable(HumidityOut);
        }
        //funkcje zwracające wartość przynależności do przedziału dla podanej temperatury zewnętrznej
        public float valueTmpColdOut(float temperature)
        {
            return tmpColdOut.GetMembership(temperature);

        }
        public float valueTmpHotOut(float temperature)
        {
            return tmpHotOut.GetMembership(temperature);
        }
        //funkcje zwracające wartość przynależności do przedziału dla podanej temperatury
        public float valueTmpVeryCold(float temperature)
        {
            return tmpVeryCold.GetMembership(temperature);
        }
        public float valueTmpCold(float temperature)
        {
            return tmpCold.GetMembership(temperature);
            
        }
        public float valueTmpMedium(float temperature)
        {
            return tmpMedium.GetMembership(temperature);
        }
        public float valueTmpHot(float temperature)
        {
            return tmpHot.GetMembership(temperature);
        }
        public float valueTmpVeryHot(float temperature)
        {
            return tmpVeryHot.GetMembership(temperature);
        }
        //funkcje zwracające wartość przynależności do przedziału dla podanej wilgotnosci
        public float valueSmallHumidity(float temperature)
        {
            return smallHumidity.GetMembership(temperature);
        }
        public float valueGoodHumidity(float temperature)
        {
            return goodHumidity.GetMembership(temperature);
        }
        public float valueBigHumidity(float temperature)
        {
            return bigHumidity.GetMembership(temperature);
        }
        //funkcje zwracające wartość przynależności do przedziału dla podanej sily wiatraka
        public float valueVeryLowStrenght(float temperature)
        {
            return veryLowStrenght.GetMembership(temperature);
        }
        public float valueLowStrenght(float temperature)
        {
            return lowStrenght.GetMembership(temperature);
        }
        public float valueMediumStrenghty(float temperature)
        {
            return mediumStrenght.GetMembership(temperature);
        }
        public float valueHightStrenght(float temperature)
        {
            return hightStrenght.GetMembership(temperature);
        }
        public float valueVeryHightStrenght(float temperature)
        {
            return veryHightStrenght.GetMembership(temperature);
        }
        //funkcja zwracająca wartość reguły: do dopracowania
        public int GetFunTemperature(InputData input) //zmiena zwracająca finalną wartość temperatury wiatraka
        {
            
            airTemperature.NumericInput = input.Temperature;
            Humidity.NumericInput = input.Humidity;
            desiredTemperature.NumericInput = input.DesiredTemperature;
            airOutTemperature.NumericInput = input.TemperatureOut;
            HumidityOut.NumericInput = input.HumidityOut;
            int result;          
            int tmp = Convert.ToInt32(IS.Evaluate("FunTemperature"));
            result = tmp;
            return result;

        }
        public int GetFunStrenght(InputData input) //zmiena zwracająca finalną wartość siły wiatraka
        {
            airOutTemperature.NumericInput = input.TemperatureOut;
            HumidityOut.NumericInput = input.HumidityOut;
            airTemperature.NumericInput = input.Temperature;
            Humidity.NumericInput = input.Humidity;
            desiredTemperature.NumericInput = input.DesiredTemperature;

            int result;
            
            int tmp = Convert.ToInt32(IS.Evaluate("FunStrenght"));
            result = tmp;
            return result;

        }
    }
}
