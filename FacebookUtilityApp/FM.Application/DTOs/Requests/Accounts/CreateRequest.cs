﻿namespace FM.Application.DTOs.Requests.Accounts
{
    public class CreateRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Roles { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
