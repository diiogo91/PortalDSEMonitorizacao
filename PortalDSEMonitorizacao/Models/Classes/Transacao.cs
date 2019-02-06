using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Transacao
    {
        public Transacao( String nome, long nrTransacao)
        {
            this.Nome = nome;
            this.nrTransacao = nrTransacao;
        }
        public Transacao()
        {
        }
        public String Nome { set; get;}
        public long nrTransacao { set; get;}
    }
}