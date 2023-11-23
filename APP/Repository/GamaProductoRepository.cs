using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.Repository;
public class GamaProductoRepository : GenericRepositoryVarchar<GamaProducto>, IGamaProducto
{
    private readonly FiltroContext _context;

    public GamaProductoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }
}