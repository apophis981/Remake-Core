﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SubterfugeCore.Models.GameEvents
{

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
    
    public class AuthorizationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizedTokenRequest
    {
        public string Token { get; set; }
    }

    public class AuthorizationResponse : NetworkResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }

    public class AccountRegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DeviceIdentifier { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class AccountRegistrationResponse : NetworkResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
    
    public class GetRolesRequest {}

    public class GetRolesResponse : NetworkResponse
    {
        public UserClaim[] Claims { get; set; }
    }

    public enum UserClaim
    {
        Unknown,
        User,
        Moderator,
        Administrator,
        EmailVerified,
        Banned,
    }
}