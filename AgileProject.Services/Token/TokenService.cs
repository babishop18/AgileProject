using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AgileProject.Data;
using AgileProject.Models.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AgileProject.Services.Token
{
   public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        // this will generate the token for authorization, before giving token, need to check if user and password pair exist in database
        // takes in a tokenrequest model
        // remember this is just services, so this means the controller will be caller, services only handle logic
        public async Task<TokenResponse> GetTokenAsync(TokenRequest model)
        {
            // our getvaliduserasync method returns a userentity
            UserEntity userEntity = await GetValidUserAsync(model);
            if (userEntity is null)
            {
                // System.Console.WriteLine("INSIDE TOKEN SERVICE: USER IS NULL");
                return null;
            }


            // our GenerateToken method returns a TokenResponse
            return GenerateToken(userEntity);
        }
        // will check to see if user and password matches a user in database
        private async Task<UserEntity> GetValidUserAsync(TokenRequest model)
        {

            // Major issue encounter before was that if we call FirstOrDefaultAsync, it will do whatever and loop through all columns of a particular User, and if a column is 
            // non nullable and has a null value, the toString() method will fail, toString will skip columns if it knows that the columns are nullable, but if you have null values in
            // non null columns the method will fail, though this doesnt make sense because i made sure to make firstName and lastName nullable, but still failed, so i added the ? 
            // and it fixed it
            // System.Console.WriteLine("\n\n\n\nGOING TO EXECUTE\n\n\n\n");
            UserEntity? userEntity = await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == model.Username.ToLower());
            // System.Console.WriteLine("\n\n\n\nJUST EXECUTED\n\n\n\n");
            if (userEntity is null)
                return null;
            // since username exists, we need to unhash the associated password and check with the tokenrequest password, which is from the user input
            PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
            PasswordVerificationResult verifyPasswordResult = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);
            if (verifyPasswordResult == PasswordVerificationResult.Failed)
                return null;

            return userEntity;
        }
        private TokenResponse GenerateToken(UserEntity entity)
        {
            Claim[] claims = GetClaims(entity);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //changed var to SecurityTokenDescriptor
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = credentials
            };
            // changed var to JwtSecurityTokenHandler
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // changed var to SecurityToken
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse tokenResponse = new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = token.ValidFrom,
                Expires = token.ValidTo
            };

            return tokenResponse;
        }
        private Claim[] GetClaims(UserEntity user)
        {
            

            Claim[] claims = new Claim[] { new Claim("UserId", user.UserId.ToString()), new Claim("Username", user.Username), new Claim("Classifier", user.Classifier)};

            return claims;
        }
    }
}