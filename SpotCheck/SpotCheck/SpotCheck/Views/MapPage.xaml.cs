using SpotCheck.Models;
using SpotCheck.Services;
using SpotCheck.Utils;
using SpotCheck.ViewModels;
using System;
using System.ComponentModel;


using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SpotCheck.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MapPage : ContentPage
    {
        IParkingLotService parkingLotService = new ParkingLotServiceImpl();

        static double lat;
        static double lng;
        bool timerOn = false;     
        ItemsViewModel viewModel;

        CustomMap customMap = new CustomMap()
        {
            MapType = MapType.Street
        };

        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            AddPinOnLoad();

            Content = customMap;

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
      
            timerOn = true;
            AddPinOnLoad();
            Content = customMap;
            InitTimer();
        }
       
        private async void AddPinOnLoad()
        {
            await MapUtils.RetrieveLocation();
            lat = MapUtils.getLat();
            lng = MapUtils.getLng();
            /*
                        var customPin = new CustomPin
                        {
                            Type = PinType.Place,
                            Position = new Position(lat, lng),
                            Label = "My Position!",
                            id = "myPin"
                        };

                        customMap.Pins.Clear();
                        customMap.Pins.Add(customPin);
                        */
            ParkingLotList lots = await parkingLotService.GetParkingLots();
            foreach (OldParkingLot lot in lots.parkingLotList)
            {
             
                var lotPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(lot.lat, lot.lon),
                    Label = lot.lotName + " Open Spots: " + lot.OpenSpots,
                    id = "lot" + lot.lotId,
                    url = ""
                };
                if (customMap.Pins.Contains(lotPin))
                {
                    customMap.Pins.Remove(lotPin);
                }
                customMap.Pins.Clear();
                customMap.Pins.Add(lotPin);
            }

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lng), Distance.FromMiles(0.1)));

        }
        private async void AddPinsToMap()
        {
            await MapUtils.RetrieveLocation();
            lat = MapUtils.getLat();
            lng = MapUtils.getLng();

            /*var customPin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(lat, lng),
                Label = "",
                id = "userPin"
            };
            
            //Add pin to map
            customMap.Pins.Clear();
            customMap.Pins.Add(customPin);
            */

            ParkingLotList lots = await parkingLotService.GetParkingLots();
            foreach (OldParkingLot lot in lots.parkingLotList)
            {
             
                var lotPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(lot.lat, lot.lon),
                    Label = lot.lotName + " Open Spots: " + lot.OpenSpots,
                    id = "lot" + lot.lotId,
                    url = "",
                };
                customMap.Pins.Clear();
                customMap.Pins.Add(lotPin);
            }

        }

        public void InitTimer()
        {
            int secondsInterval = 1;
            Device.StartTimer(TimeSpan.FromSeconds(secondsInterval), () =>
            {
                Device.BeginInvokeOnMainThread(() => AddPinsToMap());
                return timerOn;
            });
        }
    }
}
