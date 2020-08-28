using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Data.Core;
using YamangTao.Api.Dtos.LND;
using YamangTao.Model.LND;
using YamangTao.Core.HttpParams;
using YamangTao.Api.Helpers;

namespace YamangTao.Api.Controllers.Lnd
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequireHRrole")]
    public class CertificatesController : ControllerBase
    {
        private readonly ILndRepository _repo;
        private readonly IMapper _mapper;
        public CertificatesController(ILndRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetCertificate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCertificate(string id)
        {
            var certificate = await _repo.GetById<Certificate, string>(id);
            var certificateToReturn = _mapper.Map<CertificateDto>(certificate);
            return Ok(certificateToReturn);
        }

        


        [HttpGet("all")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var certificates = await _repo.GetAll<Certificate>();
            var certificatesToReturn = _mapper.Map<IEnumerable<CertificateDto>>(certificates);
            return Ok(certificatesToReturn);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCertificatesPaged([FromQuery] LndParams lndParams)
        {
            var certificates = await _repo.GetPaged<Certificate,string>(lndParams);
            var certificatesToReturn = _mapper.Map<IEnumerable<CertificateDto>>(certificates);
            Response.AddPagination(certificates.CurrentPage, 
                                    certificates.TotalCount, 
                                    certificates.PageSize, 
                                    certificates.TotalPages);
            return Ok(certificatesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificate(string id, CertificateDto certificateForUpdate)
        {
            var certificateFromRepo = await _repo.GetById<Certificate, string>(certificateForUpdate.Id);
           
            _mapper.Map(certificateForUpdate, certificateFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Certificate of {certificateForUpdate.CertificateTypeId} failed on save.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificate(CertificateDto certificateForCreationDto)
        {
            var certificate = _mapper.Map<Certificate>(certificateForCreationDto);
           
            _repo.Add(certificate);
            if (await _repo.SaveAllAsync())
            {
                var certificateToReturn = _mapper.Map<CertificateDto>(certificate);
                return CreatedAtRoute("GetCertificate", new { id = certificate.Id }, certificateToReturn);
            }

            throw new Exception("Creating the Certificate failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(string id)
        {
            var certificateFromRepo = await _repo.GetById<Certificate, string>(id);
            _repo.Delete(certificateFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the certificate");
        }

        [HttpGet("byactivity/{activityId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList(string activityId)
        {
            var listParam = new LndParams {
                FilterByKey = "ActivityId",
                Keyword = activityId
            };
           
            var certificates = await _repo.GetList<Certificate, string>(listParam);
            var certificatesToReturn = _mapper.Map<IEnumerable<CertificateDto>>(certificates);
            return Ok(certificatesToReturn);
        }


        [HttpGet("rolelist")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleList()
        {
            return Ok(await _repo.GetDistinctField<Certificate>("Role"));
        }

        

    }
}