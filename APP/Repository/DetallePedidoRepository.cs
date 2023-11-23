using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.Repository;
public class DetallePedidoRepository : GenericRepositoryInt<DetallePedido>, IDetallePedido
{
    private readonly FiltroContext _context;

    public DetallePedidoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }
}