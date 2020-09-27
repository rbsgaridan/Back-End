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

        [HttpPost]
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
                return CreatedAtRoute("GetAddressById", new { id = addressForCreate.Id }, addressToReturn);
            }

            throw new Exception("Adding the address failed on save");

        }

        [HttpGet("{id}", Name = "GetAddressById")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            //TODO: Implement Realistic Implementation
            var address = await _repo.GetById<Address>(id);
            var addressToReturn = _mapper.Map<AddressDto>(address);
            return Ok(addressToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetAddressPaged([FromQuery] PdsParams addressParams)
        {
             if (!HasValidRole(addressParams.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }

            var address = await _repo.GetAddresses(addressParams);
            var addressToReturn = _mapper.Map<IEnumerable<AddressDto>>(address);
            Response.AddPagination(address.CurrentPage, 
                                    address.TotalCount, 
                                    address.PageSize, 
                                    address.TotalPages);
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
            throw new Exception("Error deleting the address");
        }

        [AllowAnonymous]
        [HttpGet("street")]
        public async Task<IActionResult> SearchStreet(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var streets = await _repo.SearchDistinctStreet(search);
            return Ok(streets);
        }

        [AllowAnonymous]
        [HttpGet("block")]
        public async Task<IActionResult> SearchBlock(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var blocks = await _repo.SearchDistinctBlock(search);
            return Ok(blocks);
        }

        [AllowAnonymous]
        [HttpGet("purok")]
        public async Task<IActionResult> SearchPurok(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var puroks = await _repo.SearchDistinctPurok(search);
            return Ok(puroks);
        }

        [AllowAnonymous]
        [HttpGet("barangay")]
        public async Task<IActionResult> SearchBarangay(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var barangays = await _repo.SearchDistinctBarangay(search);
            return Ok(barangays);
        }

        [AllowAnonymous]
        [HttpGet("municipality")]
        public async Task<IActionResult> SearchMunicipality(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var munis = await _repo.SearchDistinctMunicipality(search);
            return Ok(munis);
        }

        [AllowAnonymous]
        [HttpGet("province")]
        public async Task<IActionResult> SearchProvince(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search parameter!");
            }
            //TODO: Implement Realistic Implementation
            var provinces = await _repo.SearchDistinctProvince(search);
            return Ok(provinces);
        }



    }
}