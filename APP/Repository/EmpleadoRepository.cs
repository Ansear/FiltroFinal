using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.Repository;
public class EmpleadoRepository : GenericRepositoryInt<Empleado>, IEmpleado
{
    private readonly FiltroContext _context;

    public EmpleadoRepository(FiltroContext context) : base(context)
    {
        _context = context;
    }

    public Task<IEnumerable<object>> Repre()
    {
        throw new NotImplementedException();
    }
}