using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CircleButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Alert", "I'm a button", "OK");
        }
    }
}
