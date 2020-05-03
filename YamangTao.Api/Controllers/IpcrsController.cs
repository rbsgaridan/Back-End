using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos.Pms;
using YamangTao.Api.Helpers;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Core;
using YamangTao.Model.PM;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IpcrsController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public IpcrsController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{id}", Name = "GetIpcr")]
        public async Task<IActionResult> GetIpcr(int id)
        {
            //TODO: Implement Realistic Implementation
            var ipcr = await _repo.GetIpcrByID(id);
            var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
            return Ok(ipcrToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetIpcrsPaged([FromQuery] IpcrParams ipcrParams)
        {
            var ipcrs = await _repo.GetIpcrs(ipcrParams);
            var ipcrsToReturn = _mapper.Map<IEnumerable<IpcrForListDto>>(ipcrs);
            Response.AddPagination(ipcrs.CurrentPage, 
                                    ipcrs.TotalCount, 
                                    ipcrs.PageSize, 
                                    ipcrs.TotalPages);
            return Ok(ipcrsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIpcr(string id, IpcrDto ipcrForUpdate)
        {
            var ipcrFromRepo = await _repo.GetIpcrByID(ipcrForUpdate.Id);
            _mapper.Map(ipcrForUpdate, ipcrFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Ipcr {ipcrForUpdate.Id} failed on save.");
        }

        [HttpPost("sampleadd")]
        public async Task<IActionResult> TestAdd() {
            var ipcr = new Ipcr() {
                IsTemplate = false,
                EmployeeId = "08-01845",
                JobPositionId = 1,
                OrgUnitId = 1,
                PeriodFrom = DateTime.Now,
                PeriodTo = DateTime.Now,
                FinalQrating = 0,
                FinalTrating = 0,
                FinalErating = 0,
                FinalAverageRating  = 0,
                DateCreated = DateTime.Now,
                Reviewed = false,
                Compiled = false,
                Approved = false,
                isLocked = false
            };
            var kpi1 = new Kpi() {
                Code = "Kpi 1",
                OrderNumber = "1",
                KpiTypeId = 6,
                Weight = 80,
                SuccessIndicator = "Core Functions",
                HasQuality = false,
                HasEfficiency = false,
                HasTimeliness = false,
                QualityRating = 0,
                EfficiencyRating = 0,
                TimelinessRating = 0,
                AverageRating = 0,
                TaskId = "Tnon-task"
            };
            
            var kpi2 = new Kpi() {
                Code = "Kpi 1",
                OrderNumber = "1",
                KpiTypeId = 6,
                Weight = 80,
                SuccessIndicator = "Instruction",
                HasQuality = false,
                HasEfficiency = false,
                HasTimeliness = false,
                QualityRating = 0,
                EfficiencyRating = 0,
                TimelinessRating = 0,
                AverageRating = 0,
                TaskId = "Tnon-task"
            };
            
            var kpi3 = new Kpi() {
                Code = "Kpi 1",
                OrderNumber = "1",
                KpiTypeId = 6,
                Weight = 80,
                SuccessIndicator = "Instructional Materials",
                HasQuality = false,
                HasEfficiency = false,
                HasTimeliness = false,
                QualityRating = 0,
                EfficiencyRating = 0,
                TimelinessRating = 0,
                AverageRating = 0,
                TaskId = "Tnon-task"
            };
            var kpi4 = new Kpi() {
                Code = "Kpi 1",
                OrderNumber = "1",
                KpiTypeId = 6,
                Weight = 80,
                SuccessIndicator = "Develops Instructional Materials",
                HasQuality = true,
                HasEfficiency = false,
                HasTimeliness = false,
                QualityRating = 0,
                EfficiencyRating = 0,
                TimelinessRating = 0,
                AverageRating = 0,
                TaskId = "Tnon-task"
            };

            var rm1 = new RatingMatrix() {
                Dimension = "Quality",
                MeansOfVerification = "Means 1",
            };
            var rr = new Rating() {
                Rate = 1,
                Description = "Rating 1"
            };
            rm1.Ratings = new List<Rating>();
            rm1.Ratings.Add(rr);
            rm1.Ratings.Add(rr);
            rm1.Ratings.Add(rr);
            rm1.Ratings.Add(rr);
            rm1.Ratings.Add(rr);
            kpi4.RatingMatrices = new List<RatingMatrix>();
            kpi4.RatingMatrices.Add(rm1);
            kpi3.Kpis = new List<Kpi>();
            kpi3.Kpis.Add(kpi4);
            kpi2.Kpis = new List<Kpi>();
            kpi2.Kpis.Add(kpi3);
            kpi1.Kpis = new List<Kpi>();
            kpi1.Kpis.Add(kpi2);
            ipcr.KPIs = new List<Kpi>();
            ipcr.KPIs.Add(kpi1);
            _repo.Add(ipcr);
            await _repo.SaveAllAsync();
            return Ok(ipcr);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIpcr(IpcrForCreateDto newIpcrDto)
        {
            var ipcr = new Ipcr {
                IsTemplate = newIpcrDto.IsTemplate,
                EmployeeId = newIpcrDto.EmployeeId,
                JobPositionId = newIpcrDto.JobPositionId,
                OrgUnitId = newIpcrDto.OrgUnitId,
                PeriodFrom = newIpcrDto.PeriodFrom,
                PeriodTo = newIpcrDto.PeriodTo,
                FinalQrating = newIpcrDto.FinalQrating,
                FinalTrating = newIpcrDto.FinalTrating,
                FinalErating = newIpcrDto.FinalErating,
                FinalAverageRating  = newIpcrDto.FinalAverageRating,
                DateCreated = DateTime.Now,
                Reviewed = false,
                Compiled = false,
                Approved = false,
                isLocked = false
            };
            
            if (newIpcrDto.KPIs.Count > 0)
            {
                ipcr.KPIs = new List<Kpi>();
                ipcr.KPIs.AddRange(MapKpis(newIpcrDto.KPIs));
            }
            _repo.Add(ipcr);
            bool result = await _repo.SaveAllAsync(); 
            if (result)
            {
                var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
                return CreatedAtRoute("GetIpcr", new { id = ipcr.Id }, ipcrToReturn);
            }

            throw new Exception("Creating the ipcr failed on save");
        }

        private List<Kpi> MapKpis(List<KpiDto> kpiDtos)
        {
            var kpiList = new List<Kpi>();
            foreach (var kpiFromDto in kpiDtos)
                {
                    // map the a new Kpi for the DTO
                    var kpi = new Kpi() {
                        Code = kpiFromDto.Code,
                        OrderNumber = kpiFromDto.OrderNumber,
                        KpiTypeId = kpiFromDto.KpiTypeId,
                        Weight = kpiFromDto.Weight,
                        SuccessIndicator = kpiFromDto.SuccessIndicator,
                        HasQuality = kpiFromDto.HasQuality,
                        HasEfficiency = kpiFromDto.HasEfficiency,
                        HasTimeliness = kpiFromDto.HasTimeliness,
                        QualityRating = 0,
                        EfficiencyRating = 0,
                        TimelinessRating = 0,
                        AverageRating = 0,
                        TaskId = kpiFromDto.TaskId,
                        
                    };
                    
                    // If more kpis then call recursive
                    if (kpiFromDto.Kpis.Count > 0)
                    {
                        kpi.Kpis = new List<Kpi>();
                        kpi.Kpis.AddRange(MapKpis(kpiFromDto.Kpis));
                    }
                    // If may rating Matrix then call rating matrix
                    if (kpiFromDto.RatingMatrices.Count > 0)
                    {
                        kpi.RatingMatrices = new List<RatingMatrix>();
                        kpi.RatingMatrices.AddRange(MapMatrices(kpiFromDto.RatingMatrices));
                    }
                   kpiList.Add(kpi);
                }
                return kpiList;
        }

        private List<RatingMatrix> MapMatrices(List<RatingMatrixDto> rmListDto)
        {
            var rmList = new List<RatingMatrix>();
            foreach (var rmFromDto in rmListDto)
                {
                    // map the a new Kpi for the DTO
                    var rm = new RatingMatrix() {
                        Dimension = rmFromDto.Dimension,
                        MeansOfVerification = rmFromDto.MeansOfVerification,
                    };

                    if (rmFromDto.Ratings.Count > 0)
                    {
                        rm.Ratings = new List<Rating>();
                        rm.Ratings.AddRange(MapRatings(rmFromDto.Ratings));
                    }
                    rmList.Add(rm);
                }
            return rmList;
        }

        private List<Rating> MapRatings(List<RatingDto> ratingsDto)
        {
            var ratings = new List<Rating>();
            foreach (var ratingFromDto in ratingsDto)
                {
                    // map the a new Rating for the DTO
                    var rating = new Rating() {
                        Rate = ratingFromDto.Rate,
                        Description = ratingFromDto.Description,
                    };
                    ratings.Add(rating);                   
                }
            return ratings;
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteIpcr(int id)
        {
            var ipcrFromRepo = await _repo.GetIpcrByID(id);
            _repo.Delete(ipcrFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the ipcr");
        }
    }
}