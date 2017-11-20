using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestAppButtonCircle.ViewModels
{
    public class SecondPageViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; set; }
        public INavigation Navigation { get; set; }

        public SecondPageViewModel()
        {
            GoBackCommand = new Command(GoBack);
        }

        private async void GoBack()
        {
            await Navigation.PopAsync();
        }
    }
}
