using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Mds
    {
        public Mds(String idProvedor, String idServico, String disponibilidade)
        {
            this.IDProvedor = idProvedor;
            this.IDServico = idServico;
            this.Disponibilidade = disponibilidade;
        }

        public Mds()
        {
            
            
        }

        public String IDProvedor { set; get;}
        public String IDServico { set; get; }
        public String Disponibilidade { set; get;}

    }
}