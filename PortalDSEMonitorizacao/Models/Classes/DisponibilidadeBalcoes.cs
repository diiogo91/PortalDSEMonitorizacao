using System;

namespace PortalDSEMonitorizacao.Models
{
    public class DisponibilidadeBalcoes
    {
        public DisponibilidadeBalcoes(String periodo, String numeroBalcoes, String disponibilidade)
        {
            this.Periodo = periodo;
            this.Disponibilidade = disponibilidade;
            this.NumeroBalcoes = numeroBalcoes;
        }

        public DisponibilidadeBalcoes()
        {
            
            
        }

        public String Periodo { set; get;}
        public String NumeroBalcoes { set; get; }
        public String Disponibilidade { set; get;}

    }
}