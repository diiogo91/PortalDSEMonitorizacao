using System;

namespace PortalDSEMonitorizacao.Models
{
    public class LogActivity
    {
        public LogActivity(DateTime data , String user, String accao)
        {
            this.Accao = accao;
            this.Data = data;
            this.User = user;
        }

        public LogActivity()
        {
            
            
        }

       
        public DateTime Data { set; get;}
        public String User { set; get; }
        public String Accao { set; get; }
    }
}