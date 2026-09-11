using System;
using Microsoft.EntityFrameworkCore;

namespace CollegeIdManagement.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using var context = new AppDbContext();
            context.Database.EnsureCreated();
            DbSeeder.Seed(context);
        }
    }
}
