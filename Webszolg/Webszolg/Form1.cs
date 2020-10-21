using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Webszolg.Entities;
using Webszolg.MnbServiceReference;

namespace Webszolg
{
    public partial class Form1 : Form
    {
        
        
        List<RateData> Rates=new List<RateData>();
        

        List<string> Currencies;

        


        

        public Form1()
        {
            InitializeComponent();

           /* var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            comboBox1.DataSource = Currencies; */

            
            dataGridView1.DataSource = Rates;

           
            RefreshData();

        }

        private string MNBRequest()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString(),
            };

            var responses = mnbService.GetExchangeRates(request);
            var results = responses.GetExchangeRatesResult;
            return results;
        }

        private void Creatework (string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {

                var rate = new RateData();
                Rates.Add(rate);


                rate.Date = DateTime.Parse(element.GetAttribute("date"));


                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }

        }

        private void Keszitdiagram ()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
            
        }

        private void RefreshData()
        {
            Rates.Clear();
            var abc =MNBRequest();
            Creatework(abc);
            Keszitdiagram();
        }

        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            

            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
           
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        
    }
}
