using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Fuzzy;
using sterownik.classes;
using System.Windows.Forms.DataVisualization.Charting;
using sterownik.forms;
namespace sterownik
{
    public partial class Form1 : Form
    {
        private InputData inputDate= new InputData(0,0,0,0,0);
        private FuzzyControl fControl;
        private int desiredTemperature;
        int funTmp = 0;
        int funStrng = 0;
        public Form1()
        {
            InitializeComponent();
            fControl = new FuzzyControl();
            ReadtrackBarsValue();
            getOutputData();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormFuzzyChart seeSection=new FormFuzzyChart(inputDate ,fControl,funTmp,funStrng);
            seeSection.ShowDialog();
           
            
        }
        private void getOutputData() //pobiera i wyswietla dane wyjsciowe
        {
            funTmp = fControl.GetFunTemperature(this.inputDate);
            funStrng = fControl.GetFunStrenght(this.inputDate);
            string tmp="";
            tmp = funTmp.ToString();
            textBox3.Text = tmp+ "°C";
            tmp = funStrng.ToString();
            textBox4.Text = tmp+"%";

        }
        private void ReadtrackBarsValue()
        {
            CheckedRadioButton();
            textBox1.Text = trackBar1.Value.ToString()+ "°C";
            textBox2.Text = trackBar2.Value.ToString()+"%";
            textBox5.Text = trackBar3.Value.ToString() + "°C";
            textBox6.Text = trackBar4.Value.ToString() + "%";
            inputDate.Temperature = trackBar1.Value;
            inputDate.TemperatureOut = trackBar3.Value;
            inputDate.Humidity = trackBar2.Value;
            inputDate.HumidityOut = trackBar4.Value;
            inputDate.DesiredTemperature = desiredTemperature;
            getOutputData();

        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ReadtrackBarsValue();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ReadtrackBarsValue();
        }
        private void CheckedRadioButton()
        {

            if (radioButton1.Checked == true)
            {
                desiredTemperature =11;
            }
            if (radioButton2.Checked == true)
            {
                desiredTemperature = 18;
            }
            if (radioButton3.Checked == true)
            {
                desiredTemperature = 27;
            }
           

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ReadtrackBarsValue();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ReadtrackBarsValue();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ReadtrackBarsValue();
        }
    }
}
