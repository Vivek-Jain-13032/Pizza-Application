﻿using Microsoft.AspNetCore.Identity;
using Pizza.Models;

namespace Pizza.Services
{
    public interface IJwtToken
    {
        string CreateJwtToken(string email, string role);
    }
}
