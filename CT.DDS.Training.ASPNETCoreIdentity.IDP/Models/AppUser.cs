using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CT.DDS.Training.ASPNETCoreIdentity.IDP.Models
{
    public class AppUser:IdentityUser
    {
     
        [Required]
        public string Title { get; set; }
    }
}
