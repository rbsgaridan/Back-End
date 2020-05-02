using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;

namespace YamangTao.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetOrgUnit(int id)
        {
            
            var OrgUnit = await _repo.GetOrgUnit(id);
            var OrgUnitToReturn = _mapper.Map<OrgUnitDto>(OrgUnit);
            return Ok(OrgUnitToReturn);
        }

        [HttpGet("{id}/withchildren", Name = "GetOrgUnitWithChildres")]
        public async Task<IActionResult> GetOrgUnitWithChildren(int id)
        {
            
            var OrgUnit = await _repo.GetOrgUnitWithChildren(id);
            var OrgUnitToReturn = _mapper.Map<OrgUnitDto>(OrgUnit);
            return Ok(OrgUnitToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrgUnits()
        {
            var orgunits = await _repo.GetAllOrgUnit();
            var orgunitsToReturn = _mapper.Map<IEnumerable<OrgUnitListDto>>(orgunits);
            return Ok(orgunitsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrgUnit(int id, OrgUnitUpdateDto orgUnitForUpdate)
        {
            var orgUnitFromRepo = await _repo.GetOrgUnit(orgUnitForUpdate.Id);

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
            orgUnit.ParentUnit = await _repo.GetOrgUnit(orgUnitForCreationDto.ParentUnitId);
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

    }
}