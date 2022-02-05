using System.Data;

namespace SQLReportViewer.Models
{
    public class ReportViewModel
    {
        public DataTable DataTable { get; set; }

        public string ReportName { get; set; }
    }
}
