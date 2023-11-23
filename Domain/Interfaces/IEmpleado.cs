using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmpleado : IGenericRepositoryInt<Empleado>
{
    Task<IEnumerable<Object>> Repre();
}