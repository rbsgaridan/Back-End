using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto.Pms;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.PM;
using YamangTao.Model.PM.Template;
using YamangTao.Dto.Pms.Template;
using System.Collections.Generic;
using YamangTao.Api.Helpers;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequirePMTRole")]
    public class KpiTypesController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public KpiTypesController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewKpiType(KpiTypeDto rmDto)
        {
            var kpiTypeForCreate = _mapper.Map<KpiType>(rmDto);
            _repo.Add(kpiTypeForCreate);
            if (await _repo.SaveAllAsync())
            {
                var kpiTypeToReturn = _mapper.Map<KpiTypeDto>(kpiTypeForCreate);
                return CreatedAtRoute("GetKpiTypeById", new { id = kpiTypeForCreate.Id }, kpiTypeToReturn);
            }

            throw new Exception("Adding the KPI Type failed on save");

        }

        [HttpGet("{id}", Name = "GetKpiTypeById")]
        public async Task<IActionResult> GetKpiTypeById(int id)
        {
            //TODO: Implement Realistic Implementation
            var kpiType = await _repo.GetById<KpiType, int>(id);
            var kpiTypeToReturn = _mapper.Map<KpiTypeDto>(kpiType);
            return Ok(kpiTypeToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetKpiTypePaged([FromQuery] PmsParams kpiTypeParams)
        {
            
            var kpiType = await _repo.GetPaged<KpiType, int>(kpiTypeParams);
            var kpiTypeToReturn = _mapper.Map<IEnumerable<KpiTypeDto>>(kpiType);
            Response.AddPagination(kpiType.CurrentPage, 
                                    kpiType.TotalCount, 
                                    kpiType.PageSize, 
                                    kpiType.TotalPages);
            return Ok(kpiTypeToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetKpiTypeAll()
        {
            
            var kpiTypes = await _repo.GetList<KpiType, int>(new PmsParams());
            var kpiTypeToReturn = _mapper.Map<IEnumerable<KpiTypeDto>>(kpiTypes);
            
            return Ok(kpiTypeToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKpiType(int id, KpiTypeDto kpiTypeForUpdate)
        {

         
            var kpiTypeFromRepo = await _repo.GetById<KpiType, int>(kpiTypeForUpdate.Id);
            _mapper.Map(kpiTypeForUpdate, kpiTypeFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating kpiType failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteKpiType(int id)
        {
            var kpiTypeFromRepo = await _repo.GetById<KpiType, int>(id);
            
            // ToDo Delete or children
            _repo.Delete(kpiTypeFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the KPI Type");
        }

        
    }
}