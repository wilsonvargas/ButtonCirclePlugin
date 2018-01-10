using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TestAppButtonCircle.Views;
using Xamarin.Forms;

namespace TestAppButtonCircle.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }
        public ICommand DisplayAlertCommand { get; set; }
        public INavigation Navigation { get; set; }

        public MainPageViewModel()
        {
            NavigationCommand = new Command(Navigate);
            DisplayAlertCommand = new Command(DisplayAlert);
        }

        private async void Navigate()
        {
            await Navigation.PushAsync(new SecondPage());
        }

        private void DisplayAlert()
        {
            App.Current.MainPage.DisplayAlert("Alert", "I'm a button", "OK");
        }
    }
}
