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

namespace SQLReportViewer.Controllers
{
    public class ReportViewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportViewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var reportTemplate = _context.ReportTemplates.FirstOrDefault(c => c.ReportTemplateId == id);
            var dbConnection = _context.DbConnections.FirstOrDefault(c => c.DbConnectionId == reportTemplate.DbConnectionId);
            DataTable dt = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(dbConnection.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(reportTemplate.ReportSQL, sqlConnection))
                {
                    var myReader = myCommand.ExecuteReader();
                    dt.Load(myReader);

                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return View(new ReportViewModel { DataTable = dt, ReportName = reportTemplate.TemplateName });
        }
    }
}
