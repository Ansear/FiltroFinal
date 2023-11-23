using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GamaProductoDto : BaseEntityVarcharDto
    {
        public string DescripcionTexto { get; set; }

        public string DescripcionHtml { get; set; }

        public string Imagen { get; set; }
    }
}