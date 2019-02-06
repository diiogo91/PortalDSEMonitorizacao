using System;

namespace PortalDSEMonitorizacao.Models
{

    public class ResultadoProcessamento
    {
        public ResultadoProcessamento(String error, String output)
        {
            this.Error = error;
            this.Output = output;            
        }
        public ResultadoProcessamento()
        {
        }
        public String Error { set; get;}
        public String Output { set; get;}
    }
}