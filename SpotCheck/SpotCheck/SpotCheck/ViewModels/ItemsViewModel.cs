using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SpotCheck.Models;
using SpotCheck.Views;
using System.Collections.Generic;
using SpotCheck.Services;

namespace SpotCheck.ViewModels
{
   public class ItemsViewModel : BaseViewModel
   {
      public ObservableCollection<ParkingLot> Items { get; set; }
      public Command LoadItemsCommand { get; set; }

      public ItemsViewModel()
      {
         Title = "Map";
         Items = new ObservableCollection<ParkingLot>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

         MessagingCenter.Subscribe<NewItemPage, ParkingLot>(this, "AddItem", async (obj, item) =>
         {
            var newItem = item as ParkingLot;
            Items.Add(newItem);
            await DataStore.AddItemAsync(newItem);
         });
      }

      async Task ExecuteLoadItemsCommand()
      {
            ParkingLotList lotList = new ParkingLotList();
            List<OldParkingLot> lots = new List<OldParkingLot>();
            IParkingLotService parkingLotService = new ParkingLotServiceImpl();
         if (IsBusy)
            return;

         IsBusy = true;

         try
         {
            Items.Clear();
            lotList = await parkingLotService.GetParkingLots();

            foreach(OldParkingLot lot in lotList.parkingLotList)
                {
                   
                    ParkingLot parkingItem = new ParkingLot();
                    parkingItem.Id = lot.lotId.ToString();
                    parkingItem.Description = "Number of open parking spots " + lot.OpenSpots;
                    parkingItem.Text = lot.lotName;
                    Items.Add(parkingItem);
                }
            /*
            var items = await DataStore.GetItemsAsync(true);
            foreach (var item in items)
            {
               Items.Add(item);
            }
            */
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex);
         }
         finally
         {
            IsBusy = false;
         }
      }
   }
}