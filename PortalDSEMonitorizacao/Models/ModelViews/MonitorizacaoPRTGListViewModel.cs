using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class MonitorizacaoPRTGListViewModel
    {
        public IEnumerable<Computer> servidoresSC { get; set; }
        public IEnumerable<Computer> servidoresBC { get; set; }
    }
}