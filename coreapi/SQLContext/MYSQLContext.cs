using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreapi.Models;
namespace coreapi.SQLContext
{
    public class MYSQLContext :  DbContext
    {
        public MYSQLContext() : base() { }
        public MYSQLContext(DbContextOptions<MYSQLContext> options)
          : base(options)
        {
        }
        public DbSet<SYS_USER> SYS_USER { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
    
}
