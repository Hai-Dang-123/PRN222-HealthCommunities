﻿namespace HealthCommunitiesCheck2.DTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string PasswordConfirmed { get; set; } 
        public string RoleName { get; set; }
    }
}
