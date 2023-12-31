﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Auth
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }
        public Guid GetUserId(string token)
        {
            return tokenRepository.GetUserId(token);
        }
    }
}
