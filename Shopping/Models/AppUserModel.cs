using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class AppUserModel : IdentityUser
    {
        public string occupation { get; set; }
       
        public string RoleId { get; internal set; }
    }
}
