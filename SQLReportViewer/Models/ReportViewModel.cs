using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using SQLReportViewer.Controllers;
using System.Collections.Generic;
using System.Data;

namespace SQLReportViewer.Models
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {

        }

        public int ReportTemplateId { get; set; }

        public IPagedList<DataRow> DataRows { get; set; }

        public DataColumnCollection Columns { get; set; }

        public string ReportName { get; set; }

        public int TotalRowCount { get; set; }
        
        public string SearchQuery { get; set; }

        public int PageCount { get; set; }

        public List<ReportFilterModel> ReportFilters { get; set; }
    }

    public class ReportFilterModel
    {
        public int ReportFilterId { get; set; }

        public string ColumnName { get; set; }

        public int FilterTypeId { get; set; }

        public string FilterTypeName { get; set; }

        public string DefaultValue { get; set; }
        
        public bool Required { get; set; }

        public string Value { get; set; }

        public List<SelectListItem> FilterKeyValues { get; set; }
    }
}
