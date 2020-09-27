using System.Collections;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto;
using YamangTao.Core.HttpParams;
using YamangTao.Core.Repository;
using System.Collections.Generic;
using YamangTao.Api.Helpers;
using YamangTao.Model;
using YamangTao.Data.Core;
using YamangTao.Model.RSP;
using Microsoft.AspNetCore.Authorization;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireAdminRole")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IJobPositionRepository _repo;
        private readonly IMapper _mapper;
        public JobPositionsController(IJobPositionRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetJobPosition")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJobPosition(int id)
        {
            //TODO: Implement Realistic Implementation
            var jobPosition = await _repo.GetJobPosition(id);
            var jobPositionToReturn = _mapper.Map<JobPositionDto>(jobPosition);
            return Ok(jobPositionToReturn);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchPositionsPagedPaged([FromQuery] JobPositionParams jobPositionParams)
        {
            var jobPositions = await _repo.SearchPositionsPaged(jobPositionParams);
            var jobPositionsToReturn = _mapper.Map<IEnumerable<JobPositionDto>>(jobPositions);
            Response.AddPagination(jobPositions.CurrentPage, 
                                    jobPositions.TotalCount, 
                                    jobPositions.PageSize, 
                                    jobPositions.TotalPages);
            return Ok(jobPositionsToReturn);
        }


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchPositions([FromQuery] string keyword)
        {
            var positionParams = new JobPositionParams 
                            {
                                PageNumber = 1,
                                PageSize = 10,
                                Keyword = keyword
                            };
            var jobPositions = await _repo.SearchPositionsPaged(positionParams);
            var jobPositionsToReturn = _mapper.Map<IEnumerable<JobPositionDto>>(jobPositions);
            Response.AddPagination(jobPositions.CurrentPage, 
                                    jobPositions.TotalCount, 
                                    jobPositions.PageSize, 
                                    jobPositions.TotalPages);
            return Ok(jobPositionsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(string id, JobPositionDto jobPositionForUpdate)
        {
            var jobPositionFromRepo = await _repo.GetJobPosition(jobPositionForUpdate.Id);
            _mapper.Map(jobPositionForUpdate, jobPositionFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating JobPosition {jobPositionForUpdate.Id} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPosition(JobPositionDto jobPositionForCreationDto)
        {
            if (await _repo.VerifyJobPosition(jobPositionForCreationDto.Name))
            {
                throw new Exception($"{jobPositionForCreationDto.Name} alredy exists!");
            }
            var jobPosition = _mapper.Map<JobPosition>(jobPositionForCreationDto);
            await _repo.AddAsync(jobPosition);
            if (await _repo.SaveAll())
            {
                var jobPositionToReturn = _mapper.Map<JobPositionDto>(jobPosition);
                return CreatedAtRoute("GetJobPosition", new { id = jobPosition.Id }, jobPositionToReturn);
            }

            throw new Exception("Creating the Job Position failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            var jobPositionFromRepo = await _repo.GetJobPosition(id);
            _repo.Remove(jobPositionFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the Job Position");
        }
    }
}
