using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class HistoricoNotificacao
    {
        public HistoricoNotificacao(ObjectId id, DateTime dataNotif, String ViaNotif, List<Contacto> contactosNotif, String mensagem, String user)
        {
            this.Id = id;
            this.DataNotf = dataNotif;
            this.ContactosNotif = contactosNotif;
            this.mensagem = mensagem;
            this.ViaNotif = ViaNotif;
            this.User = user;
        }

        public HistoricoNotificacao()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public String ViaNotif { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataNotf { set; get; }
        public List<Contacto> ContactosNotif { set; get;}
        public String mensagem { set; get; }
        public String User { set; get; }
    }
}