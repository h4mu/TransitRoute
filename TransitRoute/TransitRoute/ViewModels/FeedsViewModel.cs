using System;
using System.Diagnostics;
using System.Threading.Tasks;

using TransitRoute.Helpers;
using TransitRoute.Models;
using TransitRoute.Views;
using TransitRoute.Services;

using Xamarin.Forms;

namespace TransitRoute.ViewModels
{
    public class FeedsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Operator> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Country Country { get; }

        public FeedsViewModel(Country country)
        {
            Title = "Feed Import";
            Items = new ObservableRangeCollection<Operator>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadFeedsCommand());
            Country = country;
        }

        async Task ExecuteLoadFeedsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await FeedsService.GetFeedsAsync(Country);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load feeds.",
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
