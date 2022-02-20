using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;


namespace PredprofMobile
{
    public partial class AutorisationPage : ContentPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Label ="January",
                ValueLabel = "200"
            },
            new Entry(400)
            {
                Label = "March",
                ValueLabel = "400"
            },
            new Entry(-100)
            {
                Label = "Octobar",
                ValueLabel = "-100"
            },
        };
        public AutorisationPage()
        {
            InitializeComponent();

            //chart.Chart = new LineChart();

            //List<ChartEntry> entries = new List<ChartEntry>();

            //for(int i = -5; i <= 5; i++)
            //{
            //    entries.Add(new Entry(i*i));
            //}

            //chart.Chart.Entries = entries;
        }

        private void loginBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage());
        }
    }
}
