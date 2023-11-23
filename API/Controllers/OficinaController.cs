using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class OficinaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
    {
        var result = await _unitOfWork.Oficinas.GetAllAsync();
        return _mapper.Map<List<OficinaDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OficinaDto>> Get(string id)
    {
        var result = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<OficinaDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Post([FromBody] OficinaDto OficinaDto)
    {
        var result = _mapper.Map<Oficina>(OficinaDto);
        _unitOfWork.Oficinas.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        OficinaDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = OficinaDto.Id }, OficinaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OficinaDto>> Put(string id, [FromBody] OficinaDto OficinaDto)
    {
        if (OficinaDto == null)
            return BadRequest();
        if (OficinaDto.Id == "")
            OficinaDto.Id = id;
        if (OficinaDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Oficina>(OficinaDto);
        _unitOfWork.Oficinas.Update(result);
        await _unitOfWork.SaveAsync();
        return OficinaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Oficinas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("frutales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetOficinaFrutales()
    {
        var result = await _unitOfWork.Oficinas.GetOficinasfrutales();
        return Ok(result);
    }
}