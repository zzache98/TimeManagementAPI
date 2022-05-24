using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeManagementAPI.Data;
using TimeManagementAPI.DTO;

namespace TimeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //<-------Uppdatera------->

        [HttpGet]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<CustomerDTO>> Get()
            => await _context.Customers.Select(x => new CustomerDTO { Id = x.Id, CustomerName = x.CustomerName }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer is null) return NotFound();
            var customerDto = new CustomerDTO
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
            };

            return Ok(customerDto);
        }

        //<-------Skapa------->

        public class CustomerCreateModel
        {
            [Required]
            [MaxLength(50)]
            public string CustomerName { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CustomerCreateModel model)
        {
            var customer = new Customer
            {
                CustomerName = model.CustomerName,
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var customerDto = new CustomerDTO
            {
                CustomerName = customer.CustomerName,
                Id = customer.Id,
            };

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customerDto);
        }

        //<-------Uppdatera------->
        public class CustomerEditModel
        {

            [Required]
            [MaxLength(50)]
            public string CustomerName { get; set; }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CustomerEditModel model)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

            if (customer is null) return NotFound();

            customer.CustomerName = model.CustomerName;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
