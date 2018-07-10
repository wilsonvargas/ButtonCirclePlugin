using TestAppButtonCircle.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAppButtonCircle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
            BindingContext = vm = new SecondPageViewModel();
            vm.Navigation = this.Navigation;
        }

        private SecondPageViewModel vm;
    }
}
