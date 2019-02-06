using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EditCartaoSIMListViewModel
    {
        public CartaoSIM cartaoSIM { get; set; }
        public IEnumerable<String> SelectedOptions2 { set; get; }
        public IEnumerable<Sonda> sondas { set; get; }
    }
}