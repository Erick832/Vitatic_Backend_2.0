using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitatic.DataAccess;
using Vitatic.DTO.Request;
using Vitatic.DTO.Response;
using Vitatic.Entities;

namespace Vitatic.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdviceController : ControllerBase
    {
        private readonly VitaticDbContext _context;
        public AdviceController(VitaticDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseGeneric<ICollection<Advice>>>> Get()
        {
            var response = new BaseResponseGeneric<ICollection<Advice>>();

            try
            {
                response.Result = await _context.Advices.ToListAsync();
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(DtoAdvice request)
        {
            var entity = new Advice
            {
                Category = request.Category,
                Content = request.Content,
                Status = true
            };

            _context.Advices.Add(entity);
            await _context.SaveChangesAsync();

            HttpContext.Response.Headers.Add("Location", $"api/advice/{entity.Id}");

            return Ok(entity);
        }
    }
}
