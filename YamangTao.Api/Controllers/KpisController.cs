using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos.Pms;
using YamangTao.Data.Core;

namespace YamangTao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KpisController : ControllerBase
    {
        private readonly IPmsRepository _repo;
        private readonly IMapper _mapper;
        public KpisController(IPmsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        // [HttpGet("{id}", Name = "GetKpi")]
        // public async Task<IActionResult> GetKpiById(int id)
        // {
        //     //TODO: Implement Realistic Implementation
        //     // var kpi = await _repo.(id);
        //     // var kpiToReturn = _mapper.Map<KpiDto>(kpi);
        //     return Ok();
        // }
    }
}