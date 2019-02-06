using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class AddCartaoSIMListViewModel
    {
        public IEnumerable<Sonda> sondas { get; set; }
        public IEnumerable<String> SelectedOptions2 { set; get; }
    }
}