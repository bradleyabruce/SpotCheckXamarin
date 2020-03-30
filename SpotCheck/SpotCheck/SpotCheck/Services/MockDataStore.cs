using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotCheck.Models;

namespace SpotCheck.Services
{
   public class MockDataStore : IDataStore<ParkingLot>
   {
        List<ParkingLot> items;
        ParkingLotList parkingLots = new ParkingLotList();
        List<OldParkingLot> lots = new List<OldParkingLot>();
        IParkingLotService parkingLotService = new ParkingLotServiceImpl();


      public MockDataStore()
      {
            GetParkingLots();

            foreach (OldParkingLot lot in lots)
            {
                ParkingLot newItem = new ParkingLot { Id = lot.lotId.ToString(), Text = lot.lotName, Description = "Open Parking Spots " + lot.OpenSpots };
                items.Add(newItem);
            }

            /*
         items = new List<Item>()
            {
                new Item { Id =S, Text = "First fuck you item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second fuck you item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third fuck you item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "4 fuck you item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "5 fuck you item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "6 fuck you item", Description="This is an item description." }
            };
            */
      }


   
        public async Task<bool> AddItemAsync(ParkingLot item)
      {
         items.Add(item);

         return await Task.FromResult(true);
      }

      public async Task<bool> UpdateItemAsync(ParkingLot item)
      {
         var oldItem = items.Where((ParkingLot arg) => arg.Id == item.Id).FirstOrDefault();
         items.Remove(oldItem);
         items.Add(item);

         return await Task.FromResult(true);
      }

      public async Task<bool> DeleteItemAsync(string id)
      {
         var oldItem = items.Where((ParkingLot arg) => arg.Id == id).FirstOrDefault();
         items.Remove(oldItem);

         return await Task.FromResult(true);
      }

      public async Task<ParkingLot> GetItemAsync(string id)
      {
         return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
      }

        public async Task<ParkingLotList> GetParkingLots()
        {
            return await Task.FromResult(parkingLots);
        }

      public async Task<IEnumerable<ParkingLot>> GetItemsAsync(bool forceRefresh = false)
      {
         return await Task.FromResult(items);
      }
   }
}