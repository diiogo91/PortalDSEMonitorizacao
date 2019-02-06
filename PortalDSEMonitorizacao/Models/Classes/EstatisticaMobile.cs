using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaMobile
    {
        public EstatisticaMobile(String dia, String numerosms, String tiposms, String operadora, String canal)
        {
            this.Dia = dia;
            this.NumeroSMS = numerosms;
            this.TipoSMS = tiposms;
            this.Operadora = operadora;
            this.Canal = canal;
        }

        public EstatisticaMobile()
        {
            
            
        }

        public String Dia { set; get;}
        public String NumeroSMS { set; get;}
        public String TipoSMS{ set; get;}
        public String Operadora{ set; get;}
        public String Canal{ set; get;}
       
    }
}