using EcommerceProject.DTO;
using EcommerceProject.models;
using EcommerceProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        // /api/account/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = userDto.Email;
                user.UserName = userDto.Name;
                user.PhoneNumber = userDto.PhoneNumber;
                user.Address = userDto.Address;
                user.Image = userDto.Image;
                
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                    return Ok("Account Added Successfully !"); // status code 200

                return BadRequest(result);
            }
            return BadRequest("Some Properties are not valid "); //status code 400
        }

        // /api/account/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(userDto.Email);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        //creat custom list of claims as token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        // get role 
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,role));
                        }

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWt:Key"]));

                        SigningCredentials signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        // Created token 
                        JwtSecurityToken Mytoken = new JwtSecurityToken(
                            issuer: _configuration["JWt:Issuer"],               //url web api provider
                            audience: _configuration["JWt:Audience"],            // url consumer Angular
                            claims:claims,
                            expires:DateTime.Now.AddDays(30),
                            signingCredentials: signCredentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(Mytoken),
                            expiration = Mytoken.ValidTo
                        });
                    }
                }


                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
