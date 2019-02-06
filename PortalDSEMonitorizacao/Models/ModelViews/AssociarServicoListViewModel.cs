using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class AssociarServicoListViewModel
    {
        public IEnumerable<Provedor> provedoras { get; set; }
        public ServicoMobile servicoMobile { get; set; }
         
    }
}