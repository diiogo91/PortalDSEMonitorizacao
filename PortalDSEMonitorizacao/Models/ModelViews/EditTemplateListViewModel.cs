using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EditTemplateListViewModel
    {
        public Template template { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public IEnumerable<Contacto> Contactos { set; get; }
    }
}