using SpotCheck.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SpotCheck.ViewModels
{


    class ParkingLotViewModel
    {
        public ObservableCollection<OldParkingLot> ParkingLots { get; set; }

    }
}
