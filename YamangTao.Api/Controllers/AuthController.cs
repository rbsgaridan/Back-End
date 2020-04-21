using System.Linq;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YamangTao.Api.Dtos;
using YamangTao.Model.Auth;
using YamangTao.Core.Repository;

namespace YamangTao.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmployeeRepository _repo;

        public AuthController(IConfiguration config, IMapper mapper,
                              UserManager<User> userManager, SignInManager<User> signInManager,
                              RoleManager<Role> roleManager,
                              IEmployeeRepository repo)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _repo = repo;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;


        }

        // [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // 1 Check if the ID and user is already an employee
            if (await _repo.IdExists(userForRegisterDto.Id))
            {
                throw new Exception($"The ID NUMBER: {userForRegisterDto.Id} already exists!");
            }
            
            bool verified = await _repo.VerifyEmployee(userForRegisterDto.Lastname.ToUpper(), 
                                                        userForRegisterDto.Firstname.ToUpper(),
                                                        userForRegisterDto.Middlename.ToUpper());
            if (!verified)
            {
                throw new Exception($"The system DOES NOT RECOGNIZE this user as an employee. Please contact HRMD Office");
            }

            // 2 Check if the user already exists
            var user = await _userManager.FindByIdAsync(userForRegisterDto.Id);
                if (user != null)
                {
                    throw new Exception($"The user id already exists");
                }
            
            
            // 3 Return Created 
            var userToCreate = new User {
                Id = userForRegisterDto.Id.ToUpper(),
                UserName = userForRegisterDto.Id.ToUpper(),
                Email = userForRegisterDto.Email,
                KnownAs = userForRegisterDto.Firstname + " " + userForRegisterDto.Lastname,
                Created = userForRegisterDto.Created,
                LastActive = userForRegisterDto.LastActive,
                EmployeeId = userForRegisterDto.Id
            };
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByIdAsync(userToCreate.UserName);
                _userManager.AddToRoleAsync(createdUser, "Employee").Wait();
                var userToReturn = _mapper.Map<UserForDetailsDto>(createdUser);
                return CreatedAtRoute("GetUser", new { controller = "users", id = userToReturn.Id}, userToReturn);
            }

            return BadRequest(result.Errors);        
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());
                
                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                
                return Ok(new
                {
                    token = GenerateJwtTokenAsync(appUser).Result,
                    user = userToReturn
                });
            }

            //We wanna return as an object to our cllient
           
            return Unauthorized();
        }

        
        private async Task<string> GenerateJwtTokenAsync(User user)
        {
            //Build Token

            //First store the claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)

            };
            
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Add AppSettings to appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Generate signing credentials key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Create security token descriptor which contains claims, expirydate, and credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Create token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //Using the token handler we can create a token and pass in the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole(Role roleToBeAdded)
        {
            //TODO: Implement Realistic Implementation
          
          var result = await _roleManager.CreateAsync(roleToBeAdded);

          if (result.Succeeded)
          {
              return Ok();
          }

          return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("seedroles")]
        public async Task<IActionResult> SeedRoles()
        {
            //TODO: Implement Realistic Implementation
           var roles = new List<Role>
                {
                   new Role{Id = "Employee", Name = "Employee"},
                    new Role{Id = "Department Head",Name = "Department Head"},
                    new Role{Id = "Unit Head",Name = "Unit Head"},
                    new Role{Id = "VP",Name = "VP"},
                    new Role{Id = "President",Name = "President"},
                    new Role{Id = "PMG",Name = "PMG"},
                    new Role{Id = "Planning",Name = "Planning"},
                    new Role{Id = "HR",Name = "HR"},
                    new Role{Id = "Admin",Name = "Admin"}
                
                };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

          return Ok();
        }

        [AllowAnonymous]
        [HttpPost("addrolestoadmin")]
        public async Task<IActionResult> AddRolesToAdmin()
        {
            //TODO: Implement Realistic Implementation
          
            var admin = _userManager.FindByNameAsync("admin@root").Result;
            var result = await _userManager.AddToRolesAsync(admin, new [] {"Admin", "Unit Head"});
            if (result.Succeeded)
            {
                return Ok();
            }
            
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("seedadmin")]
        public async void SeedAdmin()
        {
            
           var superUser = new User
                {
                    UserName = "admin@root",
                    Id = "admin@root"
                };
                
                IdentityResult result = _userManager.CreateAsync(superUser,"L!fe7352").Result;
                
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("admin@root").Result;
                    await _userManager.AddToRolesAsync(admin, new [] {"Admin", "Unit Head"});
                }

        }

        [AllowAnonymous]
        [HttpGet("resetusers")]
        public async Task<IActionResult> ResetUsers()
        {
            //TODO: Implement Realistic Implementation
          
          foreach (var user in _userManager.Users)
          {
              await _userManager.DeleteAsync(user);
          }

          return Ok();
        }
    }
}
