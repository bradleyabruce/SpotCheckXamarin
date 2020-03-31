using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SpotCheck.Models;
using SpotCheck.ViewModels;
using SpotCheck.Utils;
using Xamarin.Forms.Maps;
using SpotCheck.Services;

namespace SpotCheck.Views
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ItemDetailPage : ContentPage
   {
      ItemDetailViewModel viewModel;
        IParkingLotService parkingLotService = new ParkingLotServiceImpl();

        static double lat;
        static double lng;
        CustomMap customMap = new CustomMap()
        {
            MapType = MapType.Street
        };


        public ItemDetailPage(ItemDetailViewModel viewModel)
      {
         InitializeComponent();
        

            BindingContext = this.viewModel = viewModel;
            AddPinOnLoad();
            
        }

      public ItemDetailPage()
      {
         InitializeComponent();
         var item = new ParkingLot
         {
            Text = "Item 1",
            Description = "Ryan Bunker is the fucking best around",
         };

         viewModel = new ItemDetailViewModel(item);
         BindingContext = viewModel;
         AddPinOnLoad();

        }
        private void AddPinOnLoad()
        {
           
            lat = viewModel.Item.lat;
            lng = viewModel.Item.lon;


            CustomPin lotPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(viewModel.Item.lat, viewModel.Item.lon),
                    Label = viewModel.Item.lotName + " Open Spots: " + viewModel.Item.OpenSpots,
                    id = "lot" + viewModel.Item.lotId,
                    url = ""
                };
                customMap.Pins.Add(lotPin);
           

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMiles(0.1)));
            Content = customMap;

        }
    }
}