// using System;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using YamangTao.Api.Dtos.Pms;
// using YamangTao.Core.HttpParams;
// using YamangTao.Data.Core;
// using YamangTao.Model.PM;

// namespace YamangTao.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [Authorize]
//     public class KpisController : ControllerBase
//     {
//         private readonly IPmsRepository _repo;
//         private readonly IMapper _mapper;
//         public KpisController(IPmsRepository repo, IMapper mapper)
//         {
//             _mapper = mapper;
//             _repo = repo;

//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateNewKpi(KpiDto kpiDto)
//         {
//             var kpiForCreate = _mapper.Map<Kpi>(kpiDto);
//             _repo.Add(kpiForCreate);
//             if (await _repo.SaveAllAsync())
//             {
//                 var kpiToReturn = _mapper.Map<KpiDto>(kpiForCreate);
//                 return CreatedAtRoute("GetKpiById", new { id = kpiForCreate.Id }, kpiToReturn);
//             }

//             throw new Exception("Adding the kpi failed on save");

//         }

//         [HttpGet("{id}", Name = "GetKpiById")]
//         public async Task<IActionResult> GetKpiById(int id)
//         {
//             //TODO: Implement Realistic Implementation
//             var kpi = await _repo.GetById<Kpi, int>(id);
//             var kpiToReturn = _mapper.Map<KpiDto>(kpi);
//             return Ok(kpiToReturn);
//         }

//         [HttpGet("paged")]
//         public async Task<IActionResult> GetKpiPaged([FromQuery] PmsParams kpiParams)
//         {
//              if (!HasValidRole(kpiParams.EmployeeId))
//             {
//                 return Unauthorized("You do not have clearance to update what is not yours!");
//             }

//             var kpi = await _repo.GetKpies(kpiParams);
//             var kpiToReturn = _mapper.Map<IEnumerable<KpiDto>>(kpi);
//             Response.AddPagination(kpi.CurrentPage, 
//                                     kpi.TotalCount, 
//                                     kpi.PageSize, 
//                                     kpi.TotalPages);
//             return Ok(kpiToReturn);
//         }

//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateKpi(int id, KpiDto kpiForUpdate)
//         {

//             if (!HasValidRole(kpiForUpdate.EmployeeId))
//             {
//                 return Unauthorized("You do not have clearance to update what is not yours!");
//             }

//             var kpiFromRepo = await _repo.GetById<Kpi>(kpiForUpdate.Id);
//             _mapper.Map(kpiForUpdate, kpiFromRepo);

//             if (await _repo.SaveAllAsync())
//             {
//                 return NoContent();
//             }

//             throw new Exception($"Updating Kpi of {kpiForUpdate.EmployeeId} failed on save.");
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> deleteKpi(int id)
//         {
//             var kpiFromRepo = await _repo.GetById<Kpi>(id);
//             if (!HasValidRole(kpiFromRepo.EmployeeId))
//             {
//                 return Unauthorized("You do not have clearance to update what is not yours!");
//             }
//             // ToDo Delete or children
//             _repo.Delete(kpiFromRepo);
//             if (await _repo.SaveAllAsync())
//             {
//                 return NoContent();
//             }
//             throw new Exception("Error deleting the kpi");
//         }

//         [AllowAnonymous]
//         [HttpGet("street")]
//         public async Task<IActionResult> SearchStreet(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var streets = await _repo.SearchDistinctStreet(search);
//             return Ok(streets);
//         }

//         [AllowAnonymous]
//         [HttpGet("block")]
//         public async Task<IActionResult> SearchBlock(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var blocks = await _repo.SearchDistinctBlock(search);
//             return Ok(blocks);
//         }

//         [AllowAnonymous]
//         [HttpGet("purok")]
//         public async Task<IActionResult> SearchPurok(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var puroks = await _repo.SearchDistinctPurok(search);
//             return Ok(puroks);
//         }

//         [AllowAnonymous]
//         [HttpGet("barangay")]
//         public async Task<IActionResult> SearchBarangay(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var barangays = await _repo.SearchDistinctBarangay(search);
//             return Ok(barangays);
//         }

//         [AllowAnonymous]
//         [HttpGet("municipality")]
//         public async Task<IActionResult> SearchMunicipality(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var munis = await _repo.SearchDistinctMunicipality(search);
//             return Ok(munis);
//         }

//         [AllowAnonymous]
//         [HttpGet("province")]
//         public async Task<IActionResult> SearchProvince(string search)
//         {
//             if (string.IsNullOrWhiteSpace(search))
//             {
//                 return BadRequest("Invalid search parameter!");
//             }
//             //TODO: Implement Realistic Implementation
//             var provinces = await _repo.SearchDistinctProvince(search);
//             return Ok(provinces);
//         }
//     }
// }