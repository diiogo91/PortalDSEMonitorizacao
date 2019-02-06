using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class SondasListViewModel
    {
        public IEnumerable<Sonda> Sondas { get; set; }
        public Boolean temPermissao { get; set; }
    }
}