using System;
using System.Diagnostics;
using System.Threading.Tasks;

using TransitRoute.Helpers;
using TransitRoute.Models;
using TransitRoute.Services;

using Xamarin.Forms;

namespace TransitRoute.ViewModels
{
    public class CountriesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Country> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CountriesViewModel()
        {
            Title = "Select Country";
            Items = new ObservableRangeCollection<Country>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadFeedsCommand());
        }

        async Task ExecuteLoadFeedsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var countries = await FeedsService.GetCountriesAsync();
                Items.ReplaceRange(countries);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load countries.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
