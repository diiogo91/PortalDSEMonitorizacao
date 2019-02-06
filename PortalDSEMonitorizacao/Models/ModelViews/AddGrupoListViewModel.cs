using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class AddGrupoListViewModel
    {


        public IEnumerable<Equipa> equipas { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
    }
}