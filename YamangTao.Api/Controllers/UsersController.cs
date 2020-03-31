using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Api.Dtos;
using Microsoft.AspNetCore.Identity;
using YamangTao.Model.Auth;
using YamangTao.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YamangTao.Api.Controllers
{   
    // [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UsersController(UserManager<User> userManager, IMapper mapper, DataContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        // {
        //     var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //     var userFromRepo = await _repo.GetUser(currentUserId, false);

        //     userParams.UserId = currentUserId;

        //     var users = await _repo.GetUsers(userParams);
        //     var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
        //     Response.AddPagination(users.CurrentPage, users.TotalCount, users.PageSize,  users.TotalPages);
        //     return Ok(usersToReturn);
        // }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var isCurrentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value == id;
            // var user = await _repo.GetUser(id, isCurrentUser);
            var user = await _userManager.FindByIdAsync(id);

            var userToReturn = _mapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }

        [HttpGet("withroles", Name = "GetUserRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users orderby user.UserName
                                    select new 
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        KnownAs = user.KnownAs,
                                        Roles = (from userRole in user.UserRoles
                                                 join role in _context.Roles
                                                 on userRole.RoleId
                                                 equals role.Id
                                                 select role.Name).ToList()
                                    }).ToListAsync();
                                    
            return Ok(userList);
        }
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateUser(string id, UserForUpdateDto userForUpdateDto)
        // {
        //     //only the current logged in user should be updated
        //     if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
        //         return Unauthorized();
            
        //     var userFromRepo = await _repo.GetUser(id, true);

        //     _mapper.Map(userForUpdateDto, userFromRepo);

        //     if (await _repo.SaveAll())
        //         return NoContent();

        //     //if something went whrong then throw an exception.
        //     throw new Exception($"Updating user {id} failed on save");
        // }

        // [HttpPost("{id}/like/{recipientId}")]
        // public async Task<IActionResult> LikeUser(string id, string recipientId)
        // {
        //     if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
        //         return Unauthorized();

        //     var like = await _repo.GetLike(id, recipientId);

        //     if (like != null)
        //         return BadRequest("you already like this user");

        //     if (await _repo.GetUser(recipientId, false) == null)
        //         return NotFound();

        //     like = new Like 
        //     {
        //         LikerId = id,
        //         LikeeId = recipientId
        //     };

        //     _repo.Add<Like>(like);

        //     if (await _repo.SaveAll())
        //         return Ok();
            
        //     return BadRequest("Failed to like user");
        // }
        
        
    }
}
