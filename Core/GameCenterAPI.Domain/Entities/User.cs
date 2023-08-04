using AspNetCore.Identity.MongoDbCore.Models;
using DnsClient;
using GameCenterAPI.Domain.Entities.Base;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GameCenterAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        //public List<Role> Roles { get; set; }
        public string RefreshToken { get; set; }
        public List<string> OperationClaims { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }


    }
}

//Logins
//Tokens
