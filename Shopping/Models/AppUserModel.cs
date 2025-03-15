﻿using Microsoft.AspNetCore.Identity;

namespace Shopping.Models
{
    public class AppUserModel : IdentityUser
    {
        public string occupation { get; set; }
    }
}
