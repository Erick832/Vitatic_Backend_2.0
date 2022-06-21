using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitatic.DataAccess;
using Vitatic.DTO.Request;
using Vitatic.Entities;

namespace Vitatic.Controllers;

[ApiController]
[Route("api[Controller]")]
public class ScheduleController : ControllerBase
{
    private readonly VitaticDbContext _context;
    public ScheduleController(VitaticDbContext Context)
    {
        _context = Context;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutActivity(int id, DtoActivity request)
    {
        var entitySchedule = await _context.Schedules.FindAsync(id);
        if (entitySchedule == null)
        {
            return BadRequest("Usuario no encontrado");
        }
        var entity = new Activity
        {
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            Priority = request.Priority,
            Date = request.Date,
            Minutes = request.Minutes,
            ScheduleId = entitySchedule.Id,
            Status = true
        };
        //
        Progress progress = new Progress();
        progress.ScheduleId = entity.ScheduleId;
        progress.Status = true;
        progress.Points = request.Points;
        entity.Progress = progress;
        //
        _context.Entry(entitySchedule).State = EntityState.Modified;
        _context.Activities.Add(entity);
        //
        _context.Progresses.Add(progress);
        //
        await _context.SaveChangesAsync();
        HttpContext.Response.Headers.Add("location", $"/api/activities/{entity.Id}");
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> PutPriority(DtoPriority request)
    {

        var Activity = await _context.Activities.FindAsync(request.ActivityId);
        if (Activity == null)
        {
            return BadRequest("Actividad no encontrada");
        }

        if (request.Priority != null) { Activity.Priority = request.Priority; }
        if (request.Category != null) { Activity.Category = request.Category; }

        _context.Entry(Activity).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        HttpContext.Response.Headers.Add("location", $"/api/activities/{Activity.Id}");
        return Ok();
    }


    [HttpGet]
    public async Task<ActionResult<ICollection<Schedule>>> Get()
    {
        ICollection<Schedule> response;
        response = await _context.Schedules.ToListAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ICollection<Activity>>> GetActivities(int id)
    {
        ICollection<Activity> response;
        response = await _context.Activities.Where(a => a.ScheduleId == id).ToListAsync();
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Activity>> DeleteActivity(int id)
    {
        var entity = await _context.Activities.FindAsync(id);

        if (entity == null)
        {
            return NotFound($"Activity with Id = {id} not found");
        }
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return Ok();
    }
}


