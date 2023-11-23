using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace APP.Repository;
public class OficinaRepository : GenericRepositoryVarchar<Oficina>, IOficina
{
    private readonly FiltroContext _context;

    public OficinaRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetOficinasfrutales()
    {
        var oficinas = await _context.Oficinas
        .Where(oficinas => !oficinas.Empleados.Any(clientes => clientes.Clientes.Any(clientes => clientes.Pedidos.Any(pedidos => pedidos.DetallePedidos.Any(detallePedido => detallePedido.CodigoProductoNavigation.GamaNavigation.Id == "Frutales")))) ).ToListAsync();
        
        return oficinas;
    }
}