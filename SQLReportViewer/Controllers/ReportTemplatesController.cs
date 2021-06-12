using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLReportViewer.Data;
using SQLReportViewer.Data.Model;

namespace SQLReportViewer.Controllers
{
    public class ReportTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportTemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportTemplates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReportTemplates.Include(r => r.DbConnection);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReportTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplates
                .Include(r => r.DbConnection)
                .FirstOrDefaultAsync(m => m.ReportTemplateId == id);
            if (reportTemplate == null)
            {
                return NotFound();
            }

            return View(reportTemplate);
        }

        // GET: ReportTemplates/Create
        public IActionResult Create()
        {
            ViewData["DbConnectionId"] = new SelectList(_context.DbConnections, "DbConnectionId", "ConnectionName");
            return View();
        }

        // POST: ReportTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportTemplateId,TemplateName,ReportSQL,DbConnectionId,IsActive,IsDelete")] ReportTemplate reportTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DbConnectionId"] = new SelectList(_context.DbConnections, "DbConnectionId", "ConnectionName", reportTemplate.DbConnectionId);
            return View(reportTemplate);
        }

        // GET: ReportTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplates.FindAsync(id);
            if (reportTemplate == null)
            {
                return NotFound();
            }
            ViewData["DbConnectionId"] = new SelectList(_context.DbConnections, "DbConnectionId", "ConnectionName", reportTemplate.DbConnectionId);
            return View(reportTemplate);
        }

        // POST: ReportTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportTemplateId,TemplateName,ReportSQL,DbConnectionId,IsActive,IsDelete")] ReportTemplate reportTemplate)
        {
            if (id != reportTemplate.ReportTemplateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportTemplateExists(reportTemplate.ReportTemplateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DbConnectionId"] = new SelectList(_context.DbConnections, "DbConnectionId", "ConnectionName", reportTemplate.DbConnectionId);
            return View(reportTemplate);
        }

        // GET: ReportTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplate = await _context.ReportTemplates
                .Include(r => r.DbConnection)
                .FirstOrDefaultAsync(m => m.ReportTemplateId == id);
            if (reportTemplate == null)
            {
                return NotFound();
            }

            return View(reportTemplate);
        }

        // POST: ReportTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportTemplate = await _context.ReportTemplates.FindAsync(id);
            _context.ReportTemplates.Remove(reportTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportTemplateExists(int id)
        {
            return _context.ReportTemplates.Any(e => e.ReportTemplateId == id);
        }
    }
}
