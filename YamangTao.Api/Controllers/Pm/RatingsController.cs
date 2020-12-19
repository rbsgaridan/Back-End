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
    public class RatingsController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public RatingsController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRating(RatingDto rmDto)
        {
            var ratingForCreate = _mapper.Map<Rating>(rmDto);
            _repo.Add(ratingForCreate);
            if (await _repo.SaveAllAsync())
            {
                var ratingToReturn = _mapper.Map<RatingDto>(ratingForCreate);
                return CreatedAtRoute("GetRatingById", new { id = ratingForCreate.Id }, ratingToReturn);
            }

            throw new Exception("Adding the rating failed on save");

        }

        [HttpGet("{id}", Name = "GetRatingById")]
        public async Task<IActionResult> GetRatingById(int id)
        {
            //TODO: Implement Realistic Implementation
            var rating = await _repo.GetById<Rating, long>(id);
            var ratingToReturn = _mapper.Map<RatingDto>(rating);
            return Ok(ratingToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetRatingPaged([FromQuery] PmsParams ratingParams)
        {
            
            var rating = await _repo.GetPaged<Rating, long>(ratingParams);
            var ratingToReturn = _mapper.Map<IEnumerable<RatingDto>>(rating);
            Response.AddPagination(rating.CurrentPage, 
                                    rating.TotalCount, 
                                    rating.PageSize, 
                                    rating.TotalPages);
            return Ok(ratingToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, RatingDto ratingForUpdate)
        {

         
            var ratingFromRepo = await _repo.GetById<Rating, long>(ratingForUpdate.Id);
            _mapper.Map(ratingForUpdate, ratingFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating rating failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRating(int id)
        {
            var ratingFromRepo = await _repo.GetById<Rating, long>(id);
            
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