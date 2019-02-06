using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class RegistrarIndisponibilidadeViewModel
    {
        public IEnumerable<Provedor> provedoras { get; set; }
        public IEnumerable<ServicoMobile> servicosMobile { get; set; }
        public IEnumerable<Aplicacao> aplicacoes { get; set; }
    }
}