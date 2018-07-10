using TestAppButtonCircle.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAppButtonCircle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = vm = new MainPageViewModel();
            vm.Navigation = this.Navigation;
        }

        private MainPageViewModel vm;
    }
}
