using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EditarIndisponibilidadeListView
    {
        public IEnumerable<ServicoMobile> servicosMobile { get; set; }
        public IEnumerable<Provedor> provedores { get; set; }
        public Indisponibilidade indisponibilidade { get; set; }
        public IEnumerable<Aplicacao> aplicacoes { get; set; }
    }
}