using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    public class DoorRepositoryService : IDoorRepositoryService
    {
        private readonly DoorRepositoryDatabaseContext _userRepositoryDatabaseContext;

        public DoorRepositoryService(DoorRepositoryDatabaseContext userRepositoryDatabaseContext)
        {
            _userRepositoryDatabaseContext = userRepositoryDatabaseContext;
        }

        public async Task<List<string>> GetDoorsIds()
        {
            if (_userRepositoryDatabaseContext.Doors.Any())
            {
                return await _userRepositoryDatabaseContext.Doors.Select(x => x.Id).ToListAsync();
            }

            return new List<string>();
        }

        public async Task<DoorRecordDto?> GetDoor(string doorId)
        {
            DoorRecord? doorRecord = await _userRepositoryDatabaseContext.Doors.FindAsync(doorId);

            return doorRecord == null ? null : new DoorRecordDto(doorRecord);
        }

        public async Task<DoorRecordDto> AddDoor(DoorRecordDto door)
        {
            var record = new DoorRecord
            {
                Label = door.Label,
                IsLocked = door.IsLocked,
                IsOpen = door.IsOpen
            };
            await _userRepositoryDatabaseContext.Doors.AddAsync(record);
            await _userRepositoryDatabaseContext.SaveChangesAsync();
            return new DoorRecordDto(record);
        }

        public async Task<DoorRecordDto?> RemoveDoor(string doorId)
        {
            var record = await _userRepositoryDatabaseContext.Doors.FindAsync(doorId);

            if (record == null)
            {
                return null;
            }

            _userRepositoryDatabaseContext.Remove(record);

            await _userRepositoryDatabaseContext.SaveChangesAsync();

            return new DoorRecordDto(record);
        }

        public async Task<DoorRecordDto?> UpdateDoor(DoorRecordDto door)
        {
            DoorRecord? doorRecord = await _userRepositoryDatabaseContext.Doors.FindAsync(door.Id);

            if(doorRecord == null)
            {
                return null;
            }

            doorRecord.Label = door.Label;
            doorRecord.IsLocked = door.IsLocked;
            doorRecord.IsOpen = door.IsOpen;

            this._userRepositoryDatabaseContext.Attach(doorRecord);
            await this._userRepositoryDatabaseContext.SaveChangesAsync();

            return door;
        }
    }
}
