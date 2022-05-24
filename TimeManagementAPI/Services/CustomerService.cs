using Microsoft.EntityFrameworkCore;
using TimeManagementAPI.Data;

namespace TimeManagementAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> Create(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }


        public async Task<IEnumerable<Customer>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
