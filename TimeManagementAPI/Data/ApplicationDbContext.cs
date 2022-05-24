﻿using Microsoft.EntityFrameworkCore;
using TimeManagementAPI.DTO;

namespace TimeManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeRegister> TimeRegisters { get; set; }
    }
}
