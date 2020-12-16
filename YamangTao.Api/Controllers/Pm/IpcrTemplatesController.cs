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
    [Authorize(Policy="RequireAdminRole")]
    public class IpcrTemplatesController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public IpcrTemplatesController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewIpcrTemplate(IpcrTemplateDto rmDto)
        {
            rmDto.DateCreated = DateTime.Now;
            rmDto.DateLastModified = DateTime.Now;
            var ipcrTeamplateForCreate = _mapper.Map<IpcrTemplate>(rmDto);
            _repo.Add(ipcrTeamplateForCreate);
            if (await _repo.SaveAllAsync())
            {
                var ipcrTemplateToReturn = _mapper.Map<IpcrTemplateDto>(ipcrTeamplateForCreate);
                return CreatedAtRoute("GetIpcrTemplateById", new { id = ipcrTeamplateForCreate.Id }, ipcrTemplateToReturn);
            }

            throw new Exception("Adding the IPCR Template failed on save");

        }

        [HttpGet("{id}", Name = "GetIpcrTemplateById")]
        public async Task<IActionResult> GetIpcrTemplateById(int id)
        {
            
            var ipcrTemplate = await _repo.GetById<IpcrTemplate, int>(id);
            var ipcrTemplateToReturn = _mapper.Map<IpcrTemplateDto>(ipcrTemplate);
            return Ok(ipcrTemplateToReturn);
        }

        [HttpGet("{id}/full", Name = "GetIpcrTemplateByIdFull")]
        public async Task<IActionResult> GetIpcrTemplateByIdFull(int id)
        {
            
            var ipcrTemplate = await _repo.GetById<IpcrTemplate, int>(id);
            var kpiTemplates = await _repo.GetKPITemplateForIpcr(ipcrTemplate.Id);
            IpcrTemplateFullDto forReturn = new IpcrTemplateFullDto {
                Id = ipcrTemplate.Id,
                Description = ipcrTemplate.Description,
                JobPositionId = ipcrTemplate.JobPositionId,
                JobPosition = ipcrTemplate.Description,
                OrgUnitId = ipcrTemplate.OrgUnitId,
                Kpis = _mapper.Map<List<KpiTemplateDto>>(kpiTemplates),
                DateCreated = ipcrTemplate.DateCreated,
                DateLastModified = ipcrTemplate.DateLastModified
            };

            // var ipcrTemplateToReturn = _mapper.Map<IpcrTemplateFullDto>(ipcrTemplate);
            return Ok(forReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetIpcrTemplatePaged([FromQuery] PmsParams ipcrTemplateParams)
        {
            
            var ipcrTemplate = await _repo.GetPaged<IpcrTemplate, int>(ipcrTemplateParams);
            var ipcrTemplateToReturn = _mapper.Map<IEnumerable<IpcrTemplateDto>>(ipcrTemplate);
            Response.AddPagination(ipcrTemplate.CurrentPage, 
                                    ipcrTemplate.TotalCount, 
                                    ipcrTemplate.PageSize, 
                                    ipcrTemplate.TotalPages);
            return Ok(ipcrTemplateToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIpcrTemplate(int id, IpcrTemplateDto ipcrTemplateForUpdate)
        {

         
            var ipcrTemplateFromRepo = await _repo.GetById<IpcrTemplate, int>(ipcrTemplateForUpdate.Id);
            _mapper.Map(ipcrTemplateForUpdate, ipcrTemplateFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating IPCR Template failed on save.");
        }

        [HttpPut("{id}/full")]
        public async Task<IActionResult> UpdateIpcrTemplateFull(int id, IpcrTemplateFullDto ipcrTemplateForUpdate)
        {

            var ipcrTemplateFromRepo = await _repo.GetById<IpcrTemplate, int>(ipcrTemplateForUpdate.Id);
            _mapper.Map(ipcrTemplateForUpdate, ipcrTemplateFromRepo);

            foreach (var kpiTemplateDto in ipcrTemplateForUpdate.Kpis)
            {
                var kpiTemplateFromRepo = await _repo.GetKPITemplateFullById(kpiTemplateDto.Id);
                 _mapper.Map(kpiTemplateDto, kpiTemplateFromRepo);
            }

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating IPCR Template failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteIpcrTemplate(int id)
        {
            var ipcrTemplateFromRepo = await _repo.GetById<IpcrTemplate, int>(id);
            var pmsKpiParam = new PmsParams() {
                FilterByKey = "IpcrTemplateId",
                KeyInt = ipcrTemplateFromRepo.Id
            };
            var kpisInIpcr = await _repo.GetList<KpiTemplate, int>(pmsKpiParam);
            // ToDo Delete or children
            _repo.DeleteRange(kpisInIpcr);
            _repo.Delete(ipcrTemplateFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the IPCR Template");
        }

        
    }
}