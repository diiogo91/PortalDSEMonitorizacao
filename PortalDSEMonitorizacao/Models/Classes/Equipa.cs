using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Equipa
    {
        public Equipa(ObjectId id, String nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public Equipa()
        {
        }

        public ObjectId Id { set; get;}
        public String Nome{ set; get;}
    }
}