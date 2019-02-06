using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EditSondaListViewModel
    {
        public Sonda Sonda { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public IEnumerable<Credencial> credenciais { set; get; }
    }
}