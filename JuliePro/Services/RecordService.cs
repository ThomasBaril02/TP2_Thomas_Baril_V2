using JuliePro.Data;
using JuliePro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliePro.Services
{
    public class RecordService
    {
        private readonly JulieProDbContext _context;

        public RecordService(JulieProDbContext context)
        {
            _context = context;
        }

        public async Task<List<Record>> GetAllAsync()
        {
            return await _context.Records
                .Include(r => r.Discipline)
                .Include(r => r.Trainer)
                .ToListAsync();
        }

        public async Task<Record?> GetByIdAsync(int id)
        {
            return await _context.Records
                .Include(r => r.Discipline)
                .Include(r => r.Trainer)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Record?> FindAsync(int id)
        {
            return await _context.Records.FindAsync(id);
        }

        public async Task<RecordViewModel> BuildViewModelAsync(Record? record = null)
        {
            record ??= new Record();

            return new RecordViewModel
            {
                Record = record,
                Disciplines = new SelectList(await _context.Disciplines.ToListAsync(), "Id", "Name", record.Discipline_Id),
                Trainers = new SelectList(await _context.Trainers.ToListAsync(),"Id","FullName",record.Trainer_Id)
            };
        }

        public async Task AddAsync(Record record)
        {
            _context.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Record record)
        {
            _context.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Record record)
        {
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Records.AnyAsync(r => r.Id == id);
        }
    }
}