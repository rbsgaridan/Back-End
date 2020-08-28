using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Data.Core;
using YamangTao.Api.Dtos.LND;
using YamangTao.Model.LND;

namespace YamangTao.Api.Controllers.Lnd
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireHRrole")]
    public class CertificateTypesController : ControllerBase
    {
        private readonly ILndRepository _repo;
        private readonly IMapper _mapper;
        public CertificateTypesController(ILndRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{id}", Name = "GetCertificateType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCertificateType(string id)
        {
            var certificateType = await _repo.GetById<CertificateType, string>(id);
            var certificateTypeToReturn = _mapper.Map<CertificateTypeDto>(certificateType);
            return Ok(certificateTypeToReturn);
        }

        


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var certificateTypes = await _repo.GetAll<CertificateType>();
            var certificateTypesToReturn = _mapper.Map<IEnumerable<CertificateTypeDto>>(certificateTypes);
            return Ok(certificateTypesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificateType(string id, CertificateTypeDto certificateTypeForUpdate)
        {
            var certificateTypeFromRepo = await _repo.GetById<CertificateType, string>(certificateTypeForUpdate.Id);
           
            _mapper.Map(certificateTypeForUpdate, certificateTypeFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating  {certificateTypeForUpdate.Name} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreatecertificateType(CertificateTypeDto certificateTypeForCreationDto)
        {
            var certificateType = _mapper.Map<CertificateType>(certificateTypeForCreationDto);
           
            _repo.Add(certificateType);
            if (await _repo.SaveAllAsync())
            {
                var certificateTypeToReturn = _mapper.Map<CertificateTypeDto>(certificateType);
                return CreatedAtRoute("GetCertificateType", new { id = certificateType.Id }, certificateTypeToReturn);
            }

            throw new Exception("Creating the Activity failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificateType(int id)
        {
            var certificateTypeFromRepo = await _repo.GetById<CertificateType, int>(id);
            _repo.Delete(certificateTypeFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the certificateType");
        }

        [HttpGet("list/all")]
        public async Task<IActionResult> ListAllcertificateTypes()
        {
            //TODO: Implement Realistic Implementation
          return Ok(await _repo.GetAll<CertificateType>());
        }

    }
}