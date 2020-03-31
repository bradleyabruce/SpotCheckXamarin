using System;
using System.ComponentModel;
using Xamarin.Forms;
using SpotCheck.Models;
using SpotCheck.ViewModels;
using Xamarin.Forms.Maps;

namespace SpotCheck.Views
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ItemsPage : ContentPage
   {
      ItemsViewModel viewModel;
        static double lat;
        static double lng;
        CustomMap customMap = new CustomMap()
        {
            MapType = MapType.Street
        };

        public ItemsPage()
      {
         InitializeComponent();

         BindingContext = viewModel = new ItemsViewModel();
      }

      async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
      {
         var item = args.SelectedItem as ParkingLot;
         if (item == null)
            return;

         await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            AddPinOnLoad(item);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
      }

        private void AddPinOnLoad(ParkingLot lot)
        {

            lat = lot.lat;
            lng = lot.lon;


            CustomPin lotPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(lot.lat, lot.lon),
                Label = lot.lotName + " Open Spots: " + lot.OpenSpots,
                id = "lot" + lot.lotId,
                url = ""
            };
            customMap.Pins.Clear();
            customMap.Pins.Add(lotPin);


            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMiles(0.1)));

        }

        async void AddItem_Clicked(object sender, EventArgs e)
      {
         await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();

       
        viewModel.LoadItemsCommand.Execute(null);
      }
   }
}