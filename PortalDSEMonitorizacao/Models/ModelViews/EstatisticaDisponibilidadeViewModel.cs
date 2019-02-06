using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaDsiponibilidadeViewModel
    {

        public IEnumerable<DisponibilidadeMobile> DisponibilidadeMobileList { get; set; }
        public IEnumerable<DisponibilidadeMobile> IndisponibilidadesList { get; set; }
        public IEnumerable<Mdo> MdoList { get; set; }
        public IEnumerable<Mds> MdsList { get; set; }
        public IEnumerable<Associacao> Associacoes { get; set; }
        public IEnumerable<ServicoMobile> ServicosMobile { get; set; }
        public IEnumerable<Provedor> Provedores { get; set; }
        public IDictionary<String,String> cores { get; set; }
        public String periodoEspecificado { get; set; }
        public String tempoIndsTotal {get;set;} 
    }

}