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
using PredprofMobile.Data;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.Net.Mime;

namespace PredprofMobile
{
    public partial class AutorisationPage : ContentPage
    {
        public static List<int> akeses = new List<int>();
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
            HttpClient client = new HttpClient();
            try
            {
                string json = JsonConvert.SerializeObject(new User(loginEntry.Text, passwordEntry.Text));
                //HttpRequestMessage request = new HttpRequestMessage();
                //request.RequestUri = new Uri("http://black-bread-board.herokuapp.com/api/login");
                //request.Method = HttpMethod.Post;
                //request.Content = new StringContent(json);
                HttpResponseMessage response = client.PostAsync("http://black-bread-board.herokuapp.com/api/login", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                LoginResponse lr = JsonConvert.DeserializeObject<LoginResponse>(response.Content.ReadAsStringAsync().Result);
                if (lr.Successfully)
                {
                    HttpResponseMessage rm = client.GetAsync($"http://black-bread-board.herokuapp.com/api/users/{lr.user.id}").Result;
                    string rmjson = rm.Content.ReadAsStringAsync().Result;
                    akeses = JsonConvert.DeserializeObject<UserList>(rmjson).users.akeses.Select(a => a.id).ToList();
                    Navigation.PushAsync(new MenuPage());
                }
                else
                {
                    DisplayAlert("Ошибка", "Неверный логин/пароль", "ОК");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            akeses.Clear();
        }
    }
}
