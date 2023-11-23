using Domain.Entities;

namespace Domain.Interfaces;
public interface IPedido : IGenericRepositoryInt<Pedido>
{
    Task<IEnumerable<Object>> PedidoDespuesFecha();
}