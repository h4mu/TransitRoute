using System;
using System.Linq;
using TransitRoute.Models;
using TransitRoute.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TransitRoute.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CountriesPage : ContentPage
	{
        private readonly CountriesViewModel viewModel;

        public CountriesPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new CountriesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Country;
            if (item == null)
                return;

            await Navigation.PushAsync(new FeedsPage(item));

            CountriesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!viewModel.IsBusy)
            {
                viewModel.IsBusy = true;
                CountriesListView.BeginRefresh();

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    CountriesListView.ItemsSource = viewModel.Items;
                }
                else
                {
                    CountriesListView.ItemsSource = viewModel.Items.Where(i => i.Name.IndexOf(e.NewTextValue, StringComparison.CurrentCultureIgnoreCase) >= 0);
                }

                CountriesListView.EndRefresh();
                viewModel.IsBusy = false;
            }
        }
    }
}