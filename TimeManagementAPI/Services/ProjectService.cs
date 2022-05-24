using Microsoft.EntityFrameworkCore;
using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Project> Create(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }


        public async Task<IEnumerable<Project>> Get()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
