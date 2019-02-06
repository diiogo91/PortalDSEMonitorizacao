using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class LogActivityViewModel
    {
        public IEnumerable<LogActivity> Logs { get; set; }
        public Boolean temPermissao { get; set; }
    }
}