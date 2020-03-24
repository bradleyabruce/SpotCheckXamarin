using SpotCheck.Models;
using SpotCheck.Services;
using SpotCheck.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

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

        CustomMap customMap = new CustomMap()
        {
            MapType = MapType.Street
        };

        public MapPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            timerOn = true;
            AddPinOnLoad();
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
            foreach (ParkingLot lot in lots.parkingLotList)
            {
                var lotPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(lot.lat, lot.lon),
                    Label = lot.lotName + " Open Spots: " + lot.OpenSpots,
                    id = "lot" + lot.lotId,
                    url = ""
                };
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
            foreach (ParkingLot lot in lots.parkingLotList)
            {
                var lotPin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(lot.lat, lot.lon),
                    Label = lot.lotName + " Open Spots: " + lot.OpenSpots,
                    id = "lot" + lot.lotId,
                    url = "",
                };
                customMap.Pins.Add(lotPin);
            }

        }

        public void InitTimer()
        {
            int secondsInterval = 5;
            Device.StartTimer(TimeSpan.FromSeconds(secondsInterval), () =>
            {
                Device.BeginInvokeOnMainThread(() => AddPinsToMap());
                return timerOn;
            });
        }
    }
}
