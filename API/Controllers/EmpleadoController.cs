using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Controllers;
public class EmpleadoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly FiltroContext _context;

    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper, FiltroContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var result = await _unitOfWork.Empleados.GetAllAsync();
        return _mapper.Map<List<EmpleadoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpleadoDto>> Get(int id)
    {
        var result = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<EmpleadoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Post([FromBody] EmpleadoDto EmpleadoDto)
    {
        var result = _mapper.Map<Empleado>(EmpleadoDto);
        _unitOfWork.Empleados.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        EmpleadoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = EmpleadoDto.Id }, EmpleadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto EmpleadoDto)
    {
        if (EmpleadoDto == null)
            return BadRequest();
        if (EmpleadoDto.Id == 0)
            EmpleadoDto.Id = id;
        if (EmpleadoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Empleado>(EmpleadoDto);
        _unitOfWork.Empleados.Update(result);
        await _unitOfWork.SaveAsync();
        return EmpleadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Empleados.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}