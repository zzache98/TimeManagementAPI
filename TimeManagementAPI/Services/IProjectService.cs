using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> Get();

        Task<Project> GetById(int id);

        Task<Project> Create(Project project);

        Task Update(Project project);
    }
}
