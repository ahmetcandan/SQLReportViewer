using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLReportViewer.Data;
using SQLReportViewer.Data.Model;
using ADO.Net.Client.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using SQLReportViewer.Models;
using PagedList.Core;

namespace SQLReportViewer.Controllers
{
    public class ReportViewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportViewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string query, int page = 1, int count = 10)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            foreach (var item in query.Split(";"))
            {
                var p = item.Split(":");
                if (p.Length == 2)
                    queryParams.Add(p[0], p[1]);
            }
            int templateId = Convert.ToInt32(queryParams["id"]);
            queryParams.Remove("id");
            var reportTemplate = _context.ReportTemplates.FirstOrDefault(c => c.ReportTemplateId == templateId);
            var dbConnection = _context.DbConnections.FirstOrDefault(c => c.DbConnectionId == reportTemplate.DbConnectionId);
            var reportFilter = (from f in _context.ReportFilters
                                    join t in _context.ReportFilterTypes
                                        on f.ReportFilterTypeId equals t.ReportFilterTypeId
                                    where f.ReportTemplateId == templateId
                                    select new { Filter = f, t.FilterTypeName });

            Dictionary<string, string> filterParams = new Dictionary<string, string>();
            foreach (var item in queryParams)
            {
                int reportFilterId = Convert.ToInt32(item.Key.Substring(6));
                var filter = reportFilter.FirstOrDefault(c => c.Filter.ReportFilterId == reportFilterId);
                filterParams.Add(filter.Filter.ColumnName, item.Value);
            }

            var reportyQuery = new ReportQuery(dbConnection.ConnectionString, reportTemplate.ReportSQL, page, count, filterParams);
            var result = reportyQuery.GetResult();
            result.ReportFilters = new List<ReportFilterModel>();
            foreach (var filter in reportFilter)
            {
                var dt = reportyQuery.ExecuteQuery(filter.Filter.ChooseSelectQuery);
                List<SelectListItem> filterKeyValues = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                    filterKeyValues.Add(new SelectListItem(dr["text"].ToString(), dr["value"].ToString()));
                result.ReportFilters.Add(new ReportFilterModel
                {
                    ReportFilterId = filter.Filter.ReportFilterId,
                    ColumnName = filter.Filter.ColumnName,
                    DefaultValue = filter.Filter.DefaultValue,
                    FilterTypeId = filter.Filter.ReportFilterTypeId,
                    FilterTypeName = filter.FilterTypeName,
                    Required = filter.Filter.Required,
                    FilterKeyValues = filterKeyValues,
                    Value = "42"
                });
            }
            result.SearchQuery = query;
            result.PageCount = count;
            result.ReportTemplateId = templateId;

            return View(result);
        }
    }
}
