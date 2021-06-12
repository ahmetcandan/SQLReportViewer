using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLReportViewer.Data.Model
{
    public class ReportTemplateAuthorization
    {
        public int ReportTemplateAuthorizationId { get; set; }

        [Required]
        public int ReportTemplateId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
