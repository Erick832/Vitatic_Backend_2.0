﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitatic.DataAccess;
using Vitatic.DTO.Request;
using Vitatic.Entities;

namespace Vitatic.Controllers;
[ApiController]
[Route("api[Controller]")]
public class ProgressController : ControllerBase
{
    private readonly VitaticDbContext _context;
    public ProgressController(VitaticDbContext Context)
    {
        _context = Context;
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Progress>> PutProgrees(int id, DtoProgress request)
    {
        var entity = await _context.Progresses.FindAsync(id);
        if (entity == null) return NotFound("Actividad no encontrada");
        entity.Points = request.Points;
        entity.Repetitions = request.Repetitions;
        entity.Category = request.Category;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Progress>>> GetAllProgress()
    {
        ICollection<Progress> response;
        response = await _context.Progresses.ToListAsync();
        return Ok(response);
    }
    [HttpGet("categoria")]
    public async Task<ActionResult<ICollection<int>>> GetSuma(string categoria)
    {
        int suma = 0;
        ICollection<Progress> response;
        response = await _context.Progresses.Where(a=>a.Category==categoria).ToListAsync();
        foreach (var progress in response)
        {
            suma += progress.Points;
        }
        return Ok(suma);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Progress>> GetProgressById(int id)
    {
        var progress = await _context.Progresses.FindAsync(id);
        if (progress == null)
        {
            return NotFound("No se encontró el progreso");
        }
        return Ok(progress);
    }

}
