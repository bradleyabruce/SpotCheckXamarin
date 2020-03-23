using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotCheck.Models;

namespace SpotCheck.Services
{
    class ParkingLotServiceImpl : IParkingLotService
    {
        static HttpClient client = new HttpClient();

          async Task<ParkingLotList> IParkingLotService.GetParkingLots()
        {
            var response = await client.GetStringAsync("http://173.91.255.135:8080/SpotCheckServer-2.1.8.RELEASE/parkingLot/getParkingLots/");
            var lots = JsonConvert.DeserializeObject<ParkingLotList>(response);
            return lots;
        }
    }
}
