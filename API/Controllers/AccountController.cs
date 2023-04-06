using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context , ITokenService tokenService)
            {
                _tokenService = tokenService;
                _context = context;
                
            }

        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

         if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
         using var hmac = new HMACSHA512();
         var user = new AppUser 
            {UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key};
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
       
            }
            [HttpPost("login")]
            public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
                if(user == null) return Unauthorized("invalid Username");
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for(int i = 0; i<computedHash.Length; i++){
                    if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
                }
                  return new UserDto{
                Username = user.UserName,
                 Token = _tokenService.CreateToken(user)
            };
            }   
            private async Task<bool> UserExists(string username)
            {
                return await _context.Users.AnyAsync(x => x.UserName == username.ToLower() );
            }
    }
}