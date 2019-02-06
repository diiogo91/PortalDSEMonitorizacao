using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Provedor
    {
        public Provedor(ObjectId id, String nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public Provedor()
        {


        }

        public ObjectId Id { set; get; }
        public String Nome { set; get; }
     
    }
}