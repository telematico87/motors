﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class TipoCambio : BaseEntity
    {
        public decimal Venta { get; set; }
        public decimal Compra { get; set; }
        public DateTime Fecha { get; set; }
    }
}
