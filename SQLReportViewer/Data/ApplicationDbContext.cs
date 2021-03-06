using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLReportViewer.Data.Model;

namespace SQLReportViewer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReportTemplate> ReportTemplates { get; set; }
        public DbSet<ReportFilter> ReportFilters { get; set; }
        public DbSet<ReportFilterType> ReportFilterTypes { get; set; }
        public DbSet<ReportTemplateAuthorization> ReportTemplateAuthorizations { get; set; }
        public DbSet<DbConnection> DbConnections { get; set; }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
