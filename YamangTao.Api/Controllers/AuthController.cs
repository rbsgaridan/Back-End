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
using YamangTao.Dto;
using YamangTao.Model.Auth;
using YamangTao.Core.Repository;
using YamangTao.Core.HttpParams;
using YamangTao.Data.Helpers;
using YamangTao.Api.Helpers;
using YamangTao.Model.RSP.Pds;
using YamangTao.Data.Core;
using YamangTao.Model.OrgStructure;

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
        private readonly IOrgUnitRepository _orgRepo;
        private readonly IPdsRepository _pdsRepo;

        public AuthController(IConfiguration config, IMapper mapper,
                              UserManager<User> userManager, SignInManager<User> signInManager,
                              RoleManager<Role> roleManager,
                              IEmployeeRepository repo,
                              IOrgUnitRepository orgRepo,
                              IPdsRepository pdsRepo)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _repo = repo;
            _orgRepo = orgRepo;
            _pdsRepo = pdsRepo;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;

        }

        [AllowAnonymous]
        // [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // 1 Check if the ID and user is already an employee
            // if (await _repo.IdExists(userForRegisterDto.Id))
            // {
            //     throw new Exception($"The ID NUMBER: {userForRegisterDto.Id} already exists!");
            // }
            
            bool verified = await _repo.VerifyEmployee(userForRegisterDto.Lastname.ToUpper(), 
                                                        userForRegisterDto.Firstname.ToUpper(),
                                                        userForRegisterDto.Id);
            if (!verified)
            {
                if (await _repo.VerifyEmployee(userForRegisterDto.Lastname.ToUpper(), 
                                                        userForRegisterDto.Firstname.ToUpper())) // If employee exists the ID Number is incorrect
                {
                    throw new ArgumentException($"Wrong ID number for {userForRegisterDto.Firstname} {userForRegisterDto.Lastname}!");
                }
                else
                {
                    throw new ArgumentException($"The system DOES NOT RECOGNIZE this user as an employee. Please contact HRMD Office");
                }
            }

            // 2 Check if the user already exists
            var user = await _userManager.FindByIdAsync(userForRegisterDto.Id);
                if (user != null)
                {
                    throw new ArgumentException($"The user id already exists");
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
                // Update Employee Details
                var employee = await _repo.GetEmployeeByID(userToCreate.EmployeeId);
                employee.MobileNumber = userForRegisterDto.Mobile;
                employee.EmailAddress = userForRegisterDto.Email;
                employee.BranchCampusId = userForRegisterDto.CampusId;
                employee.OrgUnitId = userForRegisterDto.OrgUnitId;
                employee.Sex = userForRegisterDto.Sex;
                employee.BirthDate = userForRegisterDto.Birthdate;
                
                
                var newAddress = new Address() {
                    EmployeeId = employee.Id,
                    Description = "Permanent Address",
                    Block = "",
                    Street = userForRegisterDto.Street,
                    Purok = userForRegisterDto.Purok,
                    Barangay = userForRegisterDto.Barangay,
                    BarangayCode = userForRegisterDto.BarangayCode,
                    Municipality = userForRegisterDto.Municipality,
                    MunicipalityCode = userForRegisterDto.MunicipalityCode,
                    Province = userForRegisterDto.Province,
                    ProvinceCode = userForRegisterDto.ProvinceCode,
                    Region = userForRegisterDto.Region,
                    RegionCode = userForRegisterDto.RegionCode,
                    DateCreated = DateTime.Now
                };
                var newPds = new PersonalDataSheet() {
                    EmployeeId = employee.Id,
                    Addresses = new List<Address>()
                };
                newPds.Addresses.Add(newAddress);
                var pdsFromRepo = await _pdsRepo.GetPdsFullByEmployeeID(employee.Id);
                if (pdsFromRepo == null)
                {
                    _pdsRepo.Add(newPds);
                }
                else
                {
                    pdsFromRepo.Addresses.Add(newAddress);
                }
                
                

                await _repo.SaveAllAsync();

                var createdUser = await _userManager.FindByIdAsync(userToCreate.UserName);
                // Add user as employee
                // _userManager.AddToRoleAsync(createdUser, "Employee").Wait();

                // find org units where the user is the head and add it to the roles
                var orgs = await _orgRepo.OrgUnitByUser(createdUser.Id);
                List<string> roleList = new List<string>();
                roleList.Add("Employee");
                foreach (OrgUnit org in orgs)
                {
                    switch (org.UnitType)
                    {
                        case "College":
                        case "Center":
                        case "Unit":
                            roleList.Add("Unit Head");
                        break;
                        
                        case "Office":
                            if (org.UnitName.Contains("Vice-President"))
                            {
                                roleList.Add("VP");
                            }
                            else if (org.UnitName.Contains("President"))
                            {
                                roleList.Add("President");
                            }
                            else if (org.UnitName.Contains("Human Resource"))
                            {
                                roleList.Add("HR");
                            }
                            else if (org.UnitName.Contains("Planning"))
                            {
                                roleList.Add("Planning");
                            }
                            else 
                            {
                                roleList.Add("Unit Head");
                            }
                        break;
                       
                        default:
                            roleList.Add("Department Head");
                        break;
                    }
                }
                _userManager.AddToRolesAsync(createdUser, roleList.Distinct()).Wait();
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
                appUser.LastActive = DateTime.Now;
                _userManager.UpdateAsync(appUser).Wait();
                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                var employeeDetails = await _repo.GetEmployeeByID(appUser.EmployeeId);
                var currentOrgInit = await _orgRepo.GetOrgUnit(employeeDetails.OrgUnitId);
                
                return Ok(new
                {
                    token = GenerateJwtTokenAsync(appUser).Result,
                    user = userToReturn,
                    detail = new {
                        group = employeeDetails.EmployeeGroup,
                        status = employeeDetails.CurrentStatus,
                        orgunit = employeeDetails.OrgUnitId,
                        supervisor = currentOrgInit.CurrentHeadId
                    }
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

        [Authorize(Policy="RequireAdminRole")]
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

        // [Authorize(Policy="RequireAdminRole")]
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

        // [Authorize(Policy="RequireAdminRole")]
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

        // [Authorize(Policy="RequireAdminRole")]
        [HttpPost("seedadmin")]
        public async Task<IActionResult> SeedAdmin()
        {
            
           var superUser = new User
                {
                    UserName = "admin@root",
                    Id = "admin@root",
                    
                };
                
                IdentityResult result = await _userManager.CreateAsync(superUser,"L!fe7352");
                
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("admin@root").Result;
                    await _userManager.AddToRolesAsync(admin, new [] {"Admin", "Unit Head"});
                    return Ok(admin);
                }
            return BadRequest();

        }

        [HttpPost]
        [Authorize(Policy="RequireAdminRole")]
        public async Task<IActionResult> ResetPassword(PasswordResetDto passwordResetDto)
        {
            var user = await _userManager.FindByIdAsync(passwordResetDto.UsedId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user,token,passwordResetDto.NewPassword);
            //TODO: Implement Realistic Implementation
            return Ok("Password has been reset");
        }

        // [Authorize(Policy="RequireAdminRole")]
        // [HttpPost("resetusers")]
        // public async Task<IActionResult> ResetUsers()
        // {
        //     //TODO: Implement Realistic Implementation
          
        //   foreach (var user in _userManager.Users)
        //   {
        //       if (!user.Id.Equals("admin@root"))
        //       {
        //         await _userManager.DeleteAsync(user);
        //       }
        //   }
        //   return Ok();
        // }

        [HttpGet("users")]
        [Authorize(Policy="RequireAdminRole")]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await _userManager.Users.Where(u => u.KnownAs.ToUpper().Contains(userParams.Keyword) 
                                                    || u.Email.ToUpper().Contains(userParams.Keyword) 
                                                    || u.Id.ToUpper().Contains(userParams.Keyword)
                                                ).ToListAsync();
            // if (!string.IsNullOrEmpty(userParams.OrderBy))
            // {
            //     switch (userParams.OrderBy)
            //     {
            //         case "knownAs":
            //         users = users.OrderBy(s => s.KnownAs);
            //         break;

            //         case "id":
            //         users = users.OrderBy(s => s.Id);
            //         break;

            //         case "email":
            //         users = users.OrderBy(s => s.Email);
            //         break;

            //         default:
            //         break;
            //     }
            // }
            // // users = users.Include(u => u.UserRoles);
            // var pagedUsersToReturn = await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
            // var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(pagedUsersToReturn);
            // Response.AddPagination(pagedUsersToReturn.CurrentPage, pagedUsersToReturn.TotalCount, pagedUsersToReturn.PageSize,  pagedUsersToReturn.TotalPages);
            return Ok(users);
        }
    }
}
