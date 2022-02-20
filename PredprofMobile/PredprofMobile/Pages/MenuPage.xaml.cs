using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PredprofMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void chartBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChartPage());
        }

        private void tableBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TablePage(0, DateTime.Now, 0));
        }

        private void registrationBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private void acessBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AcessPage());
        }
    }
}