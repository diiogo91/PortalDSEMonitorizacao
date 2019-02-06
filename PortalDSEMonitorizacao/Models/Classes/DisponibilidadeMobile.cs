using System;

namespace PortalDSEMonitorizacao.Models
{
    public class DisponibilidadeMobile
    {
        public DisponibilidadeMobile(bool indisponibilidade,Associacao associacao, Decimal disponibilidade, String disponibilidadeString, String periodoIndisponibilidade, String nrOcorrencias,String dias)
        {
            this.Associacao = associacao;
            this.Disponibilidade = disponibilidade;
            this.DisponibilidadeString = disponibilidadeString;
            this.PeriodoIndisponibilidade = periodoIndisponibilidade;
            this.Indisponibilidade = indisponibilidade;
            this.NrOcorrencias = nrOcorrencias;
            this.Dias = dias;
        }

        public DisponibilidadeMobile()
        {
            
            
        }
        public Boolean Indisponibilidade { set; get; }
        public Associacao Associacao { set; get;}
        public Decimal Disponibilidade { set; get;}
        public String DisponibilidadeString { set; get; }
        public String PeriodoIndisponibilidade { set; get; }
        public String NrOcorrencias { set; get; }
        public String Dias { set; get; }
    }
}