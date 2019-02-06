using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class GestaoServicosMobileListViewModel
    {
        public IEnumerable<Associacao> Associacoes { get; set; }
        public IEnumerable<Provedor> Provedores { get; set; }
        public IEnumerable<ServicoMobile> ServicosMobile { get; set; }

    }
}