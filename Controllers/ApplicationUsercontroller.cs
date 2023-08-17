using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using gantt_backend.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : ControllerBase
    {
         private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ITokenService _tokenService;
        private  RoleManager<ApplicationRole> _roleManager;

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IUnitOfWork unitOfWork,
        IMapper mapper,RoleManager<ApplicationRole> roleManager,ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> Get()
    {
        try {
           var tasks = await _unitOfWork.Users.GetAll();         
            if (tasks != null)
            {
                var task = _mapper.Map<IEnumerable<UserViewModel>>(tasks);
                return Ok(task);
            }
            }
            catch(Exception ex)
            {
             Console.Write(ex.Message);
            }

            
        return NotFound();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto model)
    {
        try {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
         var res = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
        var isVal = await _userManager.CheckPasswordAsync(user, model.Password);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        }
        catch( Exception ex)
        {
            Console.Write(ex.Message);
        }
        return Unauthorized();
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUser( UserForRegistrationDto userForRegistration) 
    {
        if (userForRegistration == null || !ModelState.IsValid) 
            return BadRequest(); 
            
        var user = _mapper.Map<ApplicationUser>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password); 
        if (!result.Succeeded) 
        { 
            var errors = result.Errors.Select(e => e.Description); 
                
            return BadRequest(new RegistrationResponseDto { Errors = errors }); 
        }
            
        return StatusCode(200); 
    }
        [AllowAnonymous]
        [HttpPost("register")]
        //Post ApplicationUser/Register
        public async Task<Object> PostApplicationUser( ApplicationUserModel model)
        {
            var appplicationUser = new ApplicationUser() {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try 
            {
                var result = await _userManager.CreateAsync(appplicationUser, model.Password);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("register_role")]
        public async Task<IActionResult> RegisterRole([FromBody] UserForRegistrationDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegistrationResponseDto { IsSuccessfulRegistration = false, Errors = new List<string>().Append("User already exists!") });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = model.LastName + " " + model.FirstName,
                UserName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,  new RegistrationResponseDto { IsSuccessfulRegistration = false, Errors = new List<string>().Append( "User creation failed! Please check user details and try again.") });

            try
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    ApplicationRole applicationRole = new ApplicationRole  
                {                     
                                    Name = model.Role,
                                    Description = "Description"
                };  
                    await _roleManager.CreateAsync(applicationRole);
                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }            
                return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true});
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                 return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true});
            }
        }


    }
}