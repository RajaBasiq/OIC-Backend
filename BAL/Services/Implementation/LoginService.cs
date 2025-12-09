using AutoMapper;
using BAL.DataModel;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LoginService(UserManager<ApplicationUser> userManager, IConfiguration config, ILogger<LoginService> logger, IRefreshTokenRepo refreshTokenRepo, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _refreshTokenRepo = refreshTokenRepo;
            _mapper = mapper;
        }
        public async Task<LoginDto> Login(LoginRequest dto)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(dto.Email);
                if (user != null) 
                {
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);
                    if (!isPasswordCorrect) 
                    {
                        return null;
                    }
                    var accessToken = await GenerateJwtToken(user);
                    var refreshTokenDataModel = GenerateRefreshToken(user.Id);
                    var refreshToken=_mapper.Map<RefreshTokenDto>(refreshTokenDataModel);
                    var _=_refreshTokenRepo.AddAsync(refreshToken).Result;
                    var loginDto = new LoginDto
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken.Token
                    };
                    return loginDto;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        [HttpPost("refresh")]
        public async Task<LoginDto> Refresh(string refreshToken)
        {
            var storedToken = _refreshTokenRepo.Get(refreshToken);

            if (storedToken == null)
                throw new Exception("Invalid refresh token");

            if (storedToken.IsUsed)
                throw new Exception("Token has already been used");

            if (storedToken.IsRevoked)
                throw new Exception("Token has been revoked");

            if (storedToken.Expires < DateTime.UtcNow)
                throw new Exception("Token has expired");

            storedToken.IsUsed = true;
            await _refreshTokenRepo.UpdateAsync(storedToken);

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            var newJwt = await GenerateJwtToken(user);
            var newRefresh = GenerateRefreshToken(user.Id);
            var refreshTokenDto = _mapper.Map<RefreshTokenDto>(newRefresh);

            await _refreshTokenRepo.AddAsync(refreshTokenDto);

            return new LoginDto()
            {
                AccessToken = newJwt,
                RefreshToken = refreshTokenDto.Token
            };
        }

        private RefreshTokenDataModel GenerateRefreshToken(string userId)
        {
            return new RefreshTokenDataModel
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                IsUsed = false,
                IsRevoked = false
            };
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            // Add user roles as claims
            claims.AddRange(userRoles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // short expiry = more secure
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
