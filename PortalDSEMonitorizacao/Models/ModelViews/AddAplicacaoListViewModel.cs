using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class AddAplicacaoListViewModel
    {
        public IEnumerable<Credencial> credenciais { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public IEnumerable<Sonda> sondas { get; set; }
        public IEnumerable<String> SelectedOptions2 { set; get; }
    }
}