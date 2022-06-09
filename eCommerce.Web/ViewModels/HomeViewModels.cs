﻿using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.ViewModels
{
    public class HomeSlidersViewModel
    {
        public List<Configuration> SlidersConfigurations { get; set; }
    }

    public class HomeMarcasMoto
    {
        public List<Marca> Marcas { get; set; }
    }  

}