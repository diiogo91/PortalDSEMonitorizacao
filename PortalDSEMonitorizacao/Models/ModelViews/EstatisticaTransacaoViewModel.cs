using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaTransacaoViewModel
    {

        public IEnumerable<Transacao> transacoesComsucesso { get; set; }
        public IEnumerable<Transacao> transacoesSemsucesso { get; set; }
        public String periodoEspecificado { get; set; }
    }

}