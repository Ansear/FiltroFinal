using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Entities;

public partial class DetallePedido : BaseEntityInt
{

    public string CodigoProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }

    public virtual Producto CodigoProductoNavigation { get; set; } = null!;

    public virtual Pedido IdNavigation { get; set; } = null!;
}
