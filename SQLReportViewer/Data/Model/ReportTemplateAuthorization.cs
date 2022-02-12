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

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
