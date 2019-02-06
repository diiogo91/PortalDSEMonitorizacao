using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class AplicacoesListViewModel
    {
        public IEnumerable<Aplicacao> Aplicacoes { get; set; }
        public Boolean temPermissao { get; set; }
    }
}