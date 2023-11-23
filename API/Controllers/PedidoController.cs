using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Controllers;
public class PedidoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly FiltroContext _context;

    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper, FiltroContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
    {
        var result = await _unitOfWork.Pedidos.GetAllAsync();
        return _mapper.Map<List<PedidoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoDto>> Get(int id)
    {
        var result = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<PedidoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Post([FromBody] PedidoDto PedidoDto)
    {
        var result = _mapper.Map<Pedido>(PedidoDto);
        _unitOfWork.Pedidos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        PedidoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = PedidoDto.Id }, PedidoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto PedidoDto)
    {
        if (PedidoDto == null)
            return BadRequest();
        if (PedidoDto.Id == 0)
            PedidoDto.Id = id;
        if (PedidoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Pedido>(PedidoDto);
        _unitOfWork.Pedidos.Update(result);
        await _unitOfWork.SaveAsync();
        return PedidoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Pedidos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("ProductosDespuesFecha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductosDespuesFecha()
    {
        var result = await _unitOfWork.Pedidos.PedidoDespuesFecha();
        if (result == null)
            return NotFound();
        return Ok(result);

    }

}