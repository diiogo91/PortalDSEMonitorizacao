using System;

namespace PortalDSEMonitorizacao.Models
{
    public class PowershellModel
    {
        public PowershellModel (String error, String output)
        {
            this.Error = error;
            this.Output = output;            
        }
        public PowershellModel()
        {
        }
        public String Error { set; get;}
        public String Output { set; get;}
    }
}