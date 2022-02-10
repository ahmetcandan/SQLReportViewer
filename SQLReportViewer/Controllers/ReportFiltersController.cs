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
    public class ReportFiltersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportFiltersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportFilters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReportFilters.Include(r => r.ReportFilterType).Include(r => r.ReportTemplate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReportFilters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilter = await _context.ReportFilters
                .Include(r => r.ReportFilterType)
                .Include(r => r.ReportTemplate)
                .FirstOrDefaultAsync(m => m.ReportFilterId == id);
            if (reportFilter == null)
            {
                return NotFound();
            }

            return View(reportFilter);
        }

        // GET: ReportFilters/Create
        public IActionResult Create(int id)
        {
            ViewData["ReportFilterTypeId"] = new SelectList(_context.ReportFilterTypes, "ReportFilterTypeId", "FilterTypeName");
            var reportFilter = new ReportFilter();
            reportFilter.ReportTemplateId = id;
            return View(reportFilter);
        }

        // POST: ReportFilters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportFilterId,ReportTemplateId,ColumnName,ChooseSelectQuery,DefaultValue,ReportFilterTypeId,Required")] ReportFilter reportFilter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportFilter);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "ReportTemplates", new { id = reportFilter.ReportTemplateId });
            }
            ViewData["ReportFilterTypeId"] = new SelectList(_context.ReportFilterTypes, "ReportFilterTypeId", "FilterTypeName", reportFilter.ReportFilterTypeId);
            return View(reportFilter);
        }

        // GET: ReportFilters/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilter = _context.ReportFilters.FirstOrDefault(c => c.ReportFilterId == id);
            if (reportFilter == null)
            {
                return NotFound();
            }
            ViewData["ReportFilterTypeId"] = new SelectList(_context.ReportFilterTypes, "ReportFilterTypeId", "FilterTypeName", reportFilter.ReportFilterTypeId);
            return View(reportFilter);
        }

        // POST: ReportFilters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportFilterId,ReportTemplateId,ColumnName,ChooseSelectQuery,DefaultValue,ReportFilterTypeId,Required,IsActive")] ReportFilter reportFilter)
        {
            if (id != reportFilter.ReportFilterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportFilter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportFilterExists(reportFilter.ReportFilterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "ReportTemplates", new { id = reportFilter.ReportTemplateId });
            }
            ViewData["ReportFilterTypeId"] = new SelectList(_context.ReportFilterTypes, "ReportFilterTypeId", "FilterTypeName", reportFilter.ReportFilterTypeId);
            return View(reportFilter);
        }

        // GET: ReportFilters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilter = await _context.ReportFilters
                .Include(r => r.ReportFilterType)
                .Include(r => r.ReportTemplate)
                .FirstOrDefaultAsync(m => m.ReportFilterId == id);
            if (reportFilter == null)
            {
                return NotFound();
            }

            return View(reportFilter);
        }

        // POST: ReportFilters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportFilter = await _context.ReportFilters.FindAsync(id);
            reportFilter.IsDelete = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportFilterExists(int id)
        {
            return _context.ReportFilters.Any(e => e.ReportFilterId == id);
        }
    }
}
