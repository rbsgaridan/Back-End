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
    [Route("api/[controller]")]
    [Authorize(Policy="RequirePMTRole")]
    public class RatingTemplatesController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public RatingTemplatesController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRatingTemplate(RatingTemplateDto rmDto)
        {
            var ratingForCreate = _mapper.Map<RatingTemplate>(rmDto);
            _repo.Add(ratingForCreate);
            if (await _repo.SaveAllAsync())
            {
                var ratingToReturn = _mapper.Map<RatingTemplateDto>(ratingForCreate);
                return CreatedAtRoute("GetRatingTemplateById", new { id = ratingForCreate.Id }, ratingToReturn);
            }

            throw new Exception("Adding the rating failed on save");

        }

        [HttpGet("{id}", Name = "GetRatingTemplateById")]
        public async Task<IActionResult> GetRatingTemplateById(int id)
        {
            //TODO: Implement Realistic Implementation
            var rating = await _repo.GetById<RatingTemplate, long>(id);
            var ratingToReturn = _mapper.Map<RatingTemplateDto>(rating);
            return Ok(ratingToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetRatingTemplatePaged([FromQuery] PmsParams ratingParams)
        {
            
            var rating = await _repo.GetPaged<RatingTemplate, long>(ratingParams);
            var ratingToReturn = _mapper.Map<IEnumerable<RatingTemplateDto>>(rating);
            Response.AddPagination(rating.CurrentPage, 
                                    rating.TotalCount, 
                                    rating.PageSize, 
                                    rating.TotalPages);
            return Ok(ratingToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatingTemplate(int id, RatingTemplateDto ratingForUpdate)
        {

         
            var ratingFromRepo = await _repo.GetById<RatingTemplate, long>(ratingForUpdate.Id);
            _mapper.Map(ratingForUpdate, ratingFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating rating failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRatingTemplate(int id)
        {
            var ratingFromRepo = await _repo.GetById<RatingTemplate, long>(id);
            
            // ToDo Delete or children
            _repo.Delete(ratingFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the rating");
        }

        
    }
}