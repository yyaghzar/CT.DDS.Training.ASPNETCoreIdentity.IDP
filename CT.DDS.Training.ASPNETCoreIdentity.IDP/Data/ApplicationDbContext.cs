using System;
using System.Collections.Generic;
using System.Text;
using CT.DDS.Training.ASPNETCoreIdentity.IDP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CT.DDS.Training.ASPNETCoreIdentity.IDP.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
