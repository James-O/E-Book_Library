﻿using E_Book_Library.Models;
using Ebook.Service.Services.Interfases;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ebook.Service.Services.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public JWTService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                Issuer = _config.GetSection("JWT:Issuer").Value,
                Audience = _config.GetSection("JWT:Audience").Value,
                SigningCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256)
            };

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.CreateToken(securityTokenDescriptor);

            return securityTokenHandler.WriteToken(token);
        }
    }
}
