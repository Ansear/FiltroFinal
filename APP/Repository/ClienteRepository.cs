using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace APP.Repository;
public class ClienteRepository : GenericRepositoryInt<Cliente>, ICliente
{
    private readonly FiltroContext _context;

    public ClienteRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> NoPagos()
    {

        var clientes = await _context.Clientes.Join(
                              _context.Empleados,  
                              cliente => cliente.CodigoEmpleadoRepVentas ,
                              empleado => empleado.Id,
                              (cliente,empleado) => new {Cliente = cliente.NombreCliente, RepresentanteVentas = $"{empleado.Nombre} {empleado.Apellido1} {empleado.Apellido1}", CodigoOficina = empleado.CodigoOficinaNavigation.Ciudad})
                              .GroupJoin(_context.Pagos,
                              ce => ce.Cliente, 
                              pagos => pagos.IdNavigation.NombreCliente,
                              (ce,pagos) => new {ce.Cliente, ce.RepresentanteVentas, ce.CodigoOficina, EstadoPago = pagos.Any() ? "Realizo Pago" : "No hay pago"})
                              .Where(ce => ce.EstadoPago == "Realizo Pago").ToListAsync();
                                    
        return clientes;
    }
}