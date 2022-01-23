using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service.IService
{
    public interface IStoreService
    {
        public Task<IEnumerable<StoreDTO>> GetAllStores();
        //public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}
