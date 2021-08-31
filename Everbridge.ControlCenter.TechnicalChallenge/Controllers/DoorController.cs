using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;
using Everbridge.ControlCenter.TechnicalChallenge.Models;

namespace Everbridge.ControlCenter.TechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private readonly DoorRepositoryService _doorRepositoryService;

        public DoorController(ILogger<DoorController> logger, DoorRepositoryDatabaseContext databaseContext)
        {
            _logger = logger;
            _doorRepositoryService = new DoorRepositoryService(databaseContext);
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _doorRepositoryService.GetDoorsIds();
        }

        [HttpGet]
        [Route("{doorId}")]
        public async Task<DoorModel> GetDoor([FromRoute] [Required] string doorId)
        {
            var doorRecord = await _doorRepositoryService.GetDoor(doorId);

            return (doorRecord == null)
                ? null
                : new DoorModel
                {
                    Id = doorRecord.Id,
                    Label = doorRecord.Label,
                    IsOpen = doorRecord.IsOpen,
                    IsLocked = doorRecord.IsLocked
                };
        }
    }
}
