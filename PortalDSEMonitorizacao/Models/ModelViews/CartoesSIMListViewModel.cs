using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class CartoesSIMListViewModel
    {
        public IEnumerable<CartaoSIM> CartoesSIM { get; set; }
        public Boolean temPermissao { get; set; }
    }
}