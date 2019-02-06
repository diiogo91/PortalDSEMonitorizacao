using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EditAplicacaoListViewModel
    {
        public Aplicacao Aplicacao { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public IEnumerable<Credencial> credenciais { set; get; }
        public IEnumerable<String> SelectedOptions2 { set; get; }
        public IEnumerable<Sonda> sondas { set; get; }
    }
}