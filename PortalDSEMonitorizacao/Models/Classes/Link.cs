using MongoDB.Bson;
using System;
using System.Collections.Generic;
namespace PortalDSEMonitorizacao.Models
{
    public class Link
    {
        public Link(ObjectId id, String endereco, String descricao, List<Credencial> credencial, List<Sonda> sonda)
        {
            this.Id = id;
            this.Endereco = endereco;
            this.Descricao = descricao;
            this.Credencial = credencial;
            this.Sonda = sonda;
        }

        public Link()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public String Endereco{ set; get;}
        public String Descricao { set; get; }
        public List<Credencial> Credencial { set; get; }
        public List<Sonda> Sonda { set; get; }
    }
}