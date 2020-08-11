using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Data.Core;
using YamangTao.Api.Dtos.LND;
using YamangTao.Model.LND;

namespace YamangTao.Api.Controllers.Lnd
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireHRrole")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILndRepository _repo;
        private readonly IMapper _mapper;
        public ActivitiesController(ILndRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetActivity")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _repo.GetById<Activity, int>(id);
            var activityToReturn = _mapper.Map<ActivityDto>(activity);
            return Ok(activityToReturn);
        }

        


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var activitys = await _repo.GetAll<Activity>();
            var activitysToReturn = _mapper.Map<IEnumerable<ActivityDto>>(activitys);
            return Ok(activitysToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int id, ActivityDto activityForUpdate)
        {
            var activityFromRepo = await _repo.GetById<Activity, string>(activityForUpdate.Id);
           
            _mapper.Map(activityForUpdate, activityFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating activity {activityForUpdate.Title} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> Createactivity(ActivityDto activityForCreationDto)
        {
            var activity = _mapper.Map<Activity>(activityForCreationDto);
           
            _repo.Add(activity);
            if (await _repo.SaveAllAsync())
            {
                var activityToReturn = _mapper.Map<ActivityDto>(activity);
                return CreatedAtRoute("GetActivity", new { id = activity.Id }, activityToReturn);
            }

            throw new Exception("Creating the Activity failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activityFromRepo = await _repo.GetById<Activity, int>(id);
            _repo.Delete(activityFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the activity");
        }

        [HttpGet("list/all")]
        public async Task<IActionResult> ListAllactivitys()
        {
            //TODO: Implement Realistic Implementation
          return Ok(await _repo.GetAll<Activity>());
        }

    }
}