using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaEMC
    {
        public EstatisticaEMC(String state, String name, String total, String free, String used, String subscribed)
        {
            this.State = state;
            this.Name = name;
            this.TotalCapacityTB = total;
            this.FreeCapacityTB = free;
            this.Used = used;
            this.Subscribed = subscribed;
        }

        public EstatisticaEMC()
        {
            
            
        }

        public String Name { set; get;}
        public String State{ set; get;}
        public String TotalCapacityTB { set; get; }
        public String FreeCapacityTB { set; get; }
        public String Used { set; get; }
        public String Subscribed { set; get; }

    }
}