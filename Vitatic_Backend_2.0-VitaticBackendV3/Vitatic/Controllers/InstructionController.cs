using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitatic.DataAccess;
using Vitatic.DTO.Request;
using Vitatic.Entities;

namespace Vitatic.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InstructionController : ControllerBase
    {
        private readonly VitaticDbContext _context;

        public InstructionController(VitaticDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DtoInstruction request)
        {
            var entity = new Instruction
            {
                InstructionDetail = request.InstructionDetail,
                Status = true
            };

            _context.Instructions.Add(entity);
            await _context.SaveChangesAsync();

            HttpContext.Response.Headers.Add("Location", $"api/instruction/{entity.Id}");

            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Instruction>>> Get()
        {
            ICollection<Instruction> response;
            response = await _context.Instructions.ToListAsync();
            return Ok(response);
        }

    }


}
