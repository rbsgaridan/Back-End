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
    public class IdCardsController : ControllerBase
    {
        private readonly IPdsRepository _repo;
        private readonly IMapper _mapper;
        public IdCardsController(IPdsRepository repo,
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
        public async Task<IActionResult> CreateNewIdentification(IdentificationDto identificationDto)
        {
            if (!HasValidRole(identificationDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var identificationForCreate = _mapper.Map<Identification>(identificationDto);
            _repo.Add(identificationForCreate);
            if (await _repo.SaveAllAsync())
            {
                var identificationToReturn = _mapper.Map<IdentificationDto>(identificationForCreate);
                return CreatedAtRoute("GetIdentificationById", new { id = identificationForCreate.Id }, identificationToReturn);
            }

            throw new Exception("Adding the identification failed on save");

        }

        [HttpGet("{id}", Name = "GetIdentificationById")]
        public async Task<IActionResult> GetIdentificationById(int id)
        {
            //TODO: Implement Realistic Implementation
            var identification = await _repo.GetById<Identification,int>(id);
            var identificationToReturn = _mapper.Map<IdentificationDto>(identification);
            return Ok(identificationToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetIdCardsPaged([FromQuery] PdsParams idcardParams)
        {
             if (!HasValidRole(idcardParams.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var idCard = await _repo.GetIdCards(idcardParams);
            var idCardToReturn = _mapper.Map<IEnumerable<IdentificationDto>>(idCard);
            Response.AddPagination(idCard.CurrentPage, 
                                    idCard.TotalCount, 
                                    idCard.PageSize, 
                                    idCard.TotalPages);
            return Ok(idCardToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdentification(int id, IdentificationDto identificationForUpdate)
        {

            if (!HasValidRole(identificationForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var identificationFromRepo = await _repo.GetById<Identification,int>(identificationForUpdate.Id);
            _mapper.Map(identificationForUpdate, identificationFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Identification of {identificationForUpdate.EmployeeId} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteIdentification(int id)
        {
            var identificationFromRepo = await _repo.GetById<Identification,int>(id);
            if (!HasValidRole(identificationFromRepo.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            // ToDo Delete or children
            _repo.Delete(identificationFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the identification");
        }

        [AllowAnonymous]
        [HttpGet("type")]
        public async Task<IActionResult> SearchIdentificationType(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var streets = await _repo.SearchDistinctIdTypes(search);
            return Ok(streets);
        }

        
        
    }
}