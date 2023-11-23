using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class ClienteController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var result = await _unitOfWork.Clientes.GetAllAsync();
        return _mapper.Map<List<ClienteDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var result = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<ClienteDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Post([FromBody] ClienteDto ClienteDto)
    {
        var result = _mapper.Map<Cliente>(ClienteDto);
        _unitOfWork.Clientes.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        ClienteDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = ClienteDto.Id }, ClienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto ClienteDto)
    {
        if (ClienteDto == null)
            return BadRequest();
        if (ClienteDto.Id == 0)
            ClienteDto.Id = id;
        if (ClienteDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Cliente>(ClienteDto);
        _unitOfWork.Clientes.Update(result);
        await _unitOfWork.SaveAsync();
        return ClienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Clientes.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("Sinpagos")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSinPagos()
    {
        var result = await _unitOfWork.Clientes.NoPagos();
        if (result == null)
            return NotFound();
        return Ok(result);
    }
    
}