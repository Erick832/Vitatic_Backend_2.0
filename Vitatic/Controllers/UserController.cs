using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitatic.DataAccess;
using Vitatic.DTO.Request;
using Vitatic.DTO.Response;
using Vitatic.Entities;

namespace Vitatic.Controllers;
[ApiController]
[Route("api[Controller]")]
public class UserController:ControllerBase
{
    private readonly VitaticDbContext _context;
    public UserController(VitaticDbContext Context)
    {
        _context = Context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> Get()
    {
        ICollection<User> response;
        response = await _context.Users.ToListAsync();
        return Ok(response);
    }

    
    [HttpPost]
    public async Task<ActionResult> Post(DtoUser request)
    {
        if (request.Name=="" || request.Password=="" ||request.Password=="")
        {
            return BadRequest("Error Todos los datos deben de estar llenos");
        };
        if (!request.Email.Contains(".com") || !request.Email.Contains("@"))
        {
            return BadRequest("Error El correo no es válido");
        }
        foreach (User user in _context.Users)
        {
            if (user.Email == request.Email)
            {
                return BadRequest("Error este correo ya ha sido registrado por un usuario");
            }
        }
        if (request.Password.Length < 9)
        {
            return BadRequest("Error la contraseña debe de tener más de 8 caracteres");
        }

        var entity = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Status = true,
        };
        Interface Interface = new Interface();
        Interface.UserId=entity.Id;
        Interface.Status = true;
        entity.Interface = Interface;

        Schedule schedule = new Schedule();
        schedule.InterfaceId = entity.Id;
        schedule.Status = true;
        Interface.Schedule = schedule;

        AdviceSection advicesection = new AdviceSection();
        advicesection.InterfaceId = entity.Id;
        advicesection.Status = true;
        advicesection.Category = "Categoria";
        Interface.AdviceSection = advicesection;

        /*Instruction instruction = new Instruction();
        instruction.InterfaceId = entity.Id;
        instruction.Status = true;
        Interface.Instruction = instruction;*/

        _context.Users.Add(entity);
        _context.Interfaces.Add(entity.Interface);
        _context.Schedules.Add(Interface.Schedule);
        _context.AdviceSections.Add(Interface.AdviceSection);
        //_context.Instructions.Add(Interface.Instruction);

        await _context.SaveChangesAsync();
        HttpContext.Response.Headers.Add("location", $"/api/users/{entity.Id}");
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }
        return Ok(entity);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, DtoUser request)
    {
        var entity = await _context.Users.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Name = request.Name;
        entity.Email = request.Email;
        entity.Password = request.Password;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new{Id = id});
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        var entity = await _context.Users.FindAsync(id);

        if (entity == null)
        {
            return NotFound($"User with Id = {id} not found");
        }
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return Ok();
    }
}
