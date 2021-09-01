using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;
using Everbridge.ControlCenter.TechnicalChallenge.Models;
using System;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;

namespace Everbridge.ControlCenter.TechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private readonly IDoorRepositoryService _doorRepositoryService;

        //public DoorController(ILogger<DoorController> logger, DoorRepositoryDatabaseContext databaseContext)
        //{
        //    _logger = logger;
        //    _doorRepositoryService = new DoorRepositoryService(databaseContext);
        //}

        public DoorController(ILogger<DoorController> logger, IDoorRepositoryService doorRepositoryService)
        {
            _logger = logger;
            _doorRepositoryService = doorRepositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return await _doorRepositoryService.GetDoorsIds();
        }

        [HttpGet]
        [Route("{doorId}")]
        public async Task<ActionResult<DoorModel>> GetDoor([FromRoute] [Required] string doorId)
        {
            var doorRecord = await _doorRepositoryService.GetDoor(doorId);
            
            if(doorRecord == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new DoorModel
                {
                    Id = doorRecord.Id,
                    Label = doorRecord.Label,
                    IsOpen = doorRecord.IsOpen,
                    IsLocked = doorRecord.IsLocked
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoorModel>> AddDoor([FromBody] DoorModelInput doorModel)
        {
            try
            {
                DoorRecordDto doorRecord = await this._doorRepositoryService.AddDoor(new DoorRecordDto
                {
                    Label = doorModel.Label,
                    IsOpen = doorModel.IsOpen,
                    IsLocked = doorModel.IsLocked
                });

                return Ok(new DoorModel
                {
                    Id = doorRecord.Id,
                    Label = doorRecord.Label,
                    IsOpen = doorRecord.IsOpen,
                    IsLocked = doorRecord.IsLocked
                });
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"The door could not be added:{doorModel}");
            }
        }

        [HttpDelete]
        [Route("{doorId}")]
        public async Task<ActionResult<DoorModel>> RemoveDoor([FromRoute][Required] string doorId)
        {
            try
            {
                DoorRecordDto? doorRecord = await this._doorRepositoryService.RemoveDoor(doorId);

                if (doorRecord == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, $"A door doesn't exist with id:{doorId}");
                }
                else
                {
                    return Ok(new DoorModel
                    {
                        Id = doorRecord.Id,
                        Label = doorRecord.Label,
                        IsOpen = doorRecord.IsOpen,
                        IsLocked = doorRecord.IsLocked
                    });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"The door with id:{doorId} could not be removed.");
            }
        }

        [HttpPatch]
        [Route("{doorId}")]
        public async Task<ActionResult<DoorModel>> PatchDoor([FromRoute][Required] string doorId, JsonPatchDocument<DoorModelInput> patchDoc)
        {
            try
            {
                DoorRecordDto? doorRecord = await _doorRepositoryService.GetDoor(doorId);

                if (doorRecord == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, $"A door doesn't exist with id:{doorId}");
                }
                else
                {
                    var doorModel = new DoorModelInput
                    {
                        Label = doorRecord.Label,
                        IsOpen = doorRecord.IsOpen,
                        IsLocked = doorRecord.IsLocked
                    };

                    patchDoc.ApplyTo(doorModel);

                    doorRecord.Label = doorModel.Label;
                    doorRecord.IsLocked = doorModel.IsLocked;
                    doorRecord.IsOpen = doorModel.IsOpen;
                    
                    var updatedDoor = await this._doorRepositoryService.UpdateDoor(doorRecord);

                    if(updatedDoor == null)
                    {
                        return StatusCode((int)HttpStatusCode.NotFound, $"A door doesn't exist with id:{doorId}");
                    }

                    return Ok(new DoorModel
                    {
                        Id = doorRecord.Id,
                        Label = doorRecord.Label,
                        IsOpen = doorRecord.IsOpen,
                        IsLocked = doorRecord.IsLocked
                    });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"The door with id:{doorId} could not be removed.");
            }
        }
    }
}
