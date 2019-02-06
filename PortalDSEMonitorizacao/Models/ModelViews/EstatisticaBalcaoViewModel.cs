using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaBalcaoViewModel
    {

     public IEnumerable<EstatisticaBalcao> TopIndisponiveis { get; set; }
        public IEnumerable<EstatisticaBalcao> EstatisticasBalcoes { get; set; }
            public String numeroBalcoes { get; set; }
                 public String sladefinido { get; set; }
                     public String disponibilidadeGeral { get; set; }
                         public String periodoEspecificado { get; set; }
    }

}