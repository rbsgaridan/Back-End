using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto.Pms;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.PM;
using System.Collections.Generic;
using YamangTao.Api.Helpers;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class KpisController : ControllerBase
    {
        
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public KpisController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewKpi(KpiDto kpiDto)
        {
            var kpiForCreate = _mapper.Map<Kpi>(kpiDto);
            _repo.Add(kpiForCreate);
            if (await _repo.SaveAllAsync())
            {
                var kpiToReturn = _mapper.Map<KpiDto>(kpiForCreate);
                return CreatedAtRoute("GetKpiById", new { id = kpiForCreate.Id }, kpiToReturn);
            }

            throw new Exception("Adding the kpi failed on save");

        }

        [HttpGet("{id}", Name = "GetKpiById")]
        public async Task<IActionResult> GetKpiById(int id)
        {
            
            var kpi = await _repo.GetById<Kpi, int>(id);
            var kpiToReturn = _mapper.Map<KpiDto>(kpi);
            return Ok(kpiToReturn);
        }

        [HttpGet("{id}/full", Name = "GetKpiFullById")]
        public async Task<IActionResult> GetKpiFullById(int id)
        {
            
            var kpi = await _repo.GetKPIFullById(id);
            var kpiToReturn = _mapper.Map<KpiDto>(kpi);
            return Ok(kpiToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetKpiPaged([FromQuery] PmsParams kpiParams)
        {
            //  if (!HasValidRole(kpiParams.EmployeeId))
            // {
            //     return Unauthorized("You do not have clearance to update what is not yours!");
            // }

            var kpis = await _repo.GetPaged<Kpi, int>(kpiParams);
            var kpisToReturn = _mapper.Map<IEnumerable<KpiDto>>(kpis);
            Response.AddPagination(kpis.CurrentPage, 
                                    kpis.TotalCount, 
                                    kpis.PageSize, 
                                    kpis.TotalPages);
            return Ok(kpisToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKpi(int id, KpiDto kpiForUpdate)
        {

            // if (!HasValidRole(kpiForUpdate.EmployeeId))
            // {
            //     return Unauthorized("You do not have clearance to update what is not yours!");
            // }

            var kpiFromRepo = await _repo.GetById<Kpi, int>(kpiForUpdate.Id);
            _mapper.Map(kpiForUpdate, kpiFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Kpi of {kpiForUpdate.EmployeeId} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteKpi(int id)
        {
            var kpiFromRepo = await _repo.GetById<Kpi, int>(id);
            // if (!HasValidRole(kpiFromRepo.EmployeeId))
            // {
            //     return Unauthorized("You do not have clearance to update what is not yours!");
            // }
            // ToDo Delete or children
            _repo.Delete(kpiFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the kpi");
        }

        
    }
}