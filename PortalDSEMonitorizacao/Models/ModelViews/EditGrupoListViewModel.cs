using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EditGrupoListViewModel
    {
        public Grupo grupo { get; set; }
        public IEnumerable<Equipa> equipas { set; get; }
        public IEnumerable<String> SelectedOptions { set; get; }
    }
}