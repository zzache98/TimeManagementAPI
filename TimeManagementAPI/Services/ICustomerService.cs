using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> Get();

        Task<Customer> GetById(int id);

        Task<Customer> Create(Customer customer);

        Task Update(Customer customer);

    }
}
