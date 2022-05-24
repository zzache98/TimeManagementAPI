using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public interface ITimeRegisterService
    {
        Task<IEnumerable<TimeRegister>> Get();

        Task<TimeRegister> GetById(int id);

        Task<TimeRegister> Create(TimeRegister timeRegister);

        Task Update(TimeRegister timeRegister);
    }
}
