using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using YamangTao.Api.Dtos.Rsp;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.RSP.Pds;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IPdsRepository _repo;
        private readonly IMapper _mapper;
        public AddressesController(IPdsRepository repo,
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
            var address = await _repo.GetById<Address>(id);
            var addressToReturn = _mapper.Map<AddressDto>(address);
            return Ok(addressToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, AddressDto addressForUpdate)
        {

            if (!HasValidRole(addressForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var addressFromRepo = await _repo.GetById<Address>(addressForUpdate.Id);
            _mapper.Map(addressForUpdate, addressFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Address of {addressForUpdate.EmployeeId} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAddress(int id)
        {
            var addressFromRepo = await _repo.GetById<Address>(id);
            if (!HasValidRole(addressFromRepo.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            // ToDo Delete or children
            _repo.Delete(addressFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the pds");
        }

        [AllowAnonymous]
        [HttpGet("search/street")]
        public async Task<IActionResult> SearchStreet(string search)
        {
            //TODO: Implement Realistic Implementation
            var streets = await _repo.SearchDistinctStreet(search);
            return Ok(streets);
        }

        [AllowAnonymous]
        [HttpGet("search/block")]
        public async Task<IActionResult> SearchBlock(string search)
        {
            //TODO: Implement Realistic Implementation
            var blocks = await _repo.SearchDistinctBlock(search);
            return Ok(blocks);
        }

        [AllowAnonymous]
        [HttpGet("search/purok")]
        public async Task<IActionResult> SearchPurok(string search)
        {
            //TODO: Implement Realistic Implementation
            var puroks = await _repo.SearchDistinctPurok(search);
            return Ok(puroks);
        }

        [AllowAnonymous]
        [HttpGet("search/barangay")]
        public async Task<IActionResult> SearchBarangay(string search)
        {
            //TODO: Implement Realistic Implementation
            var barangays = await _repo.SearchDistinctBarangay(search);
            return Ok(barangays);
        }

        [AllowAnonymous]
        [HttpGet("search/municipality")]
        public async Task<IActionResult> SearchMunicipality(string search)
        {
            //TODO: Implement Realistic Implementation
            var munis = await _repo.SearchDistinctMunicipality(search);
            return Ok(munis);
        }

        [AllowAnonymous]
        [HttpGet("search/province")]
        public async Task<IActionResult> SearchProvince(string search)
        {
            //TODO: Implement Realistic Implementation
            var provinces = await _repo.SearchDistinctProvince(search);
            return Ok(provinces);
        }



    }
}