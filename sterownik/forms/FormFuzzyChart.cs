using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sterownik.classes;
using System.Windows.Forms;

namespace sterownik.forms
{
    public partial class FormFuzzyChart : Form
    {
        InputData inputData = new InputData(0, 0, 0,0,0);
        private int x1;
        private int funTmp;
        private int funStrng;
        public FormFuzzyChart(InputData inputData, FuzzyControl fControl, int funTmp, int funStrng)
        {
            this.funTmp = funTmp;
            this.funStrng = funStrng;

            InitializeComponent();
            this.inputData = inputData;
            x1 = inputData.Temperature;
            DrawCharts(fControl);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void changeChartVisibly()
        {
            if (radioButton1.Checked == true)
            {
                chart1.Visible = true;
                chart2.Visible = false;
                chart3.Visible = false;
            }
            if (radioButton2.Checked == true)
            {
                chart1.Visible = false;
                chart2.Visible = true;
                chart3.Visible = false;
            }
            if (radioButton3.Checked == true)
            {
                chart1.Visible = false;
                chart2.Visible = false;
                chart3.Visible = true;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            changeChartVisibly();
        }
        private void DrawCharts(FuzzyControl fControl)
        {
            chart1.Series[6].Points.AddXY(funTmp, 1);
            chart1.Series[5].Points.AddXY(x1, 1);
            chart1.Series[9].Points.AddXY(inputData.TemperatureOut, 1);
            chart2.Series[3].Points.AddXY(inputData.Humidity, 1);
            chart2.Series[4].Points.AddXY(inputData.HumidityOut, 1);
            chart3.Series[5].Points.AddXY(funStrng, 1);
            for (int i = 0; i < 11; i++)
            {
                chart1.Series[0].Points.AddXY(i, fControl.valueTmpVeryCold(i));

            }
            for (int i = 5; i < 17; i++)
            {

                chart1.Series[1].Points.AddXY(i, fControl.valueTmpCold(i));

            }
            for (int i = 13; i < 27; i++)
            {

                chart1.Series[2].Points.AddXY(i, fControl.valueTmpMedium(i));

            }
            for (int i = 21; i < 36; i++)
            {

                chart1.Series[3].Points.AddXY(i, fControl.valueTmpHot(i));

            }
            for (int i = -21; i < 31; i++)
            {

                chart1.Series[7].Points.AddXY(i, fControl.valueTmpColdOut(i));

            }
            for (int i = -21; i < 36; i++)
            {

                chart1.Series[8].Points.AddXY(i, fControl.valueTmpHotOut(i));

            }
            for (int i = 30; i < 42; i++)
            {
                chart1.Series[4].Points.AddXY(i, fControl.valueTmpVeryHot(i));
            }
            for (int i = 0; i < 41; i++)
            {
                chart2.Series[0].Points.AddXY(i, fControl.valueSmallHumidity(i));
                
            }
            for (int i = 20; i < 81; i++)
            {
                chart2.Series[1].Points.AddXY(i, fControl.valueGoodHumidity(i));
            }
            for (int i = 60; i < 101; i++)
            {
                chart2.Series[2].Points.AddXY(i, fControl.valueBigHumidity(i));
            }
            for (int i = 1; i < 31; i++)
            {
                chart3.Series[0].Points.AddXY(i, fControl.valueVeryLowStrenght(i));
            }
            for (int i = 20; i < 51; i++)
            {
                chart3.Series[1].Points.AddXY(i, fControl.valueLowStrenght(i));
            }
            for (int i = 40; i < 71; i++)
            {
                chart3.Series[2].Points.AddXY(i, fControl.valueMediumStrenghty(i));


            }
            for (int i = 60; i < 91; i++)
            {
                chart3.Series[3].Points.AddXY(i, fControl.valueHightStrenght(i));

            }
            for (int i = 80; i < 101; i++)
            {
                chart3.Series[4].Points.AddXY(i, fControl.valueVeryHightStrenght(i));

            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            changeChartVisibly();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            changeChartVisibly();
        }
    }
}
