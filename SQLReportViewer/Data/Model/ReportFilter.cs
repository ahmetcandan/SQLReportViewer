using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLReportViewer.Data.Model
{
    public class ReportFilter
    {
        public int ReportFilterId { get; set; }

        [Required]
        public int ReportTemplateId { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        [Required]
        public string ColumnName { get; set; }

        public string ChooseSelectQuery { get; set; }

        public string DefaultValue { get; set; }

        [Required]
        public int ReportFilterTypeId { get; set; }

        public ReportFilterType ReportFilterType { get; set; }

        [Required]
        public bool Required { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    }

    public class ReportFilterType
    {
        public int ReportFilterTypeId { get; set;}
        public string FilterTypeName { get; set; }
    }
}
