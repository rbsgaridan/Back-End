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
            var pdsToReturn = _mapper.Map<PersonalDataSheetDto>(pds);
            return Ok(pdsToReturn);
        }

        [HttpGet("complete/{id}", Name = "GetPdsComplete")]
        public async Task<IActionResult> GetPdsCompleteById(int id)
        {
            //TODO: Implement Realistic Implementation
            var pds = await _repo.GetCompletePdsByID(id);
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

            var pds = await _repo.GetPdsPaged(pdsParams);
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

        [HttpPost("{pdsId}/address")]
        public async Task<IActionResult> CreateNewAddress(AddressDto addressDto)
        {
            if (!HasValidRole(addressDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var addressForCreate = _mapper.Map<Address>(addressDto);
            _repo.Add(addressForCreate);
            if (await _repo.SaveAllAsync())
            {
                var addressToReturn = _mapper.Map<AddressDto>(addressForCreate);
                return CreatedAtRoute("GetAddress", new { id = addressForCreate.Id }, addressToReturn);
            }

            throw new Exception("Adding the address failed on save");

        }

        [HttpGet("address/{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            //TODO: Implement Realistic Implementation
            var address = await _repo.GetById<PersonalDataSheet,int>(id);
            var addressToReturn = _mapper.Map<AddressDto>(address);
            return Ok(addressToReturn);
        }

        [HttpPost("{pdsId}/idcards")]
        public async Task<IActionResult> CreateNewIdCard(IdentificationDto idCardDto)
        {
            if (!HasValidRole(idCardDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var idCardForCreate = _mapper.Map<Identification>(idCardDto);
            _repo.Add(idCardForCreate);
            if (await _repo.SaveAllAsync())
            {
                var idCardToReturn = _mapper.Map<IdentificationDto>(idCardForCreate);
                return CreatedAtRoute("GetIdCard", new { id = idCardForCreate.Id }, idCardToReturn);
            }

            throw new Exception("Adding the ID Card failed on save");

        }

        [HttpGet("idcards/{id}", Name = "GetIdCard")]
        public async Task<IActionResult> GetIdCardById(int id)
        {
            //TODO: Implement Realistic Implementation
            var idCard = await _repo.GetById<Identification,int>(id);
            var idCardToReturn = _mapper.Map<IdentificationDto>(idCard);
            return Ok(idCardToReturn);
        }

        [HttpPost("{pdsId}/eligibilities")]
        public async Task<IActionResult> CreateEligibility(EligibilityDto eligibilityDto)
        {
            if (!HasValidRole(eligibilityDto.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var elegibilityForCreate = _mapper.Map<Eligibility>(eligibilityDto);
            _repo.Add(elegibilityForCreate);
            if (await _repo.SaveAllAsync())
            {
                var eligibilityToReturn = _mapper.Map<EligibilityDto>(elegibilityForCreate);
                return CreatedAtRoute("GetEligibility", new { id = elegibilityForCreate.Id }, eligibilityToReturn);
            }

            throw new Exception("Adding the eligibility failed on save");

        }

        [HttpGet("eligibilities/{id}", Name = "GetEligibility")]
        public async Task<IActionResult> GetEligibilityById(int id)
        {
            //TODO: Implement Realistic Implementation
            var eligibility = await _repo.GetById<Eligibility,int>(id);
            var eligibilityToReturn = _mapper.Map<EligibilityDto>(eligibility);
            return Ok(eligibilityToReturn);
        }
    }
}