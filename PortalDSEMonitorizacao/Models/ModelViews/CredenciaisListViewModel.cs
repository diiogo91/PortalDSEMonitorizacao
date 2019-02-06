using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class CredenciaisListViewModel
    {
        public IEnumerable<Credencial> Credenciais { get; set; }
        public Boolean temPermissao { get; set; }
    }
}