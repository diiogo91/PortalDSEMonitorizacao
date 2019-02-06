using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaBalcao
    {
        public EstatisticaBalcao(String id, String nome, String provincia, String disponibilidade, String indisponibilidade, String sensor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Disponibilidade = disponibilidade;
            this.Indisponibilidade = indisponibilidade;
            this.Sensor = sensor;
            this.Provincia = provincia;
        }

        public EstatisticaBalcao()
        {
            
            
        }

        public String Id { set; get; }
        public String Nome { set; get;}
        public String Sensor { set; get; }
        public String Provincia { set; get; }
        public String Disponibilidade { set; get;}
        public String Indisponibilidade{ set; get;}
       
       
    }
}