using System.Linq;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Buffers;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto.Pms;
using YamangTao.Api.Helpers;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.PM;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class IpcrsController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public IpcrsController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        private bool HasValidRole(string employeeId)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value == employeeId;
            var roles = User.FindAll(ClaimTypes.Role);
            
            if (!isCurrentUser)
            {
                if ((User.IsInRole("Employee") && (roles.Count() == 1) ))
                    {
                        return false;
                    }
            }
            return true;
        }

        [HttpGet("{id}", Name = "GetIpcr")]
        public async Task<IActionResult> GetIpcr(int id)
        {
            
            var ipcr = await _repo.GetIpcrFullById(id);
            if (!HasValidRole(ipcr.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
            return Ok(ipcrToReturn);
        }

        [HttpGet("{id}/full", Name = "GetIpcrFull")]
        public async Task<IActionResult> GetIpcrFull(int id)
        {
            var ipcr = await _repo.GetIpcrFullById(id);
             if (!HasValidRole(ipcr.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
            var kpis = await _repo.GetKpisForIpcr(ipcr.Id);
            var kpisToReturn = _mapper.Map<List<KpiDto>>(kpis);
            ipcrToReturn.Kpis = kpisToReturn;

            return Ok(ipcrToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetIpcrsPaged([FromQuery] PmsParams ipcrParams)
        {
             // Check if logged in user is the employee requested
             if (!HasValidRole(ipcrParams.EmployeeId))
                {
                    return Unauthorized("You do not have clearance to update what is not yours!");
                }

            var ipcrs = await _repo.GetIpcrsPaged(ipcrParams);
            var ipcrsToReturn = _mapper.Map<IEnumerable<IpcrDto>>(ipcrs);
            Response.AddPagination(ipcrs.CurrentPage, 
                                    ipcrs.TotalCount, 
                                    ipcrs.PageSize, 
                                    ipcrs.TotalPages);
            return Ok(ipcrsToReturn);
        }

        [HttpGet("ofemployee/{empId}")]
        public async Task<IActionResult> GetIpcrsOfEmployeePaged([FromQuery] PmsParams ipcrParams, string empId)
        {
            // Check if logged in user is the employee requested
            if (!HasValidRole(ipcrParams.EmployeeId))
                {
                return Unauthorized("You do not have clearance to update what is not yours!");
                }

            var ipcrs = await _repo.GetPaged<Ipcr, int>(ipcrParams);
            var ipcrsToReturn = _mapper.Map<IEnumerable<IpcrForListDto>>(ipcrs);
            Response.AddPagination(ipcrs.CurrentPage, 
                                    ipcrs.TotalCount, 
                                    ipcrs.PageSize, 
                                    ipcrs.TotalPages);
            return Ok(ipcrsToReturn);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIpcr(int id, IpcrDto ipcrForUpdate)
        {
            if (!HasValidRole(ipcrForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            var ipcrFromRepo = await _repo.GetById<Ipcr, int>(id);
            _mapper.Map(ipcrForUpdate, ipcrFromRepo);
            ipcrFromRepo.DateLastModified = DateTime.Now;

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Ipcr {ipcrForUpdate.Id} failed on save.");
        }

       

        [HttpPost]
        public async Task<IActionResult> CreateIpcr(IpcrDto newIpcrDto)
        {
             if (!HasValidRole(newIpcrDto.EmployeeId))
                {
                    return Unauthorized("You do not have clearance to update what is not yours!");
                }
                newIpcrDto.DateCreated = DateTime.Now;
                newIpcrDto.DateLastModified = DateTime.Now;
                var ipcrToCreate = _mapper.Map<Ipcr>(newIpcrDto);
                _repo.Add(ipcrToCreate);
                if (await _repo.SaveAllAsync())
                {
                    var ipcrToReturn = _mapper.Map<IpcrDto>(ipcrToCreate);
                    return CreatedAtRoute("GetIpcrTemplateById", new { id = ipcrToCreate.Id }, ipcrToReturn);
                }

                throw new Exception("Creating the ipcr failed on save");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteIpcr(int id)
        {
            var ipcrFromRepo = await _repo.GetById<Ipcr, int>(id);
            if (ipcrFromRepo == null)
            {
                return BadRequest("Ipcr not found");
            }
            var pmsKpiParam = new PmsParams() {
                FilterByKey = "IpcrId",
                KeyInt = ipcrFromRepo.Id
            };
            var kpisInIpcr = await _repo.GetKpisForIpcr(ipcrFromRepo.Id);
            _repo.DeleteRange(kpisInIpcr);
            _repo.Delete(ipcrFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the ipcr");
        }

        // Delete KPI
        [HttpDelete("kpis/{kpiId}")]
        public async Task<IActionResult> deleteKpi(int kpiId)
        {
            var kpiFromRepo = await _repo.GetById<Kpi, int>(kpiId);
            if (kpiFromRepo == null)
            {
                return BadRequest("Item not found");
            }
            if (kpiFromRepo.Kpis.Count > 0)
            {
                return BadRequest("Delete children first!");
            }
            _repo.Delete(kpiFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the ipcr");
        }


    }
}