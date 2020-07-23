using System.Linq;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Buffers;
using System.Security.Claims;
using System;
using System.Collections.Generic;
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
    [Route("api/v1/[controller]")]
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

        private bool HasValidRole(string employeeId)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value == employeeId;
            var roles = User.FindAll(ClaimTypes.Role);
            
            if (!isCurrentUser)
            {
                if ((User.IsInRole("Employee") && (roles.Count() == 1) ))
                    {
                        return false;
                    }
            }
            return true;
        }

        [HttpGet("{id}", Name = "GetIpcr")]
        public async Task<IActionResult> GetIpcr(int id)
        {
            
            var ipcr = await _repo.GetById<Ipcr, int>(id);
            if (!HasValidRole(ipcr.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
            return Ok(ipcrToReturn);
        }

        [HttpGet("{id}/withchildren", Name = "GetIpcrWithChildren")]
        public async Task<IActionResult> GetIpcrWithChildren(int id)
        {
            var ipcr = await _repo.GetIpcrWithCompleteKpisById(id);
             if (!HasValidRole(ipcr.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
            return Ok(ipcrToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetIpcrsPaged([FromQuery] PmsParams ipcrParams)
        {
             // Check if logged in user is the employee requested
             if (!HasValidRole(ipcrParams.EmployeeId))
                {
                return Unauthorized("You do not have clearance to update what is not yours!");
                }

            var ipcrs = await _repo.GetPaged<Ipcr,int>(ipcrParams);
            var ipcrsToReturn = _mapper.Map<IEnumerable<IpcrForListDto>>(ipcrs);
            Response.AddPagination(ipcrs.CurrentPage, 
                                    ipcrs.TotalCount, 
                                    ipcrs.PageSize, 
                                    ipcrs.TotalPages);
            return Ok(ipcrsToReturn);
        }

        [HttpGet("ofemployee/{empId}")]
        public async Task<IActionResult> GetIpcrsOfEmployeePaged([FromQuery] PmsParams ipcrParams, string empId)
        {
            // Check if logged in user is the employee requested
            if (!HasValidRole(ipcrParams.EmployeeId))
                {
                return Unauthorized("You do not have clearance to update what is not yours!");
                }

            var ipcrs = await _repo.GetPaged<Ipcr, int>(ipcrParams);
            var ipcrsToReturn = _mapper.Map<IEnumerable<IpcrForListDto>>(ipcrs);
            Response.AddPagination(ipcrs.CurrentPage, 
                                    ipcrs.TotalCount, 
                                    ipcrs.PageSize, 
                                    ipcrs.TotalPages);
            return Ok(ipcrsToReturn);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIpcr(int id, IpcrDto ipcrForUpdate)
        {
            if (!HasValidRole(ipcrForUpdate.EmployeeId))
            {
                return Unauthorized("You do not have clearance to update what is not yours!");
            }
            var ipcrFromRepo = await _repo.GetById<Ipcr, int>(id);
            _mapper.Map(ipcrForUpdate, ipcrFromRepo);

            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }

            throw new Exception($"Updating Ipcr {ipcrForUpdate.Id} failed on save.");
        }

        [HttpPost("anotherway")]
        public async Task<IActionResult> AnotherWayToAdd(IpcrForCreateDto newIpcrDto)
        {
            //TODO: Implement Realistic Implementation
            var ipcr = _mapper.Map<Ipcr>(newIpcrDto);
            _repo.Add(ipcr);
            if (await _repo.SaveAllAsync())
            {
                var ipcrToReturn = _mapper.Map<IpcrDto>(ipcr);
                return CreatedAtRoute("GetIpcr", new { id = ipcr.Id }, ipcrToReturn);
            }

            throw new Exception("Creating the ipcr failed on save");
        }

        [HttpPost]
        public async Task<IActionResult> CreateIpcr(IpcrForCreateDto newIpcrDto)
        {
             if (!HasValidRole(newIpcrDto.EmployeeId))
                {
                    return Unauthorized("You do not have clearance to update what is not yours!");
                }
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
            var ipcrFromRepo = _repo.GetById<Ipcr, int>(id);
             if (ipcrFromRepo == null)
            {
                return BadRequest("Ipcr not found");
            }
            _repo.Delete(ipcrFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the ipcr");
        }

        // Delete KPI
        [HttpDelete("kpis/{kpiId}")]
        public async Task<IActionResult> deleteKpi(int kpiId)
        {
            var kpiFromRepo = await _repo.GetById<Kpi, int>(kpiId);
            if (kpiFromRepo == null)
            {
                return BadRequest("Item not found");
            }
            if (kpiFromRepo.Kpis.Count > 0)
            {
                return BadRequest("Delete children first!");
            }
            _repo.Delete(kpiFromRepo);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the ipcr");
        }


        // Delete Rating Matrix
        [HttpDelete("matrix/{rmId}")]
        public async Task<IActionResult> deleteRatingMatrix(int rmId)
        {
            var ratingMatrix = await _repo.GetById<RatingMatrix, int>(rmId);
            if (ratingMatrix == null)
            {
                return BadRequest("Can't find item to delete");
            }
            if (ratingMatrix.Ratings.Count > 0)
            {
                _repo.DeleteRange(ratingMatrix.Ratings);
            }
            _repo.Delete(ratingMatrix);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the rating");
        }
        
        // Delete Ratings
        [HttpDelete("matrix/{rmId}/ratings/{rate}")]
        public async Task<IActionResult> deleteRating(int rmId, sbyte rate)
        {
            var rating = await _repo.GetRating(rmId, rate);
            if (rating == null)
            {
                return BadRequest("Can't find item to delete");
            }
            _repo.Delete(rating);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the rating");
        }



    }
}