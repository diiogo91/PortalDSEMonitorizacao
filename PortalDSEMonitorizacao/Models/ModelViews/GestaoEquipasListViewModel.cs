using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class GestaoEquipasListViewModel
    {
        public IEnumerable<Equipa> equipas { get; set; }
        public IEnumerable<Grupo> grupos { get; set; }
        public IEnumerable<Contacto> membrosdeequipa { get; set; }
    }
}