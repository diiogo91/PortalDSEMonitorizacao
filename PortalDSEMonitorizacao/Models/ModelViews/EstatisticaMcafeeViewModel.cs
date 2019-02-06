using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaMcafeeViewModel
    {

     public IEnumerable<Mcafee> balcoes { get; set; }
        public IEnumerable<Mcafee> servicosCentrais { get; set; }
        public String periodoEspecificado { get; set; }
            
    }

}