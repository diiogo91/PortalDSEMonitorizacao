using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Contacto
    {
        public Contacto(ObjectId id, String nome, String equipa, String telefone, String email, ObjectId IDEquipa)
        {
            this.Id = id;
            this.Nome = nome;
            this.Equipa = equipa;
            this.Telefone = telefone;
            this.Email = email;
            this.IDEquipa = IDEquipa;
        }
        public Contacto()
        {   
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId  Id { get; set; }
        public String Nome { set; get;}
        public String Equipa{ set; get;}
        public ObjectId IDEquipa { set; get; }
        public String Telefone{ set; get;}
        public String Email{ set; get;}
    }
}