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
    [Route("api/v1/[controller]")]
    [Authorize(Policy="RequirePMTRole")]
    public class KpiTemplatesController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public KpiTemplatesController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewKpiTemplate(KpiTemplateDto kpiDto)
        {
            var KpiForCreate = _mapper.Map<KpiTemplate>(kpiDto);
            _repo.Add(KpiForCreate);
            if (await _repo.SaveAllAsync())
            {
                var kpiToReturn = _mapper.Map<KpiTemplateDto>(KpiForCreate);
                return CreatedAtRoute("GetKpiTemplateById", new { id = KpiForCreate.Id }, kpiToReturn);
            }

            throw new Exception("Adding the kpiTemplate failed on save");

        }

        [HttpGet("{id}", Name = "GetKpiTemplateById")]
        public async Task<IActionResult> GetKpiTemplateById(int id)
        {
            
            var kpi = await _repo.GetById<KpiTemplate, int>(id);
            var kpiToReturn = _mapper.Map<KpiTemplateDto>(kpi);
            return Ok(kpiToReturn);
        }

        [HttpGet("{id}/full")]
        public async Task<IActionResult> GetKpiTemplateFullById(int id)
        {
            var kpi = await _repo.GetKPITemplateFullById(id);
            var kpiToReturn = _mapper.Map<KpiTemplateDto>(kpi);
            return Ok(kpiToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetKpiTemplatePaged([FromQuery] PmsParams kpiTemplateParams)
        {
            
            var kpis = await _repo.GetPaged<KpiTemplate, int>(kpiTemplateParams);
            var kpisToReturn = _mapper.Map<IEnumerable<KpiTemplateDto>>(kpis);
            Response.AddPagination(kpis.CurrentPage, 
                                    kpis.TotalCount, 
                                    kpis.PageSize, 
                                    kpis.TotalPages);
            return Ok(kpisToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKpiTemplate(int id, KpiTemplateDto kpiForUpdate)
        {

         
            var rKpiFromRepo = await _repo.GetById<KpiTemplate, int>(kpiForUpdate.Id);
            _mapper.Map(kpiForUpdate, rKpiFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating kpiTemplate failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteKpiTemplate(int id)
        {
            var kpiFromRepo = await _repo.GetById<KpiTemplate, int>(id);
            
            // ToDo Delete or children
            _repo.Delete(kpiFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the Rating Matrix");
        }

        
    }
}