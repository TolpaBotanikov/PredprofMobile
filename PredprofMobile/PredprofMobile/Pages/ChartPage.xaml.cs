using Microcharts;
using Newtonsoft.Json;
using PredprofMobile.Data;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PredprofMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        public ChartPage()
        {
            InitializeComponent();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync("http://black-bread-board.herokuapp.com/api/akeses").Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<Akes> akesList = JsonConvert.DeserializeObject<AkesList>(json).akeses;
                akesPicker.ItemsSource = akesList.Where(a => AutorisationPage.akeses.Contains(a.id)).ToList();
                akesPicker.SelectedIndex = 0;

                #region Добавление АКЭС

                //Random r = new Random();
                //double pAOn = r.Next(0, 100000);
                //double pAOff = pAOn * (1 + r.NextDouble());
                //double pBOn = r.Next(0, 100000);
                //double pBOff = pBOn * (1 + r.NextDouble());
                //double pCOn = r.Next(0, 100000);
                //double pCOff = pCOn * (1 + r.NextDouble());
                //AkesOutput ao = new AkesOutput(r.Next(0, 100),
                //    pAOn, pAOff, pBOn, pBOff, pCOn, pCOff,
                //    1608443782,
                //    r.NextDouble() * r.Next(-1, 2),
                //    r.NextDouble() * r.Next(-1, 2),
                //    r.NextDouble() * r.Next(-1, 2),
                //    r.NextDouble() * r.Next(-1, 2),
                //    r.NextDouble() * r.Next(-1, 2),
                //    r.NextDouble() * r.Next(-1, 2),
                //    DateTime.Now, DateTime.Now, r.Next(),
                //    r.Next(-10000, 10000),
                //    r.Next(-10000, 10000),
                //    r.Next(-10000, 10000),
                //    r.Next(-10000, 10000),
                //    r.Next(-10000, 10000),
                //    r.Next(-10000, 10000),
                //    r.Next(200, 250),
                //    r.Next(200, 250),
                //    r.Next(200, 250),
                //    r.Next(200, 250),
                //    r.Next(200, 250),
                //    r.Next(200, 250));
                #endregion
            }
            catch (Exception e)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
                Navigation.PopAsync();
            }
            
            periodPicker.ItemsSource = new string[] { "День", "Неделя" };
            periodPicker.SelectedIndex = 0;
            //chart.Chart = new LineChart();
            //chart.Chart.Entries = new List<ChartEntry>()
            //{
            //    new ChartEntry(5) { Label = "24.01.22", ValueLabel = "spjepog"},
            //    new ChartEntry(10) { Label = "25.01.22", ValueLabel = "tfjegg"},
            //    new ChartEntry(4) { Label = "26.01.22", ValueLabel = "sewgt2wh"},
            //};
        }

        double CalculateEffectiveness(AkesOutput output)
        {
            double pOn = output.active_power_A +
                output.active_power_B +
                output.active_power_C;
            double pOff = output.active_power_A_off.Value +
                output.active_power_B_off.Value +
                output.active_power_C_off.Value;
            return (pOff - pOn) / pOff * 100;
        }

        bool Calculatable(AkesOutput output)
        {
            if (output.active_power_A_off == null ||
                output.active_power_B_off == null ||
                output.active_power_C_off == null)
            {
                return false;
            }
            return true;
        }

        void DisplayChart(int akesId)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                if (akes == null) return;
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync
                    ($"http://black-bread-board.herokuapp.com/api/{akes.id}/akes_outputs")
                    .Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<AkesOutput> akesList = JsonConvert.DeserializeObject<AkesOutputList>(json).
                    akes_Outputs.Where(a => AutorisationPage.akeses.Contains(a.akes_id)).OrderBy(a => a.datetime_end).ToList();
                List<DateTime> dates = new List<DateTime>();
                List<double> values = new List<double>();
                foreach (AkesOutput output in akesList)
                {
                    if(output.datetime_start < datePicker.Date ||
                        output.datetime_end > datePicker.Date.AddDays(periodPicker.SelectedIndex == 0 ? 1 : 7))
                        continue;
                    if (Calculatable(output))
                    {
                        dates.Add(output.datetime_end);
                        values.Add(CalculateEffectiveness(output));
                    }
                }
                chart.Chart = new LineChart()
                {
                    LabelOrientation = Orientation.Horizontal,
                    ValueLabelOrientation = Orientation.Horizontal,
                    MinValue = 0,
                    MaxValue = 100,
                    LineMode = LineMode.Straight
                };
                List<ChartEntry> entries = new List<ChartEntry>();
                for (int i = 0; i < values.Count; i++)
                {
                    entries.Add(new ChartEntry((float)values[i])
                    {
                        Label = dates[i].ToString("dd.MM.yy HH:mm:ss"),
                        ValueLabel = (values[i]/100).ToString("P2"),
                    });    
                }
                chart.Chart.Entries = entries;
            }
            catch
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
            }
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                DisplayChart(akes.id);
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка:\n" + ex, "ОК");
            }
        }

        private void akesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                DisplayChart(akes.id);
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка:\n" + ex, "ОК");
            }
        }

        private void periodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                DisplayChart(akes.id);
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка:\n" + ex, "ОК");
            }
        }
    }
}