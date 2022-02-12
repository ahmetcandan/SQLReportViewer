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
    public class DbConnectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DbConnectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DbConnections
        public async Task<IActionResult> Index()
        {
            return View(await _context.DbConnections.Where(c => c.IsActive && !c.IsDelete).ToListAsync());
        }

        // GET: DbConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbConnection = await _context.DbConnections
                .FirstOrDefaultAsync(m => m.DbConnectionId == id);
            if (dbConnection == null)
            {
                return NotFound();
            }

            return View(dbConnection);
        }

        // GET: DbConnections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DbConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DbConnectionId,ConnectionName,ConnectionString")] DbConnection dbConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbConnection);
        }

        // GET: DbConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbConnection = await _context.DbConnections.Where(c => c.IsActive && !c.IsDelete).FirstAsync(c => c.DbConnectionId == id);
            if (dbConnection == null)
            {
                return NotFound();
            }
            return View(dbConnection);
        }

        // POST: DbConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DbConnectionId,ConnectionName,ConnectionString,IsActive")] DbConnection dbConnection)
        {
            if (id != dbConnection.DbConnectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbConnectionExists(dbConnection.DbConnectionId))
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
            return View(dbConnection);
        }

        // GET: DbConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbConnection = await _context.DbConnections
                .Where(c => c.IsActive && !c.IsDelete)
                .FirstOrDefaultAsync(m => m.DbConnectionId == id);
            if (dbConnection == null)
            {
                return NotFound();
            }

            return View(dbConnection);
        }

        // POST: DbConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dbConnection = await _context.DbConnections.FindAsync(id);
            dbConnection.IsDelete = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbConnectionExists(int id)
        {
            return _context.DbConnections.Any(e => e.DbConnectionId == id);
        }
    }
}
