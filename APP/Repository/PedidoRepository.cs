using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace APP.Repository;
public class PedidoRepository : GenericRepositoryInt<Pedido>, IPedido
{
    private readonly FiltroContext _context;

    public PedidoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<object>> PedidoDespuesFecha()
    {
    
        var pedidos = await _context.Pedidos
        .Where(p => p.FechaEntrega > p.FechaEsperada )
        .OrderBy(p => p.FechaEsperada)
        .Select(p => new { p.Id, p.CodigoCliente, p.FechaEsperada, p.FechaEntrega })
        .ToListAsync();
        return pedidos;
    }

}