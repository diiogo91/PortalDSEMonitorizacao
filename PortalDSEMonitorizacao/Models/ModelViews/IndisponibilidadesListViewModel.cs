using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class IndisponibilidadesListViewModel
    {
        public IEnumerable<Indisponibilidade> inidsponibilidadesPendentes { get; set; }
        public IEnumerable<Indisponibilidade> inidsponibilidadesResolvidas { get; set; }
    }
}