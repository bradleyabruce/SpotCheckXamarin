using System;

using SpotCheck.Models;

namespace SpotCheck.ViewModels
{
   public class ItemDetailViewModel : BaseViewModel
   {
      public ParkingLot Item { get; set; }
      public ItemDetailViewModel(ParkingLot item = null)
      {
         Title = item?.Text;
         Item = item;
      }
   }
}
