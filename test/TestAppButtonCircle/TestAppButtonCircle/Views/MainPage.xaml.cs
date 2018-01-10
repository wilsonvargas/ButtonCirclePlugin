using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppButtonCircle.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAppButtonCircle.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        MainPageViewModel vm;
        public MainPage ()
		{
			InitializeComponent ();
            BindingContext = vm = new MainPageViewModel();
            vm.Navigation = this.Navigation;
        }
	}
}