using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    public interface IDoorRepositoryService
    {
        Task<DoorRecordDto> AddDoor(DoorRecordDto door);
        Task<DoorRecordDto?> GetDoor(string doorId);
        Task<List<string>> GetDoorsIds();
        Task<DoorRecordDto?> RemoveDoor(string doorId);
        Task<DoorRecordDto?> UpdateDoor(DoorRecordDto door);
    }
}