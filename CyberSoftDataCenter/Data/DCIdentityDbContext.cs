using CyberSoftDataCenter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Data
{
    public class DCIdentityDbContext: IdentityDbContext<DCUsers,DCRoles,string>
    {
        public DCIdentityDbContext(DbContextOptions<DCIdentityDbContext> options)
            : base(options)
        {


        }
    }
}
