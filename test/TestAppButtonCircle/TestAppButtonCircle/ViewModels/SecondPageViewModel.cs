using System.Windows.Input;
using Xamarin.Forms;

namespace TestAppButtonCircle.ViewModels
{
    public class SecondPageViewModel : ViewModelBase
    {
        public SecondPageViewModel()
        {
            GoBackCommand = new Command(GoBack);
        }

        public ICommand GoBackCommand { get; set; }
        public INavigation Navigation { get; set; }

        private async void GoBack()
        {
            await Navigation.PopAsync();
        }
    }
}
