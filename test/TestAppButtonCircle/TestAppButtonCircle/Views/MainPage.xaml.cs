using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppButtonCircle.ViewModels;
using Xamarin.Forms;

namespace TestAppButtonCircle.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = vm = new MainPageViewModel();
            vm.Navigation = this.Navigation;
        }

        private void CircleButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Alert", "I'm a button", "OK");
        }
    }
}
