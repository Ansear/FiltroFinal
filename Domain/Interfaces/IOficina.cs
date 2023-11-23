using Domain.Entities;

namespace Domain.Interfaces;
public interface IOficina : IGenericRepositoryVarchar<Oficina>
{
  Task<IEnumerable<object>> GetOficinasfrutales();
}