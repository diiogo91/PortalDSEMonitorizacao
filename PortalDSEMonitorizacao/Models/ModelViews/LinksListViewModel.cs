using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class LinksListViewModel
    {
        public IEnumerable<Link> Links { get; set; }
        public Boolean temPermissao { get; set; }
    }
}