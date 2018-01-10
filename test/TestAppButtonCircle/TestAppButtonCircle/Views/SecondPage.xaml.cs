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
	public partial class SecondPage : ContentPage
	{
        SecondPageViewModel vm;
        public SecondPage ()
		{
			InitializeComponent ();
            BindingContext = vm = new SecondPageViewModel();
            vm.Navigation = this.Navigation;
        }
	}
}