using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.Repository;
public class PagoRepository : GenericRepositoryInt<Pago>, IPago
{
    private readonly FiltroContext _context;

    public PagoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }
}