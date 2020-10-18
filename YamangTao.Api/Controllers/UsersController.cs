using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YamangTao.Dto;
using Microsoft.AspNetCore.Identity;
using YamangTao.Model.Auth;
using YamangTao.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Helpers;
using YamangTao.Api.Helpers;
using System.Collections.Generic;
using YamangTao.Data.Core;

namespace YamangTao.Api.Controllers
{
    // [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/users")]
    [ApiController]
    [Authorize(Policy = "RequireHRrole")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserManagementRepository _repo;

        public UsersController(UserManager<User> userManager, IMapper mapper, IUserManagementRepository repo)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            
        }



        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userToReturn = _mapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }

        [HttpGet("withroles", Name = "GetUserRoles")]
        public async Task<IActionResult> GetUsersWithRoles([FromQuery] UserParams userParams)
        {
            var users = await _repo.GetUsersWithRolesPaged(userParams);
            Response.AddPagination(users.CurrentPage,
                                    users.TotalCount,
                                    users.PageSize,
                                    users.TotalPages);
            return Ok(users);
            
        }

        [Authorize(Policy = "RequireHRrole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = roleEditDto.RoleNames;
            //if null the assign array of string
            selectedRoles = selectedRoles ?? new string[] { };
            
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
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
