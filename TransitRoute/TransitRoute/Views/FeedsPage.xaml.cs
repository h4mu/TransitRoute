using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitRoute.ViewModels;
using TransitRoute.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TransitRoute.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedsPage : ContentPage
	{
        FeedsViewModel viewModel;

		public FeedsPage (Country country)
		{
            InitializeComponent();

            BindingContext = viewModel = new FeedsViewModel(country);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Feed;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            FeedsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}