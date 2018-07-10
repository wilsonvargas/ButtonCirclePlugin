using System.Windows.Input;
using TestAppButtonCircle.Views;
using Xamarin.Forms;

namespace TestAppButtonCircle.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            NavigationCommand = new Command(Navigate);
            DisplayAlertCommand = new Command(DisplayAlert);
        }

        public ICommand DisplayAlertCommand { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand NavigationCommand { get; set; }

        private void DisplayAlert()
        {
            App.Current.MainPage.DisplayAlert("Alert", "I'm a button", "OK");
        }

        private async void Navigate()
        {
            await Navigation.PushAsync(new SecondPage());
        }
    }
}
