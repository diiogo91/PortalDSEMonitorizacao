using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaAplicacao
    {
        public EstatisticaAplicacao(String id, String nome,  String disponibilidade, String performance)
        {
            this.Id = id;
            this.Nome = nome;
            this.Disponibilidade = disponibilidade;
            this.Performance = performance;
        }

        public EstatisticaAplicacao()
        {
            
            
        }

        public String Id { set; get; }
        public String Nome { set; get;}
        public String Disponibilidade { set; get;}
        public String Performance { set; get;}
       
       
    }
}