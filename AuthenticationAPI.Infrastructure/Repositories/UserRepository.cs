using AuthenticationApi.Domain.Entities;
using AuthenticationAPI.Application.DTOs;
using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Infrastructure.Data;
using eCommerce.SharedLib.ResponseT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Infrastructure.Repositories
{
    public class UserRepository(AuthenticationDbContext context,IConfiguration config) : IUser
    {
        public async Task<GetUserDTO> GetUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user is null? null! : new GetUserDTO(
                                                id: user.Id,
                                                Name: user.Name ?? string.Empty,
                                                Address: user.Address ?? string.Empty,
                                                PhoneNumber: user.PhoneNumber ?? string.Empty
                                                ,Email: user.Email ?? string.Empty,
                                                Role: user.Role ?? string.Empty
                                                                                );
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user is null ? null! : user;
        }

        public async Task<Response> Login(LoginDTO appUser)
        {
           var getUser = await GetUserByEmail(appUser.Email);
            if (getUser is null)
                return new Response { Flag = false, Message = "Invalid email or password" };
           bool verifyPassword = BCrypt.Net.BCrypt.Verify(appUser.Password,  getUser.Password);
            if (!verifyPassword)
                return new Response { Flag = false, Message = "Invalid email or password" };

            string token = GenerateToken(getUser);
            return new Response (true,token);
        }

        private  string GenerateToken(AppUser user)
        {
           var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!); 
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
          
            };
            if (string.IsNullOrEmpty(user.Role) ||  !Equals("string",user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role!));
            }
            var token = new JwtSecurityToken(
                issuer: config["Authentication:Issuer"], 
                audience: config["Authentication:Audience"], 
                claims: claims,
                expires : DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials


                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Response> Register(AppUserDTO appUser)
        {
            var getUser = await GetUserByEmail(appUser.Email);
            if (getUser is not null)
                return new Response{Flag = false,Message = "User already exists"};
            var result = await context.Users.AddAsync (new AppUser
            {
                Name = appUser.Name,
                Address = appUser.Address,
                PhoneNumber = appUser.PhoneNumber,
                Email = appUser.Email,
                Password =  BCrypt.Net.BCrypt.HashPassword(appUser.Password),
                Role = appUser.Role
            });
            await context.SaveChangesAsync();
            return result.Entity.Id > 0
                ? new Response { Flag = true, Message = $"User {result.Entity.Name} registered successfully" }
                : new Response { Flag = false, Message = "User registration failed" };

        }
    }
}
