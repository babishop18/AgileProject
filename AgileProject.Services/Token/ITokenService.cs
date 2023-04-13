using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.Token;

namespace AgileProject.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest model);
    }
}