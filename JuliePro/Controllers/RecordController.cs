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
        private readonly RecordService _recordService;
        public RecordController(RecordService recordService)
        {
            _recordService = recordService;
        }

        // GET: Record
        [Route("Record")]
        public async Task<IActionResult> Index()
        {
            var julieProDbContext = _recordService.GetAllAsync();
            return View(await julieProDbContext);
        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _recordService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(RecordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm = await _recordService.BuildViewModelAsync(vm.Record);
                return View(vm);
            }

            await _recordService.AddAsync(vm.Record);
            return RedirectToAction(nameof(Index));
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

            var @record = await _recordService.GetByIdAsync(id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmé(int id)
        {
            var record = await _recordService.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            await _recordService.DeleteAsync(record);
            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> TrainerIndex(int trainerId)
        {
            var trainer = await _recordService.GetRecordByTrainer(trainerId);

            if (trainer == null)
                return NotFound();

            return View("TrainerRecords", trainer);
        }
    }
}
