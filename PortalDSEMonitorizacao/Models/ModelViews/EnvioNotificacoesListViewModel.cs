using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace PortalDSEMonitorizacao.Models
{
    public class EnvioNotificacoesListViewModel
    {
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<Contacto> Contactos { get; set; }
        public IEnumerable<String> SelectedOptions { set; get; }
        public string nomeTemplate { get; set; }
        public string mensagem { get; set; }
        public IEnumerable<HistoricoNotificacao> historico { get; set; }
    }
}