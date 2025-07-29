using AuthenticationApi.Domain.Entities;
using AuthenticationAPI.Application.DTOs;
using eCommerce.SharedLib.ResponseT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Interfaces
{
    public interface IUser
    {
        Task<Response> Register(AppUserDTO appUser);
        Task<Response> Login(LoginDTO appUser);
        Task<GetUserDTO> GetUser(int userId);
        Task<AppUser> GetUserByEmail(string email);
    }
}
