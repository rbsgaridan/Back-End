using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos.Rsp;
using YamangTao.Api.Helpers;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.RSP.Pds;

namespace YamangTao.Api.Controllers.Rsp.Pds
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EligibilitiesController : ControllerBase
    {
        private readonly IPdsRepository _repo;
        private readonly IMapper _mapper;
        public EligibilitiesController(IPdsRepository repo,
                                    IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        private bool HasValidRole(string employeeId)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value == employeeId;
            if (!isCurrentUser)
            {
                if (!(User.IsInRole("Admin") || User.IsInRole("HR")))
                    {
                        return false;
                    }
            }
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEligibility(EligibilityDto eligibilityDto)
        {
            if (!HasValidRole(eligibilityDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var eligibilityForCreate = _mapper.Map<Eligibility>(eligibilityDto);
            _repo.Add(eligibilityForCreate);
            if (await _repo.SaveAllAsync())
            {
                var eligibilityToReturn = _mapper.Map<EligibilityDto>(eligibilityForCreate);
                return CreatedAtRoute("GetEligibilityById", new { id = eligibilityForCreate.Id }, eligibilityToReturn);
            }

            throw new Exception("Adding the eligibility failed on save");

        }

        [HttpGet("{id}", Name = "GetEligibilityById")]
        public async Task<IActionResult> GetEligibilityById(int id)
        {
            //TODO: Implement Realistic Implementation
            var eligibility = await _repo.GetById<Eligibility>(id);
            var eligibilityToReturn = _mapper.Map<EligibilityDto>(eligibility);
            return Ok(eligibilityToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetEligibilitiesPaged([FromQuery] PdsParams eligibilityParams)
        {
             if (!HasValidRole(eligibilityParams.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var eligibility = await _repo.GetEligibilities(eligibilityParams);
            var eligibilityToReturn = _mapper.Map<IEnumerable<EligibilityDto>>(eligibility);
            Response.AddPagination(eligibility.CurrentPage, 
                                    eligibility.TotalCount, 
                                    eligibility.PageSize, 
                                    eligibility.TotalPages);
            return Ok(eligibilityToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEligibility(int id, EligibilityDto eligibilityForUpdate)
        {

            if (!HasValidRole(eligibilityForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var eligibilityFromRepo = await _repo.GetById<Eligibility>(eligibilityForUpdate.Id);
            _mapper.Map(eligibilityForUpdate, eligibilityFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Eligibility of {eligibilityForUpdate.EmployeeId} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteEligibility(int id)
        {
            var eligibilityFromRepo = await _repo.GetById<Eligibility>(id);
            if (!HasValidRole(eligibilityFromRepo.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            // ToDo Delete or children
            _repo.Delete(eligibilityFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the eligibility");
        }

        [AllowAnonymous]
        [HttpGet("type")]
        public async Task<IActionResult> SearchEligibility(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var streets = await _repo.SearchDistinctEligibility(search);
            return Ok(streets);
        }

        [AllowAnonymous]
        [HttpGet("examplace")]
        public async Task<IActionResult> SearchExamPlace(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var blocks = await _repo.SearchDistinctExamPlace(search);
            return Ok(blocks);
        }

        
    }
}