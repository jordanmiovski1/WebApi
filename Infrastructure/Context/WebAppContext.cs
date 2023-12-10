using Core.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Context
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options)
           : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Country> Country { get; set; }
    }
}
