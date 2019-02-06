using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaExtranetListViewModel
    {
        public IEnumerable<EstatisticaExtranet> EstatisticasExtranet { get; set; }
            public String numeroExtranets { get; set; }
                 public String sladefinido { get; set; }
                     public String disponibilidadeGeral { get; set; }
                         public String periodoEspecificado { get; set; }
    }

}