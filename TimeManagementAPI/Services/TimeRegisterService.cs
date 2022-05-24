using Microsoft.EntityFrameworkCore;
using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public class TimeRegisterService : ITimeRegisterService
    {
        private readonly ApplicationDbContext _context;

        public TimeRegisterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TimeRegister> Create(TimeRegister timeRegister)
        {
            _context.TimeRegisters.Add(timeRegister);
            await _context.SaveChangesAsync();

            return timeRegister;
        }


        public async Task<IEnumerable<TimeRegister>> Get()
        {
            return await _context.TimeRegisters.ToListAsync();
        }

        public async Task<TimeRegister> GetById(int id)
        {
            return await _context.TimeRegisters.FindAsync(id);
        }

        public async Task Update(TimeRegister timeRegister)
        {
            _context.Entry(timeRegister).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
