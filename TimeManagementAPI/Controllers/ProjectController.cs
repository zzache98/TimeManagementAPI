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
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //<-------Uppdatera------->

        [HttpGet]
        [ProducesResponseType(typeof(ProjectDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ProjectDTO>> Get()
            => await _context.Projects.Select(x => new ProjectDTO { Id = x.Id, ProjectName = x.ProjectName }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project is null) return NotFound();
            var projectDto = new ProjectDTO
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
            };

            return Ok(projectDto);
        }

        //<-------Skapa------->

        public class ProjectCreateModel
        {
           
            
            [Required]
            [MaxLength(50)]
            public string ProjectName { get; set; }
            
            [Required]
            public int CustomerId { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ProjectCreateModel model)
        {
            var customer = await _context.Customers.FindAsync(model.CustomerId);

            if (customer is null) return NotFound("Customer does not exist");

            var project = new Project
            {
                ProjectName = model.ProjectName,
                Customer = customer,
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var projectDto = new ProjectDTO
            {
                ProjectName = project.ProjectName,
                Id = project.Id,
            };

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, projectDto);
        }

        //<-------Uppdatera------->
        public class ProjectEditModel
        {
            [Required]
            [MaxLength(50)]
            public string ProjectName { get; set; }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ProjectEditModel model)
        {
            var project = _context.Projects.FirstOrDefault(x => x.Id == id);

            if (project is null) return NotFound();

            project.ProjectName = model.ProjectName;

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
