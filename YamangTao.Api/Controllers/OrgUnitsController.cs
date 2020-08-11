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

namespace YamangTao.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireHRrole")]
    public class OrgUnitsController : ControllerBase
    {
        private readonly IOrgUnitRepository _repo;
        private readonly IMapper _mapper;
        public OrgUnitsController(IOrgUnitRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{id}", Name = "GetOrgUnit")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrgUnit(int id)
        {
            var OrgUnit = await _repo.GetOrgUnit(id);
            var OrgUnitToReturn = _mapper.Map<OrgUnitDto>(OrgUnit);
            return Ok(OrgUnitToReturn);
        }

        [HttpGet("{id}/withchildren", Name = "GetOrgUnitWithChildres")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrgUnitWithChildren(int id)
        {
            
            var OrgUnit = await _repo.GetOrgUnitWithChildren(id);
            var OrgUnitToReturn = _mapper.Map<OrgUnitDto>(OrgUnit);
            return Ok(OrgUnitToReturn);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchOrgUnits([FromQuery] string keyword)
        {
            var unitParams = new OrgUnitParams() {
                                PageSize = 10,
                                PageNumber = 1,
                                Keyword = keyword
                            };
            var orgunits = await _repo.SearchOrgUnitsPaged(unitParams);
            var orgunitsToReturn = _mapper.Map<IEnumerable<OrgUnitListDto>>(orgunits);
            return Ok(orgunitsToReturn);
        }

        [HttpGet("orgunitsfulltree")]
        [AllowAnonymous]
        public async Task<IActionResult> LoadFullTree()
        {
            var orgUnits = await _repo.GetAllOrgUnitsWithChildren();
            var OrgUnitsToReturn = _mapper.Map<IEnumerable<OrgUnitDto>>(orgUnits);
            return Ok(OrgUnitsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrgUnit(int id, OrgUnitUpdateDto orgUnitForUpdate)
        {
            var orgUnitFromRepo = await _repo.GetOrgUnit(orgUnitForUpdate.Id);
            if (orgUnitForUpdate.ParentUnitId == 0)
            {
                orgUnitForUpdate.ParentUnitId = null;
            }
            _mapper.Map(orgUnitForUpdate, orgUnitFromRepo);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating OrgUnit {orgUnitForUpdate.UnitName} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrgUnit(OrgUnitUpdateDto orgUnitForCreationDto)
        {
            var orgUnit = _mapper.Map<OrgUnit>(orgUnitForCreationDto);
            if (orgUnitForCreationDto.ParentUnitId != null)
            {
                orgUnit.ParentUnit = await _repo.GetOrgUnit(orgUnitForCreationDto.ParentUnitId);
            }
            await _repo.AddAsync(orgUnit);
            if (await _repo.SaveAll())
            {
                var orgUnitToReturn = _mapper.Map<OrgUnitDto>(orgUnit);
                return CreatedAtRoute("GetOrgUnit", new { id = orgUnit.Id }, orgUnitToReturn);
            }

            throw new Exception("Creating the  failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteOrgUnit(int id)
        {
            var orgUnitFromRepo = await _repo.GetOrgUnit(id);
            _repo.Remove(orgUnitFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the orgUnit");
        }

        [HttpGet("list/all")]
        public async Task<IActionResult> ListAllOrgUnits()
        {
            //TODO: Implement Realistic Implementation
          return Ok(await _repo.GetAllOrgUnit());
        }

    }
}