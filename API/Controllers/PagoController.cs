using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class PagoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
    {
        var result = await _unitOfWork.Pagos.GetAllAsync();
        return _mapper.Map<List<PagoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagoDto>> Get(int id)
    {
        var result = await _unitOfWork.Pagos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<PagoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Post([FromBody] PagoDto PagoDto)
    {
        var result = _mapper.Map<Pago>(PagoDto);
        _unitOfWork.Pagos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        PagoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = PagoDto.Id }, PagoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto PagoDto)
    {
        if (PagoDto == null)
            return BadRequest();
        if (PagoDto.Id == 0)
            PagoDto.Id = id;
        if (PagoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Pago>(PagoDto);
        _unitOfWork.Pagos.Update(result);
        await _unitOfWork.SaveAsync();
        return PagoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Pagos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Pagos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}