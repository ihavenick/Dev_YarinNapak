using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dev_YarinNeYapak.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string facebookid { get; set; }
        public bool facelekayitolmus { get; set; }
        public string adsoyad { get; set; }
        public string AvatarBase64 { get; set; }
    }
}
