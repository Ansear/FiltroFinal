using Domain.Entities;

namespace Domain.Interfaces;
public interface ICliente : IGenericRepositoryInt<Cliente>
{
    Task<IEnumerable<Object>> NoPagos();
}