using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaExtranet
    {
        public EstatisticaExtranet(String id, String nome, String disponibilidade, String indisponibilidade, String sensor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Disponibilidade = disponibilidade;
            this.Indisponibilidade = indisponibilidade;
            this.Sensor = sensor;
        }

        public EstatisticaExtranet()
        {
            
            
        }

        public String Id { set; get; }
        public String Nome { set; get;}
        public String Sensor { set; get; }
        public String Disponibilidade { set; get;}
        public String Indisponibilidade{ set; get;}
       
       
    }
}