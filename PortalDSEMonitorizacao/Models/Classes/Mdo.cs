using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Mdo
    {
        public Mdo(String idProvedor, String idServico, String disponibilidade)
        {
            this.IDProvedor = idProvedor;
            this.IDServico = idServico;
            this.Disponibilidade = disponibilidade;
        }

        public Mdo()
        {
            
            
        }

        public String IDProvedor { set; get;}
        public String IDServico { set; get; }
        public String Disponibilidade { set; get;}

    }
}