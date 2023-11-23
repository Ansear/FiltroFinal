using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.Repository;
public class ProductoRepository : GenericRepositoryVarchar<Producto>, IProducto
{
    private readonly FiltroContext _context;

    public ProductoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }
}