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
    public class TimeRegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TimeRegisterController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //<-------Uppdatera------->

        [HttpGet]
        [ProducesResponseType(typeof(TimeRegisterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<TimeRegisterDTO>> Get()
            => await _context.TimeRegisters.Select(x => new TimeRegisterDTO { Id = x.Id, Date = x.Date, 
                NumberOfMinutes = x.NumberOfMinutes, Description = x.Description }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var timeRegister = await _context.TimeRegisters.FindAsync(id);
            

            if (timeRegister is null) return NotFound();
            var timeRegisterDto = new TimeRegisterDTO
            {
                Id = timeRegister.Id,
                Date = timeRegister.Date,
                NumberOfMinutes = timeRegister.NumberOfMinutes,
                Description = timeRegister.Description,
            };

            return Ok(timeRegisterDto);
        }

        //<-------Skapa------->

        public class TimeRegisterCreateModel
        {
            [Required]
            public DateTime Date { get; set; }

            [Required]
            public int NumberOfMinutes { get; set; }
            public string Description { get; set; }

            public int ProjectId { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(TimeRegisterCreateModel model)
        {
            var project = await _context.Projects.FindAsync(model.ProjectId);

            if (project is null) return NotFound("Project does not exist");
            
            var timeRegister = new TimeRegister
            {
                Date = model.Date,
                NumberOfMinutes = model.NumberOfMinutes,
                Description = model.Description,
                Project = project,
            };

            await _context.TimeRegisters.AddAsync(timeRegister);
            await _context.SaveChangesAsync();

            var timeRegisterDto = new TimeRegisterDTO
            {
                Id = timeRegister.Id,
                Date = timeRegister.Date,
                NumberOfMinutes = timeRegister.NumberOfMinutes,
                Description = timeRegister.Description,

                
            };

            return CreatedAtAction(nameof(GetById), new { id = timeRegister.Id }, timeRegisterDto);
        }

        //<-------Uppdatera------->
        public class TimeRegisterEditModel
        {

            [Required]
            public DateTime Date { get; set; }

            [Required]
            public int NumberOfMinutes { get; set; }
            public string Description { get; set; }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, TimeRegisterEditModel model)
        {
            var timeRegister = _context.TimeRegisters.FirstOrDefault(x => x.Id == id);

            if (timeRegister is null) return NotFound();

            timeRegister.Date = model.Date;
            timeRegister.NumberOfMinutes = model.NumberOfMinutes;
            timeRegister.Description = model.Description;

            _context.Entry(timeRegister).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
