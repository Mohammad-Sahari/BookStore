﻿using Microsoft.AspNetCore.Identity;
using System;

namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }

    }
}
