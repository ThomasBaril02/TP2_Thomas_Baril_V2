using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JuliePro.Data;
using JuliePro.Models;
using JuliePro.Services;

namespace JuliePro.Controllers
{
    public class RecordController : Controller
    {
        private readonly JulieProDbContext _context;
        private readonly RecordService _recordService;
        public RecordController(JulieProDbContext context, RecordService recordService)
        {
            _context = context;
            _recordService = recordService;
        }

        // GET: Record
        [Route("Record")]
        public async Task<IActionResult> Index()
        {
            var julieProDbContext = _context.Records.Include(a => a.Discipline).Include(a => a.Trainer);
            return View(await julieProDbContext.ToListAsync());
        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(a => a.Discipline)
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // GET: Record/Create
        public async Task<IActionResult> Create()
        {
            var record = await _recordService.BuildViewModelAsync();
            return View(record);
        }

        // POST: Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecordViewModel record)
        {
            if (!ModelState.IsValid)
            {
                var a = await _recordService.BuildViewModelAsync(record.Record);
                return RedirectToAction(nameof(Index));
            }
            await _recordService.AddAsync(record.Record);
            return View(record);
        }

        // GET: Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var record = await _recordService.FindAsync(id.Value);
            if (record == null)
            {
                return NotFound();
            }

            var vm = await _recordService.BuildViewModelAsync(record);

            return View(vm);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecordViewModel vm)
        {
            if (id != vm.Record.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                vm = await _recordService.BuildViewModelAsync(vm.Record);
                return View(vm);
            }

            try
            {
                await _recordService.UpdateAsync(vm.Record);
            }
            catch
            {
                if (!await _recordService.ExistsAsync(vm.Record.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(a => a.Discipline)
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @record = await _context.Records.FindAsync(id);
            if (@record != null)
            {
                _context.Records.Remove(@record);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
