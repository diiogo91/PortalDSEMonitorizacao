using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class AddContactoListViewModel
    {
        public IEnumerable<Equipa> equipas { get; set; }
        public string associar { get; set; }
    }
}