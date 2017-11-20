using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestAppButtonCircle.Views;
using Xamarin.Forms;

namespace TestAppButtonCircle.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand NavigationCommand { get; set; }
        public INavigation Navigation { get; set; }

        public MainPageViewModel()
        {
            NavigationCommand = new Command(Navigate);
        }

        private async void Navigate()
        {
            await Navigation.PushAsync(new SecondPage());
        }
    }
}
