using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class CartaoSIM
    {
        public CartaoSIM(ObjectId id, String numero, String operadora, DateTime dataRegisto, String funcao, List<Sonda> sonda)
        {
            this.Id = id;
            this.Numero = numero;
            this.Operadora = operadora;
            this.DataRegisto = dataRegisto;
            this.Funcao = funcao;
            this.Sonda = sonda;
        }
        public CartaoSIM()
        {   
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId  Id { get; set; }
        public String Numero { set; get;}
        public String Operadora{ set; get;}
        public DateTime DataRegisto { set; get; }
        public String Funcao{ set; get;}
        public List<Sonda> Sonda { set; get; }
    }
}