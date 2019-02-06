using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class AddTemplateListViewModel
    {

        public IEnumerable<String> servicos { get; set; }
        public IEnumerable<Provedor> entidades { get; set; }
        public IEnumerable<Equipa> equipas { get; set; }
        public IEnumerable<Contacto> Contactos { get; set; }
        public IEnumerable<Grupo> grupos { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public IEnumerable<String> SelectedGruposOptions { set; get; }
        public IEnumerable<String> SelectedEquipasOptions { set; get; }
        public String selectedEntidade { get; set; }
    }
}