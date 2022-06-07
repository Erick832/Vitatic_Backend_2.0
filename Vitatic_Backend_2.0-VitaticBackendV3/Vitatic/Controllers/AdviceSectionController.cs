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
    public class AdviceSectionController : ControllerBase
    {
        private readonly VitaticDbContext _context;
        public AdviceSectionController(VitaticDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseGeneric<ICollection<AdviceSection>>>> Get()
        {
            var response = new BaseResponseGeneric<ICollection<AdviceSection>>();

            try
            {
                response.Result = await _context.AdviceSections.ToListAsync();
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult> Post(DtoAdviceSection request)
        {
            ICollection<Advice> response;
            response = await _context.Advices.Where(a => a.Category == request.Category).ToListAsync();
            return Ok(response);
        }
    }
}
