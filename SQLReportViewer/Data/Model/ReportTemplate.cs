using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLReportViewer.Data.Model
{
    public class ReportTemplate
    {
        public int ReportTemplateId { get; set; }

        [Required]
        public string TemplateName { get; set; }

        [Required]
        public string ReportSQL { get; set; }

        [Required]
        public int DbConnectionId { get; set; }

        public DbConnection DbConnection { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }
}
