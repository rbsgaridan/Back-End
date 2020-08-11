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
using YamangTao.Model.LND;

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
            var activityType = await _repo.GetById<ActivityType, int>(id);
            var activityTypeToReturn = _mapper.Map<ActivityTypeDto>(activityType);
            return Ok(activityTypeToReturn);
        }

        


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var activityTypes = await _repo.GetAll<ActivityType>();
            var activityTypesToReturn = _mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);
            return Ok(activityTypesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivityType(int id, ActivityTypeDto activityTypeForUpdate)
        {
            var activityTypeFromRepo = await _repo.GetById<ActivityType, int>(activityTypeForUpdate.Id);
           
            _mapper.Map(activityTypeForUpdate, activityTypeFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating activityType {activityTypeForUpdate.Description} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateactivityType(ActivityTypeDto activityTypeForCreationDto)
        {
            var activityType = _mapper.Map<ActivityType>(activityTypeForCreationDto);
           
            _repo.Add(activityType);
            if (await _repo.SaveAllAsync())
            {
                var activityTypeToReturn = _mapper.Map<ActivityTypeDto>(activityType);
                return CreatedAtRoute("GetActivityType", new { id = activityType.Id }, activityTypeToReturn);
            }

            throw new Exception("Creating the Activity failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityType(int id)
        {
            var activityTypeFromRepo = await _repo.GetById<ActivityType, int>(id);
            _repo.Delete(activityTypeFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the activityType");
        }

        [HttpGet("list/all")]
        public async Task<IActionResult> ListAllactivityTypes()
        {
            //TODO: Implement Realistic Implementation
          return Ok(await _repo.GetAll<ActivityType>());
        }

    }
}