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
    public class ReportFilterTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportFilterTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportFilterTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportFilterTypes.ToListAsync());
        }

        // GET: ReportFilterTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilterType = await _context.ReportFilterTypes
                .FirstOrDefaultAsync(m => m.ReportFilterTypeId == id);
            if (reportFilterType == null)
            {
                return NotFound();
            }

            return View(reportFilterType);
        }

        // GET: ReportFilterTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportFilterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportFilterTypeId,FilterTypeName")] ReportFilterType reportFilterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportFilterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportFilterType);
        }

        // GET: ReportFilterTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilterType = await _context.ReportFilterTypes.FindAsync(id);
            if (reportFilterType == null)
            {
                return NotFound();
            }
            return View(reportFilterType);
        }

        // POST: ReportFilterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportFilterTypeId,FilterTypeName")] ReportFilterType reportFilterType)
        {
            if (id != reportFilterType.ReportFilterTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportFilterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportFilterTypeExists(reportFilterType.ReportFilterTypeId))
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
            return View(reportFilterType);
        }

        // GET: ReportFilterTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportFilterType = await _context.ReportFilterTypes
                .FirstOrDefaultAsync(m => m.ReportFilterTypeId == id);
            if (reportFilterType == null)
            {
                return NotFound();
            }

            return View(reportFilterType);
        }

        // POST: ReportFilterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportFilterType = await _context.ReportFilterTypes.FindAsync(id);
            _context.ReportFilterTypes.Remove(reportFilterType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportFilterTypeExists(int id)
        {
            return _context.ReportFilterTypes.Any(e => e.ReportFilterTypeId == id);
        }
    }
}
