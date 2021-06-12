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
    public class ReportTemplateAuthorizationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportTemplateAuthorizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportTemplateAuthorizations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportTemplateAuthorizations.ToListAsync());
        }

        // GET: ReportTemplateAuthorizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplateAuthorization = await _context.ReportTemplateAuthorizations
                .FirstOrDefaultAsync(m => m.ReportTemplateAuthorizationId == id);
            if (reportTemplateAuthorization == null)
            {
                return NotFound();
            }

            return View(reportTemplateAuthorization);
        }

        // GET: ReportTemplateAuthorizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportTemplateAuthorizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportTemplateAuthorizationId,ReportTemplateId,UserId,RoleId")] ReportTemplateAuthorization reportTemplateAuthorization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportTemplateAuthorization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportTemplateAuthorization);
        }

        // GET: ReportTemplateAuthorizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplateAuthorization = await _context.ReportTemplateAuthorizations.FindAsync(id);
            if (reportTemplateAuthorization == null)
            {
                return NotFound();
            }
            return View(reportTemplateAuthorization);
        }

        // POST: ReportTemplateAuthorizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportTemplateAuthorizationId,ReportTemplateId,UserId,RoleId")] ReportTemplateAuthorization reportTemplateAuthorization)
        {
            if (id != reportTemplateAuthorization.ReportTemplateAuthorizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportTemplateAuthorization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportTemplateAuthorizationExists(reportTemplateAuthorization.ReportTemplateAuthorizationId))
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
            return View(reportTemplateAuthorization);
        }

        // GET: ReportTemplateAuthorizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTemplateAuthorization = await _context.ReportTemplateAuthorizations
                .FirstOrDefaultAsync(m => m.ReportTemplateAuthorizationId == id);
            if (reportTemplateAuthorization == null)
            {
                return NotFound();
            }

            return View(reportTemplateAuthorization);
        }

        // POST: ReportTemplateAuthorizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportTemplateAuthorization = await _context.ReportTemplateAuthorizations.FindAsync(id);
            _context.ReportTemplateAuthorizations.Remove(reportTemplateAuthorization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportTemplateAuthorizationExists(int id)
        {
            return _context.ReportTemplateAuthorizations.Any(e => e.ReportTemplateAuthorizationId == id);
        }
    }
}
