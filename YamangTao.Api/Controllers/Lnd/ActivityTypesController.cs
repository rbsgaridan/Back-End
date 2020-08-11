using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;
using YamangTao.Api.Dtos.LND;

namespace YamangTao.Api.Controllers.Lnd
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireHRrole")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly ILndRepository _repo;
        private readonly IMapper _mapper;
        public ActivityTypesController(ILndRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{id}", Name = "GetActivityType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivityType(int id)
        {
            var activityType = await _repo.GetById<int>(id);
            var activityTypeToReturn = _mapper.Map<ActivityTypeDto>(activityType);
            return Ok(activityTypeToReturn);
        }

        

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchactivityTypes([FromQuery] string keyword)
        {
            var unitParams = new activityTypeParams() {
                                PageSize = 10,
                                PageNumber = 1,
                                Keyword = keyword
                            };
            var activityTypes = await _repo.SearchactivityTypesPaged(unitParams);
            var activityTypesToReturn = _mapper.Map<IEnumerable<activityTypeListDto>>(activityTypes);
            return Ok(activityTypesToReturn);
        }

        [HttpGet("activityTypesfulltree")]
        [AllowAnonymous]
        public async Task<IActionResult> LoadFullTree()
        {
            var activityTypes = await _repo.GetAllactivityTypesWithChildren();
            var activityTypesToReturn = _mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);
            return Ok(activityTypesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateactivityType(int id, activityTypeUpdateDto activityTypeForUpdate)
        {
            var activityTypeFromRepo = await _repo.GetActivityType(activityTypeForUpdate.Id);
            if (activityTypeForUpdate.ParentUnitId == 0)
            {
                activityTypeForUpdate.ParentUnitId = null;
            }
            _mapper.Map(activityTypeForUpdate, activityTypeFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating activityType {activityTypeForUpdate.UnitName} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateactivityType(activityTypeUpdateDto activityTypeForCreationDto)
        {
            var activityType = _mapper.Map<activityType>(activityTypeForCreationDto);
            if (activityTypeForCreationDto.ParentUnitId != null)
            {
                activityType.ParentUnit = await _repo.GetActivityType(activityTypeForCreationDto.ParentUnitId);
            }
            await _repo.AddAsync(activityType);
            if (await _repo.SaveAll())
            {
                var activityTypeToReturn = _mapper.Map<ActivityTypeDto>(activityType);
                return CreatedAtRoute("GetActivityType", new { id = activityType.Id }, activityTypeToReturn);
            }

            throw new Exception("Creating the  failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteactivityType(int id)
        {
            var activityTypeFromRepo = await _repo.GetActivityType(id);
            _repo.Remove(activityTypeFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the activityType");
        }

        [HttpGet("list/all")]
        public async Task<IActionResult> ListAllactivityTypes()
        {
            //TODO: Implement Realistic Implementation
          return Ok(await _repo.GetAllactivityType());
        }

    }
}