using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class GamaProductoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GamaProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
    {
        var result = await _unitOfWork.GamaProductos.GetAllAsync();
        return _mapper.Map<List<GamaProductoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GamaProductoDto>> Get(string id)
    {
        var result = await _unitOfWork.GamaProductos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<GamaProductoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> Post([FromBody] GamaProductoDto GamaProductoDto)
    {
        var result = _mapper.Map<GamaProducto>(GamaProductoDto);
        _unitOfWork.GamaProductos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        GamaProductoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = GamaProductoDto.Id }, GamaProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GamaProductoDto>> Put(string id, [FromBody] GamaProductoDto GamaProductoDto)
    {
        if (GamaProductoDto == null)
            return BadRequest();
        if (GamaProductoDto.Id == "")
            GamaProductoDto.Id = id;
        if (GamaProductoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<GamaProducto>(GamaProductoDto);
        _unitOfWork.GamaProductos.Update(result);
        await _unitOfWork.SaveAsync();
        return GamaProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.GamaProductos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.GamaProductos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}