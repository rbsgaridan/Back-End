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
    public class RatingMatrixTemplatesController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public RatingMatrixTemplatesController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRatingMatrixTemplate(RatingMatrixTemplateDto rmDto)
        {
            var ratingMatrixForCreate = _mapper.Map<RatingMatrixTemplate>(rmDto);
            _repo.Add(ratingMatrixForCreate);
            if (await _repo.SaveAllAsync())
            {
                var ratingMatrixToReturn = _mapper.Map<RatingMatrixTemplateDto>(ratingMatrixForCreate);
                return CreatedAtRoute("GetRatingMatrixTemplateById", new { id = ratingMatrixForCreate.Id }, ratingMatrixToReturn);
            }

            throw new Exception("Adding the ratingMatrix failed on save");

        }

        [HttpGet("{id}", Name = "GetRatingMatrixTemplateById")]
        public async Task<IActionResult> GetRatingMatrixTemplateById(int id)
        {
            //TODO: Implement Realistic Implementation
            var ratingMatrix = await _repo.GetById<RatingMatrixTemplate, int>(id);
            var ratingMatrixToReturn = _mapper.Map<RatingMatrixTemplateDto>(ratingMatrix);
            return Ok(ratingMatrixToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetRatingMatrixTemplatePaged([FromQuery] PmsParams ratingMatrixParams)
        {
            
            var ratingMatrix = await _repo.GetPaged<RatingMatrixTemplate, int>(ratingMatrixParams);
            var ratingMatrixToReturn = _mapper.Map<IEnumerable<RatingMatrixTemplateDto>>(ratingMatrix);
            Response.AddPagination(ratingMatrix.CurrentPage, 
                                    ratingMatrix.TotalCount, 
                                    ratingMatrix.PageSize, 
                                    ratingMatrix.TotalPages);
            return Ok(ratingMatrixToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatingMatrixTemplate(int id, RatingMatrixTemplateDto ratingMatrixForUpdate)
        {

         
            var ratingMatrixFromRepo = await _repo.GetById<RatingMatrixTemplate, int>(ratingMatrixForUpdate.Id);
            _mapper.Map(ratingMatrixForUpdate, ratingMatrixFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating ratingMatrix failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRatingMatrixTemplate(int id)
        {
            var ratingMatrixFromRepo = await _repo.GetById<RatingMatrixTemplate, int>(id);
            
            // ToDo Delete or children
            _repo.Delete(ratingMatrixFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the Rating Matrix");
        }

        
    }
}