using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaAplicacaoViewModel
    {
        public IEnumerable<EstatisticaAplicacao> EstatisticasAplicacoes { get; set; }
            public String numeroAplicacoes { get; set; }
                 public String sladefinidoD { get; set; }
                    public String sladefinidoP { get; set; }
                        public String disponibilidadeGeral { get; set; }
                            public String performanceGeral { get; set; }
                                public String periodoEspecificado { get; set; }
    }

}