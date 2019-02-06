using System;

namespace PortalDSEMonitorizacao.Models
{
    public class DisponibilidadeExtranets
    {
        public DisponibilidadeExtranets(String periodo, String numeroExtranets, String disponibilidade)
        {
            this.Periodo = periodo;
            this.Disponibilidade = disponibilidade;
            this.NumeroExtranets = numeroExtranets;
        }

        public DisponibilidadeExtranets()
        {
        }

        public String Periodo { set; get;}
        public String NumeroExtranets { set; get; }
        public String Disponibilidade { set; get;}

    }
}