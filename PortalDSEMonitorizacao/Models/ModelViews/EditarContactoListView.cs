using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EditarContactoListView
    {
        public IEnumerable<Equipa> equipas { get; set; }
        public Contacto contacto { get; set; }
        public string associar { get; set; }
    }
}