using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitRoute.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TransitRoute.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        public Search Search { get; set; }

        public SearchPage ()
		{
			InitializeComponent ();

            Search = new Search();

            BindingContext = this;
        }

        async void Search_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage(Search));
        }
    }
}