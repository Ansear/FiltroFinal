using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class DetallePedidoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DetallePedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var result = await _unitOfWork.DetallePedidos.GetAllAsync();
        return _mapper.Map<List<DetallePedidoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Get(int id)
    {
        var result = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<DetallePedidoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Post([FromBody] DetallePedidoDto DetallePedidoDto)
    {
        var result = _mapper.Map<DetallePedido>(DetallePedidoDto);
        _unitOfWork.DetallePedidos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        DetallePedidoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = DetallePedidoDto.Id }, DetallePedidoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto DetallePedidoDto)
    {
        if (DetallePedidoDto == null)
            return BadRequest();
        if (DetallePedidoDto.Id == 0)
            DetallePedidoDto.Id = id;
        if (DetallePedidoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<DetallePedido>(DetallePedidoDto);
        _unitOfWork.DetallePedidos.Update(result);
        await _unitOfWork.SaveAsync();
        return DetallePedidoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.DetallePedidos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}