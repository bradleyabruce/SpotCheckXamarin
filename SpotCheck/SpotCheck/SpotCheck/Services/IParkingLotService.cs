
using SpotCheck.Models;
using System.Threading.Tasks;


namespace SpotCheck.Services
{
    public interface IParkingLotService
    {
         Task<ParkingLotList> GetParkingLots();
    }
}
