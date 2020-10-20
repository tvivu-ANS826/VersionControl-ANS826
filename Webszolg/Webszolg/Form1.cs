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
        RateData context = new RateData();
        List<RateData> Rates;
        private string result;

        List<string> Currencies;

        


        

        public Form1()
        {
            InitializeComponent();

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            comboBox1.DataSource = Currencies;

            Rates = context.Rate.ToList();
            dataGridView1.DataSource = Rates;

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var responses = mnbService.GetExchangeRates(request);
            var results = response.GetExchangeRatesResult;


        }

        private void Creatework ()
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
            Clear(Rates);
        }

        private void Clear(List<RateData> rates)
        {
           /* dateTimePicker1 ClickEvent();
            dateTimePicker2 ClickEvent();
            ComboBox ClickEevnt(); */


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            

            request =dateTimePicker1.Value ;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            request = dateTimePicker2.Value;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            RefreshData();
            CurrencyManager = ComboBox.SelectedItem;
        }
    }
}
