using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class AddSondaListViewModel
    {
        public IEnumerable<Credencial> credenciais { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
    }
}