# FiltroFinal

## Consultas

### 1
    - Devuelve un listado con el código de pedido, código de cliente, fecha 
    esperada y fecha de entrega de los pedidos que no han sido entregados a 
    tiempo.

    -URL: /api/Pedido/ProductosDespuesFecha

    -Codigo: public async Task<IEnumerable<object>> PedidoDespuesFecha()
    {
    
        var pedidos = await _context.Pedidos
        .Where(p => p.FechaEntrega > p.FechaEsperada)
        .OrderBy(p => p.FechaEsperada)
        .Select(p => new { p.Id, p.CodigoCliente, p.FechaEsperada, p.FechaEntrega })
        .ToListAsync();
        return pedidos;
    }
    Explicacion: se toma la tabla pedidos y se hace la validacion si la fecha de entrega del pedido es mayor a la esperada, despues se crea un nuevo objeto con los datos solicitados.
### 2
    - Devuelve nombre de los clientes que no han hecho pagos 

    -URL: /api/Cliente/Sinpagos

    -Codigo: 
        var clientes = await _context.Clientes.Join(
                              _context.Empleados,  
                              cliente => cliente.CodigoEmpleadoRepVentas ,
                              empleado => empleado.Id,
                              (cliente,empleado) => new {Cliente = cliente.NombreCliente, RepresentanteVentas = $"{empleado.Nombre} {empleado.Apellido1} {empleado.Apellido1}", CodigoOficina = empleado.CodigoOficinaNavigation.Ciudad})
                              .GroupJoin(_context.Pagos,
                              ce => ce.Cliente, 
                              pagos => pagos.IdNavigation.NombreCliente,
                              (ce,pagos) => new {ce.Cliente, ce.RepresentanteVentas, ce.CodigoOficina, EstadoPago = pagos.Any() ? "Realizo Pago" : "No hay pago"})
                              .Where(ce => ce.EstadoPago == "Realizo Pago").ToListAsync();
                                    
        return clientes;
    Explicacion: Se hace el join de clientes con empleados se crea el objeto con new, despues cliente se une con Pagos, con un operador ternario se realiza asigna el string  y por ultimo se saca hace la condicion.

### 3
    - Devuelve nombre de los clientes que no han hecho pagos 

    -URL: /api/Oficina/frutales

    -Codigo: 
        public async Task<IEnumerable<object>> GetOficinasfrutales()
    {
        var oficinas = await _context.Oficinas
        .Where(oficinas => !oficinas.Empleados.Any(clientes => clientes.Clientes.Any(clientes => clientes.Pedidos.Any(pedidos => pedidos.DetallePedidos.Any(detallePedido => detallePedido.CodigoProductoNavigation.GamaNavigation.Id == "Frutales")))) ).ToListAsync();
        
        return oficinas;
    }
                                    
        return clientes;
    Explicacion: Con el metodo any se valida que  no existan o no este, para poder hacer la condicion.