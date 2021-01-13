using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto.Pms;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.PM;
using System.Collections.Generic;
using YamangTao.Api.Helpers;
using System.Security.Claims;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="RequirePMTRole")]
    public class RatingPeriodsController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public RatingPeriodsController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRatingPeriod(RatingPeriodDto rpDto)
        {
            rpDto.DateCreated = DateTime.Now;
            rpDto.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ratingPeriodForCreate = _mapper.Map<RatingPeriod>(rpDto);
            _repo.Add(ratingPeriodForCreate);
            if (await _repo.SaveAllAsync())
            {
                var ratingToReturn = _mapper.Map<RatingPeriodDto>(ratingPeriodForCreate);
                return CreatedAtRoute("GetRatingPeriodById", new { id = ratingPeriodForCreate.Id }, ratingToReturn);
            }

            throw new Exception("Adding the rating period failed on save");

        }

        [HttpGet("{id}", Name = "GetRatingPeriodById")]
        public async Task<IActionResult> GetRatingPeriodById(int id)
        {
            //TODO: Implement Realistic Implementation
            var rating = await _repo.GetById<RatingPeriod, int>(id);
            var ratingToReturn = _mapper.Map<RatingPeriodDto>(rating);
            return Ok(ratingToReturn);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetRatingPeriodPaged([FromQuery] PmsParams ratingParams)
        {
            
            var rating = await _repo.GetPaged<RatingPeriod, int>(ratingParams);
            var ratingToReturn = _mapper.Map<IEnumerable<RatingPeriodDto>>(rating);
            Response.AddPagination(rating.CurrentPage, 
                                    rating.TotalCount, 
                                    rating.PageSize, 
                                    rating.TotalPages);
            return Ok(ratingToReturn);
        }

        
        [HttpGet("list")]
        public async Task<IActionResult> GetRatingPeriodList([FromQuery] PmsParams ratingParams)
        {
            
            var rating = await _repo.GetList<RatingPeriod, int>(ratingParams);
            var ratingToReturn = _mapper.Map<IEnumerable<RatingPeriodDto>>(rating);
            return Ok(ratingToReturn);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatingPeriod(int id, RatingPeriodDto ratingForUpdate)
        {

         
            var ratingFromRepo = await _repo.GetById<RatingPeriod, int>(ratingForUpdate.Id);
            _mapper.Map(ratingForUpdate, ratingFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating rating period failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRatingPeriod(int id)
        {
            var ratingFromRepo = await _repo.GetById<RatingPeriod, int>(id);
            
            // ToDo Delete or children
            _repo.Delete(ratingFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the rating period");
        }

        
    }
}