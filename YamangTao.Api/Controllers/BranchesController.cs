using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequirePMTRole")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchCampusRepository _repo;
        private readonly IMapper _mapper;
        public BranchesController(IBranchCampusRepository repo,
                                    IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetBranch")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBranch(int id)
        {
            //TODO: Implement Realistic Implementation
            var campusBranch = await _repo.GetBranchCampus(id);
            var campusBranchToReturn = _mapper.Map<BranchDto>(campusBranch);
            return Ok(campusBranchToReturn);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _repo.GetAllCampuses();
            var branchesToReturn = _mapper.Map<IEnumerable<BranchDto>>(branches);
            return Ok(branchesToReturn);
        }

        [HttpPut("{id}")]
        [Authorize(Policy="RequireAdminRole")]
        public async Task<IActionResult> UpdateBranch(int id, BranchDto branchForUpdate)
        {
            var branchFromRepo = await _repo.GetBranchCampus(branchForUpdate.Id);
            _mapper.Map(branchForUpdate, branchFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating Branch {branchForUpdate.Campus} failed on save.");
        }

        [HttpPost]
        [Authorize(Policy="RequireAdminRole")]
        public async Task<IActionResult> CreateBranch(BranchDto branchForCreationDto)
        {
            var branch = _mapper.Map<BranchCampus>(branchForCreationDto);
            await _repo.AddAsync(branch);
            if (await _repo.SaveAll())
            {
                var branchToReturn = _mapper.Map<BranchDto>(branch);
                return CreatedAtRoute("GetBranch", new { id = branch.Id }, branchToReturn);
            }

            throw new Exception("Creating the campus failed on save");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy="RequireAdminRole")]
        public async Task<IActionResult> deleteBranch(int id)
        {
            var branchFromRepo = await _repo.GetBranchCampus(id);
            _repo.Remove(branchFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the branch");
        }
    }
}