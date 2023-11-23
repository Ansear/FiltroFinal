using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers;
public class ProductoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var result = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Get(string id)
    {
        var result = await _unitOfWork.Productos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return _mapper.Map<ProductoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Post([FromBody] ProductoDto ProductoDto)
    {
        var result = _mapper.Map<Producto>(ProductoDto);
        _unitOfWork.Productos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
            return BadRequest();
        ProductoDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { Id = ProductoDto.Id }, ProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductoDto>> Put(string id, [FromBody] ProductoDto ProductoDto)
    {
        if (ProductoDto == null)
            return BadRequest();
        if (ProductoDto.Id == "")
            ProductoDto.Id = id;
        if (ProductoDto.Id != id)
            return NotFound();
        var result = _mapper.Map<Producto>(ProductoDto);
        _unitOfWork.Productos.Update(result);
        await _unitOfWork.SaveAsync();
        return ProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.Productos.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        _unitOfWork.Productos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}