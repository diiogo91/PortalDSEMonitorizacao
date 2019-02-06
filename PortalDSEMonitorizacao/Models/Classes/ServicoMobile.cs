using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ServicoMobile
    {
        public ServicoMobile(ObjectId id, String nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
        public ServicoMobile()
        {
        }

        public ObjectId Id { set; get;}
        public String Nome { set; get;}
    }
}