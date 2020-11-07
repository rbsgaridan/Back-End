using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto.Rsp;
using YamangTao.Api.Helpers;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.RSP.Pds;

namespace YamangTao.Api.Controllers.Rsp.Pds
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonalDataSheetsController : ControllerBase
    {
        private readonly IPdsRepository _repo;
        private readonly IMapper _mapper;
        public PersonalDataSheetsController(IPdsRepository repo,
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

        [HttpGet("{id}", Name = "GetPds")]
        public async Task<IActionResult> GetPdsById(int id)
        {
            //TODO: Implement Realistic Implementation
            var pds = await _repo.GetCompletePdsByID(id);
            if (pds == null)
            {
                return BadRequest("No PDS Found");
            }
            if (!HasValidRole(pds.EmployeeId))
            {
                return Unauthorized("You do not have clearance to see what is not yours!");
            }

            var pdsToReturn = _mapper.Map<PersonalDataSheetDto>(pds);

            return Ok(pdsToReturn);
        }

       
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetPdsByEmployeeId(string employeeId)
        {
            if (!HasValidRole(employeeId))
            {
                return Unauthorized("You do not have clearance to see what is not yours!");
            }

            var pds = await _repo.GetPdsFullByEmployeeID(employeeId);
            if (pds == null)
            {
                return BadRequest("No PDS Found");
            }

            var pdsToReturn = _mapper.Map<PersonalDataSheetDto>(pds);
            return Ok(pdsToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPdsPaged([FromQuery] PdsParams pdsParams)
        {
             if (!HasValidRole(pdsParams.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var pds = await _repo.GetPaged<PersonalDataSheet,int>(pdsParams);
            var pdsToReturn = _mapper.Map<IEnumerable<PersonalDataSheetDto>>(pds);
            Response.AddPagination(pds.CurrentPage, 
                                    pds.TotalCount, 
                                    pds.PageSize, 
                                    pds.TotalPages);
            return Ok(pdsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePds(int id, PersonalDataSheetDto pdsForUpdate)
        {

            if (!HasValidRole(pdsForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var pdsFromRepo = await _repo.GetById<PersonalDataSheet,int>(pdsForUpdate.Id);
            if (pdsFromRepo == null)
            {
                return BadRequest("No PDS Found");
            }
            _mapper.Map(pdsForUpdate, pdsFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating PDS of {pdsForUpdate.EmployeeId} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePds(PersonalDataSheetDto pdsForCreationDto)
        {
            if (!HasValidRole(pdsForCreationDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var pds = _mapper.Map<PersonalDataSheet>(pdsForCreationDto);
            _repo.Add(pds);
            if (await _repo.SaveAllAsync())
            {
                var pdsToReturn = _mapper.Map<PersonalDataSheetDto>(pds);
                return CreatedAtRoute("GetPds", new { id = pds.Id }, pdsToReturn);
            }

            throw new Exception("Creating the campus failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePds(int id)
        {
            var pdsFromRepo = await _repo.GetById<PersonalDataSheet,int>(id);
            if (pdsFromRepo == null)
            {
                return BadRequest("PDS not found!");
            }
            
            if (!HasValidRole(pdsFromRepo.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            // ToDo Delete or children
            _repo.Delete(pdsFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the pds");
        }

        [HttpDelete("{employeeId}/{field}/{id}")]
        public async Task<IActionResult> deletePdsdetail(string employeeId, string field, int id)
        {
            
            if (!HasValidRole(employeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            bool deleted = false;
            switch (field)
            {
              case "address":
                 deleted = await delete<Address,int>(id);
              break;
              
              case "reference":
                 deleted = await delete<CharacterReference,int>(id);
              break;

              case "child":
                 deleted = await delete<Child,int>(id);
              break;

              case "educationalbackground":
                 deleted = await delete<EducationalBackground,int>(id);
              break;

              case "eligibility":
                 deleted = await delete<Eligibility,int>(id);
              break;

              case "idcard":
                 deleted = await delete<Identification,int>(id);
              break;

              case "membership":
                 deleted = await delete<Membership,int>(id);
              break;

              case "recognition":
                 deleted = await delete<Recognition,long>(id);
              break;

              case "skill":
                 deleted = await delete<Skill,long>(id);
              break;

              case "trainingattended":
                 deleted = await delete<TrainingAttended,int>(id);
              break;

              case "voluntarywork":
                 deleted = await delete<VoluntaryWork,int>(id);
              break;

              case "workexperience":
                 deleted = await delete<WorkExperience,int>(id);
              break;

              default:
              break;
            }
           
            // ToDo Delete or children
           
            if (deleted)
            {
                return NoContent();
            }
            throw new Exception("Error deleting!");
        }

        private async Task<bool> delete<T,K>(K id) where T : class
        {
             var fromRepo = await _repo.GetById<T,K>(id);
             if (fromRepo != null)
             {
                  _repo.Delete(fromRepo);
                  
             }
            return await _repo.SaveAllAsync();
        }



    }
}