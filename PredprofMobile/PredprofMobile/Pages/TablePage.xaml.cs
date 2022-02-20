using Newtonsoft.Json;
using PredprofMobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PredprofMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablePage : ContentPage
    {
        bool isSetuped = false;
        public TablePage(int id, DateTime date, int period)
        {
            InitializeComponent();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync("http://black-bread-board.herokuapp.com/api/akeses").Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<Akes> akesList = JsonConvert.DeserializeObject<AkesList>(json).akeses;
                akesPicker.ItemsSource = akesList.Where(a => AutorisationPage.akeses.Contains(a.id)).ToList();
                akesPicker.SelectedIndex = id == 0 ? 0 : akesList.IndexOf(akesList.FirstOrDefault(a => a.id == id));
                periodPicker.ItemsSource = new string[] { "День", "Неделя" };
                periodPicker.SelectedIndex = period;
                datePicker.Date = date;
                isSetuped = true;
            }
            catch (Exception e)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
                Navigation.PopAsync();
            }
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

        private void akesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                if (akes == null) return;
                if (isSetuped)
                {
                    Navigation.PushAsync(new TablePage(akes.id, datePicker.Date, periodPicker.SelectedIndex));
                    Navigation.RemovePage(this);
                    return;
                }
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync
                    ($"http://black-bread-board.herokuapp.com/api/{akes.id}/akes_outputs")
                    .Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<AkesOutput> akesList = JsonConvert.DeserializeObject<AkesOutputList>(json).
                    akes_Outputs.Where(a => AutorisationPage.akeses.Contains(a.akes_id)).OrderBy(a => a.datetime_end).ToList();
                List<AkesOutput> toRemove = new List<AkesOutput>();
                foreach (AkesOutput output in akesList)
                {
                    if (output.datetime_start < datePicker.Date ||
                        output.datetime_end > datePicker.Date.AddDays(periodPicker.SelectedIndex == 0 ? 1 : 7))
                    {
                        toRemove.Add(output);
                    }
                    if (Calculatable(output))
                        output.effectiveness = CalculateEffectiveness(output) / 100;
                }
                foreach (AkesOutput output in toRemove)
                {
                    akesList.Remove(output);
                }
                dataGrid.ItemsSource = akesList;
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
                Navigation.PopAsync();
            }
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                if (akes == null) return;
                if (isSetuped)
                {
                    Navigation.PushAsync(new TablePage(akes.id, datePicker.Date, periodPicker.SelectedIndex));
                    Navigation.RemovePage(this);
                    return;
                }
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync
                    ($"http://black-bread-board.herokuapp.com/api/{akes.id}/akes_outputs")
                    .Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<AkesOutput> akesList = JsonConvert.DeserializeObject<AkesOutputList>(json).
                    akes_Outputs.Where(a => AutorisationPage.akeses.Contains(a.akes_id)).OrderBy(a => a.datetime_end).ToList();
                List<AkesOutput> toRemove = new List<AkesOutput>();
                foreach (AkesOutput output in akesList)
                {
                    if (output.datetime_start < datePicker.Date ||
                        output.datetime_end > datePicker.Date.AddDays(periodPicker.SelectedIndex == 0 ? 1 : 7))
                    {
                        toRemove.Add(output);
                    }
                    if (Calculatable(output))
                        output.effectiveness = CalculateEffectiveness(output) / 100;
                }
                foreach (AkesOutput output in toRemove)
                {
                    akesList.Remove(output);
                }
                dataGrid.ItemsSource = akesList;
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
                Navigation.PopAsync();
            }
        }

        private void periodPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Akes akes = akesPicker.SelectedItem as Akes;
                if (akes == null) return;
                if (isSetuped)
                {
                    Navigation.PushAsync(new TablePage(akes.id, datePicker.Date, periodPicker.SelectedIndex));
                    Navigation.RemovePage(this);
                    return;
                }
                HttpClient client = new HttpClient();
                HttpResponseMessage result = client.GetAsync
                    ($"http://black-bread-board.herokuapp.com/api/{akes.id}/akes_outputs")
                    .Result;
                string json = result.Content.ReadAsStringAsync().Result;
                List<AkesOutput> akesList = JsonConvert.DeserializeObject<AkesOutputList>(json).
                    akes_Outputs.Where(a => AutorisationPage.akeses.Contains(a.akes_id)).OrderBy(a => a.datetime_end).ToList();
                List<AkesOutput> toRemove = new List<AkesOutput>();
                foreach (AkesOutput output in akesList)
                {
                    if (output.datetime_start < datePicker.Date ||
                        output.datetime_end > datePicker.Date.AddDays(periodPicker.SelectedIndex == 0 ? 1 : 7))
                    {
                        toRemove.Add(output);
                    }
                    if (Calculatable(output))
                        output.effectiveness = CalculateEffectiveness(output) / 100;
                }
                foreach (AkesOutput output in toRemove)
                {
                    akesList.Remove(output);
                }
                dataGrid.ItemsSource = akesList;
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Ошибка при получении данных", "ОК");
                Navigation.PopAsync();
            }
        }
    }
}